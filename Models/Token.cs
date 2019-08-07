using System;

namespace ApiSite.Models {
    public class Token {
        public string StaffId { get; set; }
        public string StaffName { get; set; }
        public string IpAddress { get; set; }
        public DateTime TokenTime { get; set; }
        public string To { get; set; }
    }
}