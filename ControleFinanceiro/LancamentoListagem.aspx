<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LancamentoListagem.aspx.cs" Inherits="ControleFinanceiro.LancamentoListagem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Listagem de Lançamentos</h2>

    <h3>
        Saldo:
        <asp:Label ID="lblSaldo" runat="server" />
    </h3>

    <h3>Exportação</h3>

        Competência:
        <asp:TextBox ID="txtCompetenciaExportacao" runat="server" placeholder="MM/YYYY" onkeyup="formatarCompetencia(this)" MaxLength="7"/>
        
        <asp:Button
            ID="btnExportarCsv"
            runat="server"
            Text="Exportar CSV"
            OnClick="btnExportarCsv_Click" />
        
        <br /><br />

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

                    <asp:Button
                        ID="btnEditar"
                        runat="server"
                        Text="Editar"
                        CommandName="Editar"
                        CommandArgument='<%# Eval("Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

    </asp:GridView>

            <asp:Label ID="lblMensagem" runat="server" />
            <br /><br />

        <script>
        function formatarCompetencia(campo) {

            var valor = campo.value.replace(/\D/g, '');

            valor = valor.substring(0, 6);

            if (valor.length > 2) {
                valor = valor.substring(0, 2) + "/" + valor.substring(2);
            }

            campo.value = valor;
        }
        </script>

</asp:Content>
