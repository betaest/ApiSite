using System.Collections.Generic;

namespace ApiSite.Models.BillQuery {
    public class JsColumn {
        public string Key { get; set; }
        public string Title { get; set; }
        public bool Sortable { get; set; }
        public int Width { get; set; }
        public Dictionary<int, string> Menu { get; set; }
    }
}
