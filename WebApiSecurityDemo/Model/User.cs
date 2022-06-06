using System;

namespace WebApiSecurityDemo.Model
{
    public class User
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModificatedDate { get; set; }

        public DateTime LastAccess { get; set; }

        public int FailedAttempts { get; set; }
    }
}