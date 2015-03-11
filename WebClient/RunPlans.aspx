<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="RunPlans.aspx.cs" Inherits="RunPlans" EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <%--<link rel="stylesheet" type="text/css" href="tabs.css" />--%>
    <script type="text/javascript" src="Scripts/jquery.js"></script>
    <script type="text/javascript" src="Scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript">
        var xPos, yPos;
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        function BeginRequestHandler(sender, args) {
            if ($get('<%=ScrollPanel.ClientID%>') != null) {
                xPos = $get('<%=ScrollPanel.ClientID%>').scrollLeft;
                yPos = $get('<%=ScrollPanel.ClientID%>').scrollTop;
            }
        }
        function EndRequestHandler(sender, args) {
            if ($get('<%=ScrollPanel.ClientID%>') != null) {
                $get('<%=ScrollPanel.ClientID%>').scrollLeft = xPos;
                $get('<%=ScrollPanel.ClientID%>').scrollTop = yPos;
            }
        }
        prm.add_beginRequest(BeginRequestHandler);
        prm.add_endRequest(EndRequestHandler);


        $(document).ready(function () {

            $(".tab_content").hide();
            $(".tab_content:first").show();

            $("ul.tabs li").click(function () {
                $("ul.tabs li").removeClass("active");
                $(this).addClass("active");
                $(".tab_content").hide();
                var activeTab = $(this).attr("rel");
                $("#" + activeTab).fadeIn();
            });
        });

    </script>
    <style type="text/css">
        .header {
            font-weight: bold;
            position: relative;
            background-color: #006699;
            color: #ffffff;
            height: 25px;
        }

        ul.tabs {
            margin: 0;
            padding: 0;
            float: left;
            list-style: none;
            height: 32px;
            /*border-bottom: 1px solid #999999;
            border-left: 1px solid #999999;*/
            width: 100%;
        }

            ul.tabs li {
                float: left;
                margin: 0;
                cursor: pointer;
                padding: 0px 21px;
                height: 31px;
                width: 20%;
                line-height: 31px;
                text-align: center;
                border: 1px solid #999999;
                /*border-left: none;*/
                font-weight: bold;
                background: #EEEEEE;
                overflow: hidden;
                position: relative;
            }

                ul.tabs li:hover {
                    background: #CCCCCC;
                }

                ul.tabs li.active {
                    background: #FFFFFF;
                    /*border-bottom: 1px solid #FFFFFF;*/
                }

        .tab_container {
            /*border: 1px solid #999999;
            border-top: none;*/
            clear: both;
            float: left;
            width: 100%;
            background: #FFFFFF;
        }

        .tab_content {
            padding: 20px;
            font-size: 1.2em;
            display: none;
        }

        #rightMiddleDiv {
            width: 600px;
            margin: 0 auto;
        }
    </style>

    <div style="width: 100%; height: 100%;">
        <input hidden id="hdnScrollPosition" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="leftDiv" style="float: left; font-size: small; height: 85%; width: 25%; padding: 10px; border-right: outset; border-width: 1px;">
                    <asp:Label ID="lblRunPlanListHeader" runat="server" Text="All Run Plans" Font-Size="Large"></asp:Label>
                    <asp:Panel ID="ScrollPanel" runat="server" ScrollBars="Vertical" Height="560px">
                            <asp:GridView ID="dgvRunPlans" runat="server" AutoGenerateColumns="False"
                                OnRowDataBound="OnRowDataBound" OnSelectedIndexChanged="dgvRunPlans_SelectedIndexChanged"
                                EnableModelValidation="False" ShowHeaderWhenEmpty="True">
                                <Columns>
                                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                                    <asp:BoundField DataField="Task" HeaderText="Task" SortExpression="Task" />
                                </Columns>
                                <HeaderStyle CssClass="header" />
                            </asp:GridView>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="float: left; font-size: small; height: 85%; width: 73%; padding: 10px;">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div id="rightTopDiv" style="float: left; margin-bottom: 5px; font-size: small; width: 100%; height: 20%; min-height: 112px; padding: 10px; border: outset; border-width: 1px;">
                        <div style="float: left; width: 100%; border: outset; border-width: 1px;" class="header">
                            <div style="float: left; width: 85%;">
                                <div style="float: left; border-right: outset; border-width: 1px;">
                                    <asp:Label ID="lblRunPlanHeader" runat="server" Text="Run Plan &nbsp&nbsp;" Font-Size="Large"></asp:Label>
                                </div>
                                <div style="float: left;">
                                    <asp:Label ID="lblPublishHeader" runat="server" Text="&nbsp;&nbsp; Published" Font-Size="Large"></asp:Label>
                                </div>
                            </div>
                            <div style="float: left; width: 15%;">
                                <asp:Button ID="btnSave" runat="server" Text="Save" BorderStyle="None" BackColor="#006699" ForeColor="White" />
                                <asp:Button ID="btnPublish" runat="server" Text="Publish" BorderStyle="None" BackColor="#006699" ForeColor="White" />
                            </div>
                        </div>
                        <div style="float: left; width: 100%; padding: 5px">
                            <div style="float: left; width: 100%; height: 25%;">
                                <div style="float: left; width: 70px">
                                    <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>
                                </div>
                                <div style="float: left; width: 200px;">
                                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                                </div>
                                <div style="float: left; width: 40px;">
                                    <asp:Label ID="lblUnit" runat="server" Text="Unit"></asp:Label>
                                </div>
                                <div style="float: left; width: 200px;">
                                    <asp:DropDownList ID="cmbUnit" runat="server" Width="175"></asp:DropDownList>
                                </div>
                                <div style="float: left; width: 130px;">
                                    <asp:Label ID="lblSharedProject" runat="server" Text="SharedProject"></asp:Label>
                                </div>
                                <div style="float: left; width: 200px;">
                                    <asp:DropDownList ID="cmbSharedProject" runat="server" Width="175"></asp:DropDownList>
                                </div>
                                <div style="float: left; width: 80px;">
                                    <asp:CheckBox ID="chkDelete" runat="server" Text="&nbsp;&nbsp;Delete" />
                                </div>
                            </div>
                            <br />
                            <br />
                            <div style="float: left; width: 100%; height: 25%;">
                                <div style="float: left; width: 70px;">
                                    <asp:Label ID="lblCustomer" runat="server" Text="Customer"></asp:Label>
                                </div>
                                <div style="float: left; width: 200px;">
                                    <asp:TextBox ID="txtCustomer" runat="server"></asp:TextBox>
                                </div>
                                <div style="float: left; width: 40px;">
                                    <asp:Label ID="lblWell" runat="server" Text="Well"></asp:Label>
                                </div>
                                <div style="float: left; width: 200px;">
                                    <asp:TextBox ID="txtWell" runat="server"></asp:TextBox>
                                </div>
                                <div style="float: left; width: 130px;">
                                    <asp:Label ID="lblEstimatedDuration" runat="server" Text="EstimatedDuration"></asp:Label>
                                </div>
                                <div style="float: left; width: 190px;">
                                    <div style="float: left; width: 135px;">
                                        <asp:TextBox ID="txtEstimatedDuration" runat="server" Width="130"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 55px;">
                                        <asp:Label ID="lblDurationUnit" runat="server" Text=" Hours"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <%--  <br />
                            <br />
                            <div style="float: left; width: 100%; height: 25%;">
                                <div style="float: left; width: 70px;">
                                    <asp:Label ID="lblRunTask" runat="server" Text="RunTask"></asp:Label>
                                </div>
                                <div style="float: left; width: 220px; height: 100px;">
                                    <asp:TextBox ID="txtRunTask" runat="server" Wrap="true" Height="80" Width="175" TextMode="MultiLine"></asp:TextBox>
                                </div>

                            </div>--%>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div id="rightMiddleDiv" style="float: left; margin-bottom: 5px; font-size: small; width: 100%; height: 55%; min-height: 308px; padding: 10px; border: outset; border-width: 1px">
                <div style="float: left; width: 100%">
                    <ul class="tabs">
                        <li class="active" rel="tab1">Drum and Cable Configuration</li>
                        <li rel="tab2">Tool String</li>
                        <li rel="tab3">Tension and Speed Limits</li>
                        <li rel="tab4">Depth Correction</li>
                        <li rel="tab5">Auto Speed</li>
                    </ul>
                </div>
                <div class="tab_container">
                    <div id="tab1" class="tab_content">
                        <strong>Tab 1</strong>
                    </div>
                    <!-- #tab1 -->
                    <div id="tab2" class="tab_content">
                        <strong>Tab 2</strong>
                    </div>
                    <!-- #tab2 -->
                    <div id="tab3" class="tab_content">
                        <strong>Tab 3</strong>
                    </div>
                    <!-- #tab3 -->
                    <div id="tab4" class="tab_content">
                        <strong>Tab 4</strong>
                    </div>
                    <!-- #tab4 -->
                    <div id="tab5" class="tab_content">
                        <strong>Tab 5</strong>
                    </div>
                    <!-- #tab5 -->

                </div>
                <!-- .tab_container -->
            </div>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <div id="rightBottomDiv" style="float: left; font-size: small; width: 100%; height: 23%; min-height: 132px; padding: 10px; border: outset; border-width: 1px">
                        <asp:Label ID="lblRunGridHeader" runat="server" Text="Runs with this Run Plan" Font-Size="Large"></asp:Label>
                        <asp:Panel ID="RunPanel" runat="server" ScrollBars="Vertical">
                            <asp:GridView ID="dgvRuns" runat="server" AutoGenerateColumns="false" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="RunNumber" HeaderText="RunNumber" SortExpression="Name" />
                                    <asp:BoundField DataField="StartTime" HeaderText="StartTime" SortExpression="Name" />
                                    <asp:BoundField DataField="EndTime" HeaderText="EndTime" SortExpression="Task" />
                                    <asp:BoundField DataField="TimeZone" HeaderText="TimeZone" SortExpression="Name" />
                                    <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Task" />
                                    <asp:BoundField DataField="Comment" HeaderText="Comment" SortExpression="Task" />
                                </Columns>
                                <HeaderStyle CssClass="header " />
                            </asp:GridView>
                        </asp:Panel>
                        <br />
                        <div style="text-align: center;">
                            <div style="display: inline-block;">
                                &nbsp;
                                <asp:Label ID="lblRunCount" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
