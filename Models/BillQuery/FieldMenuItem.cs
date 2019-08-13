using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiSite.Models.BillQuery {
    public class FieldMenuItem {
        #region Public Properties

        [Required] public int Id { get; set; }

        [Required] public virtual Method Method { get; set; }

        [Required] public string Params { get; set; }

        [Required] public virtual IEnumerable<DynamicText> Title { get; set; }

        #endregion Public Properties
    }
}