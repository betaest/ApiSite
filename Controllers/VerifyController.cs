using ApiSite.Contexts;
using ApiSite.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ApiSite.Controllers {
    [Route("v"), EnableCors("cors"), ApiController]
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
        [HttpGet("{token}")]
        public VerifyReturn Get(string token) {
            Response.Cookies.Append("guid", token, new CookieOptions {
                HttpOnly = true
            });
            return context.Verify(token);
        }

        #endregion Public Methods
    }
}