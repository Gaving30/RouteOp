<%@ Page Title="Admin Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="RouteOp.Admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 id="jumbotronHeading"><%: Title %></h2>
    <div id="gridViewDiv">
        <asp:GridView ID="GridView1" DataKeyNames="Id" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource1" Height="135px" Width="963px" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:CommandField ShowEditButton="True" />
                <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="eMail" HeaderText="eMail" SortExpression="eMail" HtmlEncode="false" DataFormatString="<a href=mailto:{0}?subject=The&nbsp;Route&nbsp;Optimsing&nbsp;Team>{0}</a>" />
                <asp:BoundField DataField="Subject" HeaderText="Subject" SortExpression="Subject" />
                <asp:BoundField DataField="Comments" HeaderText="Comments" SortExpression="Comments" />
                <asp:CheckBoxField DataField="Replied" HeaderText="Replied" SortExpression="Replied" />
                <asp:BoundField DataField="PostDate" HeaderText="PostDate" SortExpression="PostDate" />
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle ForeColor="#333333" BackColor="#F7F6F3" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" DeleteCommand="DELETE FROM [QueryTable] WHERE [Id] = @original_Id AND [Name] = @original_Name AND [eMail] = @original_eMail AND [Subject] = @original_Subject AND [Comments] = @original_Comments AND (([Replied] = @original_Replied) OR ([Replied] IS NULL AND @original_Replied IS NULL)) AND (([PostDate] = @original_PostDate) OR ([PostDate] IS NULL AND @original_PostDate IS NULL))" InsertCommand="INSERT INTO [QueryTable] ([Name], [eMail], [Subject], [Comments], [Replied], [PostDate]) VALUES (@Name, @eMail, @Subject, @Comments, @Replied, @PostDate)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT * FROM [QueryTable]" UpdateCommand="UPDATE [QueryTable] SET [Name] = @Name, [eMail] = @eMail, [Subject] = @Subject, [Comments] = @Comments, [Replied] = @Replied, [PostDate] = @PostDate WHERE [Id] = @original_Id AND [Name] = @original_Name AND [eMail] = @original_eMail AND [Subject] = @original_Subject AND [Comments] = @original_Comments AND (([Replied] = @original_Replied) OR ([Replied] IS NULL AND @original_Replied IS NULL)) AND (([PostDate] = @original_PostDate) OR ([PostDate] IS NULL AND @original_PostDate IS NULL))">
            <DeleteParameters>
                <asp:Parameter Name="original_Id" Type="Int32" />
                <asp:Parameter Name="original_Name" Type="String" />
                <asp:Parameter Name="original_eMail" Type="String" />
                <asp:Parameter Name="original_Subject" Type="String" />
                <asp:Parameter Name="original_Comments" Type="String" />
                <asp:Parameter Name="original_Replied" Type="Boolean" />
                <asp:Parameter DbType="Date" Name="original_PostDate" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="Name" Type="String" />
                <asp:Parameter Name="eMail" Type="String" />
                <asp:Parameter Name="Subject" Type="String" />
                <asp:Parameter Name="Comments" Type="String" />
                <asp:Parameter Name="Replied" Type="Boolean" />
                <asp:Parameter DbType="Date" Name="PostDate" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="Name" Type="String" />
                <asp:Parameter Name="eMail" Type="String" />
                <asp:Parameter Name="Subject" Type="String" />
                <asp:Parameter Name="Comments" Type="String" />
                <asp:Parameter Name="Replied" Type="Boolean" />
                <asp:Parameter DbType="Date" Name="PostDate" />
                <asp:Parameter Name="original_Id" Type="Int32" />
                <asp:Parameter Name="original_Name" Type="String" />
                <asp:Parameter Name="original_eMail" Type="String" />
                <asp:Parameter Name="original_Subject" Type="String" />
                <asp:Parameter Name="original_Comments" Type="String" />
                <asp:Parameter Name="original_Replied" Type="Boolean" />
                <asp:Parameter DbType="Date" Name="original_PostDate" />
            </UpdateParameters>
        </asp:SqlDataSource>
    </div>
</asp:Content>