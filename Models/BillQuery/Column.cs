using System.Collections.Generic;

namespace ApiSite.Models.BillQuery {
    public class Column {
        public string Title { get; set; }
        public Dictionary<int, string> Menu { get; set; }
        public int Width { get; set; }
        public string Key { get; set; }
        public bool Sortable { get; set; }
        public bool Fixed { get; set; }
    }
}
