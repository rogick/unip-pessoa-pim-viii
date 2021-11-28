<%@ Page Title="" Language="C#" MasterPageFile="~/MacBoot.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="Web.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <h2>Pessoas - Cadastro</h2>

        <asp:HiddenField ID="hdId" runat="server" />
        <div class="row">
            <div class="col-md-8 form-group">
                <asp:Label runat="server" Text="Nome" AssociatedControlID="txtNome"></asp:Label>
                <asp:TextBox ID="txtNome" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-4 form-group">
                <asp:Label runat="server" Text="CPF" AssociatedControlID="txtCPF"></asp:Label>
                <asp:TextBox ID="txtCPF" TextMode="Number" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-8 form-group">
                <asp:Label runat="server" Text="Logradouro" AssociatedControlID="txtLogradouro"></asp:Label>
                <asp:TextBox ID="txtLogradouro" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-4 form-group">
                <asp:Label runat="server" Text="Número" AssociatedControlID="txtNumero"></asp:Label>
                <asp:TextBox ID="txtNumero" TextMode="Number" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4 form-group">
                <asp:Label runat="server" Text="Bairro" AssociatedControlID="txtBairro"></asp:Label>
                <asp:TextBox ID="txtBairro" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-3 form-group">
                <asp:Label runat="server" Text="Cidade" AssociatedControlID="txtCidade"></asp:Label>
                <asp:TextBox ID="txtCidade" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-2 form-group">
                <asp:Label runat="server" Text="Estado" AssociatedControlID="ddlEstado"></asp:Label>
                <asp:DropDownList ID="ddlEstado" CssClass="dropdown form-control" runat="server"></asp:DropDownList>
            </div>
            <div class="col-md-3 form-group">
                <asp:Label runat="server" Text="CEP" AssociatedControlID="txtCep"></asp:Label>
                <asp:TextBox TextMode="Number" ID="txtCep" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12 form-group">
                <asp:GridView ID="gridTelefones" runat="server" AutoGenerateColumns="false" CssClass="table table-striped"
                        OnRowDataBound="gridTelefones_RowDataBound" 
                        OnRowDeleting="gridTelefones_RowDeleting"
                    >
                    <Columns>
                        <asp:TemplateField HeaderText="Tipo" ItemStyle-CssClass="form-control-sm" HeaderStyle-CssClass="table-dark">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlTipoTelefone" CssClass="dropdown form-control" runat="server" OnSelectedIndexChanged="ddlTipoTelefone_SelectedIndexChanged" OnTextChanged="ddlTipoTelefone_SelectedIndexChanged" >
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DDD" ItemStyle-CssClass="form-control-sm" HeaderStyle-CssClass="table-dark">
                            <ItemTemplate>
                                <asp:TextBox TextMode="Number" runat="server" CssClass="form-control" Text='<%# Eval("Ddd") %>' MaxLength="2" OnTextChanged="txtDdd_TextChanged"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Número" ItemStyle-CssClass="form-control-sm" HeaderStyle-CssClass="table-dark">
                            <ItemTemplate>
                                <asp:TextBox TextMode="Number" runat="server" CssClass="form-control" Text='<%# Eval("Numero") %>' MaxLength="9" OnTextChanged="txtNumero_TextChanged"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="true" DeleteText="Deletar" HeaderStyle-CssClass="table-dark text-center" ItemStyle-CssClass="text-center" ControlStyle-CssClass="btn btn-danger" />
                    </Columns>
                </asp:GridView>
                <asp:Button ID="btnAdicionarTelefone" runat="server" CssClass="btn btn-info" Text="Novo Telefone" OnClick="btnAdicionarTelefone_Click"   />
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <asp:Button ID="btnSalvar" runat="server" CssClass="btn btn-success margin-right" Text="Salvar" OnClick="btnSalvar_Click" />
                <asp:Button ID="btnLimpar" runat="server" CssClass="btn btn-danger" Text="Limpar" OnClick="btnLimpar_Click" />
            </div>
        </div>


    </form>
</asp:Content>
