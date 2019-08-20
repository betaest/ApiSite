using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiSite.Models.BillQuery {
    public enum ResultType {
        Normal,
        Expandable,
    }

    [Table("result")]
    public class Result {
        [Required] public int Id { get; set; }

        [Required] public ResultType Type { get; set; }

        [Required] public Value Value { get; set; }

        public IEnumerable<Result> Children { get; set; }
    }
}
