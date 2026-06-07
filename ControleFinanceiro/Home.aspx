<%@ Page Title="Início"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Home.aspx.cs"
    Inherits="ControleFinanceiro.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Controle Financeiro</h1>

        <p>
            Bem-vindo ao meu projeto de Controle Financeiro.
        </p>

        <p>
            Este sistema permite:
        </p>

        <ul>
            <li>Cadastrar lançamentos</li>
            <li>Editar lançamentos</li>
            <li>Pagar lançamentos</li>
            <li>Cancelar lançamentos</li>
            <li>Exportar dados em CSV</li>
            <li>Controlar saldo financeiro</li>
        </ul>

        <br />

    </div>

</asp:Content>