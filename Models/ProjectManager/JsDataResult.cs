using System.Collections.Generic;

namespace ApiSite.Models.ProjectManager {
    public class JsDataResult {
        public int Total { get; set; }
        public IEnumerable<Project> Rows { get; set; }
    }
}