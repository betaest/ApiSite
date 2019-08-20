using System;
using System.Linq;
using System.Text;
using ApiSite.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ApiSite.Contexts {
    public class VerifyContext : DbContext {
        private static readonly JsVerifyResult failure = new JsVerifyResult {
            Success = false,
            Name = string.Empty,
            Guid = string.Empty
        };

        #region Public Constructors

        public VerifyContext(DbContextOptions<VerifyContext> options) : base(options) {
        }

        #endregion Public Constructors

        private DbSet<LogonHistory> LogonHistories { get; set; }

        public JsVerifyResult Verify(string token, string ip = null) {
            var existed = LogonHistories.Any(l => l.Token == token);

            if (existed) return failure;

            try {
                var tokenString = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                var tk = JsonConvert.DeserializeObject<Token>(tokenString);

                if (tk.TokenTime < DateTime.Now.AddMinutes(-5))
                    throw new ArgumentOutOfRangeException();

                var guid = Guid.NewGuid().ToString();

                LogonHistories.Add(new LogonHistory {
                    Token = token,
                    StaffId = tk.StaffId,
                    StaffName = tk.StaffName,
                    IpAddr = ip ?? tk.IpAddress,
                    Date = tk.TokenTime,
                    Guid = guid
                });

                SaveChanges();

                return new JsVerifyResult {
                    Success = true,
                    Name = tk.StaffName,
                    Guid = guid,
                    To = tk.To
                };
            } catch {
                return failure;
            }
        }

        public JsVerifyResult VerifyByGuid(string guid) {
            var token = LogonHistories.FirstOrDefault(l => l.Guid == guid);

            if (token == default)
                return failure;

            return new JsVerifyResult {
                Success = true,
                Name = token.StaffName,
                Guid = token.Guid
            };
        }
    }
}