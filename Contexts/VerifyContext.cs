using System;
using System.Linq;
using System.Text;
using ApiSite.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiSite.Contexts {
    public class VerifyContext : DbContext {
        private static readonly VerifyReturn failure = new VerifyReturn {
            Success = false,
            Name = string.Empty,
            Guid = string.Empty
        };

        #region Public Constructors

        public VerifyContext(DbContextOptions<VerifyContext> options) : base(options) {
        }

        #endregion Public Constructors

        private DbSet<LogonHistory> LogonHistory { get; set; }

        public VerifyReturn Verify(string token) {
            var existed = LogonHistory.Any(l => l.Token == token && l.State == 'A');

            if (existed) return failure;

            try {
                var guid = Guid.NewGuid().ToString();

                var tokenString = Encoding.UTF8.GetString(Convert.FromBase64String(token))
                    .Split(',', StringSplitOptions.RemoveEmptyEntries);

                LogonHistory.Add(new LogonHistory {
                    Token = token,
                    StaffId = int.Parse(tokenString[0]),
                    StaffName = tokenString[1],
                    IpAddr = tokenString[2],
                    Date = DateTime.Now,
                    Guid = guid,
                    State = 'A'
                });

                this.SaveChanges();

                return new VerifyReturn {
                    Success = true,
                    Name = tokenString[1],
                    Guid = guid
                };
            } catch {
                return failure;
            }
        }

        public bool HasGuid(string guid) =>
            LogonHistory.Any(l => l.State == 'A' && l.Guid == guid);
    }
}