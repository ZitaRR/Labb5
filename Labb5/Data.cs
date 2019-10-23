using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Labb5
{
    public static class Data
    {
        public static string Path { get; private set; } =
            Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\users.json";
        public static List<User> Users { get; private set; } = new List<User>();

        public static void Save()
        {
            string json = JsonConvert.SerializeObject(Users, Formatting.Indented);
            File.WriteAllText(Path, json);
        }

        public static List<User> Load()
        {
            if (!File.Exists(Path))
                return null;

            string json = File.ReadAllText(Path);
            Users = JsonConvert.DeserializeObject<List<User>>(json);
            return Users;
        }

        public static User AddUser(User user)
        {
            if (user is null)
                return null;
            Users.Add(user);
            Save();

            return user;
        }

        public static User GetUserByEmail(string email)
        {
            foreach (User _user in Users)
            {
                if (_user.Email == email)
                    return _user;
            }
            return null;
        }

        public static bool DeleteUser(User user)
        {
            for (int i = 0; i < Users.Count; i++)
            {
                if (Users[i].ID == user.ID)
                {
                    Users.Remove(user);
                    Save();
                    return true;
                }
            }
            return false;
        }

        public static User UpdateUser(User user)
        {
            for (int i = 0; i < Users.Count; i++)
            {
                if (Users[i].ID == user.ID)
                {
                    Users[i] = user;
                    Save();
                    return Users[i];
                }
            }
            return null;
        }

        public static List<User> SortNormalUsers()
        {
            List<User> users = new List<User>();
            foreach (User user in Users)
            {
                if (user.Permission == User.Permissions.Normal)
                    users.Add(user);
            }
            return users;
        }

        public static List<User> SortAdminUsers()
        {
            List<User> users = new List<User>();
            foreach (User user in Users)
            {
                if (user.Permission == User.Permissions.Admin)
                    users.Add(user);
            }
            return users;
        }
    }
}
