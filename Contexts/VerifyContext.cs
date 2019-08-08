using System;
using System.Linq;
using System.Text;
using ApiSite.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
                var tokenString = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                var tk = JsonConvert.DeserializeObject<Token>(tokenString);

                if (tk.TokenTime < DateTime.Now.AddMinutes(-5))
                    throw new ArgumentOutOfRangeException();

                var guid = Guid.NewGuid().ToString();

                LogonHistory.Add(new LogonHistory {
                    Token = token,
                    StaffId = tk.StaffId,
                    StaffName = tk.StaffName,
                    IpAddr = tk.IpAddress,
                    Date = tk.TokenTime,
                    Guid = guid,
                    State = 'A'
                });

                SaveChanges();

                return new VerifyReturn {
                    Success = true,
                    Name = tk.StaffName,
                    Guid = guid,
                    To = tk.To
                };
            } catch {
                return failure;
            }
        }

        //public bool HasGuid(string guid) {
        //    return LogonHistory.Any(l => l.State == 'A' && l.Guid == guid);
        //}

        public VerifyReturn VerifyByGuid(string guid) {
            var token = LogonHistory.FirstOrDefault(l => l.State == 'A' && l.Guid == guid);

            if (token == default)
                return failure;

            return new VerifyReturn {
                Success = true,
                Name = token.StaffName,
                Guid = token.Guid
            };
        }
    }
}