using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiSite.Models.BillQuery {
    public enum ActionType {
        DynamicString,
        StaticString,
        DynamicSQL,
        StaticSQL,
    }

    public class Method {
        #region Public Properties

        [Required] public int Id { get; set; }

        [Required] public string Action { get; set; }

        [Required] public ActionType Type { get; set; }

        public virtual Connection Connection { get; set; }

        #endregion Public Properties
    }
}