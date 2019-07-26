using System;
using System.Collections.Generic;
using System.Linq;
using ApiSite.Models;
using ApiSite.Utils;
using Microsoft.EntityFrameworkCore;

namespace ApiSite.Contexts {
    public class ProjectManagerContext : DbContext {
        #region Public Constructors

        public ProjectManagerContext(DbContextOptions<ProjectManagerContext> options) : base(options) {
        }

        #endregion Public Constructors

        #region Overrides of DbContext

#if DEBUG

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.EnableDetailedErrors(true).EnableSensitiveDataLogging(true);
        }

#endif

        #endregion Overrides of DbContext

        #region Private Properties

        private DbSet<LogonHistory> LogonHistory { get; set; }
        private DbSet<ProjectAttachment> ProjectAttachment { get; set; }
        private DbSet<ProjectInfo> ProjectInfo { get; set; }

        #endregion Private Properties

        #region Public Methods

        public bool AddInfo(ProjectInfo info) {
            try {
                ProjectInfo.Add(info);

                SaveChanges();

                return true;
            } catch (Exception e) {
                Console.WriteLine(e);

                return false;
            }
        }

        public bool DeleteById(int id) {
            try {
                var info = ProjectInfo.First(p => p.Id == id && p.State == 'A');

                if (info == null)
                    return false;

                info.State = 'X';

                if (info.Attachments != null)
                    foreach (var a in info.Attachments)
                        a.State = 'X';

                SaveChanges();

                return true;
            } catch {
                return false;
            }
        }

        public void DeleteFile(int id) {
            var attachment = ProjectAttachment.First(pa => pa.Id == id && pa.State == 'A');

            if (attachment != default) {
                attachment.State = 'X';
                SaveChanges();
            }
        }

        public IEnumerable<ProjectInfo> GetInfoByKeyword(int page, int pageSize, string sorter, string order,
            string keyword) {
            var result = ProjectInfo.Include(p => p.Attachments).Where(p => p.State == 'A');

            if (!string.IsNullOrEmpty(keyword))
                result = result.Where(p => p.Name.Contains(keyword) || p.Description.Contains(keyword));

            if (!string.IsNullOrEmpty(sorter) && !string.IsNullOrEmpty(order)) {
                switch (order.ToLower()) {
                    case "normal":
                        break;

                    default:
                        if (sorter.ToLower() == "$custom")
                            result = order.ToLower() == "asc"
                                ? result.OrderBy(p => p.Department + p.Handler)
                                : result.OrderByDescending(p => p.Department + p.Handler);
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

        public ProjectAttachment GetFile(int fileId) =>
            ProjectAttachment.FirstOrDefault(pa => pa.Id == fileId && pa.State == 'A');

        public List<ProjectAttachment> GetFiles(int id) =>
            ProjectAttachment.Where(pa => pa.State == 'A' && pa.ProjectInfoId == id).ToList();

        public bool UpdateInfo(ProjectInfo info) {
            var pr = ProjectInfo.FirstOrDefault(p => p.Id == info.Id && p.State == 'A');

            if (pr == default(ProjectInfo)) return false;

            try {
                pr.Name = info.Name;
                pr.Department = info.Department;
                pr.Description = info.Description;
                pr.Handler = info.Handler;
                pr.Operator = info.Operator;
                pr.OperateDateTime = info.OperateDateTime;

                if (pr.Attachments == null)
                    pr.Attachments = info.Attachments;
                else
                    foreach (var a in info.Attachments)
                        pr.Attachments.Add(a);

                SaveChanges();

                return true;
            } catch {
                return false;
            }
        }

        #endregion Public Methods
    }
}