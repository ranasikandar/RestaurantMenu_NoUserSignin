<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmailVerification.aspx.cs" Inherits="RestaurantMenu.Public.EmailVerification" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <title><% Response.Write(System.Configuration.ConfigurationManager.AppSettings["BusinessNicName"].ToString());%> | Email Verification</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width" />
    <!-- Favicon -->
    <link rel="apple-touch-icon" sizes="180x180" href="~/Template/assets/img/apple-touch-icon.png" />
    <link rel="icon" type="image/png" sizes="32x32" href="~/Template/assets/img/favicon-32x32.png" />
    <link rel="icon" type="image/png" sizes="16x16" href="~/Template/assets/img/favicon-16x16.png" />

    <style type="text/css">
        @import url('https://fonts.googleapis.com/css?family=Montserrat|Noto+Sans');

        #outlook a {
            padding: 0;
        }

        .ReadMsgBody {
            width: 100%;
        }

        .ExternalClass {
            width: 100%;
        }

            .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {
                line-height: 100%;
            }

        body, table, td, a {
            -webkit-text-size-adjust: 100%;
            -ms-text-size-adjust: 100%;
        }

        table, td {
            mso-table-lspace: 0pt;
            mso-table-rspace: 0pt;
        }

        img {
            -ms-interpolation-mode: bicubic;
        }

        body {
            margin: 0;
            padding: 0;
        }

        img {
            border: 0;
            height: auto;
            line-height: 100%;
            outline: none;
            text-decoration: none;
        }

        table {
            border-collapse: collapse !important;
        }

        body {
            height: 100% !important;
            margin: 0;
            padding: 0;
            width: 100% !important;
        }

        * {
            -ms-text-size-adjust: 100%;
            -webkit-text-size-adjust: none;
            -webkit-text-resize: 100%;
            text-resize: 100%;
        }

        .appleBody a {
            color: #68440a;
            text-decoration: none;
        }

        .appleFooter a {
            color: #999999;
            text-decoration: none;
        }

        @media screen and (max-width: 525px) {

            table[class="wrapper"] {
                width: 100% !important;
            }

            td[class="logo"] {
                text-align: left;
                padding: 0 !important;
            }

                td[class="logo"] img {
                    margin: 0 auto !important;
                    padding: 30px 0 !important;
                }

            td[class="mobile-hide"] {
                display: none;
            }

            img[class="mobile-hide"] {
                display: none !important;
            }

            img[class="img-max"] {
                max-width: 100% !important;
                height: auto !important;
            }

            table[class="responsive-table"] {
                width: 100% !important;
            }

            td[class="padding-copy"] {
                text-align: center;
            }

            td[class="padding-meta"] {
                text-align: center;
            }

            td[class="no-pad"] {
                padding: 0 0 20px 0 !important;
            }

            td[class="no-padding"] {
                padding: 0 !important;
            }

            td[class="mobile-wrapper"] {
                padding: 10px 5% 15px 5% !important;
            }

            table[class="mobile-button-container"] {
                margin: 0 auto;
                width: 100% !important;
            }

            a[class="mobile-button"] {
                width: 80% !important;
                padding: 15px !important;
                border: 0 !important;
                font-size: 16px !important;
            }
        }
    </style>
