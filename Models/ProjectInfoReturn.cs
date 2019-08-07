using System.Collections.Generic;

namespace ApiSite.Models {
    public class ProjectInfoReturn {
        public int Total { get; set; }
        public IEnumerable<ProjectInfo> Rows { get; set; }
    }
}