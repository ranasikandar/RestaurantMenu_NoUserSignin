<%@ Page Title="" Language="C#" MasterPageFile="~/Restaurant/Restaurant.Master" AutoEventWireup="true" CodeBehind="FoodMenu.aspx.cs" Inherits="RestaurantMenu.Restaurant.FoodMenu" %>

<%--EnableEventValidation="false"--%>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHHead" runat="server">
    <title><% Response.Write(System.Configuration.ConfigurationManager.AppSettings["BusinessName"].ToString());%> | Food Menu</title>
    <link rel="stylesheet" href="<%: ResolveUrl("~/Template/assets/css/lity/lity.min.css") %>" />
    <link rel="stylesheet" href="<%: ResolveUrl("~/Template/assets/css/bootstrap-select/bootstrap-select.min.css") %>" />

    <script>

        function clearEditIfram() {
            document.getElementById('IframCategories').src = 'about:blank';
            $('.close').click();
            UpdateCategoryList();
        }

        function openCategoryModel(val) {
            document.getElementById('IframCategories').src = val;
        }

        function UpdateCategoryList() {

            var requestManager = Sys.WebForms.PageRequestManager.getInstance();
            function EndRequestHandler(sender, args) {
                requestManager.remove_endRequest(EndRequestHandler);
            }
            requestManager.add_endRequest(EndRequestHandler);
            __doPostBack('<%=updEdit.UniqueID %>', 'updateddlcategory');

            //window.location.reload(true);
        }
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHBody" runat="server">
    <div class="container">
        <!-- Begin Page Header-->
        <div class="row">
            <div class="page-header">
                <div class="d-flex align-items-center">
                    <h2 class="page-header-title">Food Menu Management</h2>
                </div>
            </div>
        </div>
        <!-- End Page Header -->

        <!-- End Row -->
        <div class="row flex-row">
            <div class="col-xl-6 os-animation" data-os-animation="fadeInUp" id="addEditpnl">
                <!-- Begin Widget 05 -->
                <div class="widget widget-05 has-shadow">
                    <!-- Begin Widget Header -->
                    <div class="widget-header bordered d-flex align-items-center">
                        <h2>Add/Edit <small>Food Menu</small></h2>
                    </div>
                    <!-- End Widget Header -->
                    <asp:UpdatePanel ID="updEdit" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnSave" />
                        </Triggers>
                        <ContentTemplate>
                            <!-- Begin Widget Body -->
                            <div class="widget-body no-padding hidden">
                                <form>
                                    <asp:TextBox ID="txtId" runat="server" class="form-control" BackColor="White" Visible="false" />

                                    <div class="col-lg-12">
                                        <div class="mt-2 mb-4 position-relative">
                                            <label for="CPHBody_ddlCategory">*Category</label>
                                            <div>
                                                <span class="col-6" style="padding-left: 0px;">

                                                    <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control" data-live-search="true" AutoPostBack="false" DataTextField="CategoryName" DataValueField="Id">
                                                    </asp:DropDownList>

                                                </span>

                                                <span class="col-4" style="padding-left: 0px;">
                                                    <input type="button" value="New Category" id="btnNewCategory" class="btn btn-default" style="width: 12em;" data-toggle="modal" data-target="#model-Category" onclick="openCategoryModel('/Restaurant/UC/ProductCategories.aspx')" />
                                                </span>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-12">
                                        <div class="mt-2 mb-2 position-relative">
                                            <label for="CPHBody_txtName">*Food Name</label>
                                            <div class="group material-input">
                                                <asp:TextBox ID="txtName" runat="server" MaxLength="50"></asp:TextBox>
                                                <span class="highlight"></span>
                                                <span class="bar"></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-12">
                                        <div class="mt-2 mb-2 position-relative">
                                            <label for="CPHBody_txtDiscription">Food Description</label>
                                            <div class="group material-input">
                                                <asp:TextBox ID="txtDiscription" runat="server" MaxLength="500"></asp:TextBox>
                                                <span class="highlight"></span>
                                                <span class="bar"></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-12">
                                        <div class="mt-2 mb-2 position-relative">
                                            <label for="CPHBody_txtPrice">*Price</label>
                                            <div class="group material-input">
                                                <asp:TextBox ID="txtPrice" runat="server" MaxLength="8"></asp:TextBox>
                                                <span class="highlight"></span>
                                                <span class="bar"></span>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-12">
                                        <div class="mt-2 mb-2 position-relative">
                                            <label for="CPHBody_fileUploadID">Image</label>
                                            <div class="group material-input">
                                                <asp:FileUpload ID="fileUploadID" accept="image/*" multiple="false" data-toggle="tooltip" data-placement="top" title="Food Image 685*385px File size Lessthen 2MB" runat="server" CssClass="form-control-file" />
                                                <span class="highlight"></span>
                                                <span class="bar"></span>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row d-flex align-items-center mb-12">
                                        <label class="col-lg-12 form-control-label ml-4">Current Image</label>
                                        <div class="col-lg-12">
                                            <figure class="img-hover-01">
                                                <img src="<%: ResolveUrl(ImageUrlThumb)%>" onerror="this.onerror=null;this.src='<%: ResolveUrl("~/Template/assets/img/albums/01.jpg")%>';" alt="ID Image" class="img-fluid" />
                                                <div>
                                                    <a href="<%: ResolveUrl(ImageUrl)%>" onerror="this.onerror=null;this.src='<%: ResolveUrl("~/Template/assets/img/albums/01.jpg")%>';" data-lity="" data-lity-desc="...">
                                                        <i class="la la-expand"></i>
                                                    </a>
                                                </div>
                                            </figure>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-lg-12 text-center">
                                            <small>Status:</small>
                                            <div class="col-md-12 col-sm-12">
                                                <div id="UserEnable" class="btn-group" data-toggle="buttons">
                                                    <label class="btn btn-gradient-02" data-toggle-class="btn-primary" data-toggle-passive-class="btn-primary" data-toggle='tooltip' data-placement='bottom' title='' data-original-title='Food will be Available for Orders'>
                                                        <asp:RadioButton runat="server" GroupName="UserEnable" ID="rdbEnable" AutoPostBack="false" Checked="true" />
                                                        &nbsp; Enable &nbsp;
                                                    </label>
                                                    <label class="btn btn-gradient-01" data-toggle-class="btn-primary" data-toggle-passive-class="btn-default" data-toggle='tooltip' data-placement='bottom' title='' data-original-title='Food will be not visible to customers for order'>
                                                        <asp:RadioButton runat="server" GroupName="UserEnable" ID="rdbDisable" AutoPostBack="false" />
                                                        Disable
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-12 text-center">
                                        <div class="col-md-12 col-sm-12" style="margin: 10px 0px 20px 0px;">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnCancel" runat="server" Width="90px" Text="Cancel" CssClass="btn btn-primary" OnClick="btnCancel_Click" />
                                        </div>
                                    </div>

                                </form>
                            </div>
                            <!-- End Widget Body -->

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <!-- End Widget 05 -->
            </div>

            <div class="col-xl-6 os-animation" data-os-animation="fadeInUp">
                <!-- Begin Widget 06 -->
                <div class="widget widget-06 has-shadow">
                    <!-- Begin Widget Header -->
                    <div class="widget-header bordered d-flex align-items-center">
                        <h2>Search <small>Foods</small></h2>
                    </div>
                    <!-- End Widget Header -->
                    <asp:UpdatePanel ID="updSearch" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true" EnableViewState="true">
                        <ContentTemplate>
                            <!-- Begin Widget Body -->
                            <div class="widget-body p-0">
                                <div id="list-group" class="widget-scroll" style="height: auto;"> <%--max-height: 55em;--%>
                                    <div class="col-lg-12">
                                        <div class="mt-5 mb-5 position-relative">
                                            <div class="group material-input">
                                                <asp:TextBox ID="txtFoodSearch" runat="server"></asp:TextBox>
                                                <span class="highlight"></span>
                                                <span class="bar"></span>
                                                <label for="CPHBody_txtFoodSearch">Search By Food Name</label>
                                                <br />
                                                <label for="CPHBody_ddlCategorySearch" style="margin-top: 65px;">Search By Category</label>
                                                <asp:DropDownList runat="server" ID="ddlCategorySearch" CssClass="form-control" data-live-search="true" AutoPostBack="false" DataTextField="CategoryName" DataValueField="Id">
                                                </asp:DropDownList>
                                                <br />
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-lg btn-gradient-02" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-12 table-responsive">
                                        <asp:GridView ID="dgvSearch" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                                            OnRowCommand="dgvSearch_RowCommand" DataKeyNames="Id" AllowPaging="True" PageSize="10" Width="100%"
                                            OnPageIndexChanging="dgvSearch_PageIndexChanging" CellPadding="4" ForeColor="#73879C"
                                            GridLines="None" AllowSorting="True" BorderStyle="None" CssClass="table table-bordered mb-0">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="Id" HeaderText="Id" Visible="false">
                                                    <ItemStyle Width="125px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Name" HeaderText="Name">
                                                    <ItemStyle Width="75%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:0.00}">
                                                    <ItemStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btneditg" runat="server" CommandName="editx" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            Text="EDIT" CausesValidation="false" CssClass="btn btn-primary btn-sm" OnClientClick="window.location='#addEditpnl'" />
                                                    </ItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                            </Columns>
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="#3F5367" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#EDEDED" Font-Bold="True" ForeColor="#73879C" />
                                            <PagerSettings FirstPageText="&lt;" LastPageText="&gt;" Mode="NumericFirstLast" />
                                            <PagerStyle BackColor="#bbbbbb" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#F9F9F9" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <PagerStyle CssClass="gridview" />
                                        </asp:GridView>

                                        <asp:HiddenField ID="HiddenFieldGridEdit" runat="server" />
                                    </div>
                                </div>
                                <!-- End List -->
                            </div>
                            <!-- End Widget Body -->
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <!-- End Widget 06 -->
            </div>
        </div>
        <!-- End Row -->

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" runat="server">
    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/lity/lity.min.js") %>"></script>
    <script src="<%: ResolveUrl("~/Template/assets/vendors/js/bootstrap-select/bootstrap-select.min.js") %>"></script>

    <div id="model-Category" class="modal fade">
        <div class="modal-dialog modal-dialog-centered modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Add Edit Food Categories</h4>
                    <button type="button" class="close" data-dismiss="modal">
                        <span aria-hidden="true" onclick="clearEditIfram();return false;">x</span>
                        <span class="sr-only">close</span>
                    </button>
                </div>
                <div class="modal-body" style="height: 340px;">
                    <iframe id="IframCategories" src='about:blank' style='width: 100%; height: 100%; border: 0;'></iframe>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-shadow" data-dismiss="modal" onclick="clearEditIfram();return false;">Close</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
