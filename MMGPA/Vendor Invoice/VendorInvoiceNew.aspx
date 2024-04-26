<%@ Page Language="C#" UnobtrusiveValidationMode="None" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="VendorInvoiceNew.aspx.cs" Inherits="Bills_And_Dispatch_VendorInvoiceNew" %>

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

            <!-- Heading -->
            <div class="col-md-12 mx-auto fw-normal fs-3 fw-medium ps-0 pb-2 text-dark-emphasis mt-1 mb-1">
                <asp:Literal Text="Vendor Invoice" runat="server"></asp:Literal>
            </div>

            <!-- Header UI Starts -->
            <div class="card col-md-12 mx-auto mt-1 py-2 shadow rounded-3">
                <div class="card-body">

                    <!-- Heading 1 -->
                    <div class="fw-normal fs-5 fw-medium text-body-secondary border-bottom pb-2 mb-4">
                        <asp:Literal Text="P.O. Details" runat="server"></asp:Literal>
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
                            <asp:TextBox ID="InvoiceNo" type="text" runat="server" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                        </div>

                        <!-- Invoice Date -->
                        <div class="col-md-6 align-self-end">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal6" Text="" runat="server">Invoice Date</asp:Literal>
                                <div>
                                    <asp:RequiredFieldValidator ID="rr35" ControlToValidate="InvoiceDate" ValidationGroup="finalSubmit" CssClass="invalid-feedback" InitialValue="" runat="server" ErrorMessage="select invoice date" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <asp:TextBox ID="InvoiceDate" type="date" runat="server" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                        </div>

                    </div>
                    <!-- 1st row Ends -->

                    <!-- 2nd row Starts -->
                    <div class="row mb-2">

                        <!-- PO No. -->
                        <div class="col-md-6 align-self-end">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal3" Text="" runat="server">P.O. Number <em style="color: red">*</em></asp:Literal>
                                <div>
                                    <asp:RequiredFieldValidator ID="rr1" ControlToValidate="PoNo" ValidationGroup="finalSubmit" CssClass="invalid-feedback" InitialValue="0" runat="server" ErrorMessage="select purchase order number and title" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <asp:DropDownList ID="PoNo" OnSelectedIndexChanged="PoNo_SelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control is-invalid" CssClass=""></asp:DropDownList>
                        </div>

                        <!-- PO date -->
                        <div class="col-md-6 align-self-end">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal1" Text="" runat="server">P.O. Date</asp:Literal>
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
                                <asp:Literal ID="Literal4" Text="" runat="server">P.O.Amount</asp:Literal>
                            </div>
                            <asp:TextBox ID="PoAmount" type="text" ReadOnly="false" runat="server" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                        </div>

                        <!-- Vendor Name -->
                        <div class="col-md-6 align-self-end">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal2" Text="" runat="server">Vendor Name</asp:Literal>
                            </div>
                            <asp:TextBox ID="VendorName" type="text" ReadOnly="true" runat="server" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                        </div>

                    </div>
                    <!-- 3rd row Ends -->


                </div>
            </div>
            <!-- Header UI Ends -->

            <!-- Details UI Starts -->
            <div id="DetailsDiv" runat="server" visible="false" class="card col-md-12 mx-auto mt-5 mb-5 rounded-3 shadow">
                <div class="card-body">

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>

                            <!-- Heading 2 -->
                            <div class="fw-normal fs-5 fw-medium text-body-secondary border-bottom px-0 pb-2 mb-4">
                                <asp:Literal Text="P.O. Item Details" runat="server"></asp:Literal>
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

                                        <asp:TemplateField HeaderText="Balance Qty" ItemStyle-Font-Size="15px" ItemStyle-CssClass="col-md-1 align-middle">
                                            <ItemTemplate>
                                                <asp:TextBox ID="BalanceQty" Text='<%# Bind("BalanceQty") %>' type="number" step="any" ReadOnly="true" Enabled="false" title="please enter billing amount" runat="server" CssClass="col-md-12 bg-white fw-light border-0 border-white rounded-1 py-1 px-2"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="ItemUOMText" HeaderText="UOM" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />
                                        <asp:BoundField DataField="ItemRate" HeaderText="Rate" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />

                                        <asp:TemplateField HeaderText="Bill Qty" ItemStyle-Font-Size="15px" ItemStyle-CssClass="col-md-2 align-middle">
                                            <ItemTemplate>
                                                <asp:TextBox ID="BillQty" Text='<%# Bind("BillQty") %>' type="number" step="any" OnTextChanged="BillQty_TextChanged" AutoPostBack="true" ReadOnly="false" Enabled="true" title="please enter billing amount" runat="server" CssClass="col-md-12 bg-light fw-light border border-secondary-subtle shadow-sm rounded-1 py-1 px-2"></asp:TextBox>
                                                <asp:CustomValidator ID="BillQtyValidator" ControlToValidate="BillQty" OnServerValidate="BillQtyValidator_ServerValidate" runat="server" ErrorMessage="exceeds balance qty" ForeColor="Red" Display="Dynamic" ValidationGroup="finalSubmit"></asp:CustomValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Sub Total" ItemStyle-Font-Size="15px" ItemStyle-CssClass="col-md-1 align-middle">
                                            <ItemTemplate>
                                                <asp:TextBox ID="ItemSubTotal" Text='<%# Bind("ItemSubTotal") %>' type="number" step="any" ReadOnly="true" Enabled="false" title="please enter billing amount" runat="server" CssClass="col-md-12 bg-white fw-light border-0 border-white rounded-1 py-1 px-2"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Add" ItemStyle-CssClass="align-middle">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckStatus" OnCheckedChanged="CheckStatus_CheckedChanged" AutoPostBack="true" Checked='<%# Eval("CheckStatus") %>' runat="server" CssClass="" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

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
                                                <asp:TextBox ID="GLAccountName" Text='<%# Bind("GLAccountName") %>' runat="server" Enabled="true" CssClass="col-md-9 fw-light bg-white border-0 py-1 px-2"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Tax Rate" ItemStyle-Font-Size="15px" ItemStyle-CssClass="col-md-2 align-middle">
                                            <ItemTemplate>
                                                <asp:TextBox ID="Value" Text='<%# Bind("Value") %>' Enabled="true" type="number" step="0.01" title="Enter a number two decimals" runat="server" CssClass="col-md-9 fw-light border border-secondary-subtle shadow-sm rounded-1 py-1 px-2"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Factor (% / ₹)" ItemStyle-Font-Size="15px" ItemStyle-CssClass="col-md-2 align-middle">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="Factor" runat="server" Enabled="true" CssClass="col-md-6 text-center fw-light border border-secondary-subtle shadow-sm rounded-1 py-1 px-2">
                                                    <asp:ListItem Text="%" Value="Percentage"></asp:ListItem>
                                                    <asp:ListItem Text="₹" Value="Amount"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Add / Less" ItemStyle-Font-Size="15px" ItemStyle-CssClass="col-md-2 align-middle">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="AddLess" runat="server" Enabled="true" CssClass="col-md-6 text-center fw-light border border-secondary-subtle shadow-sm rounded-1 py-1 px-2">
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


                                <!-- Re-Calculate Tax -->
                                <div class="mt-4 mb-3">
                                    <div class="text-end">
                                        <asp:Button ID="btnReCalTax" runat="server" Text="Re-Calculate" OnClick="btnReCalTax_Click" CssClass="btn btn-custom text-white mb-3" />
                                    </div>
                                </div>


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
                                    <asp:Literal ID="Literal8" Text="" runat="server">Remark</asp:Literal>
                                </div>
                                <textarea id="InvoiceRemark" name="w3review" rows="3" cols="50" class="form-control border border-secondary-subtle bg-white rounded-1 fs-6 fw-light py-1 shadow-sm" runat="server"></textarea>
                            </div>



                            <!-- Documents Upload UI Starts -->
                            <div class="mb-3 mt-5 py-3">

                                <!-- Heading Document -->
                                <div class="border-top border-bottom border-secondary-subtle py-2 mt-4">
                                    <div class="fw-normal fs-5 fw-medium text-body-secondary">
                                        <asp:Literal Text="Add Invoice Documents" runat="server"></asp:Literal>
                                    </div>
                                </div>

                                <!-- Documents Upload -->
                                <div id="docDiv" runat="server" visible="true" class="row mt-4">

                                    <!-- DD Document Type -->
                                    <div id="docTypeDiv" runat="server" visible="true" class="col-md-4 align-self-start">
                                        <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                            <asp:Literal ID="Literal16" Text="" runat="server">Document Type</asp:Literal>
                                        </div>
                                        <asp:DropDownList ID="DocType" AutoPostBack="false" runat="server" class="form-control is-invalid" CssClass=""></asp:DropDownList>
                                        <div>
                                            <asp:RequiredFieldValidator ID="rr6" ControlToValidate="DocType" ValidationGroup="DocumentUpload" CssClass="invalid-feedback" InitialValue="0" runat="server" ErrorMessage="select document type" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <!-- Document Reference Name -->
                                    <div id="docRefnameDiv" runat="server" visible="false" class="col-md-3 align-self-start">
                                        <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                            <asp:Literal ID="Literal17" Text="" runat="server">Document Name<em style="color: red">*</em></asp:Literal>
                                        </div>
                                        <asp:TextBox runat="server" ID="DocRefName" type="text" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                                        <div>
                                            <asp:RequiredFieldValidator ID="rr7" ControlToValidate="DocRefName" ValidationGroup="DocumentUpload" CssClass="invalid-feedback" InitialValue="" runat="server" ErrorMessage="please enter document name" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <!-- Document Upload -->
                                    <div class="col-md-6 align-self-start">
                                        <h6 class="fw-lighter fs-6 text-secondary-subtle">User can add invoice Documents !</h6>
                                        <div class="input-group has-validation">
                                            <asp:FileUpload ID="fileDoc" runat="server" CssClass="form-control" aria-describedby="inputGroupPrepend" />
                                            <asp:Button ID="btnDocUpload" Text="Add +" OnClick="btnDocUpload_Click" ValidationGroup="DocumentUpload" runat="server" AutoPost="true" CssClass="btn btn-custom btn-outline-secondary" />
                                        </div>
                                        <div>
                                            <asp:RequiredFieldValidator ID="rr8" ControlToValidate="fileDoc" ValidationGroup="DocumentUpload" CssClass="invalid-feedback" InitialValue="" runat="server" ErrorMessage="(please choose a file to upload)" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>


                                    <div class="col-md-6"></div>
                                </div>
                                <!-- Documents Upload Ends -->


                                <!-- Document Grid Starts -->
                                <div id="docGrid" class="mt-5" runat="server" visible="false">
                                    <asp:GridView ShowHeaderWhenEmpty="true" ID="GridDocument" EnableViewState="true" runat="server" AutoGenerateColumns="false" OnRowDeleting="Grid_RowDeleting"
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

                                            <asp:TemplateField HeaderText="Actions">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" CommandArgument='<%# Container.DataItemIndex %>'>
                                                        <asp:Image runat="server" ImageUrl="../assests/img/modern-cross-fill.svg" AlternateText="Edit" style="width: 28px; height: 28px;"/>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <!-- Document Grid Ends -->


                            </div>
                            <!-- Documents Upload UI Ends -->

                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnDocUpload" />
                        </Triggers>
                    </asp:UpdatePanel>





                    <hr class="border-bottom border-secondary-subtle mt-5 mb-5" />

                    <!-- Submit Button UI Starts -->
                    <div class="">
                        <div class="row mt-5 mb-2">
                            <div class="col-md-6 text-start">
                                <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn btn-custom text-white shadow mb-5" />
                            </div>
                            <div class="col-md-6 text-end">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="finalSubmit" CssClass="btn btn-custom text-white shadow mb-5" />
                            </div>
                        </div>
                    </div>
                    <!-- Submit Button UI Ends -->


                </div>
            </div>
            <!-- Details UI Ends -->

        </div>



        <script type="text/javascript">

            // function to toggle select / de-select all checkboxes
            function toggleCheckBoxes(chkAll) {
                var gridView = document.getElementById('<%= itemGrid.ClientID %>');
                var checkBoxes = gridView.getElementsByTagName('input');
                var basicAmount = 0;

                for (var i = 0; i < checkBoxes.length; i++) {
                    if (checkBoxes[i].type == 'checkbox') {
                        checkBoxes[i].checked = chkAll.checked;

                        // Calculate the total item sub total if checkbox is checked
                        if (checkBoxes[i].checked) {
                            var row = checkBoxes[i].closest('tr');
                            var textBox = row.querySelector('input[id$="ItemSubTotal"]');
                            if (textBox) {
                                basicAmount += parseFloat(textBox.value);
                            }
                        }
                    }
                }

                document.getElementById('<%= BasicPOAmount.ClientID %>').value = basicAmount.toFixed(2);
            }

        </script>
    </form>
</body>
</html>
