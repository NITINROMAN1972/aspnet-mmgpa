<%@ Page Language="C#" UnobtrusiveValidationMode="None" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="VendorInvoice.aspx.cs" Inherits="Vendor_Invoice_VendorInvoice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Vendor Invoice</title>

    <!-- Boottrap CSS -->
    <link href="../assests/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assests/css/bootstrap1.min.css" rel="stylesheet" />

    <!-- Bootstrap JS -->
    <script src="../assests/js/bootstrap.bundle.min.js"></script>
    <script src="../assests/js/bootstrap1.min.js"></script>

    <!-- Popper.js -->
    <script src="../assests/js/popper.min.js"></script>
    <script src="../assests/js/popper1.min.js"></script>

    <!-- jQuery -->
    <script src="../assests/js/jquery-3.6.0.min.js"></script>
    <script src="../assests/js/jquery.min.js"></script>
    <script src="../assests/js/jquery-3.3.1.slim.min.js"></script>

    <!-- Select2 library CSS and JS -->
    <link href="../assests/select2/select2.min.css" rel="stylesheet" />
    <script src="../assests/select2/select2.min.js"></script>

    <!-- Sweet Alert CSS and JS -->
    <link href="../assests/sweertalert/sweetalert2.min.css" rel="stylesheet" />
    <script src="../assests/sweertalert/sweetalert2.all.min.js"></script>

    <!-- Sumo Select CSS and JS -->
    <link href="../assests/sumoselect/sumoselect.min.css" rel="stylesheet" />
    <script src="../assests/sumoselect/jquery.sumoselect.min.js"></script>

    <!-- DataTables CSS & JS -->
    <link href="../assests/DataTables/datatables.min.css" rel="stylesheet" />
    <script src="../assests/DataTables/datatables.min.js"></script>

    <script src="vendorinvoice.js"></script>
    <link rel="stylesheet" href="vendorinvoice.css" />


