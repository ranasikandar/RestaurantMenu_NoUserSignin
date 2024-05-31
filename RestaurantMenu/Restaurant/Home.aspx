<%@ Page Title="" Language="C#" MasterPageFile="~/Restaurant/Restaurant.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="RestaurantMenu.Restaurant.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHHead" runat="server">
    <title><% Response.Write(System.Configuration.ConfigurationManager.AppSettings["BusinessName"].ToString());%> | Restaurant Home</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHBody" runat="server">
    <div class="container">
        <!-- Begin Page Header-->
        <div class="row">
            <div class="page-header">
                <div class="d-flex align-items-center">
                    <h4 class="page-header-title">Home</h4>

                    <div class="col-2 text-right">
                        <ul class="list-inline">
                            <li class="list-inline-item">
                                <a href="javascript:window.location.reload(true)">
                                    <i class="la la-refresh la-2x"></i>
                                </a>
                            </li>
                        </ul>
                    </div>

                </div>
            </div>
        </div>
        <!-- End Page Header -->

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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" runat="server">
    <%Response.Write(importantMsg);%>
</asp:Content>
