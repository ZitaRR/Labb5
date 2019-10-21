using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb5
{
    public sealed class User
    {
        public static int TotalUsers = 0;

        public string Name { get; private set; }
        public string Email { get; private set; }
        public int ID { get; private set; }

        public User(string name, string email)
        {
            TotalUsers++;
            ID = TotalUsers;

            Name = name;
            Email = email;
        }
    }
}