</head>
<body>
    <form id="form1" runat="server">


        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>

        <div id="loginDiv" runat="server" visible="true">


            <!-- top Searching div Starts -->
            <div id="divTopSearch" runat="server" visible="true">
                <div class="col-md-12 mx-auto">

                    <!-- Heading Start -->
                    <div class="justify-content-end d-flex px-0 text-body-secondary mb-0 mt-4">
                        <div class="col-md-6 px-0">
                            <div class="fw-semibold fs-3 text-dark">
                                <asp:Literal ID="Literal14" Text="Vendor Invoice" runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="col-md-6 text-end px-0">
                            <div class="fw-semibold fs-5">
                                <asp:Button ID="btnNewBill" runat="server" Text="New Invoice +" OnClick="btnNewBill_Click" CssClass="btn btn-custom text-white shadow" />
                            </div>
                        </div>
                    </div>

                    <!-- Control UI Starts -->
                    <div class="card mt-2 shadow">
                        <div class="card-body">


                            <!-- Search UI Starts -->
                            <div id="divSearchEmb" runat="server" visible="true" class="">
                                <div class="card-body">

                                    <!-- 1st Row Starts -->
                                    <div class="row mb-2">

                                        <!-- Search DD - A.A. No -->
                                        <div class="col-md-4 align-self-end">
                                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                                <asp:Literal ID="Literal15" Text="" runat="server">Invoice Number</asp:Literal>
                                            </div>
                                            <asp:DropDownList ID="ddScInvoiceNo" runat="server" AutoPostBack="false" class="form-control is-invalid" CssClass=""></asp:DropDownList>
                                        </div>

                                        <!-- Search From Date -->
                                        <div class="col-md-4 align-self-end">
                                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                                <asp:Literal ID="Literal22" Text="" runat="server">From Date</asp:Literal>
                                            </div>
                                            <asp:TextBox runat="server" ID="ScFromDate" type="date" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                                        </div>

                                        <!-- Search To Date -->
                                        <div class="col-md-4 align-self-end">
                                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                                <asp:Literal ID="Literal23" Text="" runat="server">To Date</asp:Literal>
                                            </div>
                                            <asp:TextBox runat="server" ID="ScToDate" type="date" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                                        </div>

                                    </div>
                                    <!-- 1st Row Ends -->

                                    <!-- Search Button -->
                                    <div class="row mb-2 mt-4">
                                        <div class="col-md-10"></div>
                                        <div class="col-md-2">
                                            <div class="text-end">
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-custom col-md-10 text-white shadow" />
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <!-- Search UI Ends -->

                            <!-- Search Grid Div Starts -->
                            <div id="searchGridDiv" visible="false" runat="server" class="mt-3">

                                <hr class="border-dark py-3" />

                                <asp:GridView ShowHeaderWhenEmpty="true" ID="gridSearch" runat="server" AutoGenerateColumns="false" OnRowCommand="gridSearch_RowCommand" AllowPaging="true" PageSize="10"
                                    CssClass="datatable table table-bordered border border-1 border-dark-subtle table-hover text-center grid-custom" OnPageIndexChanging="gridSearch_PageIndexChanging" PagerStyle-CssClass="gridview-pager">
                                    <HeaderStyle CssClass="" />
                                    <Columns>
                                        <asp:TemplateField ControlStyle-CssClass="col-md-1" HeaderText="Sr.No">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="id" runat="server" Value="id" />
                                                <span>
                                                    <%#Container.DataItemIndex + 1%>
                                                </span>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="col-md-1" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="InvoiceNo" HeaderText="Invoice No." ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" />
                                        <asp:BoundField DataField="InvoiceDate" HeaderText="Invoice Date" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="NetAmount" HeaderText="Invoice Amount (₹)" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" />
                                        <asp:BoundField DataField="PoNo" HeaderText="P.O. No." ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" />
                                        <asp:BoundField DataField="PoAmt" HeaderText="P.O. Amount (₹)" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" />
                                        <asp:BoundField DataField="VendorName" HeaderText="Vendor Name" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" />
                                        <asp:BoundField DataField="InvoiceItemsCount" HeaderText="Invoice Items Count" ItemStyle-CssClass="col-xs-1 align-middle text-start fw-light" />

                                        <asp:TemplateField HeaderText="View" ItemStyle-CssClass="align-middle">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="btnedit" CommandArgument='<%# Eval("RefNo") %>' CommandName="lnkView" ToolTip="Edit" CssClass="shadow-sm">
                                                    <asp:Image runat="server" ImageUrl="../assests/img/pencil-square.svg" AlternateText="Edit" style="width: 16px; height: 16px;"/>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="" />
                                </asp:GridView>
                            </div>
                            <!-- Search Grid Div Ends -->


                        </div>
                    </div>
                    <!-- Control UI Ends -->


                </div>
            </div>
            <!-- top Searching div Ends -->

            <!-- Update Div Starts -->
            <div id="UpdateDiv" runat="server" visible="false">



                <!-- Heading -->
                <div class="col-md-12 mx-auto fw-normal fs-3 fw-medium ps-0 pb-2 text-dark-emphasis mt-1 mb-1">
                    <asp:Literal Text="Vendor Invoice" runat="server"></asp:Literal>
                </div>

                <!-- Header UI Starts -->
                <div class="card col-md-12 mx-auto mt-1 py-2 shadow rounded-3">
                    <div class="card-body">


                        <!-- Heading 1 -->
                        <div class="fw-normal fs-5 fw-medium text-body-secondary border-bottom pb-2 mb-4">
                            <asp:Literal Text="Invoice Details" runat="server"></asp:Literal>
                        </div>

                        <!-- 1st row Starts -->
                        <div class="row mb-2">

                            <!-- Invoice No -->
                            <div class="col-md-6 align-self-end">
                                <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                    <asp:Literal ID="Literal5" Text="" runat="server">Invoice No.</asp:Literal>
                                    <div>
                                        <asp:RequiredFieldValidator ID="rr68" ControlToValidate="InvoiceNo" ValidationGroup="finalSubmit" CssClass="invalid-feedback" InitialValue="" runat="server" ErrorMessage="enter invoice number" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <asp:TextBox ID="InvoiceNo" ReadOnly="true" type="text" runat="server" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                            </div>

                            <!-- Invoice Date -->
                            <div class="col-md-6 align-self-end">
                                <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                    <asp:Literal ID="Literal3" Text="" runat="server">Invoice Date</asp:Literal>
                                    <div>
                                        <asp:RequiredFieldValidator ID="rr35" ControlToValidate="InvoiceDate" ValidationGroup="finalSubmit" CssClass="invalid-feedback" InitialValue="" runat="server" ErrorMessage="select invoice date" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <asp:TextBox ID="InvoiceDate" ReadOnly="true" type="date" runat="server" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                            </div>

                        </div>
                        <!-- 1st row Ends -->

                        <!-- 2nd row Starts -->
                        <div class="row mb-2">

                            <!-- PO No. -->
                            <div class="col-md-6 align-self-end">
                                <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                    <asp:Literal ID="Literal10" Text="" runat="server">P.O. Number <em style="color: red">*</em></asp:Literal>
                                    <div>
                                        <asp:RequiredFieldValidator ID="rr1" ControlToValidate="PoNo" ValidationGroup="finalSubmit" CssClass="invalid-feedback" InitialValue="0" runat="server" ErrorMessage="select purchase order number and title" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <asp:DropDownList ID="PoNo" AutoPostBack="false" runat="server" class="form-control is-invalid" CssClass=""></asp:DropDownList>
                            </div>

                            <!-- PO date -->
                            <div class="col-md-6 align-self-end">
                                <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                    <asp:Literal ID="Literal11" Text="" runat="server">P.O. Date</asp:Literal>
                                </div>
                                <asp:TextBox ID="PoDate" type="date" ReadOnly="true" runat="server" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                            </div>

                        </div>
                        <!-- 2nd row Ends -->

                        <!-- 3rd row Starts -->
                        <div class="row mb-2">

                            <!-- PO Amount -->
                            <div class="col-md-6 align-self-end">
                                <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                    <asp:Literal ID="Literal12" Text="" runat="server">P.O.Amount</asp:Literal>
                                </div>
                                <asp:TextBox ID="PoAmount" type="text" ReadOnly="true" runat="server" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                            </div>

                            <!-- Vendor Name -->
                            <div class="col-md-6 align-self-end">
                                <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                    <asp:Literal ID="Literal13" Text="" runat="server">Vendor Name</asp:Literal>
                                </div>
                                <asp:TextBox ID="VendorName" type="text" ReadOnly="true" runat="server" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                            </div>

                        </div>
                        <!-- 3rd row Ends -->


                    </div>
                </div>
                <!-- Header UI Ends -->




                <!-- Details UI Starts -->
                <div class="card col-md-12 mx-auto mt-5 mb-5 rounded-3 shadow">
                    <div class="card-body">

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

                                <!-- Heading 2 -->
                                <div class="fw-normal fs-5 fw-medium text-body-secondary border-bottom px-0 pb-2 mb-4">
                                    <asp:Literal Text="Item Details" runat="server"></asp:Literal>
                                </div>


                                <!-- Item GridView Starts -->
                                <div id="itemDiv" runat="server" visible="false" class="mt-3">

                                    <asp:GridView ShowHeaderWhenEmpty="true" ID="itemGrid" runat="server" AutoGenerateColumns="false"
                                        CssClass="table table-bordered  border border-1 border-dark-subtle text-center grid-custom mb-3">
                                        <HeaderStyle CssClass="align-middle" />
                                        <Columns>
                                            <asp:TemplateField ControlStyle-CssClass="col-md-1" HeaderText="Sr.No">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="id" runat="server" Value="id" />
                                                    <span>
                                                        <%#Container.DataItemIndex + 1%>
                                                    </span>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="col-md-1" />
                                                <ItemStyle Font-Size="15px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ItemCategoryText" HeaderText="Item Category" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />
                                            <asp:BoundField DataField="ItemSubCategoryText" HeaderText="Item Sub Category" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />
                                            <asp:BoundField DataField="ItemNameText" HeaderText="Item Name" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />
                                            <asp:BoundField DataField="PoQuantity" HeaderText="P.O. Qty" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />
                                            <asp:BoundField DataField="ItemUOMText" HeaderText="UOM" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />
                                            <asp:BoundField DataField="ItemRate" HeaderText="Rate/Unit" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />
                                            <asp:BoundField DataField="BillQty" HeaderText="Bill Qty" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />

                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <!-- Item GridView Ends -->

                                <!-- Baisc Amount -->
                                <div class="row px-0">
                                    <div class="col-md-8">
                                    </div>
                                    <div class="col-md-4 align-self-end text-end">
                                        <asp:Literal ID="Literal7" Text="" runat="server">Basic P.O. Amount</asp:Literal>
                                        <div class="input-group">
                                            <span class="input-group-text fs-5 fw-semibold">₹</span>
                                            <asp:TextBox runat="server" ID="BasicPOAmount" CssClass="form-control fw-lighter border border-2" ReadOnly="true" placeholder="Total Item Amount"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>


                                <!-- Tax Grid Starts -->
                                <div id="divTaxHead" runat="server" visible="false">

                                    <!-- Heading Document -->
                                    <div class="border-top border-bottom border-secondary-subtle py-2 mt-4 mb-4">
                                        <div class="fw-normal fs-5 fw-medium text-body-secondary">
                                            <asp:Literal Text="GL Account Head" runat="server"></asp:Literal>
                                        </div>
                                    </div>

                                    <asp:GridView ShowHeaderWhenEmpty="true" ID="GridTax" runat="server" AutoGenerateColumns="false" OnRowDataBound="GridTax_RowDataBound" CssClass="table text-center">
                                        <HeaderStyle CssClass="align-middle grid-custom fw-light" />
                                        <Columns>

                                            <asp:TemplateField HeaderText="GL Account Name" ItemStyle-Font-Size="15px" ItemStyle-CssClass="col-md-4 align-middle text-start fw-light">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="GLAccountName" Text='<%# Bind("GLAccountName") %>' runat="server" Enabled="false" CssClass="col-md-9 fw-light bg-white border-0 py-1 px-2"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Tax Rate" ItemStyle-Font-Size="15px" ItemStyle-CssClass="col-md-2 align-middle">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="Value" Text='<%# Bind("Value") %>' Enabled="false" type="number" step="0.01" title="Enter a number two decimals" runat="server" CssClass="col-md-9 fw-light border border-secondary-subtle shadow-sm rounded-1 py-1 px-2"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Factor (% / ₹)" ItemStyle-Font-Size="15px" ItemStyle-CssClass="col-md-2 align-middle">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="Factor" runat="server" Enabled="false" CssClass="col-md-6 text-center fw-light border border-secondary-subtle shadow-sm rounded-1 py-1 px-2">
                                                        <asp:ListItem Text="%" Value="Percentage"></asp:ListItem>
                                                        <asp:ListItem Text="₹" Value="Amount"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Add / Less" ItemStyle-Font-Size="15px" ItemStyle-CssClass="col-md-2 align-middle">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="AddLess" runat="server" Enabled="false" CssClass="col-md-6 text-center fw-light border border-secondary-subtle shadow-sm rounded-1 py-1 px-2">
                                                        <asp:ListItem Text="+" Value="Add"></asp:ListItem>
                                                        <asp:ListItem Text="-" Value="Less"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Amount" ItemStyle-Font-Size="15px" ItemStyle-CssClass="col-md-3 align-middle">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TaxAmount" Text='<%# Bind("TaxAmount") %>' type="number" step="0.01" runat="server" Enabled="false" ReadOnly="false" CssClass="col-md-9 fw-light border border-secondary-subtle shadow-sm rounded-1 py-1 px-2"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>


                                    <!-- Net Deduction, Addition & Total Bill Amounts -->
                                    <div class="row mb-3">
                                        <!-- Total Deduction -->
                                        <div class="col-md-3 align-self-end">
                                            <asp:Literal ID="Literal28" Text="Total Deductions :" runat="server"></asp:Literal>
                                            <div class="input-group text-end">
                                                <span class="input-group-text fs-5 fw-light">₹</span>
                                                <asp:TextBox runat="server" ID="txtTotalDeduct" CssClass="form-control fw-lighter border border-2" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>

                                        <!-- Total Addition -->
                                        <div class="col-md-3 align-self-end">
                                            <asp:Literal ID="Literal29" Text="Total Additions :" runat="server"></asp:Literal>
                                            <div class="input-group text-end">
                                                <span class="input-group-text fs-5 fw-light">₹</span>
                                                <asp:TextBox runat="server" ID="txtTotalAdd" CssClass="form-control fw-lighter border border-2" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-3"></div>

                                        <!-- Net Amount -->
                                        <div class="col-md-3 align-self-end text-end">
                                            <asp:Literal ID="Literal30" Text="Net Amount :" runat="server"></asp:Literal>
                                            <div class="input-group text-end">
                                                <span class="input-group-text fs-5 fw-light">₹</span>
                                                <asp:TextBox runat="server" ID="txtNetAmnt" CssClass="form-control fw-lighter border border-2" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>


                                </div>
                                <!-- Tax Grid Ends -->


                                <hr class="mt-5 mb-3 border border-dark-subtle" />

                                <!-- Remark -->
                                <div class="col-md-12 px-0 align-self-end">
                                    <div class="mb-1 fs-6">
                                        <asp:Literal ID="Literal8" Text="" runat="server">Remark (optional)</asp:Literal>
                                    </div>
                                    <textarea id="InvoiceRemark" readonly="readonly" rows="3" cols="50" class="form-control border border-secondary-subtle bg-white rounded-1 fs-6 fw-light py-1 shadow-sm" runat="server"></textarea>
                                </div>


                                <!-- Documents Upload UI Starts -->
                                <div class="mb-3 mt-5 py-3">

                                    <!-- Heading Document -->
                                    <div class="border-top border-bottom border-secondary-subtle py-2 mt-4">
                                        <div class="fw-normal fs-5 fw-medium text-body-secondary">
                                            <asp:Literal Text="Vendor Invoice Documents" runat="server"></asp:Literal>
                                        </div>
                                    </div>

                                    <!-- Document Grid Starts -->
                                    <div id="docGrid" class="mt-3" runat="server" visible="false">
                                        <asp:GridView ShowHeaderWhenEmpty="true" ID="GridDocument" EnableViewState="true" runat="server" AutoGenerateColumns="false"
                                            CssClass="table table-bordered border border-light-subtle text-start mt-3 grid-custom">
                                            <HeaderStyle CssClass="align-middle fw-light fs-6" />
                                            <Columns>

                                                <asp:TemplateField ControlStyle-CssClass="col-md-1" HeaderText="Sr.No">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="id" runat="server" Value="id" />
                                                        <span>
                                                            <%#Container.DataItemIndex + 1%>
                                                        </span>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="DocType" HeaderText="Document Type" ReadOnly="true" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />
                                                <asp:BoundField DataField="DocTypeText" HeaderText="Document Type" ReadOnly="true" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />
                                                <asp:BoundField DataField="DocRefName" HeaderText="Document Name" ReadOnly="true" Visible="false" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />
                                                <asp:BoundField DataField="DocName" HeaderText="File Name" ReadOnly="true" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />

                                                <asp:TemplateField HeaderText="View Document" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="DocPath" runat="server" Text="View Uploaded Document" NavigateUrl='<%# Eval("DocPath") %>' Target="_blank" CssClass="text-decoration-none"></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <!-- Document Grid Ends -->


                                </div>
                                <!-- Documents Upload UI Ends -->


                            </ContentTemplate>
                            <Triggers>
                            </Triggers>
                        </asp:UpdatePanel>

                        <!-- Submit Button UI Starts -->
                        <div class="">
                            <div class="row mt-5 mb-2">
                                <div class="col-md-6 text-start">
                                    <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn btn-custom text-white shadow mb-5" />
                                </div>
                                <div class="col-md-6 text-end">
                                </div>
                            </div>
                        </div>
                        <!-- Submit Button UI Ends -->


                    </div>
                </div>
                <!-- Details UI Ends -->

            </div>
            <!-- Update Div Ends -->


        </div>


    </form>
</body>
</html>
