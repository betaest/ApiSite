using System.IO;
using ApiSite.Contexts;
using ApiSite.Models;
using ApiSite.Utils;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ApiSite.Controllers {
    [Route("a"), EnableCors("cors"), ApiController]
    public class AttachmentController : ControllerBase {
        private readonly ApiConf cfg;
        private readonly ProjectManagerContext context;

        private static readonly MessageResult downloadFailure = new MessageResult {
            Success = false,
            Message = "下载文件未找到"
        };

        public AttachmentController(IOptionsMonitor<ApiConf> cfg, ProjectManagerContext context) {
            this.cfg = cfg.CurrentValue;
            this.context = context;
        }

        [HttpGet("{id}")]
        public object Download(int id) {
            var attachment = context.GetFile(id);

            if (attachment == default)
                return downloadFailure;

            try {
                var file = System.IO.File.ReadAllBytes($"{cfg.SavePath}/{attachment.Url}");
                var ext = Path.GetExtension(attachment.Name);
                var mimeType = Helper.MimeTypes.ContainsKey(ext.ToLower())
                    ? Helper.MimeTypes[ext.ToLower()]
                    : "application/octet-stream";

                return File(file, mimeType, attachment.Name);
            } catch {
                return downloadFailure;
            }
        }
    }
}