</head>
<body style="margin: 0; padding: 0;">
    <!-- Webapp by ranasikandar@mail.com -->
    <!-- Begin Copy -->
    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="table-layout: fixed;">
        <tbody>
            <tr>
                <td bgcolor="#1f1f1f" align="center">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
                        <tbody>
                            <tr>
                                <td style="padding-top: 30px;">
                                    <!-- UNSUBSCRIBE COPY -->
                                    <table width="600" border="0" cellspacing="0" cellpadding="0" align="center" class="responsive-table">
                                        <tbody>
                                            <tr>
                                                <td align="center" valign="middle" style="font-size: 12px; line-height: 24px; font-family: Noto Sans, Arial, sans-serif; color: #aea9c3; padding-bottom: 35px;">
                                                    <a style="color: #aea9c3; text-decoration: none;" href="#"></a>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
    <!-- End Copy -->
    <!-- Begin Header -->
    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="table-layout: fixed;">
        <tbody>
            <tr>
                <td bgcolor="#1f1f1f">
                    <div align="center" style="padding: 0 15px 0 15px;">
                        <table border="0" cellpadding="0" cellspacing="0" width="600" class="wrapper">
                            <!-- Begin Logo -->
                            <tbody>
                                <tr>
                                    <td class="logo">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tbody>
                                                <tr>
                                                    <td bgcolor="#ffffff" width="100" align="left" style="border-radius: 4px 0 0 0;">
                                                        <a href="<%: ResolveUrl("~/")%>" target="_blank">
                                                            <img alt="Logo" src="<%: ResolveUrl("~/Template/assets/img/smallLogoWhite.png")%>" width="180" height="120" style="display: block; font-family: Helvetica, Arial, sans-serif; color: #666666; font-size: 16px; padding: 30px 0 30px 15px;" border="0">
                                                        </a>
                                                    </td>
                                                    <td bgcolor="#ffffff" width="400" align="right" class="mobile-hide" style="border-radius: 0 4px 0 0;">
                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td align="right" style="padding: 30px 15px 30px 0; font-size: 15px; font-family: Noto Sans, Arial, sans-serif; color: #94a4b0; text-decoration: none;">
                                                                        <span style="color: #94a4b0; text-decoration: none;">Email Verification</span>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <!-- End Logo -->
                            </tbody>
                        </table>
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
    <!-- End Header -->
    <!-- Begin Section -->
    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="table-layout: fixed;">
        <tbody>
            <tr>
                <td bgcolor="#1f1f1f" align="center" style="padding: 0 15px 0 15px;" class="section-padding">
                    <table border="0" cellpadding="0" cellspacing="0" width="600" class="responsive-table">
                        <tbody>
                            <tr>
                                <td>
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tbody>
                                            <tr>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <!-- Begin Content -->
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" bgcolor="#ffffff">
                                                        <tbody>
                                                            <tr>
                                                                <td align="center" style="font-size: 35px; font-family: Montserrat, Arial, sans-serif; color: #2c304d; padding-top: 30px;" class="padding-copy">Welcome To <% Response.Write(System.Configuration.ConfigurationManager.AppSettings["BusinessName"].ToString());%>!</td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" style="padding: 20px 0 0 0; font-size: 16px; line-height: 25px; font-family: Noto Sans, Arial, sans-serif; color: #aea9c3;" class="padding-copy">'<%Response.Write(RestaurantMenu.Public.EmailVerification.MessageOut);%>'</td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <!-- End Content -->
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <!-- Begin Button -->
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="mobile-button-container" bgcolor="#ffffff">
                                                        <tbody>
                                                            <tr>
                                                                <td align="center" style="padding: 25px 0 0 0;" class="padding-copy">
                                                                    <table border="0" cellspacing="0" cellpadding="0" class="responsive-table">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td align="center"><a href="<%: ResolveUrl("~/Public/Signin.aspx")%>" target="_blank" style="font-size: 16px; font-family: Helvetica, Arial, sans-serif; font-weight: normal; color: #ffffff; text-decoration: none; background-color: #1f1f1f; border-top: 15px solid #1f1f1f; border-bottom: 15px solid #1f1f1f; border-left: 35px solid #1f1f1f; border-right: 35px solid #1f1f1f; border-radius: 35px; -webkit-border-radius: 35px; -moz-border-radius: 35px; display: inline-block;" class="mobile-button">Sign In</a></td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <!-- End Button -->
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <!-- Begin Content -->
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" bgcolor="#ffffff">
                                                        <tbody>
                                                            <tr>
                                                                <td align="center" style="padding: 20px 0 40px 0; font-size: 16px; line-height: 25px; font-family: Noto Sans, Arial, sans-serif; color: #aea9c3;" class="padding-copy">Welcome to
                                                                    <br />
                                                                    The <% Response.Write(System.Configuration.ConfigurationManager.AppSettings["BusinessNicName"].ToString());%> Family</td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <!-- End Content -->
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
    <!-- End Section -->

    <!-- Begin Footer -->
    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="table-layout: fixed;">
        <tbody>
            <tr>
                <td bgcolor="#1f1f1f" align="center">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
                        <tbody>
                            <tr>
                                <td style="padding: 0 15px 0 15px;">
                                    <!-- UNSUBSCRIBE COPY -->
                                    <table width="600" border="0" cellspacing="0" cellpadding="0" align="center" class="responsive-table">
                                        <tbody>
                                            <tr>
                                                <td align="center" valign="middle" style="font-size: 12px; line-height: 24px; font-family: Noto Sans, Arial, sans-serif; color: #aea9c3; padding-bottom: 35px; border-radius: 0 0 4px 4px;" bgcolor="#ffffff">
                                                    <span class="appleFooter" style="color: #aea9c3;"><% Response.Write(System.Configuration.ConfigurationManager.AppSettings["BusinessAddress"].ToString());%></span>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
    <!-- End Footer -->
    <!-- Begin Copy -->
    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="table-layout: fixed;">
        <tbody>
            <tr>
                <td bgcolor="#1f1f1f" align="center">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
                        <tbody>
                            <tr>
                                <td style="padding-top: 30px;">
                                    <!-- UNSUBSCRIBE COPY -->
                                    <table width="600" border="0" cellspacing="0" cellpadding="0" align="center" class="responsive-table">
                                        <tbody>
                                            <tr>
                                                <td align="center" valign="middle" style="font-size: 12px; line-height: 24px; font-family: Noto Sans, Arial, sans-serif; color: #aea9c3; padding-bottom: 35px;">
                                                    <span class="appleFooter" style="color: #aea9c3;"><% Response.Write(System.Configuration.ConfigurationManager.AppSettings["FooterShort"].ToString());%></span>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
    <!-- Webapp by ranasikandar@mail.com -->
    <!-- End Copy -->
</body>
</html>
