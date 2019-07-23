using ApiSite.Contexts;
using ApiSite.Models;

using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ApiSite.Controllers {
    [Route("p"), EnableCors("cors"), ApiController]
    public class ProjectController : ControllerBase {
        #region Private Fields

        private readonly ProjectManagerContext context;

        #endregion Private Fields

        #region Private Properties

        private bool Verified {
            get {
                if (!Request.Cookies.ContainsKey("guid")) return false;

                var guid = Request.Cookies["guid"];

                return context.HasGuid(guid);
            }
        }

        #endregion Private Properties

        #region Public Constructors

        public ProjectController(ProjectManagerContext context) {
            this.context = context;
        }

        #endregion Public Constructors

        #region Public Methods

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
            if (!Verified) return;

            context.DeleteById(id);
        }

        // GET api/values
        [HttpGet("{keyword?}")]
        public ActionResult<ProjectInfoReturn> Get(string keyword = "", int page = 1, int pageSize = 10, string sorter = "", string order = "normal") {
            if (!Verified) return new ProjectInfoReturn {
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
        public async void Post([FromForm]IFormCollection form) {
            var info = new ProjectInfo {
                Name = form["name"],
                Department = form["department"],
                Description = form["description"],
                Handler = form["handler"],
                Operator = form["operator"],
                OperateDateTime = DateTime.Now,//JsonConvert.DeserializeObject<DateTime>(form["operateDateTime"]),
                State = 'A',
            };

            var attachments = new List<ProjectAttachment>();

            foreach (var file in form.Files) {
                var name = Path.GetFileName(file.FileName);
                var url = $"{DateTime.Now:yyyyMMddHHmmss}.{Guid.NewGuid():B}{Path.GetExtension(name)}";
                //System.IO.File.WriteAllBytes($"D:\{file.FileName}", )
                using (var fs = new FileStream($"D:\\{url}", FileMode.Create))
                    await file.CopyToAsync(fs);

                attachments.Add(new ProjectAttachment {
                    Name = name,
                    Url = url
                });
            }

            context.AddInfo(info, attachments);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) { }

        #endregion Public Methods
    }
}