﻿using System.Collections.Generic;

namespace ApiSite.Models.BillQuery {
    public class Result {
        #region Public Properties

        public List<object> Title { get; set; }
        public List<Column> Header { get; set; }
        public List<Dictionary<string, object>> Body { get; set; }
        public Dictionary<string, object> Footer { get; set; }
        public int Total { get; set; }

        #endregion Public Properties
    }
}