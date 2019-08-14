using System.ComponentModel.DataAnnotations;

namespace ApiSite.Models.BillQuery {
    public class DynamicText {
        #region Public Properties

        [Required] public int Id { get; set; }

        [Required] public string Type { get; set; }

        [Required] public string Props { get; set; }

        #endregion Public Properties
    }
}