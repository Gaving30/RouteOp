<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="RouteOp.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2 id="jumbotronHeading"><%: Title %> Form</h2>
    <p  >We are here to answer any questions in relation to your experience using our website.</p>
    <p>&nbsp;Feel free to contact us with any feedback, queries or complaints you have for us. </p>
    <p>If there is anything you would like to see introduced to our site, please dont hesitate to ask and we will see what we can do for you.</p>
    
    <ul>
        <li>
            <asp:Label ID="nameLabel" runat="server" Text="Name:" CssClass="formsLabels" />
        </li>
        <li>
            <asp:TextBox ID="nameTextbox" runat="server" class="formsUserInput" placeholder="Please Enter Your Name" ValidateRequestMode="Enabled" />
        </li>
        <li>
            <asp:Label ID="emailLabel" runat="server" Text="e-Mail:" CssClass="formsLabels" />
        </li>
        <li>
            <asp:TextBox ID="emailTextbox" runat="server" class="formsUserInput" placeholder="Please Enter Your e-Mail" TextMode="Email" />
        </li>
        <li>
            <asp:Label ID="subjectLabel" runat="server" Text="Subject:" CssClass="formsLabels" />
        </li>
        <li>
             <asp:TextBox ID="subjectTextBox" runat="server" class="formsUserInput" placeholder="Please Enter A Subject" />
        </li>
        <li>
            <asp:Label ID="commentsLabel" runat="server" Text="Comments:" CssClass="formsLabels" />
        </li>
        <li>
            <asp:TextBox id="commentsTextbox" class="commentsTextbox" TextMode="multiline" runat="server" placeholder="Please Enter Your Comments" />
        </li>
        <li>
            <asp:label ID="successLabel" runat="server" CssClass="text-success" Font-Size="16pt" ></asp:label>
        </li>
        <li>
            <asp:Label ID="feedbackLabel" runat="server" Text="Feedback:" CssClass="formsLabels" /> 
        </li>
        <li>
            <asp:TextBox id="feedbackTextBox" class="commentsTextbox" TextMode="multiline" runat="server" placeholder="Optional Feedback. State your likes or dislikes within our application. This will assist us to continously improve this application for you" />
        </li>
        <li>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="nameTextbox" CssClass="text-danger" ErrorMessage="A Name Is Required." EnableClientScript="true" />
        </li>
        <li>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="emailTextbox" CssClass="text-danger" ErrorMessage="An eMail Address Is Required." EnableClientScript="true"  />
        </li>
        <li>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="subjectTextBox" CssClass="text-danger" ErrorMessage="A Subject Heading Is Required." />            
        </li>
        <li>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="commentsTextbox" CssClass="text-danger" ErrorMessage="Some Comments Are Required." />
        </li>
        <li>
            <asp:Button ID="SubmitFormButton" runat="server" Text="Submit Form" OnClick="SubmitFormButton_Click" CssClass="btn btn-info" />
        </li>
    </ul>
</asp:Content>
