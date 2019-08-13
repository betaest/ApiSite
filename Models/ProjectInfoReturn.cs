using System.Collections.Generic;

namespace ApiSite.Models {
    public class ProjectInfoReturn {
        public int Total { get; set; }
        public IEnumerable<ProjectManager.ProjectInfo> Rows { get; set; }
    }
}