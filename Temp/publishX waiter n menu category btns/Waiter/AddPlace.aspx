<%@ Page Title="" Language="C#" MasterPageFile="~/Waiter/Waiter.Master" AutoEventWireup="true" CodeBehind="AddPlace.aspx.cs" Inherits="RestaurantMenu.Waiter.AddPlace" %>

<%--NOTE: this page has been replaced with waiter/uc/addplace.aspx--%>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHHead" runat="server">
<title><% Response.Write(System.Configuration.ConfigurationManager.AppSettings["BusinessName"].ToString());%> | Add Place Table</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHBody" runat="server">
    <div class="container-fluid">
        <div class="row justify-content-center">
            <div class="col-12">
                <!-- Begin Page Header-->
                <div class="page-header pr-0 pl-0">
                    <div class="d-flex align-items-center">
                        <h2 class="page-header-title">Add Place/Table</h2>
                    </div>
                </div>
                <!-- End Page Header -->
                <!-- Begin Content -->
                <asp:UpdatePanel runat="server" ID="upd_Dashboard" UpdateMode="Conditional" ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <asp:Panel runat="server" ID="pnlDashboard">
                            <asp:Panel runat="server" ID="PnlMsg" Style="margin-bottom: 45px;"></asp:Panel>
                            <form>
                                <div class="form-group row mb-3">
                                    <div class="col-xl-12 mb-3">
                                        <label class="form-control-label">Name<span class="text-danger ml-2">*</span></label>
                                        <asp:TextBox ID="txtName" CssClass="form-control" runat="server" MaxLength="50" required></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group row mb-3">
                                    <div class="col-xl-12 mb-3">
                                        <label class="form-control-label">Description</label>
                                        <asp:TextBox ID="txtDisc" CssClass="form-control" runat="server" MaxLength="200" TextMode="MultiLine" Height="100px"></asp:TextBox>
                                    </div>
                                </div>
                                <hr class="hrline" />
                            </form>
                            <div class="col-12 button text-center">
                                <asp:Button ID="btnSave" class="btn btn-lg btn-gradient-02" runat="server" OnClientClick="this.disabled = true; this.value = 'Please Wait...';" OnClick="btnSave_Click" Text="Save" UseSubmitBehavior="false" />
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <!-- End Content-->
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" runat="server">
</asp:Content>
