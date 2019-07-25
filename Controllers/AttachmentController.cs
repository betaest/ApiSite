using System;
using ApiSite.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace ApiSite.Controllers {
    [Route("a"), EnableCors("cors"), ApiController]
    public class AttachmentController: ControllerBase {
        private ApiConf cfg;
        private IHostingEnvironment env;

        public AttachmentController(IOptionsMonitor<ApiConf> cfg, IHostingEnvironment env) {
            this.cfg = cfg.CurrentValue;
            this.env = env;
        }

        [HttpDelete("{id}")]
        public MessageResult Delete(int fileId) {
            return null;
        }
    }
}