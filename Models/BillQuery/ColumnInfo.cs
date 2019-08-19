using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ApiSite.Models.BillQuery {
    [Table("column")]
    public class ColumnInfo {
        #region Public Properties

        [Key] [Required] public string Key { get; set; }
        [Required] public virtual string Title { get; set; }
        public virtual IEnumerable<FieldMenuItem> Menu { get; set; }
        public bool Sortable { get; set; }
        public int Width { get; set; }

        [NotMapped]
        public Column Result =>
            new Column {
                Key = Key,
                Title = Title,
                Sortable = Sortable,
                Width = Width,
                Menu = Menu.ToDictionary(m => m.Id, m => m.Title),
            };

        #endregion Public Properties
    }
}