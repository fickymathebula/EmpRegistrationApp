<%@ Page Title="Employee Details" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Details.aspx.cs" Inherits="Employee_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
       
    <h2>Employee Details</h2>
    <hr />

    <div class="row">
        
        <div class="col-sm-3">
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

        <div class="col-sm-3"></div>
        <div class="col-sm-3"></div>

    </div>
</asp:Content>

