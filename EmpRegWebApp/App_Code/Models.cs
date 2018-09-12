using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public static class Models
{
    // Employee Model
    public class EmployeeModel
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int OccupationId { get; set; }
        public int TeamId { get; set; }
    }

    // Employee Audit Model
    public class EmployeeAuditModel
    {
        public int EmployeeAuditId { get; set; }
        public int EmployeeId { get; set; }
        public int TeamLeadId { get; set; }
        public int ManagerId { get; set; }
        public DateTime AuditDate { get; set; }
        public bool IsActive { get; set; }
    }

    // Occupation Model
    public class OccupationModel
    {
        public int OccupationId { get; set; }
        public string Description { get; set; }
    }

    // Team Model
    public class TeamModel
    {
        public int TeamId { get; set; }
        public string Description { get; set; }
    }

    // User Account Model
    public class UserAccountModel
    {
        public int UserAccountId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int UserRoleId { get; set; }
    }

    // User Role Model
    public class UserRoleModel
    {
        public int UserRoleId { get; set; }
        public string Description { get; set; }
    }
}