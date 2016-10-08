<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="MaterialWebForm.aspx.cs" Inherits="Jorge_Parcial1_AP2.MaterialWebForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h1>Registro Materiales</h1>
    </div>
    <\br><\br>
    <%--Id--%>
        <div>
            <asp:Label ID="IdLabel" runat="server" Text="Material Id:"></asp:Label>
            <asp:TextBox ID="IdTextBox" runat="server"></asp:TextBox>
            <asp:Button ID="BuscarButton" runat="server" Text="Buscar" OnClick="BuscarButton_Click"/><\br><\br>
        </div>

    <%--descripcion--%>
        <div>
            <asp:Label ID="DescripcionLabel" runat="server" Text="Descripcion:"></asp:Label>
            <asp:TextBox ID="DescripcionTextBox" runat="server"></asp:TextBox><\br><\br>
        </div>

    <%--Precio--%>
         <div>
            <asp:Label ID="PrecioLabel" runat="server" Text="Precio:"></asp:Label>
            <asp:TextBox ID="PrecioTextBox" runat="server"></asp:TextBox><\br><\br>
        </div>

    <%--botones--%>
    <asp:Button ID="NuevoButton" runat="server" Text="Nuevo" OnClick="NuevoButton_Click" /><\br><\br>
    <asp:Button ID="GuardarButton" runat="server" Text="Guardar" OnClick="GuardarButton_Click" /><\br><\br>
    <asp:Button ID="EliminarButton" runat="server" Text="Eliminar" OnClick="EliminarButton_Click" />
</asp:Content>
