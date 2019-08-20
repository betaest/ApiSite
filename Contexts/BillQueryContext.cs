using ApiSite.Models.BillQuery;
using Microsoft.EntityFrameworkCore;

namespace ApiSite.Contexts {
    public class BillQueryContext : DbContext {
        #region Public Constructors

        public BillQueryContext(DbContextOptions<BillQueryContext> options) : base(options) {
        }

        #endregion Public Constructors

        #region Public Properties

        public DbSet<Column> Columns { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }

        #endregion Public Properties
    }
}