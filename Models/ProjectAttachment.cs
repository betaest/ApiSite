using System.ComponentModel.DataAnnotations;

namespace ApiSite.Models {
    public class ProjectAttachment {
        #region Public Properties

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Url { get; set; }

        public int ProjectInfoId { get; set; }
        //public virtual ProjectInfo ProjectInfo { get; set; }

        #endregion Public Properties
    }
}