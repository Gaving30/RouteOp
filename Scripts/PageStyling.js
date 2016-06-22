/*The Route Optimising SideKick was created as a 4th year final project for Gavin Gaughran x12107077 National College of Ireland 16.05.16

Code used throughout was written by Gavin Gaughran using, references and modified snippets from to the following websites:
    StackOverflow(General coding practice and troubleshooting)
    Code.MSDN(Algorithm mathematical equation)
    Damien Dennehy(Haversine information)
    Rubicite(PMX Crossover information)
    JSFiddle(JQuery and JavaScript functionality)
    W3Schools(CSS and design)
*/

$(document).ready(function () {
    //Added to stop the "enter" button submitting any input buttons if pressed
    $('html').bind('keypress', function (e) {
        if (e.keyCode == 13) {
            return false;
        }
    });

    //var counter = 5;
    var max_fields = 8; //With original 5 and this adds 3 to allow 8 textboxs (Google API Limit)
    var wrapper = $(".input_fields_wrap");
    
    var add_button = $(".add_field_button"); 
    var textFieldsCreated = 5;
    $(add_button).on("click", function (e) { //on add input button click
        e.preventDefault();
        if (textFieldsCreated < max_fields) { //max input box allowed
            textFieldsCreated++; //text box increment

            $(wrapper).append('<div><label class="numberLabelsForDestinations">' + textFieldsCreated +
                '</label><input type="text" class="userInputTextbox" placeholder="Enter A Location" id="tb_' + textFieldsCreated +
                '" name="tb_' + textFieldsCreated + '"/><a href="#" class="remove_field">Remove</a></div>'); //add input box
            setUpHandler('tb_' + textFieldsCreated);
        }
    });

    //Listener to remove textboxes
    $(wrapper).on("click", ".remove_field", function (e) {
        e.preventDefault(); $(this).parent('div').remove();
        textFieldsCreated--;
    })

    //Print route details function
    //Code modified for this application but main content from http://www.aspsnippets.com/
    $(function () {
        $("#btnPrint").click(function () {
            var contents = $("#returnResults").html();
            var frame1 = $('<iframe />');
            frame1[0].name = "frame1";
            frame1.css({ "position": "absolute", "top": "-1000000px" });
            $("body").append(frame1);
            var frameDoc = frame1[0].contentWindow ? frame1[0].contentWindow : frame1[0].contentDocument.document ? frame1[0].contentDocument.document : frame1[0].contentDocument;
            frameDoc.document.open();
            //Create a new HTML document.
            frameDoc.document.write('<html><head><title>The Route Optimising SideKick</title>');
            frameDoc.document.write('</head><body>');

            frameDoc.document.write(contents);
            frameDoc.document.write('</body></html>');
            frameDoc.document.close();
            setTimeout(function () {
                window.frames["frame1"].focus();
                window.frames["frame1"].print();
                frame1.remove();
            }, 500);
        });
    });
});