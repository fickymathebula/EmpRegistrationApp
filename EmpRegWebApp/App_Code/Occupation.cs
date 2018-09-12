using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using static Models;

public class Occupation
{
    // Connection String
    readonly string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;


    // Add New Occupation
    public void AddOccupation(string description)
    {
        SqlConnection con = new SqlConnection(connectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand("insert into Occupation (description) values (@description)", con);
        cmd.Parameters.AddWithValue("@description", description);

        cmd.ExecuteScalar();
        con.Close();
    }


    // Get All Occupations
    public List<OccupationModel> GetAllOccupations()
    {
        // Declare list to work with
        List<OccupationModel> occupationsList = new List<OccupationModel>();

        // Establish database connection
        SqlConnection con = new SqlConnection(connectionString);

        con.Open();

        // sql command for Occupation
        SqlCommand cmd = new SqlCommand("select * from Occupation order by Description", con);

        // This read database command
        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            // create new occupation instance
            OccupationModel occupations = new OccupationModel
            {
                OccupationId = reader.GetInt32(0),
                Description = reader.GetString(1)
            };

            // add all occupation to the list
            occupationsList.Add(occupations);
        }

        // Close current connection to the database
        con.Close();

        return occupationsList;
    }


    // Get All Occupation By ID
    public OccupationModel GetOccupationById(int id)
    {
        // Declare list to work with
        OccupationModel occupation = new OccupationModel();

        // Establish database connection
        SqlConnection con = new SqlConnection(connectionString);

        con.Open();

        // sql command get occupation by provided id
        SqlCommand cmd = new SqlCommand("select * from Occupation where OccupationId = " + id, con);

        // This read database command
        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read() == true)
        {
            occupation.OccupationId = reader.GetInt32(0);
            occupation.Description = reader.GetString(1);
        }

        // Close current connection to the database
        con.Close();

        return occupation;
    }

    // Edit Occupation
    public void EditOccupation(int id, string description)
    {
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("update Occupation set Description = @description where OccupationId = @id", con);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@description", description);

        con.Open();
        cmd.ExecuteScalar();
        con.Close();
    }

    // Delete Occupation
    public void DeleteOccupation(int id)
    {
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("delete Occupation where OccupationId = @id ", con);
        cmd.Parameters.AddWithValue("@id", id);

        con.Open();
        cmd.ExecuteScalar();
        con.Close();
    }
}