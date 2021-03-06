﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Epam.Task7.Users.Entities;

namespace Epam.Task7.Users.DAL
{
    public class UserFakeDao : IUserDao
    {
        private const string FilePath = "Users.txt";
        
        private int max = 0;

        public void AddUser(User user)
        {
            if (!File.Exists(FilePath))
            {
                using (FileStream fs = File.Create(FilePath))
                {
                }
            }
            
            var listOfUsers = File.ReadAllLines(FilePath);
           
            if (listOfUsers.Length == 0)
            {
                user.Id = 0;
            }
            else
            {
                foreach (var item in listOfUsers)
                {
                    string[] infoUser = item.Split(' ');
                    if (int.TryParse(infoUser[0], out int id) && this.max < id)
                    {
                        this.max = id;
                    }
                }

                user.Id = ++this.max;
            }
            
            using (StreamWriter newUser = File.AppendText(FilePath))
            {
                newUser.WriteLine(user.ToString());
            }
        }

        public void DeleteUser(int id)
        {
            var arrayOfUsers = File.ReadAllLines(FilePath);
            List<string> listOfUsers = arrayOfUsers.ToList();

            if (listOfUsers.Count != 0)
            {
                foreach (var item in listOfUsers.ToList())
                {
                    string[] infoUser = item.Split(' ');
                    if (id == int.Parse(infoUser[0]))
                    {
                        listOfUsers.Remove(item);
                        File.WriteAllLines(FilePath, listOfUsers.ToArray());
                        break;
                    }
                }
            }
        }

        public bool UpdateUser(User user)
        {
            var arrayOfUsers = File.ReadAllLines(FilePath);
           
           List<string> listOfUsers = arrayOfUsers.ToList();

            if (listOfUsers.Count != 0)
            {
                foreach (var item in listOfUsers.ToList())
                {
                    string[] infoUser = item.Split(' ');
                    if (user.Id == int.Parse(infoUser[0]))
                    {
                        listOfUsers.Remove(item);
                        listOfUsers.Add(user.ToString());
                        File.WriteAllLines(FilePath, listOfUsers.ToArray());
                        return true;
                    }
                }
            }

            return false;
        }
        
        public IEnumerable<User> GetAll()
        {
            var arrayOfUsers = File.ReadAllLines(FilePath);
            List<User> userList = new List<User>();
            string line;
            using (StreamReader streamReader = File.OpenText(FilePath))
            {
                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] oneUser = line.Split(' ');
                    int id = int.Parse(oneUser[0]);
                    string name = oneUser[1];
                    DateTime birth = DateTime.Parse(oneUser[2]);
                    User userForDict = new User
                    {
                        Id = id,
                        Name = name,
                        DateOfBirth = birth,
                    };
                    userList.Add(userForDict);
                }

                return userList;
            }
        }
    }
}
