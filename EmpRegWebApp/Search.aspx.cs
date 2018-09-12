using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Models;

public partial class Employee_Search : System.Web.UI.Page
{
    Employee employee = new Employee();
    Occupation occupation = new Occupation();
    Team team = new Team();

    protected void Page_Load(object sender, EventArgs e)
    {
        // if no active session exist, insist user to login
        if (Session["Name"] == null) { Response.Redirect("~/Login"); }
    }

    protected void BtnSearchEmp_Click(object sender, EventArgs e)
    {
        string name = txtSearch.Text;
        if(!name.Equals(""))
        {
            List<OccupationModel> occupationModels = occupation.GetAllOccupations();
            List<TeamModel> teamModels = team.GetAllTeams();

            List<EmployeeModel> emp = employee.SearchEmployee(name);
        
        // create dynamic table to display on screen
        string pageData = "<table class='table'><thead>" +
                          "<tr><th>Name</th>" +
                          "<th>Surname</th>" +
                          "<th>Occupation</th>" +
                          "<th>Team</th>" +
                          "<th></th></tr></thead><tbody>";
        foreach (var item in emp)
        {
                string occupation = "";
                string team = "";

                // get occupation
                foreach (var occ in occupationModels)
                {
                    if (occ.OccupationId == item.OccupationId)
                    {
                        occupation = occ.Description;
                        break;
                    }
                }
                // Get employee team description
                foreach (var t in teamModels)
                {
                    if (t.TeamId == item.TeamId)
                    {
                        team = t.Description;
                        break;
                    }
                }

                // populate table rows for each employee
                pageData += "<tr><td>" + item.Name + "</td>" +
                    "<td>" + item.Surname + "</td>" +
                    "<td>"+occupation+"</td>" +
                    "<td>"+team+"</td>" +
                    "<td><a href='Edit?Id=" + item.EmployeeId + "'>" +
                    "<span class='glyphicon glyphicon-pencil'></span>Edit</a> | " +
                    "<a href='Details?Id=" + item.EmployeeId + "'>" +
                    "<span class='glyphicon glyphicon-book'></span>Details</a> | " +
                    "<a href='Delete?Id=" + item.EmployeeId + "'>" +
                    "<span class='glyphicon glyphicon-trash'></span>Delete</a>" +
                    "</td></tr>";
        }

        pageData += "</tbody></table>"; // Close dynamic table


        divSearchResults.InnerHtml = pageData;
        }
    }


}
