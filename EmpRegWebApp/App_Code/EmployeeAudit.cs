using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using static Models;

public class EmployeeAudit
{
    // Connection String
    readonly string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

    // Add new Employee Audit
    public void AddEmployeeAudit(int employeeId, int teamLeadId, int managerId, DateTime auditDate, bool isActive)
    {
        SqlConnection con = new SqlConnection(connectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand("insert into EmployeeAudit (EmployeeId, TeamLeadId, ManagerId, AuditDate, IsActive) " +
                    " values (@employeeId, @teamLeadId, @managerId, @auditDate, @isActive) ", con);
        cmd.Parameters.AddWithValue("@employeeId", employeeId);
        cmd.Parameters.AddWithValue("@teamLeadId", teamLeadId);
        cmd.Parameters.AddWithValue("@managerId", managerId);
        cmd.Parameters.AddWithValue("@auditDate", auditDate);
        cmd.Parameters.AddWithValue("isActive", isActive);

        cmd.ExecuteScalar();
        con.Close();
    }


    // Get list of Employee Audits
    public List<EmployeeAuditModel> GetAllEmployeeAudit() 
    {
        // Declare list to work with
        List<EmployeeAuditModel> employeeAuditList = new List<EmployeeAuditModel>();

        // Establish database connection
        SqlConnection con = new SqlConnection(connectionString);

        con.Open();

        // sql command for employeeAudit
        SqlCommand cmd = new SqlCommand("select * from EmployeeAudit", con);

        // This read database command
        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            // create new employeeAudit instance
            EmployeeAuditModel employeeAudits = new EmployeeAuditModel
            {
                EmployeeAuditId = reader.GetInt32(0),
                EmployeeId = reader.GetInt32(1),
                TeamLeadId = reader.GetInt32(2),
                ManagerId = reader.GetInt32(3),
                AuditDate = reader.GetDateTime(4),
                IsActive = reader.GetBoolean(5)
            };

            // add all EmployeeAudits to the list
            employeeAuditList.Add(employeeAudits);
        }

        // Close current connection to the database
        con.Close();

        return employeeAuditList;
    }


    // Get Audit by employee Id
    public EmployeeAuditModel GetEmployeeAuditByEmpId(int id) 
    {
        // Declare list to work with
        EmployeeAuditModel employeeAudit = new EmployeeAuditModel();

        // Establish database connection
        SqlConnection con = new SqlConnection(connectionString);

        con.Open();

        // sql command get EmployeeAudit by provided id
        SqlCommand cmd = new SqlCommand("select * from EmployeeAudit where EmployeeId = @id and IsActive = @activeAudit", con);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@activeAudit", true);
        // This read database command
        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read() == true)
        {
            employeeAudit.EmployeeAuditId = reader.GetInt32(0);
            employeeAudit.EmployeeId = reader.GetInt32(1);
            employeeAudit.TeamLeadId = reader.GetInt32(2);
            employeeAudit.ManagerId = reader.GetInt32(3);
            employeeAudit.AuditDate = reader.GetDateTime(4);
            employeeAudit.IsActive = reader.GetBoolean(5);
        }

        // Close current connection to the database
        con.Close();

        return employeeAudit;
    }


    // Edit Employee Audit
    public void EditEmployeeAudit(int id, int employeeId, int teamLeadId, int managerId, DateTime auditDate, bool isActive)
    {
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("update EmployeeAudit set EmployeeId = @employeeId, TeamLeadId = @teamLeadId, ManagerId = @managerId, " +
                         "AuditDate = @auditDate, IsActive = @isActive where EmployeeAuditId = @id", con);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@employeeId", employeeId);
        cmd.Parameters.AddWithValue("@teamLeadId", teamLeadId);
        cmd.Parameters.AddWithValue("@managerId", managerId);
        cmd.Parameters.AddWithValue("@auditDate", auditDate);
        cmd.Parameters.AddWithValue("isActive", isActive);

        con.Open();
        cmd.ExecuteScalar();
        con.Close();
    }


    // Delete employee audit
    public void DeleteEmployeeAudit(int id) 
    {
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("delete EmployeeAudit where EmployeeAuditId = @id ", con);
        cmd.Parameters.AddWithValue("@id", id);

        con.Open();
        cmd.ExecuteScalar();
        con.Close();

    }
}