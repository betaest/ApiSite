using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ApiSite.Contexts;
using ApiSite.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ApiSite.Controllers {
    [Route("p"), EnableCors("cors"), ApiController]
    public class ProjectController : ControllerBase {
        #region Public Constructors

        public ProjectController(IConfiguration cfg, IHostingEnvironment env, ProjectManagerContext context) {
            this.env = env;
            this.cfg = cfg;
            this.context = context;
        }

        #endregion Public Constructors

        #region Private Properties

        private bool Verified
        {
            get
            {
                if (!Request.Cookies.ContainsKey("guid")) return false;

                var guid = Request.Cookies["guid"];

                return context.HasGuid(guid);
            }
        }

        #endregion Private Properties

        #region Private Fields

        private readonly ProjectManagerContext context;
        private readonly IHostingEnvironment env;
        private readonly IConfiguration cfg;

        #endregion Private Fields

        #region Public Methods

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
            if (!Verified) return;

            context.DeleteById(id);
        }

        // GET api/values
        [HttpGet("{keyword?}")]
        public ActionResult<ProjectInfoReturn> Get(string keyword = "", int page = 1, int pageSize = 10,
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

        // POST api/values
        [HttpPost]
        public bool Post([FromForm] IFormCollection form) {
            if (!Verified) return false;

            var info = new ProjectInfo {
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
            var wwwroot = env.WebRootPath;

            foreach (var file in form.Files) {
                var name = Path.GetFileName(file.FileName);
                var url = $"{DateTime.Now:yyyyMMddHHmmss}.{Guid.NewGuid():B}{Path.GetExtension(name)}";
                info.Attachments.Add(new ProjectAttachment {
                    Name = name,
                    Url = url
                });
                files.Add(url, file);
            }

            if (!context.AddInfo(info))
                return false;

            foreach (var (url, file) in files)
                using (var fs = new FileStream(cfg["FileSaveTo"].Parse(new {wwwroot, url}), FileMode.Create))
                    file.CopyTo(fs);

            return true;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) {
        }

        #endregion Public Methods
    }
}