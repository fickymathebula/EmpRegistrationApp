using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using static Models;

public class UserRole
{
    // Connection String
    readonly string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;


    // Add New User Role
    public int AddUserRole(string description)
    {
        SqlConnection con = new SqlConnection(connectionString);
        
        SqlCommand cmd = new SqlCommand("insert into UserRole (description) values (@description)", con);
        SqlParameter parmNewId = new SqlParameter()
        {
            ParameterName = "@newId",
            Value = -1,
            Direction = ParameterDirection.Output
        };
        cmd.Parameters.AddWithValue("@description",description);

        con.Open();
        int proId = Convert.ToInt32(cmd.ExecuteScalar()); // Get newly added record ID
        con.Close();

        return proId;
    }


    // Get All User Roles
    public List<UserRoleModel> GetAllUserRoles() 
    {
        // Declare list to work with
        List<UserRoleModel> usersRolesList = new List<UserRoleModel>();

        // Establish database connection
        SqlConnection con = new SqlConnection(connectionString);

        con.Open();

        // sql command for user roles
        SqlCommand cmd = new SqlCommand("select * from UserRole order by description", con);

        // This read database command
        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            // create new user roles instance
            UserRoleModel userRoles = new UserRoleModel
            {
                UserRoleId = reader.GetInt32(0),
                Description = reader.GetString(1)
            };

            // add all user roles to the list
            usersRolesList.Add(userRoles);
        }

        // Close current connection to the database
        con.Close();

        return usersRolesList;
    }


    // Get All User Role By ID
    public UserRoleModel GetUserRoleById(int id) 
    {
        // Declare list to work with
        UserRoleModel userRole = new UserRoleModel();

        // Establish database connection
        SqlConnection con = new SqlConnection(connectionString);

        con.Open();

        // sql command get user role by provided id
        SqlCommand cmd = new SqlCommand("select * from UserRole where UserRoleId = " + id, con);

        // This read database command
        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read() == true)
        {
            userRole.UserRoleId = reader.GetInt32(0);
            userRole.Description = reader.GetString(1);            
        }

        // Close current connection to the database
        con.Close();

        return userRole;
    }

    // Edit User Role
    public void EditUserRole(int id, string description)
    {
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("update UserRole set Description = @description where UserRoleId = @id", con);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@description", description);

        con.Open();
        cmd.ExecuteScalar();
        con.Close();
    }


    // Delete User Role
    public void DeleteUserRole(int id)
    {
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("delete UserRole where UserRoleId = @id ", con);
        cmd.Parameters.AddWithValue("@id", id);

        con.Open();
        cmd.ExecuteScalar();
        con.Close();
    }
}