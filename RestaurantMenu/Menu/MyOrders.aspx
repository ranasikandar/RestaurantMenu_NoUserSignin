<%@ Page Title="" Language="C#" MasterPageFile="~/Menu/Menu.Master" AutoEventWireup="true" CodeBehind="MyOrders.aspx.cs" Inherits="RestaurantMenu.Menu.MyOrders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHHead" runat="server">
    <title><% Response.Write(System.Configuration.ConfigurationManager.AppSettings["BusinessName"].ToString());%> | My Orders</title>
    <link rel="stylesheet" href="<%: ResolveUrl("~/Template/assets/css/lity/lity.min.css") %>" />
    <style>
        .btclr {
            color: white !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHBody" runat="server">
    <div class="container-fluid">
        <div class="row justify-content-center">
            <div class="col-xl-11">
                <!-- Begin Page Header-->
                <div class="page-header pr-0 pl-0">
                    <div class="d-flex align-items-center">
                        <h2 class="page-header-title"><%Response.Write(RestaurantName); %>, <%Response.Write(TableName);%></h2>
                    </div>
                </div>
                <!-- End Page Header -->
                <div class="row align-items-center mt-3 mb-4">
                    <div class="col-9">
                        <h4 class="m-0"><%Response.Write(TotalFoods); %> Foods and <%Response.Write(TotalFoodsItems); %> Items Ordered from your Table</h4>
                    </div>
                    <div class="col-3 text-right">
                        <ul class="list-inline">
                            <li class="list-inline-item">
                                <a href="<%:ResolveUrl(MenuURL)%>">
                                    <i class="la la la-clipboard la-2x"></i>
                                </a>
                            </li>
                            <li class="list-inline-item">
                                <a href="javascript:window.location.reload(true)">
                                    <i class="la la-refresh la-2x"></i>
                                </a>
                            </li>
                        </ul>
                    </div>
                </div>
                <!-- Webapp by ranasikandar@mail.com -->
                <!-- Begin Content -->
                <asp:UpdatePanel runat="server" ID="upd_Dashboard" UpdateMode="Conditional" ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <asp:Panel runat="server" ID="pnlDashboard"></asp:Panel>

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="TimerDashboard" EventName="Tick" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:Timer ID="TimerDashboard" runat="server" Interval="600000" OnTick="TimerDashboard_Tick"></asp:Timer>
                <!-- End Content-->
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" runat="server">
</asp:Content>
