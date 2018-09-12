using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Models;

public partial class Login : System.Web.UI.Page
{
    UserAccount userAcc = new UserAccount();  

    protected void Page_Load(object sender, EventArgs e)
    {        
    }

    protected void BtnLogin_Click(object sender, EventArgs e) 
    {
        // Always clear login message
        loginMsg.InnerHtml = "";

        // Login inputs
        string username = txtUsername.Text;
        string password = txtPassword.Text;
        bool loginValid = false;

        List<UserAccountModel> users = userAcc.GetAllUserAccounts();
        UserAccountModel user = new UserAccountModel();
        
        foreach(var item in users)
        {
            if (item.UserName.ToLower() == username.ToLower() && item.Password == password)
            {
                // if login match is found, set login valid and exit the loop
                loginValid = true;
                // Get user details - we only take user name and role id to work with
                user.Name = item.Name;
                user.UserRoleId = item.UserRoleId;
                break;
            } 
        }

        if(loginValid == true)
        {
            // Begin session and load home page when login succeeded
            Session["Name"] = user.Name;
            Session["UserRole"] = user.UserRoleId;                        
            Response.Redirect("~/Default");
        }
        else
        {
            // Remain in login page and show error message
            loginMsg.InnerHtml = "<p class='alert alert-danger'> Username or password incorrect.</p>";            
        }
    }
}