<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BrandingLogo.aspx.cs" Inherits="RestaurantMenu.Restaurant.BrandingLogo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title><% Response.Write(System.Configuration.ConfigurationManager.AppSettings["BusinessName"].ToString());%> | Branding Logo</title>
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
    <!-- Favicon -->
    <link rel="apple-touch-icon" sizes="180x180" href="<%: ResolveUrl("~/Template/assets/img/apple-touch-icon.png") %>" />
    <link rel="icon" type="image/png" sizes="32x32" href="<%: ResolveUrl("~/Template/assets/img/favicon-32x32.png") %>" />
    <link rel="icon" type="image/png" sizes="16x16" href="<%: ResolveUrl("~/Template/assets/img/favicon-16x16.png") %>" />
    <!-- Stylesheet -->
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
    <!-- Webapp by ranasikandar@mail.com -->
    <style>
        .hrline {
            width: 100%;
            color: black;
        }
    </style>
</head>
<body class="elisyam-bg">
    <!-- Begin Preloader -->
    <div id="preloader">
        <div class="canvas">
            <img src="<%: ResolveUrl("~/Template/assets/img/smallLogoWhite.png")%>" alt="logo" class="loader-logo" />
            <div class="spinner"></div>
        </div>
    </div>
    <!-- End Preloader -->
    <!-- Begin Container -->
    <div class="container-fluid h-100 overflow-y">
        <div class="row flex-row h-100">
            <div class="col-12 my-auto">

                <form id="form1" runat="server">
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

                            var el = document.getElementById("btnSave");
                            el.disabled = false;
                            el.value = 'Update My Logos';
                        }
                    </script>

                    <div class="password-form mx-auto">
                        <div class="logo-centered">
                            <a href="<%: ResolveUrl("~/")%>">
                                <img src="<%: ResolveUrl("~/Template/assets/img/smallLogoWhite.png")%>" alt="logo" />
                            </a>
                        </div>
                        <h3>Update Logos</h3>
                        <%--<hr class="hrline"/>--%>
                        <asp:UpdatePanel ID="updUpdateProfile" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnSave" />
                            </Triggers>
                            <ContentTemplate>

                                <form>

                                    <div class="form-group row d-flex align-items-center mb-12">
                                        <label class="col-lg-12 form-control-label ml-4">Current Small Logo</label>
                                        <div class="col-lg-12">
                                            <figure class="img-hover-01">
                                                <img src="<%: ResolveUrl(SmallLogoUrl)%>" onerror="this.onerror=null;this.src='<%: ResolveUrl("~/Template/assets/img/smallLogo_tran.png")%>';" alt="Small Logo" class="img-fluid" />
                                            </figure>
                                        </div>
                                    </div>

                                    <div class="col-lg-12">
                                        <div class="mt-2 mb-2 position-relative">
                                            <label for="CPHBody_fileUploadID">Small Logo</label>
                                            <div class="input">
                                                <asp:FileUpload ID="fileUploadSmallLogo" accept="image/*" multiple="false" data-toggle="tooltip" data-placement="top" title="Small Logo Max 150*150px" runat="server" CssClass="form-control-file" />
                                                <span class="highlight"></span>
                                                <span class="bar"></span>
                                            </div>
                                        </div>
                                    </div>

                                    <hr class="hrline mt-4 mb-4" />

                                    <div class="form-group row d-flex align-items-center mb-12">
                                        <label class="col-lg-12 form-control-label ml-4">Current Big Logo</label>
                                        <div class="col-lg-12">
                                            <figure class="img-hover-01">
                                                <img src="<%: ResolveUrl(BigLogoUrl)%>" onerror="this.onerror=null;this.src='<%: ResolveUrl("~/Template/assets/img/logo-2.png")%>';" alt="Big Logo" class="img-fluid" />
                                            </figure>
                                        </div>
                                    </div>

                                    <div class="col-lg-12">
                                        <div class="mt-2 mb-2 position-relative">
                                            <label for="CPHBody_fileUploadID">Big Logo</label>
                                            <div class="input">
                                                <asp:FileUpload ID="fileUploadBigLogo" accept="image/*" multiple="false" data-toggle="tooltip" data-placement="top" title="Big Logo Max 400*150px" runat="server" CssClass="form-control-file" />
                                                <span class="highlight"></span>
                                                <span class="bar"></span>
                                            </div>
                                        </div>
                                    </div>

                                    <hr class="hrline mt-4 mb-4" />


                                    <div class="col-lg-12 text-center">
                                        <div class="col-md-12 col-sm-12" style="margin: 10px 0px 20px 0px;">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-lg btn-gradient-02" OnClick="btnSave_Click" />
                                        </div>
                                    </div>

                                </form>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="back">
                            <a href="<%: ResolveUrl("~/Restaurant/ProfileUpdate.aspx")%>">Profile</a>
                        </div>
                        <div class="back mt-2">
                            <span class="mr-2"><a class="la la-home la-2x" style="color: red;" href="<%: ResolveUrl("~/Restaurant/Home.aspx")%>"></a></span>
                            <span class="mr-2"><a class="la la-power-off la-2x" style="color: red;" href="<%: ResolveUrl("~/Public/Signin.aspx?logout=1")%>"></a></span>
                        </div>
                    </div>
                </form>
            </div>
            <!-- End Col -->
        </div>
        <!-- End Row -->
    </div>
    <!-- End Container -->

    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/base/jquery.min.js") %>"></script>
    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/base/core.min.js") %>"></script>
    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/nicescroll/nicescroll.min.js") %>"></script>
    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/app/app.min.js") %>"></script>

    <script>
        $(function () {
            $('[data-toggle="tooltip"]').tooltip()
        })
    </script>

</body>
</html>
