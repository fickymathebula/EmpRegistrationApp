using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using static Models;

public class Employee
{
    EmployeeAudit employeeAudit = new EmployeeAudit();

    // Connection String
    readonly string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

    // Add new Employee
    public int AddEmployee(string name, string surname, string occupationId, string TeamId, int TeamLeadId, int ManagerId)
    {
        SqlConnection con = new SqlConnection(connectionString);
        string query2 = "select @@Identity"; // used to get last added record
        SqlCommand cmd = new SqlCommand("insert into Employee (Name, Surname, OccupationId, TeamId) " +
                    "values (@name, @surname, @occupationId, @TeamId) ", con);
        cmd.Parameters.AddWithValue("@name", name);
        cmd.Parameters.AddWithValue("@surname", surname);
        cmd.Parameters.AddWithValue("@occupationId", occupationId);
        cmd.Parameters.AddWithValue("@TeamId", TeamId);
        
        con.Open();
        cmd.ExecuteNonQuery();
        cmd.CommandText = query2;
        int proId = Convert.ToInt32(cmd.ExecuteScalar()); // Get newly added record ID
        con.Close();

        // Add employee details to transaction table
        employeeAudit.AddEmployeeAudit(proId, TeamLeadId, ManagerId, DateTime.Now, true);

        return proId;
    }

    
    // All Employees
    public List<EmployeeModel> GetAllEmployees()
    {
        // Declare list to work with
        List<EmployeeModel> employeeList = new List<EmployeeModel>();

        // Establish database connection
        SqlConnection con = new SqlConnection(connectionString);

        con.Open();

        // sql command for Employee
        SqlCommand cmd = new SqlCommand("select * from Employee order by Name", con);

        // This read database command
        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            // create new Employee instance
            EmployeeModel employees = new EmployeeModel
            {
                EmployeeId = reader.GetInt32(0),
                Name = reader.GetString(1),
                Surname = reader.GetString(2),
                OccupationId = reader.GetInt32(3),
                TeamId = reader.GetInt32(4)
            };

            // add all Employees to the list
            employeeList.Add(employees);
        }

        // Close current connection to the database
        con.Close();

        return employeeList;
    }

    // Get Team Lead per team selected
    public List<EmployeeModel> GetTeamLeads(int teamId) 
    {
        // Declare list to work with
        List<EmployeeModel> employeeList = new List<EmployeeModel>();

        // Establish database connection
        SqlConnection con = new SqlConnection(connectionString);

        con.Open();

        // sql command for Employee
        SqlCommand cmd = new SqlCommand("select * from Employee where OccupationId = 2 and TeamId = @teamId order by Name", con);
        cmd.Parameters.AddWithValue("@teamId", teamId);

        // This read database command
        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            // create new Employee instance
            EmployeeModel employees = new EmployeeModel
            {
                EmployeeId = reader.GetInt32(0),
                Name = reader.GetString(1),
                Surname = reader.GetString(2),
                OccupationId = reader.GetInt32(3),
                TeamId = reader.GetInt32(4)
            };

            // add all Employees to the list
            employeeList.Add(employees);
        }

        // Close current connection to the database
        con.Close();

        return employeeList;
    }

    // Get manaser selected
    public List<EmployeeModel> GetManagers(int teamId) 
    {
        // Declare list to work with
        List<EmployeeModel> employeeList = new List<EmployeeModel>();

        // Establish database connection
        SqlConnection con = new SqlConnection(connectionString);

        con.Open();

        // sql command for Employee
        SqlCommand cmd = new SqlCommand("select * from Employee where OccupationId = 1 and TeamId = @teamId order by Name", con);
        cmd.Parameters.AddWithValue("@teamId", teamId);

        // This read database command
        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            // create new Employee instance
            EmployeeModel employees = new EmployeeModel
            {
                EmployeeId = reader.GetInt32(0),
                Name = reader.GetString(1),
                Surname = reader.GetString(2),
                OccupationId = reader.GetInt32(3),
                TeamId = reader.GetInt32(4)
            };

            // add all Employees to the list
            employeeList.Add(employees);
        }

        // Close current connection to the database
        con.Close();

        return employeeList;
    }

    // Used To Search For Employee by name or surname
    public List<EmployeeModel> SearchEmployee(string name)
    {
        // Declare list to work with
        List<EmployeeModel> employeeList = new List<EmployeeModel>();

        // Establish database connection
        SqlConnection con = new SqlConnection(connectionString);

        con.Open();

        // sql command for Employee
        SqlCommand cmd = new SqlCommand("select * from Employee where Name like '%"+name+"%' or Surname like'%"+name+"%'", con);
        
        // This read database command
        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            // create new Employee instance
            EmployeeModel employees = new EmployeeModel
            {
                EmployeeId = reader.GetInt32(0),
                Name = reader.GetString(1),
                Surname = reader.GetString(2),
                OccupationId = reader.GetInt32(3),
                TeamId = reader.GetInt32(4)
            };

            // add all Employees to the list
            employeeList.Add(employees);
        }

        // Close current connection to the database
        con.Close();

        return employeeList;
    }


    // Get Employee By Id
    public EmployeeModel GetEmployeeById(int id)
    {
        // Declare list to work with
        EmployeeModel employee = new EmployeeModel();

        // Establish database connection
        SqlConnection con = new SqlConnection(connectionString);

        con.Open();

        // sql command get Employee by provided id
        SqlCommand cmd = new SqlCommand("select * from Employee where EmployeeId = @id", con);
        cmd.Parameters.AddWithValue("@id", id);
        // This read database command
        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read() == true)
        {
            employee.EmployeeId = reader.GetInt32(0);
            employee.Name = reader.GetString(1);
            employee.Surname = reader.GetString(2);
            employee.OccupationId = reader.GetInt32(3);
            employee.TeamId = reader.GetInt32(4);
        }

        // Close current connection to the database
        con.Close();

        return employee;
    }

    public void EditEmployee(int id, string name, string surname, int occupationId, int TeamId, int TeamLeadId, int ManagerId)
    {
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("update Employee set Name = @name, Surname = @surname, OccupationId = @occupationId, " +
                         "TeamId = @teamId where EmployeeId = @id", con);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@name", name);
        cmd.Parameters.AddWithValue("@surname", surname);
        cmd.Parameters.AddWithValue("@occupationId", occupationId);
        cmd.Parameters.AddWithValue("@teamId", TeamId);

        con.Open();
        cmd.ExecuteScalar();
        con.Close();


    }

    public void DeleteEmployee(int id)
    {
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("delete Employee where EmployeeId = @id ", con);
        cmd.Parameters.AddWithValue("@id", id);

        con.Open();
        cmd.ExecuteScalar();
        con.Close();
    }
}