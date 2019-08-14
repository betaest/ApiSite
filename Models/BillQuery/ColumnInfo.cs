using Newtonsoft.Json;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiSite.Models.BillQuery {
    public class ColumnInfo {
        #region Public Properties

        [Required] public int Id { get; set; }
        [Required] public string Key { get; set; }
        [Required] public virtual string Title { get; set; }
        public virtual IEnumerable<FieldMenuItem> Menu { get; set; }
        public bool Sortable { get; set; }
        public bool Fixed { get; set; }

        public int Width { get; set; }

        public Column ToResult() =>
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