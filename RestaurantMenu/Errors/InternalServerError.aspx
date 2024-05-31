<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InternalServerError.aspx.cs" Inherits="RestaurantMenu.Errors.InternalServerError" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title><% Response.Write(System.Configuration.ConfigurationManager.AppSettings["BusinessName"].ToString());%> | Error 500</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
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

</head>
<body class="bg-error-01">
    <!-- Webapp by ranasikandar@mail.com -->
    <!-- Begin Preloader -->
    <div id="preloader">
        <div class="canvas">
            <img src="<%: ResolveUrl("~/Template/assets/img/smallLogoWhite.png")%>" alt="logo" class="loader-logo" />
            <div class="spinner"></div>
        </div>
    </div>
    <!-- End Preloader -->
    <form id="form1" runat="server">
        <!-- Begin Container -->
        <div class="container-fluid h-100 error-01">
            <div class="row justify-content-center align-items-center h-100">
                <div class="col-12">
                    <!-- Begin Container -->
                    <div class="error-container mx-auto text-center">
                        <h1>Oops.</h1>
                        <br /><br />
                        <p>Sorry, we had some technical problem during your last operation.</p>
                        <p>But dont worry we are working to fix it meanwhile please try again.</p>
                        <a href="<%: ResolveUrl("~/")%>" class="btn btn-lg btn-gradient-04">Go Home</a>
                        <a href="javascript:history.back()" class="btn btn-lg btn-gradient-01">Go Back</a>
                    </div>
                    <!-- End Container -->
                </div>
                <!-- End Col -->
                <p>Created with <a style="color:red;">❤</a> by <a href="mailto:ranasikandar@mail.com" style="color:#f5eee9;">Rana Sikandar</a></p>
            </div>
            <!-- End Row -->
        </div>
        <!-- End Container -->
    </form>
    <!-- Begin Vendor Js -->
    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/base/jquery.min.js") %>"></script>
    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/base/core.min.js") %>"></script>
    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/app/app.min.js") %>"></script>
</body>
</html>
