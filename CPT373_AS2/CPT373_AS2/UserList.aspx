<%@ Page Title="" Language="C#" MasterPageFile="~/GolAdmin.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="CPT373_AS2.UserList1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:GridView ID="UserListGridView" runat="server" 
        AllowPaging="True" AutoGenerateColumns="False" 
            CellPadding="4" 
            ForeColor="#333333" GridLines="None" AllowSorting="True"
            SelectedIndex="5" DataKeyNames="UserID"
        OnRowDeleting="UserListGridView_OnRowDeleting" OnSelectedIndexChanged="UserListGridView_SelectedIndexChanged">

            <AlternatingRowStyle BackColor="White" />
        <Columns>
                <asp:CommandField ShowDeleteButton="True" />
                <asp:BoundField DataField="UserID" HeaderText="UserID" SortExpression="UserID" ReadOnly="True" />
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
            <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
        </Columns>
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <SortedAscendingCellStyle BackColor="#FDF5AC" />
            <SortedAscendingHeaderStyle BackColor="#4D0000" />
            <SortedDescendingCellStyle BackColor="#FCF6C0" />
            <SortedDescendingHeaderStyle BackColor="#820000"/>

</asp:GridView>
</asp:Content>

