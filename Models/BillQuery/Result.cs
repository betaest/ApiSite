using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiSite.Models.BillQuery {
    public enum ResultType {
        Normal,
        Expandable,
    }

    public class Result {
        [Required] public int Id { get; set; }

        public ResultType Type { get; set; }

        public Value Value { get; set; }

        public IEnumerable<Result> Children { get; set; }
    }
}
