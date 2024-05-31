<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="RestaurantMenu.Public.ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title><% Response.Write(System.Configuration.ConfigurationManager.AppSettings["BusinessNicName"].ToString());%> | Forgot Password</title>
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
    <link rel="apple-touch-icon" sizes="180x180" href="~/Template/assets/img/apple-touch-icon.png" />
    <link rel="icon" type="image/png" sizes="32x32" href="~/Template/assets/img/favicon-32x32.png" />
    <link rel="icon" type="image/png" sizes="16x16" href="~/Template/assets/img/favicon-16x16.png" />
    <!-- Stylesheet -->
    <link rel="stylesheet" href="~/Template/assets/vendors/css/base/bootstrap.min.css" />
    <link rel="stylesheet" href="~/Template/assets/vendors/css/base/elisyam-1.5.min.css" />

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

    <!-- Webapp by ranasikandar@mail.com -->

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

                            var el = document.getElementById("btnSignup");
                            el.disabled = false;
                            el.value = 'Reset Password';
                        }
                    </script>

                    <div class="password-form mx-auto">
                        <div class="logo-centered">
                            <a href="<%: ResolveUrl("~/")%>">
                                <img src="<%: ResolveUrl("~/Template/assets/img/smallLogoWhite.png")%>" alt="logo" />
                            </a>
                        </div>
                        <h3>Password Recovery</h3>

                        <asp:UpdatePanel ID="updSignup" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                            <ContentTemplate>

                                <asp:Panel runat="server" ID="PnlMsg" Style="margin-bottom: 45px;"></asp:Panel>

                                <form>
                                    <div class="group material-input">
                                        <asp:TextBox ID="txtEmail" runat="server" type="email" required></asp:TextBox>
                                        <span class="highlight"></span>
                                        <span class="bar"></span>
                                        <label>Email</label>
                                    </div>
                                </form>
                                <div class="button text-center">
                                    <asp:Button ID="btnSignup" class="btn btn-lg btn-gradient-02" runat="server" OnClientClick="this.disabled = true; this.value = 'Please Wait...';" OnClick="btnSignup_Click" Text="Reset Password" UseSubmitBehavior="false" />
                                </div>

                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <div class="back">
                            <a href="<%: ResolveUrl("~/Public/Signin.aspx")%>">Sign In</a>
                        </div>
                    </div>
                </form>
            </div>
            <!-- End Col -->
        </div>
        <!-- End Row -->
    </div>
    <!-- End Container -->

    <!-- Begin Vendor Js -->
    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/base/jquery.min.js") %>"></script>
    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/base/core.min.js") %>"></script>
    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/nicescroll/nicescroll.min.js") %>"></script>
    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/app/app.min.js") %>"></script>
</body>

</html>
