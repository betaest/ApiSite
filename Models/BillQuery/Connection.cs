using System.ComponentModel.DataAnnotations;

namespace ApiSite.Models.BillQuery {
    public class Connection {
        #region Public Properties
        [Required]
        public int Id { get; set; }

        [Required]
        public string ConnectionString { get; set; }

        [Required]
        public string Name { get; set; }

        #endregion Public Properties
    }
}