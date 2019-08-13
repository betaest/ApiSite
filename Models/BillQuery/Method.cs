using System.ComponentModel.DataAnnotations;

namespace ApiSite.Models.BillQuery {
    public enum ActionType {
        SQL,
        Dynamic,
        Static,
        DynamicSQL
    }

    public class Method {
        #region Public Properties

        [Required] public int Id { get; set; }

        [Required] public string Action { get; set; }

        [Required] public string Comment { get; set; }

        [Required] public ActionType Type { get; set; }

        public virtual Connection Connection { get; set; }

        #endregion Public Properties
    }
}