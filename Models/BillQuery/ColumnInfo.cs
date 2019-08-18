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
        public bool Fixed { get; set; }

        public int Width { get; set; }

        [NotMapped]
        public Column Result =>
            new Column {
                Title = Title,
                Menu = Menu.ToDictionary(m => m.Id, m => m.Title),
                Key = Key,
                Width = Width,
                Fixed = Fixed,
                Sortable = Sortable
            };

        #endregion Public Properties
    }
}