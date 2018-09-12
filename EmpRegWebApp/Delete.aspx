<%@ Page Title="Delete Employee" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Delete.aspx.cs" Inherits="Employee_Delete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
        <h2>Delete Employee</h2>
    <hr />
        <div class="row">
        
        <div class="col-sm-12">
            <div class="row">
                <div class="col-sm-4">
                    <label>Name </label>
                </div>
                <div class="col-sm-6">
                    <p runat="server" id="lblName"></p>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-sm-4">
                    <label>Surname </label>
                </div>
                <div class="col-sm-6">
                    <p runat="server" id="lblSurname"></p>
                </div>
            </div>
            <br />
             <div class="row">
                <div class="col-sm-4">
                    <label>Occupation </label>
                </div>
                <div class="col-sm-6">
                    <p runat="server" id="lblOccupation"></p>
                </div>
            </div>
            <br />
             <div class="row">
                <div class="col-sm-4">
                    <label>Team </label>
                </div>
                <div class="col-sm-6">
                    <p runat="server" id="lblTeam"></p>
                </div>
            </div>
            <br />
        </div>
            
            <br />
            <div class="row">
                <div class="col-sm-12">
                    <asp:Button runat="server" Text="Delete" CssClass="btn btn-default" ID="BtnDeleteEmployee" OnClick="BtnDeleteEmployee_Click" />
                </div>
            </div>

    </div>
</asp:Content>

