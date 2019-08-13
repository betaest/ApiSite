using ApiSite.Models.BillQuery;
using Microsoft.EntityFrameworkCore;

namespace ApiSite.Contexts {
    public class BillQueryContext : DbContext {
        #region Public Constructors

        public BillQueryContext(DbContextOptions<BillQueryContext> options) : base(options) {
        }

        #endregion Public Constructors

        #region Public Properties

        public DbSet<ColumnInfo> ColumnInfo { get; set; }
        //public DbSet<Models.BillQuery.DynamicText> DynamicText { get; set; }
        //public DbSet<Models.BillQuery.Method> Method { get; set; }
        //public DbSet<Models.BillQuery.Connection> Connection { get; set; }

        #endregion Public Properties
    }
}