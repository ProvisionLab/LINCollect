﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using DynamicSurvey.Server.DAL.Entities;
using MySql.Data.MySqlClient;

namespace DynamicSurvey.Server.DAL.Repositories
{

    public interface IUsersRepository
    {
        void AddOrUpdate(User caller, User target);
        void Remove(User caller, string username);
        User GetUserByName(string username);
        User[] GetUsers(User caller, UsersFilter filter);

        bool Authorize(string username, string password);

        bool CheckCredentials(string username, string hashedPassword);
    }

    public class UsersRepository : IUsersRepository
    {
        public void AddOrUpdate(User caller, User target)
        {
            var isTargetExists = CheckCredentials(target.Username, target.Password);

            if (isTargetExists)
            {
                DataEngine.Engine.ExecuteStoredProcedure(DataEngine.sp_update_user, cmd =>
                {
                    cmd.Parameters.Add("creator_login", caller.Username);
                    cmd.Parameters.Add("creator_password", caller.Password);
                    cmd.Parameters.Add("target_id", target.Id);
                    cmd.Parameters.Add("target_username", target.Username);
                    cmd.Parameters.Add("target_password", target.Password);
                    cmd.Parameters.Add("target_salt", target.Salt);
                    cmd.Parameters.Add("target_right_id", target.AccessRight.Id);
                    cmd.Parameters.Add("target_right_name", target.AccessRight.Name);
                });
            }
            else
            {
                DataEngine.Engine.ExecuteStoredProcedure(DataEngine.sp_add_user, cmd =>
                {
                    cmd.Parameters.Add("creator_login", caller.Username);
                    cmd.Parameters.Add("creator_password", caller.Salt);
                    cmd.Parameters.Add("target_username", target.Username);
                    cmd.Parameters.Add("target_password", target.Password);
                    cmd.Parameters.Add("target_salt", target.Salt);
                    cmd.Parameters.Add("target_right_id", target.AccessRight.Id);
                });
            }
        }

        // TODO: move to common
        public string CalculateMD5Hash(string input)
        {

            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(i.ToString("X2"));
            }

            return sb.ToString();
        }

        public void Remove(User caller, string username)
        {
            throw new NotImplementedException();

        }

        private void DataRowToUser(DataRow row, out User user)
        {
            user = new User
            {
                Salt = Convert.ToString(row["Salt"]),
                Id = (ulong) row["Id"],
                Password = Convert.ToString(row["Password"]),
                Username = Convert.ToString(row["Login"]),
                AccessRight = new AccessRight
                {
                    Name = Convert.ToString(row["UserRight"]),
                    AccessLevel = (AccessLevel) Enum.Parse(typeof (AccessLevel), Convert.ToString(row["AccessLevel"])),
                    Id = (ulong) row["UserRightId"]
                }

                // TODO: Language
                // user.SupportedLanguages = 
            };

        }

        public User GetUserByName(string username)
        {

            var usernameClause = "@Username";
            User res = null;
            DataEngine.Engine.SelectFromView(DataEngine.vw_user, row => DataRowToUser(row, out res),
                whereClause: "WHERE Login = " + usernameClause,
                fillCommandAction: cmd =>
                {
                    cmd.Parameters.AddWithValue(usernameClause, username);
                });

            return res;
        }

        public User[] GetUsers(User caller, UsersFilter filter)
        {
            //TODO: filters

            var userList = new List<User>();
            DataEngine.Engine.SelectFromView(DataEngine.vw_user, row =>
            {
                User user = null;
                DataRowToUser(row, out user);
                userList.Add(user);
            });
            return userList.ToArray();
        }


        public bool Authorize(string username, string password)
        {

            var user = GetUserByName(username);

            if (user == null)
                return false;

            var hashedPassword = CalculateMD5Hash(user.Salt + password);
            return CheckCredentials(username, hashedPassword);
        }

        public bool CheckCredentials(string username, string hashedPassword)
        {
            var res = DataEngine.Engine.ExecuteStoredProcedure(DataEngine.sp_is_user_exists, cmd =>
            {
                cmd.Parameters.Add(new MySqlParameter("username", username));
                cmd.Parameters.Add(new MySqlParameter("password", hashedPassword));
            });

            return res != 0;
        }
    }
}
