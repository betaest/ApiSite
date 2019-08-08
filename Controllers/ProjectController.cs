using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ApiSite.Contexts;
using ApiSite.Models;
using ApiSite.Utils;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ApiSite.Controllers {
    [Route("p")]
    [EnableCors("cors")]
    [ApiController]
    public class ProjectController : ControllerBase {
        #region Public Constructors

        public ProjectController(IOptionsMonitor<ApiConf> cfg, ProjectManagerContext context, VerifyContext verify) {
            this.cfg = cfg.CurrentValue;
            this.context = context;
            this.verify = verify;
        }

        #endregion Public Constructors

        #region Private Properties

        private bool Verified {
            get {
                var token = Request.Cookies["token"];

                if (string.IsNullOrEmpty(token))
                    return false;

                return verify.VerifyByGuid(token).Success;
            }
        }

        #endregion Private Properties

        #region Private Methods

        private static MessageResult ReturnMessage(bool success) {
            return new MessageResult {
                Success = success,
                Message = success ? "提交数据成功" : "提交数据失败"
            };
        }

        [NonAction]
        private MessageResult PostOrPutContent(IFormCollection form, Func<ProjectInfo, bool> callback) {
            if (!Verified) return verifyError;

            var info = new ProjectInfo {
                Id = int.TryParse(form["id"], out var id) ? id : default,
                Name = form["name"],
                Department = form["department"],
                Description = form["description"],
                Handler = form["handler"],
                Operator = form["operator"],
                OperateDateTime = DateTime.Now,
                State = 'A',
                Attachments = new List<ProjectAttachment>()
            };

            var files = new Dictionary<string, IFormFile>();

            foreach (var file in form.Files) {
                var name = Path.GetFileName(file.FileName);
                var url = $"{Helper.GenerateFilename(cfg.NameRule)}{Path.GetExtension(name)}";
                info.Attachments.Add(new ProjectAttachment {
                    Name = name,
                    Url = url,
                    State = 'A'
                });
                files.Add(url, file);
            }

            var result = callback(info);

            if (result)
                foreach (var (url, file) in files)
                    using (var fs = new FileStream($"{cfg.SavePath}/{url}", FileMode.Create)) {
                        file.CopyTo(fs);
                    }

            return ReturnMessage(result);
        }

        #endregion Private Methods

        #region Private Fields

        private static readonly MessageResult verifyError = new MessageResult {
            Success = false,
            Message = "身份验证失败，请重新登录"
        };

        private readonly ApiConf cfg;
        private readonly ProjectManagerContext context;
        private readonly VerifyContext verify;

        #endregion Private Fields

        #region Public Methods

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public MessageResult Delete(int id) {
            if (!Verified) return verifyError;

            var success = context.DeleteById(id);

            return ReturnMessage(success);
        }

        // GET api/values
        [HttpGet("{keyword?}")]
        public ProjectInfoReturn Get(string keyword = "", int page = 1, int pageSize = 10,
            string sorter = "", string order = "normal") {
            if (!Verified)
                return new ProjectInfoReturn {
                    Total = 0,

                    Rows = new ProjectInfo[0]
                };

            var row = context.GetInfoByKeyword(page - 1, pageSize, sorter, order, keyword).ToList();

            return new ProjectInfoReturn {
                Total = row.Count,
                Rows = row
            };
        }

        // POST
        [HttpPost]
        public MessageResult Post([FromForm] IFormCollection form) {
            return PostOrPutContent(form, context.AddInfo);
        }

        // PUT
        [HttpPut]
        public MessageResult Put([FromForm] IFormCollection form) {
            return PostOrPutContent(form, context.UpdateInfo);
        }

        #endregion Public Methods
    }
}