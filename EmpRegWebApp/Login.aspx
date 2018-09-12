<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">   
    <h2>Login</h2>
    <hr />

    <div class="row">
        <div class="col-sm-3"></div>
        <div class="col-sm-6" id="loginMsg" runat="server"></div>
    </div>

    <div class="row">
        
        <div class="col-sm-3"></div>

        <div class="col-sm-6">        
            <div class="row">
                <div class="col-sm-4">
                    <label>Username</label>
                </div>
                <div class="col-sm-6">
                    <asp:TextBox runat="server" id="txtUsername" CssClass="form-control" />
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-sm-4">
                    <label>Password</label>
                </div>
                <div class="col-sm-6">
                    <asp:TextBox runat="server" id="txtPassword" TextMode="Password" CssClass="form-control" />
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-sm-12">
                    <asp:Button runat="server" Text="Log in" CssClass="btn btn-default" id="btnLogin" OnClick="BtnLogin_Click" />
                </div>
            </div>            

        </div>
        <div class="col-sm-3"></div>
    </div>
</asp:Content>