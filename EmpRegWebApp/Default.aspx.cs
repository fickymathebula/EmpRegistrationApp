using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // if no active session exist, insist user to login
        if (Session["Name"] == null) { Response.Redirect("~/Login"); }
    }
}