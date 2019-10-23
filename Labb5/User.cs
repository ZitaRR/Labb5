using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb5
{
    public sealed class User
    {
        public enum Permissions
        {
            Normal,
            Admin
        }

        public static int TotalUsers = 0;

        public string Name { get; set; }
        public string Email { get; set; }
        public int ID { get; private set; }
        public Permissions Permission { get; set; }

        public User(string name, string email, Permissions perm = Permissions.Normal)
        {
            TotalUsers++;
            ID = TotalUsers;
            Name = name;
            Email = email;
            Permission = perm;
        }

        public string Info()
            => $"[{Permission.ToString()}] {Name}, {Email}";

        public override string ToString()
            => $"{Name}";
    }
}
