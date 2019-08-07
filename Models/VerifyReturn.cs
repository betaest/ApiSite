using System;

namespace ApiSite.Models {
    public class VerifyReturn {
        #region Public Properties

        public string Guid { get; set; }
        public string Name { get; set; }
        public bool Success { get; set; }

        #endregion Public Properties
    }

    public class Token {
        public string StaffId { get; set; }
        public string StaffName { get; set; }
        public string IpAddress { get; set; }
        public DateTime TokenTime { get; set; }
        public string To { get; set; }
    }
}