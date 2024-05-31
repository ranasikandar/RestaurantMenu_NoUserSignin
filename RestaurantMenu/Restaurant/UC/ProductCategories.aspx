<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductCategories.aspx.cs" Inherits="RestaurantMenu.Restaurant.UC.ProductCategories" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <!-- Google Fonts -->
    <script src="https://ajax.googleapis.com/ajax/libs/webfont/1.6.26/webfont.js"></script>
    <script>
        WebFont.load({
            google: { "families": ["Montserrat:400,500,600,700", "Noto+Sans:400,700"] },
            active: function () {
                sessionStorage.fonts = true;
            }
        });
    </script>
    <link rel="stylesheet" href="<%: ResolveUrl("~/Template/assets/vendors/css/base/bootstrap.min.css") %>" />
    <link rel="stylesheet" href="<%: ResolveUrl("~/Template/assets/vendors/css/base/elisyam-1.5.min.css") %>" />
    <link rel="stylesheet" href="<%: ResolveUrl("~/Template/assets/css/animate/animate.min.css") %>" />

    <script type="text/javascript">
        function alertMe(type, msg, layout, timeout) {
            new Noty({
                type: type,
                layout: layout,
                text: msg,
                progressBar: true,
                timeout: timeout,
                animation: {
                    open: "animated fadeIn",
                    close: "animated fadeOut"
                }
            }).show();

        }
    </script>
</head>
<body id="page-top">
    <!-- Begin Preloader -->
    <div id="preloader">
        <div class="canvas">
            <img src="<%: ResolveUrl("~/Template/assets/img/smallLogoWhite.png")%>" alt="logo" class="loader-logo" />
            <div class="spinner"></div>
        </div>
    </div>
    <!-- End Preloader -->

    <div class="page">
        <!-- Begin Page Content -->
        <div class="page-content d-flex align-items-stretch">

            <div class="content-inner">
                <div class="container-fluid">
                    <%--<div class="row flex-row">--%>
                    <form id="form1" runat="server" class="form-horizontal">

                        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
                        </asp:ScriptManager>

                        <script type="text/javascript">
                            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
                            function EndRequestHandler(sender, args) {

                                if (args.get_error()) {
                                    alertMe('error', 'Sorry we are unable to Proceed your request. Please try again', 'center', 5000);
                                    console.log(args.get_error().description);
                                    args.set_errorHandled(true);
                                }

                                var el = document.getElementById("btnSubmit");
                                el.disabled = false;
                                //el.value = 'Save Category';
                            }
                        </script>
                        <div class="row flex-row">
                            <div class="col-12">
                                <!-- Form -->

                                <div class="widget has-shadow">
                                    <div class="widget-header bordered no-actions d-flex align-items-center">
                                        <h4>Add Edit</h4>
                                    </div>
                                    <div class="widget-body">


                                        <!-- updatepanel -->
                                        <asp:UpdatePanel ID="updAddEdit" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                            <ContentTemplate>
                                                <div class="form-group row d-flex align-items-center mb-5">
                                                    <label class="col-lg-3 form-control-label">*Category Name</label>
                                                    <div class="col-lg-9">
                                                        <asp:TextBox ID="txtId" runat="server" Visible="false" />
                                                        <asp:TextBox ID="txtCategory" runat="server" class="form-control" placeholder="Category"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-group row d-flex align-items-center mb-5">
                                                    <label class="col-lg-3 form-control-label">Display Order <small>(Optional)</small></label>
                                                    <div class="col-lg-9">
                                                        <asp:TextBox ID="txtDispayOrder" runat="server" value="0" TextMode="Number" class="form-control" placeholder="Display Order"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-group row d-flex align-items-center mb-5">
                                                    <label class="col-lg-3 form-control-label">Description</label>
                                                    <div class="col-lg-9">
                                                        <asp:TextBox ID="txtDiscription" runat="server" class="form-control" placeholder="Category Description"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="text-right">
                                                    <asp:Button ID="btnSubmit" class="btn btn-lg btn-gradient-02" runat="server" OnClientClick="this.disabled = true; this.value = 'Please Wait...';" OnClick="btnSubmit_Click" Text="Save Category" UseSubmitBehavior="false" />
                                                </div>


                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                    </div>
                                </div>
                                <!-- End Form -->
                            </div>
                        </div>



                        <%--search--%>
                        <div class="row flex-row">
                            <div class="col-12 os-animation" data-os-animation="fadeInUp">
                                <!-- Begin Widget 06 -->
                                <div class="widget widget-06 has-shadow">
                                    <asp:UpdatePanel ID="updSearch" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true" EnableViewState="true">
                                        <ContentTemplate>
                                            <!-- Begin Widget Body -->
                                            <div class="widget-body p-0">
                                                <div id="list-group" class="widget-scroll" style="max-height: 55em;">
                                                    <div class="col-lg-12 table-responsive mt-4 mb-4">
                                                        <asp:GridView ID="dgvSearch" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                                                            OnRowCommand="dgvSearch_RowCommand" DataKeyNames="Id" AllowPaging="True" PageSize="10" Width="100%"
                                                            OnPageIndexChanging="dgvSearch_PageIndexChanging" CellPadding="4" ForeColor="#73879C"
                                                            GridLines="None" AllowSorting="True" BorderStyle="None" CssClass="table table-bordered mb-0">
                                                            <AlternatingRowStyle BackColor="White" />
                                                            <Columns>
                                                                <asp:BoundField DataField="Id" HeaderText="Id" Visible="false">
                                                                    <ItemStyle Width="125px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CategoryName" HeaderText="Name">
                                                                    <ItemStyle Width="60%" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="DisplayOrder" HeaderText="Order">
                                                                    <ItemStyle Width="10%" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Edit">
                                                                    <ItemTemplate>
                                                                        <asp:Button ID="btneditg" runat="server" CommandName="editx" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                            Text="_" CausesValidation="false" CssClass="btn btn-primary btn-sm" OnClientClick="window.location='#updAddEdit'" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Delete">
                                                                    <ItemTemplate>
                                                                        <asp:Button ID="btndelete" runat="server" CommandName="deletex" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                            Text="X" CausesValidation="false" CssClass="btn btn-danger btn-sm" OnClientClick="return confirm('Are you sure to DELETE this Category?');" />
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
                            <%--search end--%>
                        </div>
                    </form>



                    <%--</div>--%>
                    <!-- End Row -->
                </div>
                <!-- End Container -->

                <!-- End Page Footer -->
                <a href="#" class="go-top"><i class="la la-arrow-up"></i></a>
            </div>
        </div>
        <!-- End Page Content -->
    </div>

    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/base/jquery.min.js") %>"></script>
    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/base/core.min.js") %>"></script>
    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/noty/noty.min.js") %>"></script>
    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/app/app.min.js") %>"></script>
    <script src="<%: ResolveUrl("~/Template/assets/js/components/notifications/notifications.min.js") %>"></script>
</body>

</html>
