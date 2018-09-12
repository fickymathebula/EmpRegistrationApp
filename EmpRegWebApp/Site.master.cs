using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteMaster : MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {        if (!IsPostBack)
        {
            // if no active session exist, insist user to login
            if (Session["Name"] == null)
            {
                lblLoginOrLogout.InnerHtml = "";// "<a runat='server' href='Login'>Login</a>";
                                                //Response.Redirect("~/Login");            
            }
            else
            {
                // Custom display of menu
                divEmployee.InnerHtml = "";

                divEmployee.InnerHtml = "<a class='dropdown-toggle' data-toggle='dropdown' href='#'>Employees" +
                           "<span class='caret'></span></a>" +
                           "<ul class='dropdown-menu'>" +
                           "<li><a runat='server' href='Add'>Add Employee</a></li>" +
                           "<li><a runat='server' href='List'>Employees List</a></li>" +
                           "<li><a runat='server' href='Search'>Search Employees</a></li>"+
                           "<li><a runat='server' href='Teams'>Teams</a></li></ul>";
            }
        }
    }

    // Release session and show login page
    protected void Logout_Click(object sender, EventArgs e)
    {
        Session.RemoveAll();
        Response.Redirect("~/Login");
    }
}