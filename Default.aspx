<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RouteOp._Default" ClientIDMode="Static" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h2 id="jumbotronHeading">
            The Route Optimising SideKick
        </h2>
        <p class="lead">We save you time and money by optimising your driving routes. Simply enter your destinations below and our algorithms will find you the shortest path to take.</p>
    </div>
    
    <!--<div id="GoogleMapDiv">-->
    <div id="mapInputSection">
        <h3 class="mainHeadings">
            Enter Your Destinations Below:<br />
        </h3>
        <ul id="ulDestinations">
            <li>
                <label class="numberLabelsForDestinations">Start</label>
                <input type="text" class="userInputTextbox" id="tb_1" name="tb_1" placeholder="Enter A Location" />
            </li>
            <li>
                <label class="numberLabelsForDestinations">2</label>
                <input type="text" class="userInputTextbox" id="tb_2" name="tb_2" placeholder="Enter A Location"/>             
            </li>
            <li>
                <label class="numberLabelsForDestinations">3</label>
                <input type="text" class="userInputTextbox" id="tb_3" name="tb_3" placeholder="Enter A Location"/>
            </li>
            <li>
                <label class="numberLabelsForDestinations">4</label>
                <input type="text" class="userInputTextbox" id="tb_4" name="tb_4" placeholder="Enter A Location"/>
            </li>
            <li>
                <label class="numberLabelsForDestinations">5</label>
                <input type="text" class="userInputTextbox" id="tb_5" name="tb_5" placeholder="Enter A Location"/>
            </li>
            <li>
                <div class="input_fields_wrap">
                </div>
            </li>
            <li>
                <button id="AddMoreFieldsButton" class="add_field_button btn btn-info" runat="server">Add More Fields</button>
                <input type="submit" id="verifyButton" class="btn btn-info" value="Verify Addresses" title="Please Fill All Textboxs" onclick="return false;"/>
            </li>
            <b>Mode of Travel: </b>
            <asp:DropDownList ID="mode" runat="server">
                <asp:ListItem Text="Driving" Value="DRIVING"></asp:ListItem>
                <asp:ListItem Text="Walking" Value="WALKING"></asp:ListItem>
                <asp:ListItem Text="Bicycling" Value="BICYCLING"></asp:ListItem>
            </asp:DropDownList>
            <li>
                <input type="submit" style="display: none;" id="optimiseButton" class="btn btn-info" value="Optimise" title="Please Fill All Textboxs" runat="server" onserverclick="OptimiseButton_Click" />    
            </li>
        </ul>
    </div>

    <asp:HiddenField ID="hdnfldVariable" Value="" runat="server" />
    <asp:HiddenField ID="numFldVariable" Value="" runat="server" />
    <asp:HiddenField ID="returnDetailToDisplay" Value="" runat="server" />
    
    <input type="hidden" class="hiddenLAtLng" id="lng10" value="" name="lat5" runat="server" />
    <span id="log" ></span>
    
    <div id="mapAndResultDiv">
        <!--Google Map Div-->
          <div id="map"></div>
    </div>
    
    <div id="returnResults" runat="server">
        <div id="headingLeft">
            <h3 class="mainHeadings">
                Optimal Route
            </h3>
        </div>

        <div id="headingRight">
            <h3 class="mainHeadings">
                Route Specifics
            </h3>
        </div>
        
        <div id="resultSide" runat = "server" >
        </div>
        
        <div id="mapside">
        </div>

        <div id="resultFooter">

            <input type="button" id="btnPrint" value="Print" class="btn btn-info" />       
        
        </div>
        <asp:HiddenField ID="hiddenFavPlaceName" Value="" runat="server" />
        <asp:HiddenField ID="hiddenFavLatLng" Value="" runat="server" />
    </div>
</asp:Content>