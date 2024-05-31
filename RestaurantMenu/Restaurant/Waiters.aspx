<%@ Page Title="" Language="C#" MasterPageFile="~/Restaurant/Restaurant.Master" AutoEventWireup="true" CodeBehind="Waiters.aspx.cs" Inherits="RestaurantMenu.Restaurant.Waiters" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHHead" runat="server">
    <title><% Response.Write(System.Configuration.ConfigurationManager.AppSettings["BusinessName"].ToString());%> | Waiters</title>
    <script>
        function changeInputType() {
            document.getElementById('CPHBody_txtPassword').type = 'password';
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHBody" runat="server">
    <div class="container">
        <!-- Begin Page Header-->
        <div class="row">
            <div class="page-header">
                <div class="d-flex align-items-center">
                    <h2 class="page-header-title">Waiters Management</h2>
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
                        <h2>Add/Edit <small>Waiter</small></h2>
                    </div>
                    <!-- End Widget Header -->
                    <asp:UpdatePanel ID="updEdit" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <ContentTemplate>

                            <!-- Begin Widget Body -->
                            <div class="widget-body no-padding hidden">
                                <form>
                                    <asp:TextBox ID="txtId" runat="server" Visible="false" />
                                    <div class="col-lg-12">
                                        <div class="mt-2 mb-2 position-relative">
                                            <label for="CPHBody_txtName">*Name</label>
                                            <div class="group material-input">
                                                <asp:TextBox ID="txtName" runat="server" MaxLength="50"></asp:TextBox>
                                                <span class="highlight"></span>
                                                <span class="bar"></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-12">
                                        <div class="mt-2 mb-2 position-relative">
                                            <label for="CPHBody_txtEmail">*Email</label>
                                            <div class="group material-input">
                                                <asp:TextBox ID="txtEmail" runat="server" MaxLength="100" TextMode="Email"></asp:TextBox>
                                                <span class="highlight"></span>
                                                <span class="bar"></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-12">
                                        <div class="mt-2 mb-2 position-relative">
                                            <label for="CPHBody_txtPassword">*Password</label>
                                            <div class="group material-input">
                                                <asp:TextBox ID="txtPassword" runat="server" MaxLength="50"></asp:TextBox>
                                                <span class="highlight"></span>
                                                <span class="bar"></span>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-12">
                                        <div class="mt-2 mb-2 position-relative">
                                            <label for="CPHBody_txtDiscription">Discription</label>
                                            <div class="group material-input">
                                                <asp:TextBox ID="txtDiscription" runat="server" MaxLength="500"></asp:TextBox>
                                                <span class="highlight"></span>
                                                <span class="bar"></span>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-lg-12 text-center">
                                            <small>Status:</small>
                                            <div class="col-md-12 col-sm-12">
                                                <div id="UserEnable" class="btn-group" data-toggle="buttons">
                                                    <label class="btn btn-gradient-02" data-toggle-class="btn-primary" data-toggle-passive-class="btn-primary" data-toggle='tooltip' data-placement='bottom' title='' data-original-title='Waiter can login and use web application'>
                                                        <asp:RadioButton runat="server" GroupName="UserEnable" ID="rdbEnable" AutoPostBack="false" Checked="true" />
                                                        &nbsp; Enable &nbsp;
                                                    </label>
                                                    <label class="btn btn-gradient-01" data-toggle-class="btn-primary" data-toggle-passive-class="btn-default" data-toggle='tooltip' data-placement='bottom' title='' data-original-title='Waiter can not login and use web application'>
                                                        <asp:RadioButton runat="server" GroupName="UserEnable" ID="rdbDisable" AutoPostBack="false" />
                                                        Disable
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-12 text-center">
                                        <div class="col-md-12 col-sm-12" style="margin: 10px 0px 20px 0px;">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" OnClientClick="this.disabled = true; this.value = 'Wait...';" OnClick="btnSave_Click" UseSubmitBehavior="false" />
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
                        <h2>Search <small>Waiters</small></h2>
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
                                                <asp:TextBox ID="txtWaiterSearch" runat="server"></asp:TextBox>
                                                <span class="highlight"></span>
                                                <span class="bar"></span>
                                                <label for="CPHBody_txtWaiterSearch">Search By Waiter Name</label>
                                                <br />
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-lg btn-gradient-02" OnClientClick="this.disabled = true; this.value = 'Please Wait...';" UseSubmitBehavior="false" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-12 table-responsive">
                                        <asp:GridView ID="dgvSearch" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                                            OnRowCommand="dgvSearch_RowCommand" DataKeyNames="UserId" AllowPaging="True" PageSize="10" Width="100%"
                                            OnPageIndexChanging="dgvSearch_PageIndexChanging" CellPadding="4" ForeColor="#73879C"
                                            GridLines="None" AllowSorting="True" BorderStyle="None" CssClass="table table-bordered mb-0">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="UserId" HeaderText="Id" Visible="false">
                                                    <ItemStyle Width="125px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Name" HeaderText="Name">
                                                    <ItemStyle Width="40%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Details" HeaderText="Detail">
                                                    <ItemStyle Width="50%" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btneditg" runat="server" CommandName="editx" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            Text="Edit" CausesValidation="false" CssClass="btn btn-primary btn-sm" OnClientClick="window.location='#addEditpnl'" />
                                                    </ItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:Button ID="delbtn" runat="server" OnClientClick="return confirm('Are you sure to DELETE this Waiter? you are adviced to set its status as disable so this waiter can not login into.');"
                                                            CommandName="deletex" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            Text="Delete" CssClass="btn btn-danger btn-sm" />
                                                    </ItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Send">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnSendEmail" runat="server" CommandName="Sendx" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            Text="Email" CausesValidation="false" CssClass="btn btn-default btn-sm"
                                                            OnClientClick="return confirm('Are you sure to SEND an Email to This Waiter With Login Details?');" />
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
    <script>
        changeInputType();
    </script>
</asp:Content>
