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
        AutoGenerateColumns="false"
        OnRowCommand="gvLancamentos_RowCommand"
        DataKeyNames="Id">
    
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" />
            <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
            <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
            <asp:BoundField DataField="ValorOriginal" HeaderText="Valor Original" DataFormatString="{0:C}" />
            <asp:BoundField DataField="ValorCalculado" HeaderText="Valor Calculado" DataFormatString="{0:C}" />
            <asp:BoundField DataField="Competencia" HeaderText="Competência" />
            <asp:BoundField DataField="Status" HeaderText="Status" />
    
            <asp:TemplateField HeaderText="Ações">
                <ItemTemplate>
                    <asp:Button
                        ID="btnPagar"
                        runat="server"
                        Text="Pagar"
                        CommandName="Pagar"
                        CommandArgument='<%# Eval("Id") %>' />

                    <asp:Button
                        ID="btnCancelar"
                        runat="server"
                        Text="Cancelar"
                        CommandName="Cancelar"
                        CommandArgument='<%# Eval("Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

</asp:Content>
