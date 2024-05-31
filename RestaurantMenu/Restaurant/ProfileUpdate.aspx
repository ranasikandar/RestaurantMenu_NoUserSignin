<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProfileUpdate.aspx.cs" Inherits="RestaurantMenu.Restaurant.RestaurantProfileUpdate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title><% Response.Write(System.Configuration.ConfigurationManager.AppSettings["BusinessName"].ToString());%> | Profile Update</title>
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
                    <asp:HiddenField runat="server" ID="IsUpdate" />
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

                            var el = document.getElementById("btnUpdate");
                            el.disabled = false;
                            el.value = 'Update My Profile';
                        }
                    </script>

                    <div class="password-form mx-auto">
                        <div class="logo-centered">
                            <a href="<%: ResolveUrl("~/")%>">
                                <img src="<%: ResolveUrl("~/Template/assets/img/smallLogoWhite.png")%>" alt="logo" />
                            </a>
                        </div>
                        <h3>Update Profile</h3>

                        <asp:UpdatePanel ID="updUpdateProfile" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                            <ContentTemplate>

                                <asp:Panel runat="server" ID="PnlMsg" Style="margin-bottom: 45px;"></asp:Panel>

                                <form>
                                    <div class="group material-input">
                                        <asp:TextBox ID="txtUserName" runat="server" required></asp:TextBox>
                                        <span class="highlight"></span>
                                        <span class="bar"></span>
                                        <label>*Owner Name</label>
                                    </div>
                                    <div class="group material-input">
                                        <asp:TextBox ID="txtRestaurantName" runat="server" required></asp:TextBox>
                                        <span class="highlight"></span>
                                        <span class="bar"></span>
                                        <label>*Restaurant Name</label>
                                    </div>
                                    <div class="group material-input">
                                        <asp:TextBox ID="txtCountry" runat="server" required></asp:TextBox>
                                        <span class="highlight"></span>
                                        <span class="bar"></span>
                                        <label>*Country</label>
                                    </div>
                                    <div class="group material-input">
                                        <asp:TextBox ID="txtCity" runat="server" required></asp:TextBox>
                                        <span class="highlight"></span>
                                        <span class="bar"></span>
                                        <label>*City</label>
                                    </div>
                                    <div class="group material-input">
                                        <asp:TextBox ID="txtAddress" runat="server" required></asp:TextBox>
                                        <span class="highlight"></span>
                                        <span class="bar"></span>
                                        <label>*Address</label>
                                    </div>
                                    <div class="group material-input">
                                        <asp:TextBox ID="txtPhone" runat="server" TextMode="Phone" required></asp:TextBox>
                                        <span class="highlight"></span>
                                        <span class="bar"></span>
                                        <label>*Phone Number</label>
                                    </div>
                                    <div class="group material-input">
                                        <asp:TextBox ID="txtCurrencyCode" runat="server" MaxLength="3" required></asp:TextBox>
                                        <span class="highlight"></span>
                                        <span class="bar"></span>
                                        <label>*Currency Code e.g EUR</label>
                                    </div>
                                    <div class="group material-input">
                                        <asp:TextBox ID="txtOrderNotiEmail" runat="server" MaxLength="50" TextMode="Email"></asp:TextBox>
                                        <span class="highlight"></span>
                                        <span class="bar"></span>
                                        <label>Order Notification Email</label>
                                    </div>

                                    <div class="col-lg-12 text-center">
                                        <small>Send Order Notification at Email:</small>
                                        <%--<div class="col-md-6 col-sm-6 col-xs-12" style="margin-left: 6%;">--%>
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <div id="UserEnable" class="btn-group" data-toggle="buttons">
                                                <label class="btn btn-gradient-02" data-toggle-class="btn-primary" data-toggle-passive-class="btn-primary">
                                                    <asp:RadioButton runat="server" GroupName="UserEnable" ID="rdbEnable" AutoPostBack="false" />
                                                    &nbsp; Enable &nbsp;
                                                </label>
                                                <label class="btn btn-gradient-01" data-toggle-class="btn-primary" data-toggle-passive-class="btn-default">
                                                    <asp:RadioButton runat="server" GroupName="UserEnable" ID="rdbDisable" AutoPostBack="false" Checked="true" />
                                                    Disable
                                                </label>
                                            </div>
                                        </div>
                                    </div>

                                </form>
                                <div class="button text-center">
                                    <asp:Button ID="btnUpdate" class="btn btn-lg btn-gradient-02" runat="server" OnClientClick="this.disabled = true; this.value = 'Please Wait...';" OnClick="btnUpdate_Click" Text="Update Profile" UseSubmitBehavior="false" />
                                </div>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="back">
                            <a href="<%: ResolveUrl("~/Restaurant/BrandingLogo.aspx")%>">Add Logo</a>
                        </div>
                        <hr class="hrline" />
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
</body>
</html>
