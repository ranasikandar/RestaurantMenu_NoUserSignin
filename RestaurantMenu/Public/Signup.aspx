<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="RestaurantMenu.Public.Signup" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title><% Response.Write(System.Configuration.ConfigurationManager.AppSettings["BusinessName"].ToString());%> | Signup</title>
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
                            el.value = 'Sign Up';
                        }
                    </script>

                    <div class="password-form mx-auto">
                        <div class="logo-centered">
                            <a href="<%: ResolveUrl("~/")%>">
                                <img src="<%: ResolveUrl("~/Template/assets/img/smallLogoWhite.png")%>" alt="logo" />
                            </a>
                        </div>
                        <h3>Restaurant Signup</h3>

                        <asp:UpdatePanel ID="updSignup" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                            <ContentTemplate>

                                <asp:Panel runat="server" ID="PnlMsg" Style="margin-bottom: 45px;"></asp:Panel>

                                <form>
                                    <div class="group material-input">
                                        <asp:TextBox ID="txtName" runat="server" required></asp:TextBox>
                                        <span class="highlight"></span>
                                        <span class="bar"></span>
                                        <label>Your Full Name</label>
                                    </div>
                                    <div class="group material-input">
                                        <asp:TextBox ID="txtEmail" runat="server" type="email" required></asp:TextBox>
                                        <span class="highlight"></span>
                                        <span class="bar"></span>
                                        <label>Email</label>
                                    </div>
                                    <div class="group material-input">
                                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" required></asp:TextBox>
                                        <span class="highlight"></span>
                                        <span class="bar"></span>
                                        <label>Password</label>
                                    </div>
                                </form>
                                <div class="row">
                                    <div class="col text-left">
                                        <div class="styled-checkbox">
                                            <input type="checkbox" name="checkbox" id="agree" runat="server" />
                                            <%--<label for="agree" style="color: #98a8b4;">I Accept <a data-toggle="modal" data-target="#scroll-modal" style="color: #808080;">Terms and Conditions.</a> <a data-toggle="modal" data-target="#scroll-modal1" style="color: #808080;">Privacy Policy.</a></label>--%>
                                            <label for="agree" style="color: #98a8b4;">
                                                I Accept 
                                            </label>
                                            <a href="https://www.iubenda.com/terms-and-conditions/77070551" class="iubenda-nostyle no-brand iubenda-embed nav-link" style="cursor: pointer;" title="Terms and Conditions ">Terms and Conditions</a><script type="text/javascript">(function (w,d) {var loader = function () {var s = d.createElement("script"), tag = d.getElementsByTagName("script")[0]; s.src="https://cdn.iubenda.com/iubenda.js"; tag.parentNode.insertBefore(s,tag);}; if(w.addEventListener){w.addEventListener("load", loader, false);}else if(w.attachEvent){w.attachEvent("onload", loader);}else{w.onload = loader;}})(window, document);</script>
                                                <a href="https://www.iubenda.com/privacy-policy/77070551" class="iubenda-nostyle no-brand iubenda-embed nav-link" style="cursor: pointer;" title="Privacy Policy ">Privacy Policy</a><script type="text/javascript">(function (w,d) {var loader = function () {var s = d.createElement("script"), tag = d.getElementsByTagName("script")[0]; s.src="https://cdn.iubenda.com/iubenda.js"; tag.parentNode.insertBefore(s,tag);}; if(w.addEventListener){w.addEventListener("load", loader, false);}else if(w.attachEvent){w.attachEvent("onload", loader);}else{w.onload = loader;}})(window, document);</script>
                                                <a href="https://www.iubenda.com/privacy-policy/77070551/cookie-policy" class="iubenda-nostyle no-brand iubenda-embed nav-link" style="cursor: pointer;" title="Cookie Policy ">Cookie Policy</a><script type="text/javascript">(function (w,d) {var loader = function () {var s = d.createElement("script"), tag = d.getElementsByTagName("script")[0]; s.src="https://cdn.iubenda.com/iubenda.js"; tag.parentNode.insertBefore(s,tag);}; if(w.addEventListener){w.addEventListener("load", loader, false);}else if(w.attachEvent){w.attachEvent("onload", loader);}else{w.onload = loader;}})(window, document);</script>
                                        </div>
                                    </div>
                                </div>
                                <div class="button text-center">
                                    <asp:Button ID="btnSignup" class="btn btn-lg btn-gradient-02" runat="server" OnClientClick="this.disabled = true; this.value = 'Please Wait...';" OnClick="btnSignup_Click" Text="Signup" UseSubmitBehavior="false" />
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
    <!-- Webapp by ranasikandar@mail.com -->
    <!-- End Container -->
    
    <!-- Begin Vendor Js -->
    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/base/jquery.min.js") %>"></script>
    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/base/core.min.js") %>"></script>
    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/nicescroll/nicescroll.min.js") %>"></script>
    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/app/app.min.js") %>"></script>
    <script type="text/javascript">
        var _iub = _iub || [];
        _iub.csConfiguration = { "whitelabel": false, "lang": "en", "siteId": 1895106, "countryDetection": true, "gdprAppliesGlobally": false, "cookiePolicyId": 77070551, "banner": { "position": "float-top-center", "acceptButtonDisplay": true, "customizeButtonDisplay": true } };
    </script>
    <script type="text/javascript" src="//cdn.iubenda.com/cs/iubenda_cs.js" charset="UTF-8" async></script>
</body>
</html>
