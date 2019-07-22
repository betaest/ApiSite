using System;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ApiSite.Contexts {
    public class VerifyContext : DbContext {
        #region Public Constructors

        public VerifyContext(DbContextOptions<VerifyContext> options) : base(options) { }

        #endregion Public Constructors

        private DbSet<Models.LogonHistory> LogonHistory { get; set; }

        private static readonly Models.VerifyReturn failure = new Models.VerifyReturn {
            Success = false,
            Name = string.Empty,
            Guid = string.Empty
        };

        public Models.VerifyReturn Verify(string token) {
            var existed = LogonHistory.Any(l => l.Token == token && l.State == 'A');

            if (existed) return failure;

            try {
                var guid = Guid.NewGuid().ToString();

                var tokenString = Encoding.UTF8.GetString(Convert.FromBase64String(token)).Split(',', StringSplitOptions.RemoveEmptyEntries);

                LogonHistory.Add(new Models.LogonHistory {
                    Token = token,
                    StaffId = int.Parse(tokenString[0]),
                    StaffName = tokenString[1],
                    IpAddr = tokenString[2],
                    Date = DateTime.Now,
                    Guid = guid,
                    State = 'A'
                });

                this.SaveChanges();

                return new Models.VerifyReturn {
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