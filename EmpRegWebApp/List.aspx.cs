using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Models;

public partial class Employee_List : System.Web.UI.Page
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
            List<EmployeeModel> empList = employee.GetAllEmployees();

            string strOccupation = "";
            string strTeam = "";

            

            // create dynamic table to display on screen
            string pageData = "<table class='table'><thead>" +
                              "<tr><th>Name</th>" +
                              "<th>Surname</th>"+
                              "<th>Occupation</th>"+
                              "<th>Team</th>"+
                              "<th></th></tr></thead><tbody>";
            foreach (var emp in empList)
            {
                // get occupation
                foreach (var occ in occupationModels)
                {
                    if (occ.OccupationId == emp.OccupationId)
                    {
                        strOccupation = occ.Description;
                        break;
                    }
                }
                // Get employee team description
                foreach (var t in teamModels)
                {
                    if (t.TeamId == emp.TeamId)
                    {
                        strTeam = t.Description;
                        break;
                    }
                }

                // populate table rows for each employee
                pageData += "<tr><td>"+emp.Name+"</td>" +
                        "<td>"+emp.Surname+"</td>" +
                        "<td>"+strOccupation+"</td>" +
                        "<td>"+strTeam+"</td>" +
                        "<td>" +
                        "<a href='Edit?Id="+emp.EmployeeId+"'>" +
                        "<span class='glyphicon glyphicon-pencil'></span>Edit</a> | " +
                        "<a href='Details?Id=" + emp.EmployeeId + "'>" +
                        "<span class='glyphicon glyphicon-book'></span>Details</a> | " +
                        "<a href='Delete?Id=" + emp.EmployeeId + "'>" +
                        "<span class='glyphicon glyphicon-trash'></span>Delete</a>" +
                        "</td></tr>";
            }

            pageData += "</tbody></table>"; // Close dynamic table



            divEmpData.InnerHtml = pageData;
        }

    }
}