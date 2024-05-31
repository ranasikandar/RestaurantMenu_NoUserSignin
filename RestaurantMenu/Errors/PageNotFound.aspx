<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageNotFound.aspx.cs" Inherits="RestaurantMenu.Errors.PageNotFound" %>

<!DOCTYPE html>

<html lang="en">
    <head>
        <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <title><% Response.Write(System.Configuration.ConfigurationManager.AppSettings["BusinessName"].ToString());%> | Error 404</title>
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
        <link rel="apple-touch-icon" sizes="180x180" href="<%: ResolveUrl("~/Template/assets/img/apple-touch-icon.png")%>">
        <link rel="icon" type="image/png" sizes="32x32" href="<%: ResolveUrl("~/Template/assets/img/favicon-32x32.png")%>">
        <link rel="icon" type="image/png" sizes="16x16" href="<%: ResolveUrl("~/Template/assets/img/favicon-16x16.png")%>">
        <!-- Stylesheet -->
        <link rel="stylesheet" href="<%: ResolveUrl("~/Template/assets/vendors/css/base/bootstrap.min.css")%>">
        <link rel="stylesheet" href="<%: ResolveUrl("~/Template/assets/vendors/css/base/elisyam-1.5.min.css")%>">
    </head>
    <body>
        <!-- Webapp by ranasikandar@mail.com -->
        <!-- Begin Preloader -->
        <div id="preloader">
            <div class="canvas">
                <img src="<%: ResolveUrl("~/Template/assets/img/smallLogoWhite.png")%>" alt="logo" class="loader-logo"/>
                <div class="spinner"></div>   
            </div>
        </div>
        <!-- End Preloader -->
        <!-- Begin Container -->
        <div class="container-fluid no-padding h-100">
            <div class="row justify-content-center align-items-center h-100">
                <!-- Begin Left Content -->
                <div class="col-xl-4 col-lg-4 col-md-8 no-padding d-flex justify-content-center">
                    <!-- Begin Error -->
                    <div class="error-02 mx-auto mb-3 text-center">
                        <h1 class="text-gradient-01">404</h1>
                        <h2>This page cannot be found! </h2>
                        <p>But we have lots of other pages for your service.</p>
                        <a href="<%: ResolveUrl("~/")%>" class="btn btn-lg btn-gradient-01">Go Home</a>
                        <a href="javascript:history.back()" class="btn btn-lg btn-gradient-02">Go Back</a>
                    </div>
                    <!-- End Error -->
                </div>
                <!-- End Left Content -->
                <!-- Begin Right Content -->
                <div class="col-xl-8 col-lg-8 col-md-4 d-none d-sm-block no-padding">
                <div class="elisyam-bg">
                    <div class="elisyam-overlay overlay-04"></div>
                    <div class="authentication-col-content mx-auto">
                        <h1 class="gradient-text-03"><% Response.Write(System.Configuration.ConfigurationManager.AppSettings["BusinessName"].ToString());%></h1>
                        <span class="description">
                            <% Response.Write(System.Configuration.ConfigurationManager.AppSettings["Description"].ToString());%>
                        </span>
                    </div>
                </div>
            </div>
            <!-- End Right Content -->
            </div> 
            <!-- End Row -->
        </div>
        <!-- End Container -->
        <!-- Webapp by ranasikandar@mail.com -->
        <!-- Begin Vendor Js -->
        <script src="<%: ResolveUrl("~/Template/assets/vendors/js/base/jquery.min.js")%>"></script>
        <script src="<%: ResolveUrl("~/Template/assets/vendors/js/base/core.min.js")%>"></script>
        <script src="<%: ResolveUrl("~/Template/assets/vendors/js/app/app.min.js")%>"></script>
    </body>
</html>
