<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderDetail.aspx.cs" Inherits="RestaurantMenu.Menu.UC.OrderDetail" %>

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
            <img src="<%: ResolveUrl("~/Template/assets/img/loading.png")%>" alt="logo" class="loader-logo" />
            <div class="spinner"></div>
        </div>
    </div>
    <!-- Webapp by ranasikandar@mail.com -->
    <!-- End Preloader -->
    <div class="page">
        <!-- Begin Page Content -->
        <div class="page-content d-flex align-items-stretch">
            <div class="content-inner">
                <div class="container-fluid">
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
                                el.value = 'Place My Order';
                            }
                        </script>

                        <div class="row flex-row">
                            <div class="col-12">
                                <!-- Form -->
                                <div class="widget has-shadow">
                                    <%--<div class="widget-header bordered no-actions d-flex align-items-center">
                                        <h4>Order Details</h4>
                                    </div>--%>
                                    <asp:HiddenField runat="server" ID="theID" />
                                    <div class="widget-body">
                                        <!-- updatepanel -->
                                        <asp:UpdatePanel ID="updAddEdit" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                            <ContentTemplate>
                                                <div class="form-group row d-flex align-items-center mb-3">
                                                    <label class="col-lg-3 form-control-label">*Quantity</label>
                                                    <div class="col-lg-9">
                                                        <asp:TextBox ID="txtQty" runat="server" TextMode="Number" value="1" class="form-control" placeholder="Quantity"></asp:TextBox>
                                                        <%--<asp:RegularExpressionValidator ID="revQty" ErrorMessage="Please Enter valid Quantity" ControlToValidate="txtQty" ValidationExpression="\d+" runat="server"></asp:RegularExpressionValidator>--%>
                                                    </div>
                                                </div>
                                                <div class="form-group row d-flex align-items-center mb-3">
                                                    <label class="col-lg-3 form-control-label">Note</label>
                                                    <div class="col-lg-9">
                                                        <asp:TextBox ID="txtNote" runat="server" class="form-control" placeholder="e.g No Tomatoes" TextMode="MultiLine" Style="min-height: 75px;"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="text-center">
                                                    <asp:Button ID="btnSubmit" class="btn btn-lg btn-gradient-02" runat="server" OnClientClick="this.disabled = true; this.value = 'Please Wait...';" OnClick="btnSubmit_Click" Text="Place Order" UseSubmitBehavior="false" />
                                                </div>

                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                    </div>
                                </div>
                                <!-- End Form -->
                            </div>
                        </div>
                    </form>
                    <!-- End Row -->
                </div>
                <!-- End Container -->
                <!-- End Page Footer -->
                <a href="#" class="go-top"><i class="la la-arrow-up"></i></a>
            </div>
        </div>
        <!-- End Page Content -->
    </div>
    <!-- Webapp by ranasikandar@mail.com -->
    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/base/jquery.min.js") %>"></script>
    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/base/core.min.js") %>"></script>
    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/noty/noty.min.js") %>"></script>
    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/app/app.min.js") %>"></script>
    <script src="<%: ResolveUrl("~/Template/assets/js/components/notifications/notifications.min.js") %>"></script>
</body>
</html>
