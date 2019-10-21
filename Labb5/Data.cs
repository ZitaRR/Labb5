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
            Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"users.json";
        public static List<User> Users { get; private set; } = new List<User>();

        public static void Save()
        {
            string json = JsonConvert.SerializeObject(Users, Formatting.Indented);
            File.WriteAllText(json, Path);
        }

        public static List<User> Load()
        {
            if (!File.Exists(Path))
                return null;

            string json = File.ReadAllText(Path);
            return JsonConvert.DeserializeObject<List<User>>(json);
        }

        public static bool AddUser(User user)
        {
            if (user is null)
                return false;
            Users.Add(user);
            Save();

            return true;
        }
    }
}
