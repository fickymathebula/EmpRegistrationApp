using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Models;

public partial class Teams : System.Web.UI.Page
{
    Employee employee = new Employee();
    Occupation occupation = new Occupation();
    Team team = new Team();

    protected void Page_Load(object sender, EventArgs e)
    {
        // if no active session exist, insist user to login
        if (Session["Name"] == null) { Response.Redirect("~/Login"); }

        if(!IsPostBack)
        {
            List<OccupationModel> occupationModels = occupation.GetAllOccupations();
            List<TeamModel> teamModels = team.GetAllTeams();
            List<EmployeeModel> empList = employee.GetAllEmployees();

            string strOccupation = "";

            string data = "";
            int count = 1;

            foreach(var t in teamModels)
            {
                data+= "<div class='row'><div class='col-sm-12'>";
                data += "<h3>"+ count + ". " + t.Description+"</h3><hr/>";

                foreach(var emp in empList)
                {
                    if(t.TeamId == emp.TeamId)
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
                        data += "<p>" + emp.Name + " " + emp.Surname + " - <label>" + strOccupation + "</label></p>";
                    }
                }
                count++;

                data += "</div></div>";
            }

            

            divTeamData.InnerHtml = data;
        }
    }
}