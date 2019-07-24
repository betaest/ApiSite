using ApiSite.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiSite.Contexts {
    public class ProjectManagerContext : DbContext {
        #region Public Constructors

        private DbSet<LogonHistory> LogonHistory { get; set; }

        private DbSet<ProjectAttachment> ProjectAttachment { get; set; }

        private DbSet<ProjectInfo> ProjectInfo { get; set; }

        public ProjectManagerContext(DbContextOptions<ProjectManagerContext> options) : base(options) {
        }

        #region Overrides of DbContext

        public override int GetHashCode() {
            return base.GetHashCode();
        }

        #endregion Overrides of DbContext

        #region Overrides of DbContext

#if DEBUG

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.EnableDetailedErrors(true).EnableSensitiveDataLogging(true);
        }

#endif

        #endregion Overrides of DbContext

        public bool AddInfo(ProjectInfo info, ICollection<ProjectAttachment> attachments = null) {
            try {
                //ProjectInfo.Add(info);

                //if (attachments != null) {
                //    foreach (var item in attachments) {
                //        item.ProjectInfo = info;
                //    }

                //    ProjectAttachment.AddRange(attachments);
                //}

                //SaveChanges();
                Database.ExecuteSqlCommand(@"insert into ProjectInfo (Name, Description, Department, Handler, Operator, OperateDateTime, State)
                                values ({info.Name}, {info.Description}, {info.Department}, {info.Handler}, {info.Operator}, {info.OperateDateTime}, {info.State})");

                return true;
            } catch (Exception e) {
                Console.WriteLine(e);

                return false;
            }
        }

        public bool DeleteById(int id) {
            var info = ProjectInfo.First(p => p.Id == id && p.State == 'A');

            if (info == null)
                return false;

            info.State = 'X';
            SaveChanges();

            return true;
        }

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

        public bool HasGuid(string guid) =>
            LogonHistory.Any(l => l.State == 'A' && l.Guid == guid);

        #endregion Public Constructors
    }
}