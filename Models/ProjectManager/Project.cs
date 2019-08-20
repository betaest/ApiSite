using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiSite.Models.ProjectManager {
    [Table("project")]
    public class Project {
        #region Public Properties

        public virtual ICollection<Attachment> Attachments { get; set; }

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