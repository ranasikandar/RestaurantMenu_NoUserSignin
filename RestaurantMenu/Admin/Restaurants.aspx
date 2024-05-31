<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Restaurants.aspx.cs" Inherits="RestaurantMenu.Admin.Restaurants" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHHead" runat="server">
    <title><% Response.Write(System.Configuration.ConfigurationManager.AppSettings["BusinessNicName"].ToString());%> | Restaurants</title>

    <link href="<%: ResolveUrl("~/Template/assets/vendors/bootstrap-daterangepicker/daterangepicker.css") %>"
        rel="stylesheet" />
    <link href="<%: ResolveUrl("~/Template/assets/vendors/bootstrap-datetimepicker/build/css/bootstrap-datetimepicker.css") %>"
        rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHBody" runat="server">
    <div class="container">
        <!-- Begin Page Header-->
        <div class="row">
            <div class="page-header">
                <div class="d-flex align-items-center">
                    <h2 class="page-header-title">Restaurants Management</h2>
                </div>
            </div>
        </div>
        <!-- End Page Header -->

        <!-- End Row -->
        <div class="row flex-row">
            <div class="col-xl-6 os-animation" data-os-animation="fadeInUp" id="addEditpnl">
                <!-- Begin Widget 05 -->
                <div class="widget widget-05 has-shadow">
                    <!-- Begin Widget Header -->
                    <div class="widget-header bordered d-flex align-items-center">
                        <h2>Edit <small>Restaurant</small></h2>
                    </div>
                    <!-- End Widget Header -->
                    <asp:UpdatePanel ID="updEdit" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <!-- Begin Widget Body -->
                            <div class="widget-body no-padding hidden">

                                <div class="col-lg-12" style="margin-top: 20px;">
                                    <asp:TextBox ID="txtId" runat="server" class="form-control" BackColor="White" Visible="false" />
                                </div>

                                <div class="col-lg-12">
                                    <div class="mt-2 mb-2 position-relative">
                                        <label>Full Name</label>
                                        <div class="group material-input">
                                            <asp:TextBox ID="txtUserName" runat="server" Enabled="false"></asp:TextBox>
                                            <span class="highlight"></span>
                                            <span class="bar"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <div class="mt-2 mb-2 position-relative">
                                        <label>Restaurant Name</label>
                                        <div class="group material-input">
                                            <asp:TextBox ID="txtRestaurantName" runat="server" Enabled="false"></asp:TextBox>
                                            <span class="highlight"></span>
                                            <span class="bar"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <div class="mt-2 mb-2 position-relative">
                                        <label>Register Date</label>
                                        <div class="group material-input">
                                            <asp:TextBox ID="txtRegDate" runat="server" Enabled="false"></asp:TextBox>
                                            <span class="highlight"></span>
                                            <span class="bar"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <div class="mt-2 mb-2 position-relative">
                                        <label>Complete Address</label>
                                        <div class="group material-input">
                                            <asp:TextBox ID="txtCompleteAddress" runat="server" Enabled="false"></asp:TextBox>
                                            <span class="highlight"></span>
                                            <span class="bar"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <div class="mt-2 mb-2 position-relative">
                                        <label>Contact</label>
                                        <div class="group material-input">
                                            <asp:TextBox ID="txtContact" runat="server" Enabled="false"></asp:TextBox>
                                            <span class="highlight"></span>
                                            <span class="bar"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <div class="mt-5 mb-3 position-relative">
                                        <div class="group material-input input-group date" id="myDatepicker2">
                                            <asp:TextBox ID="txtValidityTill" CssClass="form-control input-group-addon" runat="server" MaxLength="10"></asp:TextBox>
                                            <span class="highlight"></span>
                                            <span class="bar"></span>
                                            <label>Validity Till (Date UTC) Format 31-12-2020</label>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-lg-12">
                                        <small style="margin-left: 30px;">Restaurant Status:</small>
                                        <div class="col-md-6 col-sm-6 col-xs-12">
                                            <div id="UserEnable" class="btn-group" data-toggle="buttons">
                                                <label class="btn btn-gradient-02" data-toggle-class="btn-primary" data-toggle-passive-class="btn-primary" data-toggle='tooltip' data-placement='bottom' title='' data-original-title='Restaurant can Signin if validity period is valid'>
                                                    <asp:RadioButton runat="server" GroupName="UserEnable" ID="rdbEnable" AutoPostBack="false" Checked="true" />
                                                    &nbsp; Enable &nbsp;
                                                </label>
                                                <label class="btn btn-gradient-01" data-toggle-class="btn-primary" data-toggle-passive-class="btn-default" data-toggle='tooltip' data-placement='bottom' title='' data-original-title='Restaurant can not Signin even validity period is valid'>
                                                    <asp:RadioButton runat="server" GroupName="UserEnable" ID="rdbDisable" AutoPostBack="false" />
                                                    Disable
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-lg-12" style="margin-top: 20px; margin-bottom: 20px;">
                                    <small>Details or Note of this Restaurant if any</small>
                                    <asp:TextBox ID="txtDetails" runat="server" class="form-control" TextMode="MultiLine" />
                                </div>

                                <div class="col-lg-12">
                                    <div class="col-md-6 col-sm-6" style="margin: 20px 0px 20px 0px;">
                                        <asp:Button ID="btnSave" runat="server" Width="90px" Text="Save" CssClass="btn btn-success" OnClick="btnSave_Click" OnClientClick="this.disabled = true; this.value = 'Please Wait...';" UseSubmitBehavior="false" />
                                        <asp:Button ID="btnCancel" runat="server" Width="90px" Text="Cancel" CssClass="btn btn-primary" OnClick="btnCancel_Click" />
                                    </div>
                                </div>


                            </div>
                            <!-- End Widget Body -->
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <!-- End Widget 05 -->
            </div>

            <div class="col-xl-6 os-animation" data-os-animation="fadeInUp">
                <!-- Begin Widget 06 -->
                <div class="widget widget-06 has-shadow">
                    <!-- Begin Widget Header -->
                    <div class="widget-header bordered d-flex align-items-center">
                        <h2>Search <small>Restaurants</small></h2>
                    </div>
                    <!-- End Widget Header -->
                    <asp:UpdatePanel ID="updSearch" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true" EnableViewState="true">
                        <ContentTemplate>
                            <!-- Begin Widget Body -->
                            <div class="widget-body p-0">
                                <div id="list-group" class="widget-scroll" style="height: auto;"> <%--max-height: 50em;--%>
                                    <div class="col-lg-12">
                                        <div class="mt-5 mb-5 position-relative">
                                            <div class="group material-input">
                                                <asp:TextBox ID="txtEmailSearch" runat="server" type="email"></asp:TextBox>
                                                <span class="highlight"></span>
                                                <span class="bar"></span>
                                                <label>Search By Email</label>
                                            </div>
                                        </div>
                                        <div class="mt-5 mb-5 position-relative">
                                            <div class="group material-input">
                                                <asp:TextBox ID="txtResNameSearch" runat="server"></asp:TextBox>
                                                <span class="highlight"></span>
                                                <span class="bar"></span>
                                                <label>Search By Restaurant Name</label>
                                                <br />
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-lg btn-gradient-02" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-12 table-responsive">
                                        <asp:GridView ID="dgvSearch" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                                            OnRowCommand="dgvSearch_RowCommand" DataKeyNames="RestaurantID" AllowPaging="True" PageSize="5" Width="100%"
                                            OnPageIndexChanging="dgvSearch_PageIndexChanging" CellPadding="4" ForeColor="#73879C"
                                            GridLines="None" AllowSorting="True" BorderStyle="None" CssClass="table table-bordered mb-0">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="Id" HeaderText="RestaurantID" Visible="false">
                                                    <ItemStyle Width="125px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="RestaurantName" HeaderText="Restaurant">
                                                    <ItemStyle Width="280px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Address" HeaderText="Address">
                                                    <ItemStyle Width="70%" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btneditg" runat="server" CommandName="editx" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            Text="EDIT" CausesValidation="false" CssClass="btn btn-primary btn-sm" OnClientClick="window.location='#addEditpnl'" />
                                                    </ItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                            </Columns>
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="#3F5367" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#EDEDED" Font-Bold="True" ForeColor="#73879C" />
                                            <PagerSettings FirstPageText="&lt;" LastPageText="&gt;" Mode="NumericFirstLast" />
                                            <PagerStyle BackColor="#bbbbbb" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#F9F9F9" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <PagerStyle CssClass="gridview" />
                                        </asp:GridView>

                                        <asp:HiddenField ID="HiddenFieldGridEdit" runat="server" />
                                    </div>
                                </div>
                                <!-- End List -->
                            </div>
                            <!-- End Widget Body -->
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <!-- End Widget 06 -->
            </div>
        </div>
        <!-- End Row -->

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" runat="server">
    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/datepicker/moment.min.js") %>"></script>
    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/datepicker/daterangepicker.js") %>"></script>
    <script src="<%: ResolveUrl("~/Template/assets/vendors/bootstrap-datetimepicker/build/js/bootstrap-datetimepicker.min.js") %>"></script>
    <!-- Initialize datetimepicker -->
    <script type="text/javascript">
        function pageloadRender() {
            $('#myDatepicker2').datetimepicker({
                format: 'DD-MM-YYYY'
            });
        }

        pageloadRender();
    </script>
</asp:Content>
