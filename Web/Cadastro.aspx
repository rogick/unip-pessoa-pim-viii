<%@ Page Title="" Language="C#" MasterPageFile="~/MacBoot.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="Web.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <h2><asp:Literal ID="litTitulo" runat="server" /></h2>

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
                <asp:DropDownList ID="ddlEstado" CssClass="dropdown form-control" runat="server">
                    <asp:ListItem Value="AC" Text="AC" />
                    <asp:ListItem Value="AL" Text="AL" />
                    <asp:ListItem Value="AM" Text="AM" />
                    <asp:ListItem Value="BA" Text="BA" />
                    <asp:ListItem Value="CE" Text="CE" />
                    <asp:ListItem Value="DF" Text="DF" />
                    <asp:ListItem Value="ES" Text="ES" />
                    <asp:ListItem Value="GO" Text="GO" />
                    <asp:ListItem Value="MA" Text="MA" />
                    <asp:ListItem Value="MG" Text="MG" />
                    <asp:ListItem Value="MS" Text="MS" />
                    <asp:ListItem Value="MT" Text="MT" />
                    <asp:ListItem Value="PA" Text="PA" />
                    <asp:ListItem Value="PB" Text="PB" />
                    <asp:ListItem Value="PE" Text="PE" />
                    <asp:ListItem Value="PI" Text="PI" />
                    <asp:ListItem Value="RJ" Text="RJ" />
                    <asp:ListItem Value="RN" Text="RN" />
                    <asp:ListItem Value="RO" Text="RO" />
                    <asp:ListItem Value="RR" Text="RR" />
                    <asp:ListItem Value="RS" Text="RS" />
                    <asp:ListItem Value="SC" Text="SC" />
                    <asp:ListItem Value="SE" Text="SE" />
                    <asp:ListItem Value="SP" Text="SP" />
                    <asp:ListItem Value="TO" Text="TO" />
                </asp:DropDownList>
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
                                <asp:TextBox TextMode="Number" runat="server" CssClass="form-control" Text='<%# (int)Eval("Ddd") != 0 ? Eval("Ddd") : "" %>' MaxLength="2" OnTextChanged="txtDdd_TextChanged"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Número" ItemStyle-CssClass="form-control-sm" HeaderStyle-CssClass="table-dark">
                            <ItemTemplate>
                                <asp:TextBox TextMode="Number" runat="server" CssClass="form-control" Text='<%# (int)Eval("Numero") != 0 ? Eval("Numero") : "" %>' MaxLength="9" OnTextChanged="txtNumero_TextChanged"></asp:TextBox>
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
