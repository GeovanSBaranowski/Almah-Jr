<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LancamentoListagem.aspx.cs" Inherits="ControleFinanceiro.LancamentoListagem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Listagem de Lançamentos</h2>

    <h3>
        Saldo:
        <asp:Label ID="lblSaldo" runat="server" />
    </h3>

    <asp:GridView
        ID="gvLancamentos"
        runat="server"
        AutoGenerateColumns="true">
    </asp:GridView>

</asp:Content>
