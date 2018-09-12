using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using EmpRegWebApp;
using static Models;


public class Global
{
    // Database connection string for global use
    public string ConnectionString()
    {
        return WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    }

    // check if authorised
    public bool UserAuthorised()
    {                
        return false;
    }

}