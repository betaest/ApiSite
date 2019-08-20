using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiSite.Models.BillQuery {
    [Table("menu_item")]
    public class MenuItem {
        #region Public Properties

        [Required] public int Id { get; set; }

        [Required] public string Text { get; set; }

        public virtual Result Result { get; set; }

        #endregion Public Properties
    }
}