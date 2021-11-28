<%@ Page Title="" Language="C#" MasterPageFile="~/MacBoot.Master" AutoEventWireup="true" CodeBehind="Visualizacao.aspx.cs" Inherits="Web.Visualizacao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <h2>Pessoas - Visualização</h2>

        <div class="row">
            <div class="col-md-8 form-group">
                <asp:Label runat="server" Text="Nome"></asp:Label>
                <asp:Label runat="server" ID="lblNome" CssClass="font-weight-bold d-block"></asp:Label>
            </div>
            <div class="col-md-4 form-group">
                <asp:Label runat="server" Text="CPF"></asp:Label>
                <asp:Label runat="server" ID="lblCPF" CssClass="font-weight-bold d-block"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-8 form-group">
                <asp:Label runat="server" Text="Logradouro"></asp:Label>
                <asp:Label runat="server" ID="lblLogradouro" CssClass="font-weight-bold d-block"></asp:Label>
            </div>
            <div class="col-md-4 form-group">
                <asp:Label runat="server" Text="Número"></asp:Label>
                <asp:Label runat="server" ID="lblNumero" CssClass="font-weight-bold d-block"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4 form-group">
                <asp:Label runat="server" Text="Bairro"></asp:Label>
                <asp:Label runat="server" ID="lblBairro" CssClass="font-weight-bold d-block"></asp:Label>
            </div>
            <div class="col-md-3 form-group">
                <asp:Label runat="server" Text="Cidade"></asp:Label>
                <asp:Label runat="server" ID="lblCidade" CssClass="font-weight-bold d-block"></asp:Label>
            </div>
            <div class="col-md-2 form-group">
                <asp:Label runat="server" Text="Estado"></asp:Label>
                <asp:Label runat="server" ID="lblEstado" CssClass="font-weight-bold d-block"></asp:Label>
            </div>
            <div class="col-md-3 form-group">
                <asp:Label runat="server" Text="CEP" ></asp:Label>
                <asp:Label runat="server" ID="lblCep" CssClass="font-weight-bold d-block"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12 form-group">
                <asp:GridView ID="gridTelefones" runat="server" CssClass="table table-striped"  AutoGenerateColumns="false" >
                    <Columns>
                        <asp:TemplateField HeaderText="Tipo" HeaderStyle-CssClass="table-dark">
                            <ItemTemplate> <%# Eval("Tipo.Tipo") %> </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DDD" HeaderStyle-CssClass="table-dark">
                            <ItemTemplate> <%# Eval("Ddd") %> </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Número" HeaderStyle-CssClass="table-dark">
                            <ItemTemplate> <%# Eval("Numero") %> </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <asp:Button ID="btnAlterar" runat="server" CssClass="btn btn-success" Text="Editar" OnClick="btnAlterar_Click" />
                <asp:Button ID="btnExlcuir" runat="server" CssClass="btn btn-danger" Text="Excluir" OnClick="btnExlcuir_Click" />
            </div>
        </div>


    </form>
</asp:Content>
