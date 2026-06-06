<%@ Page Title="Editar Lancamentos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LancamentoEditar.aspx.cs" Inherits="ControleFinanceiro.LancamentoEditar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Editar Lançamento</h2>

    <asp:HiddenField ID="hdnId" runat="server" />

    Descrição:<br />
    <asp:TextBox ID="txtDescricao" runat="server" />
    <br /><br />

    Tipo:<br />
    <asp:DropDownList ID="ddlTipo" runat="server">
        <asp:ListItem Text="Selecione" Value="" />
        <asp:ListItem Text="Crédito" Value="Credito" />
        <asp:ListItem Text="Débito" Value="Debito" />
    </asp:DropDownList>
    <br /><br />

    Valor Original:<br />
    <asp:TextBox ID="txtValorOriginal" runat="server" />
    <br /><br />

    Percentual Taxa:<br />
    <asp:TextBox ID="txtPercentualTaxa" runat="server" />
    <br /><br />

    Percentual Desconto:<br />
    <asp:TextBox ID="txtPercentualDesconto" runat="server" />
    <br /><br />

    Data de Lançamento:<br />
    <asp:TextBox ID="txtDataLancamento" runat="server" TextMode="Date" />
    <br /><br />

    Competência:<br />
    <asp:TextBox ID="txtCompetencia" runat="server" />
    <br /><br />

    <asp:Button ID="btnSalvar" runat="server" Text="Salvar Alterações" OnClick="btnSalvar_Click" />

    <br /><br />

    <asp:Label ID="lblMensagem" runat="server" />

</asp:Content>