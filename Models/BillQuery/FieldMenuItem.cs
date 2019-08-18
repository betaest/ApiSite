using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiSite.Models.BillQuery {
    [Table("field_menu_item")]
    public class FieldMenuItem {
        #region Public Properties

        [Required] public int Id { get; set; }

        [Required] public string Title { get; set; }

        [Required] public virtual ComputeValue Action { get; set; }

        public virtual IEnumerable<ComputeValue> ReturnTitle { get; set; }

        #endregion Public Properties
    }
}