using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiSite.Models.BillQuery {
    public class ColumnInfo {
        #region Public Properties
        [Required]
        public int Id { get; set; }

        public bool Fixed { get; set; }

        [Required]
        public string Key { get; set; }

        public virtual IEnumerable<FieldMenuItem> Menu { get; set; }

        public bool Sortable { get; set; }

        [Required]
        public virtual IEnumerable<DynamicText> Title { get; set; }

        public int Width { get; set; }

        #endregion Public Properties
    }
}