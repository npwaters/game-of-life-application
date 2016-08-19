<%@ Page Title="" Language="C#" MasterPageFile="~/GolAdmin.Master" AutoEventWireup="true" CodeBehind="UserListCodeless.aspx.cs" Inherits="CPT373_AS2.UserList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ListView ID="ListView1" runat="server" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="ListView1_SelectedIndexChanged" DataKeyNames="UserID" InsertItemPosition="LastItem">
        <AlternatingItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                </td>
                <td>
                    <asp:Label ID="UserIDLabel" runat="server" Text='<%# Eval("UserID") %>' />
                </td>
                <td>
                    <asp:Label ID="EmailLabel" runat="server" Text='<%# Eval("Email") %>' />
                </td>
                <td>
                    <asp:Label ID="FirstNameLabel" runat="server" Text='<%# Eval("FirstName") %>' />
                </td>
                <td>
                    <asp:Label ID="LastNameLabel" runat="server" Text='<%# Eval("LastName") %>' />
                </td>
            </tr>
        </AlternatingItemTemplate>
        <EditItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                </td>
                <td>
                    <asp:Label ID="UserIDLabel1" runat="server" Text='<%# Eval("UserID") %>' />
                </td>
                <td>
                    <asp:TextBox ID="EmailTextBox" runat="server" Text='<%# Bind("Email") %>' />
                </td>
                <td>
                    <asp:TextBox ID="FirstNameTextBox" runat="server" Text='<%# Bind("FirstName") %>' />
                </td>
                <td>
                    <asp:TextBox ID="LastNameTextBox" runat="server" Text='<%# Bind("LastName") %>' />
                </td>
            </tr>
        </EditItemTemplate>
        <EmptyDataTemplate>
            <table runat="server" style="">
                <tr>
                    <td>No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <InsertItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" />
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
                </td>
                <td>&nbsp;</td>
                <td>
                    <asp:TextBox ID="EmailTextBox" runat="server" Text='<%# Bind("Email") %>' />
                </td>
                <td>
                    <asp:TextBox ID="FirstNameTextBox" runat="server" Text='<%# Bind("FirstName") %>' />
                </td>
                <td>
                    <asp:TextBox ID="LastNameTextBox" runat="server" Text='<%# Bind("LastName") %>' />
                </td>
            </tr>
        </InsertItemTemplate>
        <ItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                </td>
                <td>
                    <asp:Label ID="UserIDLabel" runat="server" Text='<%# Eval("UserID") %>' />
                </td>
                <td>
                    <asp:Label ID="EmailLabel" runat="server" Text='<%# Eval("Email") %>' />
                </td>
                <td>
                    <asp:Label ID="FirstNameLabel" runat="server" Text='<%# Eval("FirstName") %>' />
                </td>
                <td>
                    <asp:Label ID="LastNameLabel" runat="server" Text='<%# Eval("LastName") %>' />
                </td>
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table runat="server">
                <tr runat="server">
                    <td runat="server">
                        <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                            <tr runat="server" style="">
                                <th runat="server"></th>
                                <th runat="server">UserID</th>
                                <th runat="server">Email</th>
                                <th runat="server">FirstName</th>
                                <th runat="server">LastName</th>
                            </tr>
                            <tr id="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td runat="server" style=""></td>
                </tr>
            </table>
        </LayoutTemplate>
        <SelectedItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                </td>
                <td>
                    <asp:Label ID="UserIDLabel" runat="server" Text='<%# Eval("UserID") %>' />
                </td>
                <td>
                    <asp:Label ID="EmailLabel" runat="server" Text='<%# Eval("Email") %>' />
                </td>
                <td>
                    <asp:Label ID="FirstNameLabel" runat="server" Text='<%# Eval("FirstName") %>' />
                </td>
                <td>
                    <asp:Label ID="LastNameLabel" runat="server" Text='<%# Eval("LastName") %>' />
                </td>
            </tr>
        </SelectedItemTemplate>
    </asp:ListView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:GolDBConnectionString %>" 
        DeleteCommand="DELETE FROM [User] WHERE [UserID] = @original_UserID" 
        InsertCommand="INSERT INTO [User] ([Email], [FirstName], [LastName]) VALUES (@Email, @FirstName, @LastName)" 
        OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [UserID], [Email], [FirstName], [LastName] FROM [User]" 
        UpdateCommand="UPDATE [User] SET [Email] = @Email, [FirstName] = @FirstName, [LastName] = @LastName WHERE [UserID] = @original_UserID">
        <DeleteParameters>
            <asp:Parameter Name="original_UserID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Email" Type="String" />
            <asp:Parameter Name="FirstName" Type="String" />
            <asp:Parameter Name="LastName" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="Email" Type="String" />
            <asp:Parameter Name="FirstName" Type="String" />
            <asp:Parameter Name="LastName" Type="String" />
            <asp:Parameter Name="original_UserID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <p>
    </p>
</asp:Content>
