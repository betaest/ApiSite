using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiSite.Models.BillQuery {
    public enum ValueType {
        Const,
        Varient,
    }

    [Table("value")]
    public class Value {
        #region Public Properties

        [Required] public int Id { get; set; }

        [Required] public ValueType Type { get; set; }

        /// <summary>
        /// JSON values
        /// mandatory require field tag like this:
        /// {
        ///     "tag": "text",
        ///     "value": "12345",
        /// }
        /// </summary>
        [Required] public string Result { get; set; }

        public Connection Connection { get; set; }

        #endregion Public Properties
    }
}