<%@ Page Title="" Language="C#" MasterPageFile="~/Waiter/Waiter.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="RestaurantMenu.Waiter.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHHead" runat="server">
    <title><% Response.Write(System.Configuration.ConfigurationManager.AppSettings["BusinessName"].ToString());%> | Waiter</title>
    <link rel="stylesheet" href="<%: ResolveUrl("~/Template/assets/css/bootstrap-select/bootstrap-select.min.css") %>" />
    <style>
        .ddlstyle {
            border: dashed;
            border-color: #dedede;
        }

        .btclr {
            color: white !important;
        }
    </style>
    <script>
        function PlaceOrder(theID) {
            if (document.getElementById('CPHBody_ddlPlaces').value == 0) {
                alertMe('error', 'Please Select Place', 'center', 3000);
                return false;
            } else {
                openOrderModel(document.getElementById('CPHBody_hidResId').value + '_' + document.getElementById('CPHBody_ddlPlaces').value + '_' + document.getElementById('CPHBody_ddlFoods').value);
            }
        }

        function clearEditIfram(msg) {
            document.getElementById('IfOrderDetail').src = 'about:blank';
            $('.close').click();
            if (msg != "") {
                alertMe('information', msg, 'center', 3000);
                UpdateOrdersList();
            }
        }

        function openOrderModel(val) {
            document.getElementById('IfOrderDetail').src = '<%: ResolveUrl("~/Menu/UC/OrderDetail.aspx?order=") %>' + val + '';
        }

        function CancelMyOrder(theID) {
            if (theID === "") {
                alertMe('information', 'Please Select a Valid Order', 'center', 3000);
                return false;
            }
            else {
                if (confirm('Are you sure to cancel this Order?')) {
                    PageMethods.set_path('<%: ResolveUrl("~/Waiter/Home.aspx")%>')
                    PageMethods.MarkCancel(theID, NotiSuccessCancel, NotiFail, theID);
                } else {
                    return false;
                }
            }
        }

        function NotiSuccessCancel(response, context, MarkCancel) {
            if (response) {
                alertMe('information', response, 'center', 3000);
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
    <div class="container-fluid">
        <div class="row justify-content-center">
            <div class="col-xl-11">
                <!-- Begin Page Header-->
                <div class="page-header pr-0 pl-0">
                    <div class="d-flex align-items-center">
                        <h2 class="page-header-title">Welcome to <%Response.Write(RestaurantName); %></h2>
                        <asp:HiddenField runat="server" ID="hidResId" />
                    </div>
                </div>
                <!-- End Page Header -->
                <div class="row align-items-center mt-3 mb-4">
                    <div class="col-10">
                        <h4 class="m-0">Add or View Orders</h4>
                    </div>
                    <div class="col-2 text-right">
                        <ul class="list-inline">
                            <li class="list-inline-item">
                                <a href="javascript:window.location.reload(true)">
                                    <i class="la la-refresh la-2x"></i>
                                </a>
                            </li>
                        </ul>
                    </div>
                    <hr class="hrline" />
                    <div style="width: 100%;">
                        <div class="row">
                            <div class="col-6 ddlstyle">
                                <label for="CPHBody_ddlPlaces">Select Place</label>
                                <asp:DropDownList runat="server" ID="ddlPlaces" CssClass="form-control selectpicker" data-live-search="true" OnSelectedIndexChanged="ddlPlaces_SelectedIndexChanged" AutoPostBack="true" DataTextField="Name" DataValueField="Id">
                                </asp:DropDownList>
                            </div>
                            <div class="col-6 ddlstyle">
                                <label for="CPHBody_ddlFoods">Select Food</label>
                                <asp:DropDownList runat="server" ID="ddlFoods" CssClass="form-control selectpicker" data-live-search="true" DataTextField="Name" DataValueField="Id">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-12 text-center mt-3">
                            <%--<asp:Button ID="btnOrder" class="btn btn-lg btn-gradient-02" runat="server" OnClientClick="this.disabled = true; this.value = 'Please Wait...'; PlaceOrder(this)" Text="Order" UseSubmitBehavior="true" Height="40px" />--%>
                            <a class='btn btn-gradient-02 btclr' data-toggle='modal' data-target='#MoOrderDetail' onclick="PlaceOrder(this)" style="width: 100%;">Order Now</a>
                        </div>

                    </div>
                </div>

                <div class="col-12 text-right">
                    <ul class="list-inline">
                        <li class="list-inline-item">
                            <a href="javascript:UpdateOrdersList()">
                                <i class="la la-refresh la-2x"></i>
                            </a>
                        </li>
                    </ul>
                </div>

                <hr class="hrline" />

                <!-- Begin Content -->
                <asp:UpdatePanel runat="server" ID="upd_Dashboard" UpdateMode="Conditional" ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <asp:Panel runat="server" ID="pnlDashboard"></asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <!-- End Content-->
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" runat="server">
    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/bootstrap-select/bootstrap-select.min.js") %>"></script>
    <div id="MoOrderDetail" class="modal fade">
        <div class="modal-dialog modal-dialog-centered modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Order Details</h4>
                    <button type="button" class="close" data-dismiss="modal">
                        <span aria-hidden="true" onclick="clearEditIfram('');return false;">x</span>
                        <span class="sr-only">close</span>
                    </button>
                </div>
                <div class="modal-body" style="height: 340px;">
                    <iframe id="IfOrderDetail" src='about:blank' style='width: 100%; height: 100%; border: 0;'></iframe>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-shadow" data-dismiss="modal" onclick="clearEditIfram('');return false;">Close</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
