using System;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ApiSite.Controllers {
    [Route("a"), EnableCors("cors"), ApiController]
    public class AttachmentController: ControllerBase {
    }
}