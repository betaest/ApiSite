using System;
using System.Collections.Generic;
using System.Linq;
using ApiSite.Models;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace ApiSite.Contexts {
    public class ProjectManagerContext : DbContext {
        #region Public Constructors

        public ProjectManagerContext(DbContextOptions<ProjectManagerContext> options) : base(options) { }

        private DbSet<Models.ProjectInfo> ProjectInfo { get; set; }
        private DbSet<Models.LogonHistory> LogonHistory { get; set; }
        private DbSet<Models.ProjectAttachment> ProjectAttachment { get; set; }

        #region Overrides of DbContext

        public override int GetHashCode() {
            return base.GetHashCode();
        }

        #endregion

        #region Overrides of DbContext

#if DEBUG
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.EnableDetailedErrors(true).EnableSensitiveDataLogging(true);
        }
#endif

        #endregion

        public IEnumerable<Models.ProjectInfo> GetInfoByKeyword(int page, int pageSize, string sorter, string order, string keyword) {
            var result = ProjectInfo.Include(p => p.Attachments).Where(p => p.State == 'A');

            if (!string.IsNullOrEmpty(keyword))
                result = result.Where(p => p.Name.Contains(keyword) || p.Description.Contains(keyword));

            if (!string.IsNullOrEmpty(sorter) && !string.IsNullOrEmpty(order)) {
                switch (order.ToLower()) {
                    case "normal":
                        break;

                    default:
                        if (sorter.ToLower() == "$custom")
                            result = order.ToLower() == "asc" ? result.OrderBy(p => p.Department + p.Handler) : result.OrderByDescending(p => p.Department + p.Handler);
                        else
                            result = result.OrderBy(sorter, order == "asc");
                        break;
                }
            }

            result = result.Skip(page * pageSize).Take(pageSize);

            return result;
        }

        public bool DeleteById(int id) {
            var info = ProjectInfo.First(p => p.Id == id && p.State == 'A');

            if (info == null)
                return false;

            info.State = 'X';
            SaveChanges();

            return true;

        }

        public bool HasGuid(string guid) =>
            LogonHistory.Any(l => l.State == 'A' && l.Guid == guid);

        public bool AddInfo(Models.ProjectInfo info) {
            try {
                ProjectInfo.Add(info);

                // ProjectInfo.FromSql("insert into")
                SaveChanges();

                return true;
            } catch(Exception e) {
                Console.WriteLine(e);

                return false;
            }
        }
        #endregion Public Constructors
    }
}