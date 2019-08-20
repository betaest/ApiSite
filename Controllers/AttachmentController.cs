using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using ApiSite.Contexts;
using ApiSite.Models;
using ApiSite.Utils;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ApiSite.Controllers {
    [Route("a")]
    [EnableCors("cors")]
    [ApiController]
    public class AttachmentController : ControllerBase {
        private static readonly JsResult downloadFailure = new JsResult {
            Success = false,
            Message = "下载文件未找到"
        };

        private readonly ApiConf cfg;
        private readonly ProjectManagerContext context;

        public AttachmentController(IOptionsMonitor<ApiConf> cfg, ProjectManagerContext context) {
            this.cfg = cfg.CurrentValue;
            this.context = context;
        }

        [NonAction]
        private FileResult DownloadSingle(string filename) {
            var file = System.IO.File.ReadAllBytes(filename);
            var ext = Path.GetExtension(filename);
            var mimeType = Helper.MimeTypes.ContainsKey(ext.ToLower())
                ? Helper.MimeTypes[ext.ToLower()]
                : "application/octet-stream";

            return File(file, mimeType);
        }

        [NonAction]
        private FileResult DownloadZip(IEnumerable<(string url, string name)> names) {
            var mimeType = Helper.MimeTypes[".zip"];

            using (var ms = new MemoryStream()) {
                using (var zip = new ZipArchive(ms, ZipArchiveMode.Create, true)) {
                    foreach (var (url, name) in names)
                        zip.CreateEntryFromFile(url, name);
                }

                ms.Seek(0, SeekOrigin.Begin);

                return File(ms.ToArray(), mimeType);
            }
        }

        [HttpGet("{id}")]
        public object Download(int id) {
            var attachment = context.GetFile(id);

            if (attachment != default)
                try {
                    return DownloadSingle($"{cfg.SavePath}/{attachment.Url}");
                } catch {
                    Response.StatusCode = 404;
                }

            return downloadFailure;
        }

        [HttpGet("all/{id}")]
        public object DownloadAll(int id) {
            var attachments = context.GetFiles(id);

            try {
                if (attachments != default && attachments.Count == 1)
                    return DownloadSingle($"{cfg.SavePath}/{attachments[0].Url}");

                if (attachments != default)
                    return DownloadZip(attachments.Select(a => ($"{cfg.SavePath}/{a.Url}", a.Name)));
            } catch {
                Response.StatusCode = 404;
            }

            return downloadFailure;
        }

        [HttpDelete("{id}")]
        public void Delete(int id) {
            context.DeleteFile(id);
        }
    }
}