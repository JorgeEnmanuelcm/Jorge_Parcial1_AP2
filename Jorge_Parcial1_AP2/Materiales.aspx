﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Materiales.aspx.cs" Inherits="Jorge_Parcial1_AP2.Materiales" %>
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

    <%--Razon--%>
        <div>
            <asp:Label ID="RazonLabel" runat="server" Text="Razon:"></asp:Label>
            <asp:TextBox ID="RazonTextBox" runat="server"></asp:TextBox><\br><\br>
        </div>

    <%--Material--%>
        <div>
            <asp:Label ID="MaterialLabel" runat="server" Text="Material:"></asp:Label>
            <asp:TextBox ID="MaterialTextBox" runat="server"></asp:TextBox><\br><\br>
        </div>

    <%--Cantidad--%>
        <div>
            <asp:Label ID="CantidadLabel" runat="server" Text="Cantidad:"></asp:Label>
            <asp:TextBox ID="CantidadTextBox" runat="server"></asp:TextBox><\br><\br>
        </div>


    <%--Agregar--%>
    <asp:Button ID="AgregarButton" runat="server" Text="Agregar" OnClick="AgregarButton_Click" /><\br><\br>

    <%--gridview--%>
    <asp:GridView ID="MaterialGridView" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="Material" HeaderText="Material" ReadOnly="True" SortExpression="Material" />
            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" ReadOnly="True" SortExpression="Cantidad" />
        </Columns>
    </asp:GridView>

    <%--botones--%>
    <div>       
        <asp:Button ID="NuevoButton" runat="server" Text="Nuevo" OnClick="NuevoButton_Click" /><\br><\br>
        <asp:Button ID="GuardarButton" runat="server" Text="Guardar" OnClick="GuardarButton_Click" /><\br><\br>
        <asp:Button ID="EliminarButton" runat="server" Text="Eliminar" OnClick="EliminarButton_Click" /><\br><\br>
    </div>
</asp:Content>
