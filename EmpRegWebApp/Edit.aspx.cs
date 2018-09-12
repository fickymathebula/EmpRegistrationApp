using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Models;

public partial class Employee_Edit : System.Web.UI.Page
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

            // populate occupation list
            foreach (var occ in occupationModels)
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

            LoadLeaders();

            // Load employee to edit
            int id = Convert.ToInt32(Request.QueryString["Id"].ToString());
            EmployeeModel emp = employee.GetEmployeeById(id);
            
            if(emp != null)
            {
                // Populate input fields with data of the employee
                txtName.Text = emp.Name;
                txtSurname.Text = emp.Surname;
                drpOccupation.SelectedValue = emp.OccupationId.ToString();
                drpTeam.SelectedValue = emp.TeamId.ToString();

            }
        }
}

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(Request.QueryString["Id"].ToString());
        EmployeeModel emp = employee.GetEmployeeById(id);
        emp.Name = txtName.Text;
        emp.Surname = txtSurname.Text;
        emp.OccupationId = Convert.ToInt32(drpOccupation.SelectedValue.ToString());
        emp.TeamId = Convert.ToInt32(drpTeam.SelectedValue.ToString());

        if (int.TryParse(drpTeamLead.SelectedValue, out int teamLead))
        {
            teamLead = Convert.ToInt32(drpTeam.SelectedValue);
        }
        else { teamLead = 0; }

        if (int.TryParse(drpManager.SelectedValue, out int man))
        {
            man = Convert.ToInt32(drpManager.SelectedValue);
        }
        else { man = 0; }
        
        employee.EditEmployee(emp.EmployeeId, emp.Name,emp.Surname, emp.OccupationId, emp.TeamId, teamLead  , man);
        Response.Redirect("~/Details?Id=" + id);
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

    public void LoadLeaders()
    {
        // Clear drop down to allow new team leaders to be added when changing 
        //drpTeamLead.Items.Clear();
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
        //drpManager.Items.Clear();
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