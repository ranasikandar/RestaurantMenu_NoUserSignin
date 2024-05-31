<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="RestaurantMenu.Admin.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHHead" runat="server">
    <title><% Response.Write(System.Configuration.ConfigurationManager.AppSettings["BusinessName"].ToString());%> | Admin Home</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CPHBody" runat="server">
    <div class="container">
        <!-- Begin Page Header-->
        <div class="row">
            <div class="page-header">
                <div class="d-flex align-items-center">
                    <h4 class="page-header-title">Home</h4>
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
</asp:Content>
