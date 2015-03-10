var xPos, yPos;
var prm = Sys.WebForms.PageRequestManager.getInstance();
function BeginRequestHandler(sender, args) {
    if ($get('<%=ScrollPanel.ClientID%>') != null) {
        // Get X and Y positions of scrollbar before the partial postback
        xPos = $get('<%=ScrollPanel.ClientID%>').scrollLeft;
        yPos = $get('<%=ScrollPanel.ClientID%>').scrollTop;
    }
}
function EndRequestHandler(sender, args) {
    if ($get('<%=ScrollPanel.ClientID%>') != null) {
        // Set X and Y positions back to the scrollbar
        // after partial postback
        $get('<%=ScrollPanel.ClientID%>').scrollLeft = xPos;
        $get('<%=ScrollPanel.ClientID%>').scrollTop = yPos;
    }
}
prm.add_beginRequest(BeginRequestHandler);
prm.add_endRequest(EndRequestHandler);