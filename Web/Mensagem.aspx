﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MacBoot.Master" AutoEventWireup="true" CodeBehind="Mensagem.aspx.cs" Inherits="Web.Mensagem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="alert alert-success" role="alert">
        <h4 class="alert-heading"><asp:Literal ID="litMsg" runat="server" /></h4>
        
    </div>
</asp:Content>
