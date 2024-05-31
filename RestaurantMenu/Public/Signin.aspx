<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Signin.aspx.cs" Inherits="RestaurantMenu.Public.Signin" %>

<!DOCTYPE html>
<!--this web app is developed by ranasikandar@mail.com-->
<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title><% Response.Write(System.Configuration.ConfigurationManager.AppSettings["BusinessName"].ToString());%> | Signin</title>
    <meta name="description" content="<% Response.Write(System.Configuration.ConfigurationManager.AppSettings["Description"].ToString());%>" />
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
    <link rel="stylesheet" href="~/Template/assets/css/animate/animate.min.css" />
    <style>
        .hrline {
            width: 100%;
            color: black;
        }
    </style>
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
<body class="bg-white">
    <!-- Begin Preloader -->
    <div id="preloader">
        <div class="canvas">
            <img src="<%: ResolveUrl("~/Template/assets/img/smallLogoWhite.png")%>" alt="logo" class="loader-logo" />
            <div class="spinner"></div>
        </div>
    </div>
    <!-- End Preloader -->
    <!-- Begin Container -->
    <div class="container-fluid no-padding h-100">
        <div class="row flex-row h-100 bg-white">
            <div class="col-xl-8 col-lg-8 col-md-7 col-sm-12 my-auto no-padding">


                <form id="form1" runat="server">

                    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
                    </asp:ScriptManager>

                    <%--<asp:HiddenField runat="server" ID="rUrl" />--%>

                    <script type="text/javascript">
                        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
                        function EndRequestHandler(sender, args) {

                            if (args.get_error()) {
                                alertMe('error', 'Sorry we are unable to Proceed your request. Please try again', 'center', 5000);
                                console.log(args.get_error().description);
                                args.set_errorHandled(true);
                            }

                            var el = document.getElementById("btnSignin");
                            el.disabled = false;
                            el.value = 'Sign In';
                        }
                    </script>

                    <!-- Begin Form -->
                    <div class="authentication-form mx-auto">

                        <div class="logo-centered">
                            <img src="<%: ResolveUrl("~/Template/assets/img/smallLogoWhite.png")%>" alt="logo" />
                        </div>

                        <h3>Sign In To <% Response.Write(System.Configuration.ConfigurationManager.AppSettings["BusinessName"].ToString());%></h3>

                        <!-- updatepanel -->
                        <asp:UpdatePanel ID="updSignin" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <!-- updatepanel -->


                                <form>
                                    <div class="group material-input">
                                        <asp:TextBox ID="txtEmail" runat="server" type="email" required></asp:TextBox>
                                        <span class="highlight"></span>
                                        <span class="bar"></span>
                                        <label>Email</label>
                                    </div>
                                    <div class="group material-input">
                                        <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" required></asp:TextBox>
                                        <span class="highlight"></span>
                                        <span class="bar"></span>
                                        <label>Password</label>
                                    </div>
                                </form>
                                <div class="row">
                                    <div class="col text-left">
                                        <div class="styled-checkbox">
                                            <input type="checkbox" name="checkbox" id="remeber" runat="server" />
                                            <label for="remeber">Remember me</label>
                                        </div>
                                    </div>
                                    <div class="col text-right">
                                        <a href="<%: ResolveUrl("~/Public/ForgotPassword.aspx")%>">Forgot Password ?</a>
                                    </div>
                                </div>
                                <div class="sign-btn text-center">
                                    <asp:Button ID="btnSignin" class="btn btn-lg btn-gradient-02" runat="server" OnClientClick="this.disabled = true; this.value = 'Please Wait...';" OnClick="btnSignin_Click" Text="Sign In" UseSubmitBehavior="false" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <div class="register">
                            Don't have an account? 
                            <br />
                            <a href="<%: ResolveUrl("~/Public/Signup.aspx")%>">Create an Restaurant account </a><span class="ion-person-stalker mt-2 ml-1"></span>
                        </div>
                        <div class="row mt-4"> <%--row mt-4 , register , style="text-align:center"--%>
                            
                            <div class="col text-center">
                                <a href="https://www.iubenda.com/terms-and-conditions/77070551" class="iubenda-nostyle no-brand iubenda-embed nav-link" style="cursor: pointer;" title="Terms and Conditions ">Terms and Conditions</a>
                                <script type="text/javascript">(function (w, d) { var loader = function () { var s = d.createElement("script"), tag = d.getElementsByTagName("script")[0]; s.src = "https://cdn.iubenda.com/iubenda.js"; tag.parentNode.insertBefore(s, tag); }; if (w.addEventListener) { w.addEventListener("load", loader, false); } else if (w.attachEvent) { w.attachEvent("onload", loader); } else { w.onload = loader; } })(window, document);</script>
                            </div>
                            <div class="col text-center">
                                <a href="https://www.iubenda.com/privacy-policy/77070551" class="iubenda-nostyle no-brand iubenda-embed nav-link" style="cursor: pointer;" title="Privacy Policy ">Privacy Policy</a>
                                <script type="text/javascript">(function (w, d) { var loader = function () { var s = d.createElement("script"), tag = d.getElementsByTagName("script")[0]; s.src = "https://cdn.iubenda.com/iubenda.js"; tag.parentNode.insertBefore(s, tag); }; if (w.addEventListener) { w.addEventListener("load", loader, false); } else if (w.attachEvent) { w.attachEvent("onload", loader); } else { w.onload = loader; } })(window, document);</script>
                            </div>
                            <div class="col text-center">
                                <a href="https://www.iubenda.com/privacy-policy/77070551/cookie-policy" class="iubenda-nostyle no-brand iubenda-embed nav-link" style="cursor: pointer;" title="Cookie Policy ">Cookie Policy</a><script type="text/javascript">(function (w, d) { var loader = function () { var s = d.createElement("script"), tag = d.getElementsByTagName("script")[0]; s.src = "https://cdn.iubenda.com/iubenda.js"; tag.parentNode.insertBefore(s, tag); }; if (w.addEventListener) { w.addEventListener("load", loader, false); } else if (w.attachEvent) { w.attachEvent("onload", loader); } else { w.onload = loader; } })(window, document);</script>
                            </div>
                        
                        </div>

                        <div style="font-size: 11px;">
                            <div class="row mt-4">
                                <div class="col text-center">
                                    MY LEBE SOCIETA'A RESPONSABIL ITA'LIMITATA SEMPLIFICATA - VIA RIVIERA CAVETTA 16 - 30016 JESOLO (VE) - 
                                </div>
                            </div>
                            <div class="row">
                                <div class="col text-center">
                                    p.iva: 04606240275 - c.f.: 04606240275 
                                </div>
                            </div>
                            <div class="row">
                                <div class="col text-center">
                                    Tel. 3932061416 - info@mylebe.com - mylebe.com - Capitale sociale 100.00
                                </div>
                            </div>
                        </div>

                    </div>
                    <!-- End Form -->

                </form>

            </div>
            <!-- End Right Content -->

            <!-- Begin Left Content -->
            <div class="col-xl-4 col-lg-4 col-md-5 d-none d-sm-block my-auto no-padding">
                <div class="elisyam-bg">
                    <div class="elisyam-overlay overlay-04"></div>
                    <div class="authentication-col-content mx-auto">
                        <h1 class="gradient-text-03">Welcome</h1>
                        <span class="description">
                            <%--<% Response.Write(System.Configuration.ConfigurationManager.AppSettings["DescriptionShort"].ToString());%>--%>
                            <%--<br />
                            <br />--%>
                            <%--<% Response.Write(System.Configuration.ConfigurationManager.AppSettings["Description"].ToString());%>--%>
                            Per My Lebe ogni cliente è unico. Iscriviti e ti aiuteremo a portare il tuo brand esattamente dove merita: un passo avanti gli altri.
                        </span>
                    </div>
                </div>
            </div>
            <!-- End Left Content -->

        </div>
        <!-- End Row -->
    </div>
    <!-- End Container -->
    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/base/jquery.min.js") %>"></script>
    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/base/core.min.js") %>"></script>
    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/noty/noty.min.js") %>"></script>
    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/app/app.min.js") %>"></script>
    <script src="<%: ResolveUrl("~/Template/assets/js/components/notifications/notifications.min.js") %>"></script>
    <script type="text/javascript">
        var _iub = _iub || [];
        _iub.csConfiguration = { "whitelabel": false, "lang": "en", "siteId": 1895106, "countryDetection": true, "gdprAppliesGlobally": false, "cookiePolicyId": 77070551, "banner": { "position": "float-top-center", "acceptButtonDisplay": true, "customizeButtonDisplay": true } };
    </script>
    <script type="text/javascript" src="//cdn.iubenda.com/cs/iubenda_cs.js" charset="UTF-8" async></script>
</body>
<!--this web app is developed by ranasikandar@mail.com-->
</html>
