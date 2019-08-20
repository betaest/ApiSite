using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ApiSite.Models.BillQuery {
    [Table("column")]
    public class Column {
        #region Public Properties

        [Key] [Required] public string Key { get; set; }
        [Required] public virtual string Title { get; set; }
        public bool Sortable { get; set; }
        public int Width { get; set; }

        [NotMapped]
        public JsColumn Result =>
            new JsColumn {
                Key = Key,
                Title = Title,
                Sortable = Sortable,
                Width = Width,
            };

        #endregion Public Properties 
    }
}