<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="RouteOp.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2 id="jumbotronHeading"><%: Title %> Us</h2>
    <div class="aboutDiv">
        <!--<h4 class="aboutLabels">What We Do LABEL</h4>-->
        <h4 class="aboutLabels">What We Do</h4>    
        <p class="aboutParagraph">The Route Optimising SideKick calculates the optimal route to take when you are visiting multiple locations.</p>
        <p>We re-order your locations into the best optimal order, so that every location is visited once before returning to the start location in the shortest and quickest way possible.</p>
        <p>Ideal for delivery drivers, sales people on the road, or anyone who needs to make multiple stops. </p>
        <p>As we use a worldwide map, you can enter locations anywhere on the globe and we will optimise without any issue.</p>
    
        <h4 class="aboutLabels">Registration</h4>    
        <p>On the "Home" page guests can enter five locations per transaction for free</p>
        <p>By taking two minutes to register for a free acount we will then allow eight locations per transaction once you are logged in</p>
        <p><a href="Account/Register.aspx">Register Now!!!</a></p>
 
        <h4 class="aboutLabels">Pricing</h4>    
        <p>Need more than eight locations per transaction?</p>
        <p>Coming soon is a pay service that will offer more than ten locations per transaction.</p>
        <p><a href="Contact.aspx">Contact us</a> with any queries you have on getting a subscription</p>
 
        <h4 class="aboutLabels">Fair Use</h4>
        <p>The Route Optimising SideKick attempts to solve optimal route calculation requests as a 'best-effort' service.
        <p>Traffic is not taken into account with optimal results, but may be in future versions of this application</p>
        <p>These systems are constantly maintained for continous improvement of our performance and reliability.</p>

        <h4 class="aboutLabels">Privacy</h4>    
        <p>Registered email addresses provided as part of the registration process are not sold or passed on to third parties. We will only ever use your email address to contact you regarding our service.</p>
        <p>Please contact us if you wish us to delete your account.</p>
    </div>
</asp:Content>
