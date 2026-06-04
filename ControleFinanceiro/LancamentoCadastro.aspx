<h2>Cadastro de Lançamento</h2>

<div>
    <label>Descrição</label><br />
    <asp:TextBox ID="txtDescricao" runat="server" />
</div>

<br />

<div>
    <label>Tipo</label><br />
    <asp:DropDownList ID="ddlTipo" runat="server">
        <asp:ListItem Text="Selecione" Value="" />
        <asp:ListItem Text="Crédito" Value="Credito" />
        <asp:ListItem Text="Débito" Value="Debito" />
    </asp:DropDownList>
</div>

<br />

<div>
    <label>Valor Original</label><br />
    <asp:TextBox ID="txtValorOriginal" runat="server" />
</div>

<br />

<div>
    <label>Percentual Taxa</label><br />
    <asp:TextBox ID="txtPercentualTaxa" runat="server" />
</div>

<br />

<div>
    <label>Percentual Desconto</label><br />
    <asp:TextBox ID="txtPercentualDesconto" runat="server" />
</div>

<br />

<div>
    <label>Data de Lançamento</label><br />
    <asp:TextBox ID="txtDataLancamento" runat="server" TextMode="Date" />
</div>

<br />

<div>
    <label>Competência</label><br />
    <asp:TextBox ID="txtCompetencia" runat="server" placeholder="MM/YYYY" />
</div>

<br />

<asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />

<br /><br />

<asp:Label ID="lblMensagem" runat="server" />