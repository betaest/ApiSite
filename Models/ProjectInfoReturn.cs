using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSite.Models {
    public class ProjectInfoReturn {
        public int Total { get; set; }
        public IEnumerable<ProjectInfo> Rows { get; set; }
    }
}