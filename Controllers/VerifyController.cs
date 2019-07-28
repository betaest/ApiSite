using ApiSite.Contexts;
using ApiSite.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<VerifyReturn> Get(string token) {
            return context.Verify(token);
        }

        #endregion Public Methods
    }
}