using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSite.Models {
    public class VerifyReturn {
        public bool Success { get; set; }

        public string Name { get; set; }

        public string Guid { get; set; }
    }
}
