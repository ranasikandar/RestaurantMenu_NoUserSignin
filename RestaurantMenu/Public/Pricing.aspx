<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pricing.aspx.cs" Inherits="RestaurantMenu.Public.Pricing" %>

<!DOCTYPE html>
<!-- Webapp by ranasikandar@mail.com -->
<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title><% Response.Write(System.Configuration.ConfigurationManager.AppSettings["BusinessName"].ToString());%> | Pricing</title>
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

    <style>
        .title {
            font-size: 20px;
        }
    </style>
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

            <div class="content-inner" style="width: calc(100% - 2%); margin-left: 1%; margin-right: 1%;">
                <div class="container-fluid">
                    <!-- Begin Page Header-->
                    <div class="row">
                        <div class="page-header">
                            <div class="d-flex align-items-center">
                                <h2 class="page-header-title"><% Response.Write(System.Configuration.ConfigurationManager.AppSettings["BusinessName"].ToString());%> Pricing</h2>
                                <div>
                                    <ul class="breadcrumb">
                                        <li class="breadcrumb-item"><a href="<%: ResolveUrl("~/") %>"><i class="ti ti-home"></i></a></li>
                                        <li class="breadcrumb-item"><a href="<%: ResolveUrl("~/Public/Signup.aspx") %>"><i class="ti ti-user"></i></a></li>
                                        <li class="breadcrumb-item"><a href="<%: ResolveUrl("~/Public/Signin.aspx?logout=1") %>"><i class="ti ti-power-off"></i></a></li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- End Page Header -->
                    <!-- Begin Row -->
                    <div class="row">
                        <div class="col-xl-12">
                            <div class="widget has-shadow">
                                <!-- Begin Widget Header -->
                                <div class="widget-header bordered no-actions d-flex align-items-center">
                                    <h2>Scegli il piano più adatto a te!</h2>
                                </div>
                                <!-- End Widget Header -->
                                <div class="text-center mt-3">
                                    <%Response.Write(PricingMessage);%>
                                </div>
                                <!-- Begin Widget Body -->
                                <div class="widget-body" style="padding: 50px 0;">
                                    <div class="pricing-tables-fixed">
                                        <div class="row">

                                            <div class="col-lg-4 no-padding">
                                                <div class="pricing-tables-02 pricing-wrapper">
                                                    <div class="inner-container">
                                                        <div class="title">Starter</div>
                                                        <div class="pricing-image">
                                                            <img src="<%: ResolveUrl("~/Template/assets/img/icone-01.png") %>" alt="..." />
                                                        </div>
                                                        <div class="pricing-list">
                                                            <ul>
                                                                <li><i class="ion-checkmark margin-right-5"></i>Visualizzazione menù</li>
                                                            </ul>
                                                        </div>
                                                        <div class="main-number">14,90€<small> 1 Mese</small></div>
                                                        <div class="pricing-list mb-0">
                                                            <ul>
                                                                <li>(Iva incl.)</li>
                                                            </ul>
                                                            <ul>
                                                                <li>Disdici quando vuoi!</li>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-lg-4 no-padding">
                                                <div class="pricing-tables-02 purple pricing-wrapper">
                                                    <div class="inner-container">
                                                        <div class="title">Premium</div>
                                                        <div class="pricing-image">
                                                            <img src="<%: ResolveUrl("~/Template/assets/img/icone-01.png") %>" alt="..." />
                                                        </div>
                                                        <div class="pricing-list">
                                                            <ul>
                                                                <li><i class="ion-checkmark margin-right-5"></i>Visualizzazione menù</li>
                                                                <li><i class="ion-checkmark margin-right-5"></i>Accesso camerieri</li>
                                                            </ul>
                                                        </div>
                                                        <div class="main-number">199,00€<small> 12 Mese</small></div>
                                                        <div class="pricing-list mb-0">
                                                            <ul>
                                                                <li>(Iva incl.)</li>
                                                            </ul>
                                                            <ul>
                                                                <li>Disdici quando vuoi!</li>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-lg-4 no-padding">
                                                <div class="pricing-tables-02 dark pricing-wrapper">
                                                    <div class="inner-container">
                                                        <div class="title">Basic</div>
                                                        <div class="pricing-image">
                                                            <img src="<%: ResolveUrl("~/Template/assets/img/icone-03-white.png") %>" alt="..." />
                                                        </div>
                                                        <div class="pricing-list">
                                                            <ul>
                                                                <li><i class="ion-checkmark margin-right-5"></i>Visualizzazione menù</li>
                                                                <li><i class="ion-checkmark margin-right-5"></i>Accesso camerieri</li>
                                                            </ul>
                                                        </div>
                                                        <div class="main-number">24,90€<small> 1 Mese</small></div>
                                                        <div class="pricing-list mb-0">
                                                            <ul>
                                                                <li>(Iva incl.)</li>
                                                            </ul>
                                                            <ul>
                                                                <li>Disdici quando vuoi!</li>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <!-- End Widget Body -->

                                <div class="text-center mb-3">
                                    per attivare l'account eseguire un bonifico bancario presso i dati sotto indicati e inoltrare la propria ricevuta a info@mylebe.com
                                </div>

                                <div id="paymentOptions" class="invoice-footer">
                                    <!-- Begin Invoice Container -->
                                    <div class="invoice-container">
                                        <div class="row d-flex align-items-center">

                                            <div class="col-xl-6 col-md-6 col-sm-12 d-flex justify-content-xl-start justify-content-md-start justify-content-center mb-2">
                                                <div class="bank">
                                                    <div class="title">Bank Info</div>
                                                    <div class="text">Nome della banca: Banca Sella</div>
                                                    <div class="text">Nome della società: My Lebe SRLS</div>
                                                    <div class="text">IBAN: IT81F0326812000052234261600</div>
                                                    <div class="text">CAUSALE: indicare la mail utilizzata per l'iscrizione</div>
                                                    <div class="text">BIC: SELBIT2BXXX</div>
                                                </div>
                                            </div>

                                            <div class="col-xl-6 col-md-6 col-sm-12 d-flex justify-content-xl-start justify-content-md-start justify-content-center mb-2">
                                                <div class="bank">
                                                    <div class="title">Contact Details</div>
                                                    <div class="number">Call: <% Response.Write(System.Configuration.ConfigurationManager.AppSettings["BusinessPhone"].ToString());%></div>
                                                    <%--<div class="text">Skype: <% Response.Write(System.Configuration.ConfigurationManager.AppSettings["BusinessSkype"].ToString());%></div>--%>
                                                    <div class="text">Whatsapp: <% Response.Write(System.Configuration.ConfigurationManager.AppSettings["BusinessWhatsapp"].ToString());%></div>
                                                    <div class="text">Email: <% Response.Write(System.Configuration.ConfigurationManager.AppSettings["BusinessEmail"].ToString());%></div>
                                                </div>
                                            </div>

                                        </div>
                                        <%--<div class="footer-bottom">
                                                <div class="thx">
                                                    <i class="la la-heart"></i><span>Thank You!</span>
                                                </div>
                                                <div class="website"><% Response.Write(System.Configuration.ConfigurationManager.AppSettings["WebsiteAddress"].ToString());%></div>
                                            </div>--%>
                                    </div>
                                    <!-- End Invoice Container -->
                                </div>

                            </div>
                        </div>
                    </div>
                    <!-- End Row -->

                </div>
                <!-- End Container -->
                <!-- Begin Page Footer-->
                <footer class="main-footer">

                    <div class="row mt-4">
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

                    <!-- <div class="col-xl-4 col-lg-4 col-md-4 col-sm-12 d-flex align-items-center justify-content-xl-end justify-content-lg-end justify-content-md-end justify-content-center">
                                <p>Created with <a style="color:red;">❤</a> by <a href="mailto:ranasikandar@mail.com">Rana Sikandar</a></p>
                        </div> -->

                    <div class="row mt-4">
                        <div class="col text-center">
                            <a class="nav-link">MY LEBE SOCIETA'A RESPONSABIL ITA'LIMITATA SEMPLIFICATA - VIA RIVIERA CAVETTA 16 - 30016 JESOLO (VE) - </a>
                            </div>
                        </div>
                    <div class="row">
                        <div class="col text-center">
                            <a class="nav-link">p.iva: 04606240275 - c.f.: 04606240275 </a>
                            </div>
                        </div>
                    <div class="row">
                        <div class="col text-center">
                            <a class="nav-link">Tel. 3932061416 - info@mylebe.com - mylebe.com - Capitale sociale 100.00</a>
                            </div>
                        </div>

                </footer>
                <!-- End Page Footer -->
                <a href="#" class="go-top" style="display: none;"><i class="la la-arrow-up"></i></a>
            </div>
        </div>
        <!-- End Page Content -->
    </div>

    <!-- Begin Vendor Js -->
    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/base/jquery.min.js") %>"></script>
    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/base/core.min.js") %>"></script>
    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/nicescroll/nicescroll.min.js") %>"></script>
    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/noty/noty.min.js") %>"></script>
    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/app/app.min.js") %>"></script>
    <script src="<%: ResolveUrl("~/Template/assets/js/components/notifications/notifications.min.js") %>"></script>

    <script type="text/javascript">
        var _iub = _iub || [];
        _iub.csConfiguration = { "whitelabel": false, "lang": "en", "siteId": 1895106, "countryDetection": true, "gdprAppliesGlobally": false, "cookiePolicyId": 77070551, "banner": { "position": "float-top-center", "acceptButtonDisplay": true, "customizeButtonDisplay": true } };
    </script>
    <script type="text/javascript" src="//cdn.iubenda.com/cs/iubenda_cs.js" charset="UTF-8" async></script>

</body>
<!-- Webapp by ranasikandar@mail.com -->
</html>
