using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiSite.Models.ProjectManager {
    public class ProjectInfo {
        #region Public Properties

        public virtual ICollection<ProjectAttachment> Attachments { get; set; }

        [Required] public string Department { get; set; }

        [Required] public string Description { get; set; }

        [Required] public string Handler { get; set; }

        public int Id { get; set; }

        [Required] public string Name { get; set; }

        [Required] public DateTime OperateDateTime { get; set; }

        [Required] public string Operator { get; set; }

        [Required] public char State { get; set; }

        #endregion Public Properties
    }
}