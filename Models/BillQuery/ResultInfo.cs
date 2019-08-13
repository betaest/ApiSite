using System.Collections.Generic;

namespace ApiSite.Models.BillQuery {
    public class ResultInfo {
        #region Public Properties

        public List<Dictionary<string, object>> Body { get; set; }
        public Dictionary<string, object> Footer { get; set; }
        public List<ColumnInfo> Header { get; set; }
        public List<DynamicText> Title { get; set; }
        public int Total { get; set; }  

        #endregion Public Properties
    }
}