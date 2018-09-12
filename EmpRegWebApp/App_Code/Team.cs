using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using static Models;

public class Team
{
    // Connection String
    readonly string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;


    // Add New Team
    public void AddTeam(string description)
    {
        SqlConnection con = new SqlConnection(connectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand("insert into Team (description) values (@description)", con);
        cmd.Parameters.AddWithValue("@description", description);

        cmd.ExecuteScalar();
        con.Close();
    }

    // Get All Teams
    public List<TeamModel> GetAllTeams()
    {
        // Declare list to work with
        List<TeamModel> teamsList = new List<TeamModel>();

        // Establish database connection
        SqlConnection con = new SqlConnection(connectionString);

        con.Open();

        // sql command for teams
        SqlCommand cmd = new SqlCommand("select * from Team order by description", con);

        // This read database command
        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            // create new team instance
            TeamModel teams = new TeamModel
            {
                TeamId = reader.GetInt32(0),
                Description = reader.GetString(1)
            };

            // add all teams to the list
            teamsList.Add(teams);
        }

        // Close current connection to the database
        con.Close();

        return teamsList;
    }


    // Get All Team By ID
    public TeamModel GetTeamById(int id)
    {
        // Declare list to work with
        TeamModel team = new TeamModel();

        // Establish database connection
        SqlConnection con = new SqlConnection(connectionString);

        con.Open();

        // sql command get team by provided id
        SqlCommand cmd = new SqlCommand("select * from Team where TeamId = " + id, con);

        // This read database command
        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read() == true)
        {
            team.TeamId = reader.GetInt32(0);
            team.Description = reader.GetString(1);
        }

        // Close current connection to the database
        con.Close();

        return team;
    }

    // Edit Team
    public void EditTeam(int id, string description) 
    {
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("update Team set Description = @description where TeamId = @id", con);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@description", description);

        con.Open();
        cmd.ExecuteScalar();
        con.Close();
    }

    // Delete Team
    public void DeleteTeam(int id)
    {
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("delete Team where TeamId = @id ", con);
        cmd.Parameters.AddWithValue("@id", id);

        con.Open();
        cmd.ExecuteScalar();
        con.Close();
    }

}