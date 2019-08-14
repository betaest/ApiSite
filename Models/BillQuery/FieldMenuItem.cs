using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiSite.Models.BillQuery {
    public class FieldMenuItem {
        #region Public Properties

        [Required] public int Id { get; set; }

        [Required] public string Title { get; set; }

        [Required] public virtual Method Method { get; set; }

        public virtual IEnumerable<DynamicText> ResultTitle { get; set; }

        #endregion Public Properties
    }
}