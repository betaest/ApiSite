using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiSite.Models.BillQuery {
    public enum Return {
        VariantObject,
        InvariantObject,
        VariantOracle,
        InvariantOracle,
    }

    [Table("compute_value")]
    public class ComputeValue {
        #region Public Properties

        [Required] public int Id { get; set; }

        [Required] public Return Type { get; set; }

        /// <summary>
        /// JSON values
        /// mandatory require field tag like this:
        /// {
        ///     "tag": "text",
        ///     "value": "12345",
        /// }
        /// </summary>
        [Required] public string Value { get; set; }

        public string Connection { get; set; }

        #endregion Public Properties
    }
}