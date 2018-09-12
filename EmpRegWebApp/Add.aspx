<%@ Page Title="Add Employee" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="AddEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <h2>Add Employee</h2>
    <hr />

    <%--This is the error message area--%>
     <div class="row">
        <div class="col-sm-3"></div>
        <div id="dispMsg" runat="server" class="col-sm-6"></div>
    </div>

    <div class="row">
        <div class="col-sm-3"></div>
        <div class="col-sm-6">
            <div class="row">
                <div class="col-sm-4">
                    <label>Name</label>
                </div>
                <div class="col-sm-6">
                    <asp:TextBox ID="txtName" runat="server" class="form-control"/>
                </div>
            </div>
            <br />

            <div class="row">
                <div class="col-sm-4">
                    <label>Surname</label>
                </div>
                <div class="col-sm-6">
                    <asp:TextBox ID="txtSurname" runat="server" class="form-control"/>
                </div>
            </div>
            <br />

            <div class="row">
                <div class="col-sm-4">
                    <label>Occupation</label>
                </div>
                <div class="col-sm-6">
                    <asp:DropDownList ID="drpOccupation" runat="server" class="form-control">
                        <asp:ListItem Value="0">-- Select Occupation --</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <br />

            <div class="row">
                <div class="col-sm-4">
                    <label>Team</label>
                </div>
                <div class="col-sm-6">
                    <asp:DropDownList ID="drpTeam" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="DrpTeam_SelectedIndexChanged">
                        <asp:ListItem Value="0">-- Select Team --</asp:ListItem>
                    </asp:DropDownList>
                    <%--<select id="drpTeam" runat="server" class="form-control" >
                        <option value="">-- Select Team --</option>
                    </select>--%>
                </div>
            </div>
            <br />

            <div class="row">
                <div class="col-sm-4">
                    <label>Team Lead</label>
                </div>
                <div class="col-sm-6">
                     <asp:DropDownList ID="drpTeamLead" runat="server" class="form-control">
                        <%--<asp:ListItem Value="0">-- Select Team Lead --</asp:ListItem>--%>
                    </asp:DropDownList>
                </div>
            </div>
            <br />

            <div class="row">
                <div class="col-sm-4">
                    <label>Manager</label>
                </div>
                <div class="col-sm-6">
                    <asp:DropDownList ID="drpManager" runat="server" class="form-control">
                        <%--<asp:ListItem Value="0">-- Select Manager --</asp:ListItem>--%>
                    </asp:DropDownList>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-sm-12">
                    <asp:Button runat="server" Text="Add" CssClass="btn btn-default" ID="btnAddEmployee" OnClick="BtnAddEmployee_Click" />
                </div>
            </div>

        </div>
        <div class="col-sm-3"></div>
    </div>

</asp:Content>
