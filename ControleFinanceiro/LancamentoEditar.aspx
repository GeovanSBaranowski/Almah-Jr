<%@ Page Title="Editar Lancamentos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LancamentoEditar.aspx.cs" Inherits="ControleFinanceiro.LancamentoEditar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Editar Lançamento</h2>

    <asp:HiddenField ID="hdnId" runat="server" />

    Descrição:<br />
    <asp:TextBox ID="txtDescricao" runat="server" />
    <br /><br />

    Tipo:<br />
    <asp:DropDownList ID="ddlTipo" runat="server" onchange="bloqueioCamposPercentuais()">
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
    <asp:TextBox ID="txtCompetencia" runat="server" onkeyup="formatarCompetencia(this)" MaxLength="7"/>
    <br /><br />

    <asp:Button ID="btnSalvar" runat="server" Text="Salvar Alterações" OnClick="btnSalvar_Click" />

    <br /><br />

    <asp:Label ID="lblMensagem" runat="server" />

        <script>
        function bloqueioCamposPercentuais() {
        var tipo = document.getElementById("<%= ddlTipo.ClientID %>").value;
        var taxa = document.getElementById("<%= txtPercentualTaxa.ClientID %>");
        var desconto = document.getElementById("<%= txtPercentualDesconto.ClientID %>");

            if (tipo === "Credito") {
                taxa.value = "";
                taxa.readOnly = true;
                taxa.style.backgroundColor = "#e9ecef";

                desconto.readOnly = false;
                desconto.style.backgroundColor = "#ffffff";
        }
            else if (tipo === "Debito") {
                desconto.value = "";
                desconto.readOnly = true;
                desconto.style.backgroundColor = "#e9ecef";

                taxa.readOnly = false;
                taxa.style.backgroundColor = "#ffffff";
        }
            else {
                taxa.value = "";
                desconto.value = "";

                taxa.readOnly = true;
                desconto.readOnly = true;

                taxa.style.backgroundColor = "#e9ecef";
                desconto.style.backgroundColor = "#e9ecef";
        }
    }

            window.onload = bloqueioCamposPercentuais;
    </script>
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