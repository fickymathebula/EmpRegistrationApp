using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Models;

public partial class AddEmployee : System.Web.UI.Page
{
    Employee employee = new Employee();
    EmployeeAudit employeeAudit = new EmployeeAudit();

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

            //// populate manager's drop down list
            //foreach (var emp in employeeModels)
            //{
            //    ListItem item = new ListItem();
            //    item.Text = emp.Name + " " + emp.Surname ;
            //    item.Value = emp.EmployeeId.ToString();
            //    drpManager.Items.Add(item);
            //}

            // populate occupation list
            foreach(var occ in occupationModels)
            {
                ListItem item = new ListItem();
                item.Text = occ.Description;
                item.Value = occ.OccupationId.ToString();
                drpOccupation.Items.Add(item);
            }

            // populate teams list
            foreach (var t in teamModels)
            {
                ListItem item = new ListItem();
                item.Text = t.Description;
                item.Value = t.TeamId.ToString();
                drpTeam.Items.Add(item);
            }
        }

    }
    
    protected void BtnAddEmployee_Click(object sender, EventArgs e) 
    {
        string name = txtName.Text;
        string surname = txtSurname.Text;
        string occupation = drpOccupation.SelectedValue;
        string team = drpTeam.SelectedValue;
        

        // To use managing errors
        string errorMsg = "";
        bool isValid = true;

        if(name.Equals(""))
        {
            errorMsg += "The field Name is required.<br/>";
            isValid = false;
        }

        if (surname.Equals(""))
        {
            errorMsg += "The field Surname is required.<br/>";
            isValid = false;
        }

        if(Convert.ToInt32(occupation) == 0) // defaulted dropdown to zero for default dropdown item
        {
            errorMsg += "The field Occupation is required.<br/>";
            isValid = false;
        }
        else if(Convert.ToInt32(occupation) == 3 && drpManager.SelectedValue.Equals("") && drpTeam.SelectedValue.Equals(""))
        {
            // If developer then required to have a team lead and manager
            errorMsg += "Please select team lead and manager for developer is required.<br/>";
            isValid = false;
        }


        if(Convert.ToInt32(team) == 0) // defaulted dropdown to zero for default dropdown item
        {
            errorMsg += "The field Team is required.<br/>";
            isValid = false;
        }

        if (isValid == true) // all details captured correctly, proceed adding employee to db
        {
            if(int.TryParse(drpTeamLead.SelectedValue, out int teamLead))
            {
                teamLead = Convert.ToInt32(drpTeam.SelectedValue);
            }
        else { teamLead = 0; }

            if (int.TryParse(drpManager.SelectedValue, out int man))
            {
                man = Convert.ToInt32(drpManager.SelectedValue);
            }
            else { man = 0; }

            int id = employee.AddEmployee(name, surname, occupation, team,teamLead, man);
            //employeeAudit.AddEmployeeAudit(id, Convert.ToInt32(team), Convert.ToInt32(manager), DateTime.Now, true);
            Response.Redirect("~/Details?Id=" + id);
        }
        else // something went wrong, display an error message
        {
            dispMsg.InnerHtml = "<p class='alert alert-danger'>" + errorMsg + "</p>";
        }
    }


    protected void DrpTeam_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Clear drop down to allow new team leaders to be added when changing 
        drpTeamLead.Items.Clear();
       // drpTeamLead.Items.Add("-- Select Team Lead --");

        List<EmployeeModel> teamLead = employee.GetTeamLeads(Convert.ToInt32(drpTeam.SelectedValue));
        // populate teams leaders
        foreach (var tl in teamLead)
        {
            ListItem item = new ListItem();
            item.Text = tl.Name + " " + tl.Surname;
            item.Value = tl.EmployeeId.ToString();
            drpTeamLead.Items.Add(item);
        }

        // Clear dropdown to add new managers linked to a team selected
        drpManager.Items.Clear();
        //drpTeamLead.Items.Add("-- Select Manager --");
        List<EmployeeModel> manager = employee.GetManagers(Convert.ToInt32(drpTeam.SelectedValue));
        // populate teams leaders
        foreach (var man in manager)
        {
            ListItem item = new ListItem();
            item.Text = man.Name + " " + man.Surname;
            item.Value = man.EmployeeId.ToString();
            drpManager.Items.Add(item);
        }

    }
}