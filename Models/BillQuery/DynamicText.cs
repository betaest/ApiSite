using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiSite.Models.BillQuery {
    public class DynamicText{
        #region Public Properties
        [Required]
        public int Id { get; set; }

        public IEnumerable<Method> Params { get; set; }

        [Required]
        public string Type { get; set; }

        #endregion Public Properties
    }
}