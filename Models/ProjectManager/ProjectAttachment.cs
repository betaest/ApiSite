using System.ComponentModel.DataAnnotations;

namespace ApiSite.Models.ProjectManager {
    public class ProjectAttachment {
        #region Public Properties

        public int Id { get; set; }

        [Required] public string Name { get; set; }

        [Required] public string Url { get; set; }

        [Required] public char State { get; set; }

        [Required] public int ProjectInfoId { get; set; }

        #endregion Public Properties
    }
}