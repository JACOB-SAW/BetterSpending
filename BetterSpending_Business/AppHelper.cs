using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterSpending_Business
{
    public class AppHelper
    {
        public static string GetConnectionString()
        {
            return "Server=(localdb)\\MSSQLLocalDB;Database=BetterSpending;Trusted_Connection=True;";

        }

        public static string GeneratePasswordHash(string password)
        {
            string passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(password);
            return passwordHash;

        }

        public static bool ValidatePassword(string password, string passwordHash)
        {
            bool isValid = BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash);
            return isValid;
        }
    }
}
