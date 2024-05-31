<%@ Page Title="" Language="C#" MasterPageFile="~/Menu/Menu.Master" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="RestaurantMenu.Menu.Order" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHHead" runat="server">
    <title><% Response.Write(System.Configuration.ConfigurationManager.AppSettings["BusinessName"].ToString());%> | Food Menu | Order</title>
    <link rel="stylesheet" href="<%: ResolveUrl("~/Template/assets/css/lity/lity.min.css") %>" />
    <link rel="stylesheet" href="<%: ResolveUrl("~/Template/assets/css/bootstrap-select/bootstrap-select.min.css") %>" />

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
                        <h2 class="page-header-title">Welcome to <%Response.Write(RestaurantName); %>, <%Response.Write(TableName);%></h2>
                    </div>
                </div>
                <!-- End Page Header -->
                <div class="row align-items-center mt-3 mb-4">
                    <%--<div class="col-10">
                        <h4 class="m-0"><%Response.Write(TotalFoodItems); %> Food Items Available For Order</h4>
                    </div>
                    <div class="col-2 text-right">
                        <ul class="list-inline">
                            <li class="list-inline-item">
                                <a href="javascript:window.location.reload(true)">
                                    <i class="la la-refresh la-2x"></i>
                                </a>
                            </li>
                        </ul>
                    </div>--%>
                    <hr class="hrline" />
                    <div class="col-12" style="border: dashed; border-color: #dedede;">
                        <label for="CPHBody_ddlCategory">Select Category</label>
                        <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control selectpicker" data-live-search="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true" DataTextField="CategoryName" DataValueField="Id">
                        </asp:DropDownList>
                    </div>
                </div>

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
    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/lity/lity.min.js") %>"></script>
    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/bootstrap-select/bootstrap-select.min.js") %>"></script>

</asp:Content>
