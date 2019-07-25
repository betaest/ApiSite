using ApiSite.Contexts;
using ApiSite.Models;
using ApiSite.Utils;

using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ApiSite.Controllers {
    [Route("p"), EnableCors("cors"), ApiController]
    public class ProjectController : ControllerBase {
        #region Public Constructors

        public ProjectController(IOptionsMonitor<ApiConf> cfg, IHostingEnvironment env, ProjectManagerContext context) {
            this.env = env;
            this.cfg = cfg.CurrentValue;
            this.context = context;
        }

        #endregion Public Constructors

        #region Private Properties

        private bool Verified {
            get {
                if (!Request.Cookies.ContainsKey("guid")) return false;

                var guid = Request.Cookies["guid"];

                return context.HasGuid(guid);
            }
        }

        #endregion Private Properties

        #region Private Methods

        private static MessageResult ReturnMessage(bool success) {
            return new MessageResult {
                Success = success,
                Message = success ? "保存成功" : "保存数据失败"
            };
        }

        [NonAction]
        private MessageResult PostOrPutContent(IFormCollection form, Func<ProjectInfo, bool> callback) {
            if (!Verified) return VerifyError;

            var info = new ProjectInfo {
                Id = int.TryParse(form["id"], out var id) ? id : default,
                Name = form["name"],
                Department = form["department"],
                Description = form["description"],
                Handler = form["handler"],
                Operator = form["operator"],
                OperateDateTime = DateTime.Now,
                State = 'A',
                Attachments = new List<ProjectAttachment>(),
            };

            var files = new Dictionary<string, IFormFile>();
            var wwwroot = env.ContentRootPath;

            foreach (var file in form.Files) {
                var name = Path.GetFileName(file.FileName);
                var url = $"{DateTime.Now:yyyyMMddHHmmss}.{Guid.NewGuid():B}{Path.GetExtension(name)}";
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
                    using (var fs = new FileStream($"{cfg.SavePath}/{url}", FileMode.Create))
                        file.CopyTo(fs);

            return ReturnMessage(result);
        }

        #endregion Private Methods

        #region Private Fields

        private static readonly Dictionary<string, string> MimeTypes = new Dictionary<string, string> {
            {".txt", "text/plain"},
            {".pdf", "application/pdf"},
            {".doc", "application/vnd.ms-word"},
            {".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
            {".xls", "application/vnd.ms-excel"},
            {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
            {".ppt", "application/vnd.ms-powerpoint" },
            {".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation"},
            {".png", "image/png"},
            {".jpg", "image/jpeg"},
            {".jpeg", "image/jpeg"},
            {".gif", "image/gif"},
            {".csv", "text/csv"}
        };

        private static readonly MessageResult VerifyError = new MessageResult {
            Success = false,
            Message = "身份验证失败，请重新登录"
        };

        private readonly ApiConf cfg;
        private readonly ProjectManagerContext context;
        private readonly IHostingEnvironment env;

        #endregion Private Fields

        #region Public Methods

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public MessageResult Delete(int id) {
            if (!Verified) return VerifyError;

            var success = context.DeleteById(id);

            return ReturnMessage(success);
        }

        // GET api/values
        [HttpGet("{keyword?}")]
        public ProjectInfoReturn Get(string keyword = "", int page = 1, int pageSize = 10,
            string sorter = "", string order = "normal") {

            var url = cfg.NameRule.Format(new { now = DateTime.Now, guid = Guid.NewGuid() });

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