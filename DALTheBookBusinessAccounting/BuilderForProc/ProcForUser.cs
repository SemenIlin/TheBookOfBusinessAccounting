using DALTheBookBusinessAccounting.Entities;
using System.Data.SqlClient;

namespace DALTheBookBusinessAccounting.BuilderForProc
{
    public class ProcForUser
    {
        public void AddLogin(SqlCommand command, string login)
        {
            SqlParameter loginParam = new SqlParameter
            {
                ParameterName = "@UserLogin",
                Value = login
            };
            command.Parameters.Add(loginParam);
        }

        public void AddLogin(SqlCommand command, User user)
        {
            SqlParameter loginParam = new SqlParameter
            {
                ParameterName = "@UserLogin",
                Value = user.UserLogin
            };
            command.Parameters.Add(loginParam);
        }

        public void AddPassword(SqlCommand command, string password)
        {
            SqlParameter passwordParam = new SqlParameter
            {
                ParameterName = "@UserPassword",
                Value = password
            };
            command.Parameters.Add(passwordParam);
        }

        public void AddUserName(SqlCommand command, User user)
        {
            SqlParameter userNameParam = new SqlParameter
            {
                ParameterName = "@UserName",
                Value = user.UserLogin
            };
            command.Parameters.Add(userNameParam);
        }

        public void UpdateUserName(SqlCommand command, User user)
        {
            SqlParameter userNameParam = new SqlParameter
            {
                ParameterName = "@UserName",
                Value = user.UserName
            };
            command.Parameters.Add(userNameParam);
        }

        public void AddPassword(SqlCommand command, User user)
        {
            SqlParameter passwordNumberParam = new SqlParameter
            {
                ParameterName = "@UserPassword",
                Value = user.UserPassword
            };
            command.Parameters.Add(passwordNumberParam);
        }

        public void AddEmail(SqlCommand command, User user)
        {
            SqlParameter emailParam = new SqlParameter
            {
                ParameterName = "@Email",
                Value = user.Email
            };
            command.Parameters.Add(emailParam);
        }

        public void AddUserId(SqlCommand command, int id) 
        {
            SqlParameter idParam = new SqlParameter
            {
                ParameterName = "@Id",
                Value = id
            };
            command.Parameters.Add(idParam);
        }

        public void AddUserId(SqlCommand command, User user)
        {
            SqlParameter idParam = new SqlParameter
            {
                ParameterName = "@Id",
                Value = user.Id
            };
            command.Parameters.Add(idParam);
        }
    }
}
