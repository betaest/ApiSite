using System;
using System.ComponentModel.DataAnnotations;

namespace ApiSite.Models {
    public class LogonHistory {
        #region Public Properties

        [Key]
        public int Id { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public string StaffId { get; set; }

        [Required]
        public string StaffName { get; set; }

        [Required]
        public string IpAddr { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Guid { get; set; }

        [Required]
        public char State { get; set; }

        #endregion Public Properties
    }
}