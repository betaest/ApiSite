using System.Collections.Generic;
using ApiSite.Models.ProjectManager;

namespace ApiSite.Models {
    public class ProjectInfoReturn {
        public int Total { get; set; }
        public IEnumerable<ProjectInfo> Rows { get; set; }
    }
}