using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Models;

public partial class Employee_Delete : System.Web.UI.Page
{
    Employee employee = new Employee();
    Occupation occupation = new Occupation();
    Team team = new Team();

    protected void Page_Load(object sender, EventArgs e)
    {
        // if no active session exist, insist user to login
        if (Session["Name"] == null) { Response.Redirect("~/Login"); }

        if (!IsPostBack)
        {
            List<OccupationModel> occupationModels = occupation.GetAllOccupations();
            List<TeamModel> teamModels = team.GetAllTeams();
            List<EmployeeModel> employeeModels = employee.GetAllEmployees();
            
            int id = Convert.ToInt32(Request.QueryString["Id"].ToString());
            EmployeeModel emp = employee.GetEmployeeById(id);

            lblName.InnerText = emp.Name;
            lblSurname.InnerText = emp.Surname;

            // get occupation
            foreach (var occ in occupationModels)
            {
                if (occ.OccupationId == emp.OccupationId)
                {
                    lblOccupation.InnerText = occ.Description;
                    break;
                }
            }

            // Get employee team description
            foreach (var t in teamModels)
            {
                if (t.TeamId == emp.TeamId)
                {
                    lblTeam.InnerText = t.Description;
                    break;
                }
            }
        }

    }

    protected void BtnDeleteEmployee_Click(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(Request.QueryString["Id"].ToString());
        employee.DeleteEmployee(id);
        Response.Redirect("~/Employee/List"); // Redirect to employee list
    }
}