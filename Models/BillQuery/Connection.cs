using System.ComponentModel.DataAnnotations;

namespace ApiSite.Models.BillQuery {
    public class Connection {
        [Key] public string Name { get; set; }
        [Required] public string ConnectionString { get; set; }
    }
}
