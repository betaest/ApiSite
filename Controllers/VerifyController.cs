using ApiSite.Contexts;
using ApiSite.Models;

using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiSite.Controllers {
    [Route("v")]
    [EnableCors("cors")]
    [ApiController]
    public class VerifyController : ControllerBase {
        #region Private Fields

        private readonly VerifyContext context;

        #endregion Private Fields

        #region Public Constructors

        public VerifyController(VerifyContext context) {
            this.context = context;
        }

        #endregion Public Constructors

        #region Public Methods

        // GET: api/Verify
        [HttpGet("{token?}")]
        public VerifyReturn Get(string token = null) {
            var tk = Request.Cookies["token"];

            if (!string.IsNullOrEmpty(tk))
                return context.VerifyByGuid(tk);
            else if (string.IsNullOrEmpty(token))
                return new VerifyReturn { Success = false };

            var result = context.Verify(token, HttpContext.Connection.RemoteIpAddress.ToString());

            if (result.Success) {
                Response.Cookies.Append("token", result.Guid, new CookieOptions {
                    IsEssential = true,
                    HttpOnly = true,
                });
            }

            return result;
        }

        #endregion Public Methods
    }
}