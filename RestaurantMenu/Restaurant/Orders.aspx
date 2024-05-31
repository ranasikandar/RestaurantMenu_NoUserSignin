<%@ Page Title="" Language="C#" MasterPageFile="~/Restaurant/Restaurant.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="RestaurantMenu.Restaurant.Orders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHHead" runat="server">
    <title><% Response.Write(System.Configuration.ConfigurationManager.AppSettings["BusinessName"].ToString());%> | Orders</title>
    <link rel="stylesheet" href="<%: ResolveUrl("~/Template/assets/css/photoswipe/photoswipe.min.css") %>" />
    <link rel="stylesheet" href="<%: ResolveUrl("~/Template/assets/css/photoswipe/default-skin/default-skin.min.css") %>" />
    <link rel="stylesheet" href="<%: ResolveUrl("~/Template/assets/css/bootstrap-select/bootstrap-select.min.css") %>" />
    <style>
        .my-rounded-circle {
            width: 125px;
            border-radius: 10% !important;
        }

        .pointer {
            cursor: pointer;
        }

        .hrline {
            width: 100%;
            color: black;
        }

        .itemclr {
            color: #808080 !important;
        }

        .ddlstyle {
            border: dashed;
            border-color: #dedede;
        }

        .btclr {
            color: white !important;
        }
    </style>
    <script>

        function setCancelAccept(theID) {
            if (theID === "") {
                alertMe('information', 'Please Select a Valid Order', 'center', 2000);
                return false;
            }
            else {
                if (confirm('Cancel Order. it will not appear in orders list any more. Are you sure to cancel this order?')) {
                    PageMethods.set_path('<%: ResolveUrl("~/Restaurant/Orders.aspx")%>')
                    PageMethods.MarkCancel(theID, NotiMarkCancel, NotiFail, theID);
                } else {
                    //alertMe('information', 'Not cancel', 'center', 3000);
                }
            }
        }

        function NotiMarkCancel(response, context, MarkCancel) {
            if (response) {
                alertMe('information', response, 'center', 3000);
                var x = document.getElementById("CAOrd_" + context);
                x.hidden = true;
                var y = document.getElementById("OrdDiv" + context);
                y.hidden = true;
            }
        }

        function setPaid(theID) {
            if (theID === "") {
                alertMe('information', 'Please Select a Valid Order', 'center', 2000);
                return false;
            }
            else {
                if (confirm('Order Paid. it will not appear in orders list any more. Are you sure to paid this order?')) {
                    PageMethods.set_path('<%: ResolveUrl("~/Restaurant/Orders.aspx")%>')
                    PageMethods.MarkPaid(theID, NotiMarkPaid, NotiFail, theID);
                }
            }
        }

        function NotiMarkPaid(response, context, MarkPaid) {
            if (response) {
                alertMe('information', response, 'center', 3000);
                var x = document.getElementById("PaidOrd_" + context);
                x.hidden = true;
                var y = document.getElementById("OrdDiv" + context);
                y.hidden = true;
            }
        }

        function setPrepare(theID) {
            if (theID === "") {
                alertMe('information', 'Please Select a Valid Order', 'center', 2000);
                return false;
            }
            else {
                var x = document.getElementById("PreOrd_" + theID);
                if (x.classList.value === 'ion ion-bonfire la-2x pointer') {
                    PageMethods.set_path('<%: ResolveUrl("~/Restaurant/Orders.aspx")%>')
                    PageMethods.MarkPrepare(theID, NotiMarkPrepare, NotiFail, theID);
                } else {
                    PageMethods.set_path('<%: ResolveUrl("~/Restaurant/Orders.aspx")%>')
                    PageMethods.MarkUnPrepare(theID, NotiMarkUnPrepare, NotiFail, theID);
                }
            }
        }

        function NotiMarkPrepare(response, context, MarkPrepare) {
            if (response) {
                alertMe('information', response, 'center', 3000);
                var x = document.getElementById("PreOrd_" + context);
                x.classList.value = 'la la-cutlery la-2x pointer';
                x.dataset.originalTitle = "Order UnPrepare and set as Placed";
            }
        }

        function NotiMarkUnPrepare(response, context, MarkUnPrepare) {
            if (response) {
                alertMe('information', response, 'center', 3000);
                var x = document.getElementById("PreOrd_" + context);
                x.classList.value = 'ion ion-bonfire la-2x pointer';
                x.dataset.originalTitle = "Order Prepared";
            }
        }

        function paidOrders(theID) {
            if (theID === "") {
                alertMe('error', 'Please Select a Valid Table', 'center', 2000);
                return false;
            } else {
                if (confirm('Orders Paid. these orders will not appear in orders list any more. Are you sure to Paid these orders?')) {
                    PageMethods.set_path('<%: ResolveUrl("~/Restaurant/Orders.aspx")%>')
                    PageMethods.PaidOrders(theID, NotiPaidOrders, NotiFail, theID);
                }
            }
        }

        function NotiPaidOrders(response, context, PaidOrders) {
            if (response) {
                alertMe('information', response, 'center', 3000);
                var y = document.getElementById("retbl-" + context);
                y.hidden = true;
            }
        }

        function cancelOrders(theID) {
            if (theID === "") {
                alertMe('error', 'Please Select a Valid Table', 'center', 2000);
                return false;
            } else {
                if (confirm('Orders Cancel. these orders will not appear in orders list any more. Are you sure to Cancel these orders?')) {
                    PageMethods.set_path('<%: ResolveUrl("~/Restaurant/Orders.aspx")%>')
                    PageMethods.CancelOrders(theID, NotiCancelOrders, NotiFail, theID);
                }
            }
        }

        function NotiCancelOrders(response, context, CancelOrders) {
            if (response) {
                alertMe('information', response, 'center', 3000);
                var y = document.getElementById("retbl-" + context);
                y.hidden = true;
            }
        }

        function PlaceOrder(theVal) {
            if (document.getElementById("CPHBody_ddlPlaces").value <= 0) {
                alertMe('information', 'Please Select a Table', 'center', 2000);
                return false;
            } else if (document.getElementById("CPHBody_txtQty").value <= 0) {
                alertMe('information', 'Please Enter valid quantity', 'center', 2000);
                return false;
            }
            else {
                if (confirm('Are you sure to Place an Order?')) {
                    var ordDetail = document.getElementById("CPHBody_ddlPlaces").value + "_" + document.getElementById("CPHBody_ddlFoods").value + "_" + document.getElementById("CPHBody_txtQty").value + "_" + document.getElementById("CPHBody_txtNote").value;
                    PageMethods.set_path('<%: ResolveUrl("~/Restaurant/Orders.aspx")%>')
                    PageMethods.PlaceOrder(ordDetail, NotiPlaceOrder, NotiFail, ordDetail);
                }
            }
        }

        function NotiPlaceOrder(response, context, PlaceOrder) {
            if (response) {
                alertMe('information', response, 'center', 3000);
                CPHBody_txtNote.value = "";
                CPHBody_txtQty.value = 1;
                //CPHBody_ddlFoods.selectedIndex = 0;
                //CPHBody_ddlPlaces.selectedIndex = 0;
                UpdateOrdersList();
            }
        }

        function UpdateOrdersList() {
            var requestManager = Sys.WebForms.PageRequestManager.getInstance();
            function EndRequestHandler(sender, args) {
                requestManager.remove_endRequest(EndRequestHandler);
            }
            requestManager.add_endRequest(EndRequestHandler);
            __doPostBack('<%=upd_Dashboard.UniqueID %>', 'updateOrdList');
        }

        function NotiFail(error) {
            alertMe('error', error, 'center', 5000);
            console.error();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHBody" runat="server">
    <div class="page mail">
        <!-- Begin Page Content -->
        <div class="page-content d-flex align-items-stretch">
            <div class="content-innerMy">
                <!-- Begin Container -->
                <div class="container-fluid p-0">
                    <div class="row no-margin">
                        <div class="col-xl-12">

                            <%--<div class="row">
                                <div class="page-header">
                                    <div class="d-flex align-items-center">
                                        <h4 class="page-header-title">Tables and Orders</h4>
                                    </div>
                                </div>
                            </div>--%>

                            <div class="row mb-4">
                                <div class="col-xl-3 col-md-6 col-sm-12 ddlstyle">
                                    <label for="CPHBody_ddlPlaces">Select Table</label>
                                    <asp:DropDownList runat="server" ID="ddlPlaces" CssClass="form-control selectpicker" data-live-search="true" DataTextField="Name" DataValueField="Id">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-xl-3 col-md-6 col-sm-12 ddlstyle">
                                    <label for="CPHBody_ddlFoods">Select Food</label>
                                    <asp:DropDownList runat="server" ID="ddlFoods" CssClass="form-control selectpicker" data-live-search="true" DataTextField="Name" DataValueField="Id">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-xl-1 col-md-3 col-sm-12 ddlstyle">
                                    <label for="CPHBody_txtQty">Quantity</label>
                                    <asp:TextBox ID="txtQty" runat="server" TextMode="Number" value="1" class="form-control" placeholder="Quantity"></asp:TextBox>
                                </div>
                                <div class="col-xl-3 col-md-6 col-sm-12 ddlstyle">
                                    <label for="CPHBody_txtNote">Note</label>
                                    <asp:TextBox ID="txtNote" runat="server" class="form-control" placeholder="e.g No Tomatoes" TextMode="MultiLine"></asp:TextBox>
                                </div>
                                <div class="col-xl-2 col-md-3 col-sm-12 text-center ddlstyle">
                                    <a class='btn btn-gradient-02 btclr mt-4' onclick="PlaceOrder(this)" style="width: 100%;">Order Now</a>
                                </div>
                            </div>

                            <div class="row">
                                <div class="page-header">
                                    <div class="d-flex align-items-center">
                                        <h4 class="page-header-title">Tables and Orders: Details</h4>

                                        <div class="col-2 text-right">
                                            <ul class="list-inline">
                                                <li class="list-inline-item">
                                                    <a href="javascript:UpdateOrdersList()">
                                                        <i class="la la-refresh la-2x"></i>
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>

                                    </div>
                                </div>
                            </div>

                            <asp:UpdatePanel runat="server" ID="upd_Dashboard" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Panel runat="server" ID="pnlDashboard"></asp:Panel>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="TimerDashboard" EventName="Tick" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <asp:Timer ID="TimerDashboard" runat="server" Interval="600000" OnTick="TimerDashboard_Tick"></asp:Timer>


                        </div>
                        <!-- End Col -->
                    </div>
                    <!-- End Row -->
                </div>
                <!-- End Container -->
                <a href="#" class="go-top"><i class="la la-arrow-up"></i></a>
            </div>

        </div>
        <!-- end page content-->
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" runat="server">
    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/photoswipe/photoswipe.min.js") %>"></script>
    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/photoswipe/photoswipe-ui-default.min.js") %>"></script>
    <script src="<%: ResolveUrl("~/Template/assets/js/app/mail/mail.min.js") %>"></script>
    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/bootstrap-select/bootstrap-select.min.js") %>"></script>
</asp:Content>
