using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using static Models;

public class UserAccount
{
    // Connection String
    readonly string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;


    // Add New User Account
    public void AddUserAccount(string name, string surname, string username, string password, int userRoleId)
    {
        SqlConnection con = new SqlConnection(connectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand("insert into UserAccount (Name, Surname, UserName, Password, UserRoleId)" +
            " values (@name, @surname, @username, @password, @userRoleId)", con);
        cmd.Parameters.AddWithValue("@name", name);
        cmd.Parameters.AddWithValue("@surname", surname);
        cmd.Parameters.AddWithValue("@username", username);
        cmd.Parameters.AddWithValue("@password", password);
        cmd.Parameters.AddWithValue("@userRoleId", userRoleId);

        cmd.ExecuteScalar();
        con.Close();
    }


    // Get All User Accounts
    public List<UserAccountModel> GetAllUserAccounts()
    {
        // Declare list to work with
        List<UserAccountModel> usersList = new List<UserAccountModel>();

        // Establish database connection
        SqlConnection con = new SqlConnection(connectionString);

        con.Open();

        // sql command for login
        SqlCommand cmd = new SqlCommand("select * from UserAccount", con);

        // This read database command
        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {            
            // create new user instance
            UserAccountModel user = new UserAccountModel
            {
                UserAccountId = reader.GetInt32(0),
                Name = reader.GetString(1),
                Surname = reader.GetString(2),
                UserName = reader.GetString(3),
                Password = reader.GetString(4),
                UserRoleId = reader.GetInt32(5)
            };

            // add all users to the list
            usersList.Add(user);
        }

        // Close current connection to the database
        con.Close();

        return usersList;
    }

    // Edit User Account
    public void EditUserAccount(int id, string name, string surname, string username, string password, int userRoleId) 
    {
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("update UserAccount set Name = @name, Surname = @surname, UserName = @username "+
            "Password = @password, UserRoleId = @userRoleId where UserAccountId = @id", con);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@name", name);
        cmd.Parameters.AddWithValue("@surname", surname);
        cmd.Parameters.AddWithValue("@username", username);
        cmd.Parameters.AddWithValue("@password", password);
        cmd.Parameters.AddWithValue("@userRoleId", userRoleId);

        con.Open();
        cmd.ExecuteScalar();
        con.Close();
    }

    // Delete user account
    public void DeleteUserAccount(int id)
    {
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("delete UserAccount where UserAccountId = @id ", con);
        cmd.Parameters.AddWithValue("@id", id);

        con.Open();
        cmd.ExecuteScalar();
        con.Close();
    }
}