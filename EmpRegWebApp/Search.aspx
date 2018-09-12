<%@ Page Title="Search" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Employee_Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
     <h2>Search Employee</h2>
    <hr />

    <div class="row" >
        <div class="col-sm-6">
        <div class="row">
                <div class="col-sm-4">
                    <label>Enter Employee Name</label>
                </div>
                <div class="col-sm-6">
                    <asp:TextBox runat="server" id="txtSearch" CssClass="form-control"  />
                </div>
            </div>
            <br />
             <div class="row">
                <div class="col-sm-12">
                    <asp:Button runat="server" Text="Search" CssClass="btn btn-default" id="btnSearchEmp" OnClick="BtnSearchEmp_Click"  />
                </div>
            </div> 
            </div>
    </div>
    <br />

    <div class="row" id="divSearchResults" runat="server">

    </div>

</asp:Content>

