<%@ Page Language="C#" UnobtrusiveValidationMode="None" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="PurchaseOrderBOMNew.aspx.cs" Inherits="Purchase_Order_PurchaseOrderBOM" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Purchase Order BOM</title>

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

    <script src="purchaseorder.js"></script>
    <link rel="stylesheet" href="purchaseorder.css" />


</head>
<body>
    <form id="form1" runat="server">



        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>

        <div id="loginDiv" runat="server" visible="true">

            <!-- Heading -->
            <div class="col-md-12 mx-auto fw-normal fs-3 fw-medium ps-0 pb-2 text-dark-emphasis mt-1 mb-1">
                <asp:Literal Text="Create Purchase Order" runat="server"></asp:Literal>
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

                        <!-- Tender No & Title -->
                        <div class="col-md-12 align-self-end">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal5" Text="" runat="server">Tender Number & Title <em style="color: red">*</em></asp:Literal>
                                <div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="TenderNoNTitle" ValidationGroup="finalSubmit" CssClass="invalid-feedback" InitialValue="0" runat="server" ErrorMessage="select tender number & title" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <asp:DropDownList ID="TenderNoNTitle" OnSelectedIndexChanged="TenderNoNTitle_SelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control is-invalid" CssClass=""></asp:DropDownList>
                        </div>

                    </div>
                    <!-- 1st row Ends -->

                    <!-- 1st-MID row Starts -->
                    <div class="row mb-2">

                        <!-- Estimation No -->
                        <div class="col-md-6 align-self-end">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal9" Text="" runat="server">Estimation Number <em style="color: red">*</em></asp:Literal>
                                <div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="EstimationNo" ValidationGroup="finalSubmit" CssClass="invalid-feedback" InitialValue="" runat="server" ErrorMessage="enter estimation no." SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <asp:TextBox runat="server" ID="EstimationNo" type="text" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                        </div>

                        <!-- Estimation Date -->
                        <div class="col-md-6 align-self-end">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal14" Text="" runat="server">Estimation Date <em style="color: red">*</em></asp:Literal>
                                <div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="EstimationDate" ValidationGroup="finalSubmit" CssClass="invalid-feedback" InitialValue="" runat="server" ErrorMessage="select estimate date" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <asp:TextBox runat="server" ID="EstimationDate" type="date" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                        </div>

                    </div>

                    <!-- 2nd row Starts -->
                    <div class="row mb-2">

                        <!-- PO No -->
                        <div class="col-md-6 align-self-end">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal3" Text="" runat="server">P.O. Number <em style="color: red">*</em></asp:Literal>
                                <div>
                                    <asp:RequiredFieldValidator ID="rr1" ControlToValidate="PONumber" ValidationGroup="finalSubmit" CssClass="invalid-feedback" InitialValue="" runat="server" ErrorMessage="enter po no." SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <asp:TextBox runat="server" ID="PONumber" type="text" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                        </div>

                        <!-- PO Date -->
                        <div class="col-md-6 align-self-end">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal12" Text="" runat="server">PO Date <em style="color: red">*</em></asp:Literal>
                                <div>
                                    <asp:RequiredFieldValidator ID="rr2" ControlToValidate="PODate" ValidationGroup="finalSubmit" CssClass="invalid-feedback" InitialValue="" runat="server" ErrorMessage="select po date" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <asp:TextBox runat="server" ID="PODate" type="date" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                        </div>

                    </div>
                    <!-- 2nd row Ends -->

                    <!-- 3rd row Starts -->
                    <div class="row mb-2">

                        <!-- Vender Name -->
                        <div class="col-md-6 align-self-end">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal13" Text="" runat="server">Vender Name <em style="color: red">*</em></asp:Literal>
                                <div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="VenderName" ValidationGroup="finalSubmit" CssClass="invalid-feedback" InitialValue="0" runat="server" ErrorMessage="select vender name" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <asp:DropDownList ID="VenderName" AutoPostBack="false" runat="server" class="form-control is-invalid" CssClass=""></asp:DropDownList>
                        </div>

                        <!-- PO Amount -->
                        <div class="col-md-6 align-self-end">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal10" Text="" runat="server">P.O. Amount<em style="color: red">*</em></asp:Literal>
                                <div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="POAmount" ValidationGroup="finalSubmit" CssClass="invalid-feedback" InitialValue="" runat="server" ErrorMessage="enter po amount" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <asp:TextBox runat="server" ID="POAmount" type="number" step="any" min="-Infinity" max="Infinity" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                        </div>

                    </div>
                    <!-- 3rd row Ends -->

                    <!-- 4th row Starts -->
                    <div class="row mb-2">

                        <!-- PO Remark -->
                        <div class="col-md-12 align-self-end mb-3">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal11" Text="" runat="server">P.O. Remark</asp:Literal>
                            </div>
                            <textarea id="PORemark" rows="2" cols="50" class="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1" runat="server"></textarea>
                        </div>

                    </div>
                    <!-- 4th row Ends -->

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

                            <!-- Mode Of Entry Starts - Radio Buttons -->
                            <div class="">
                                <h6>Mode Of Entry <em style="color: red">*</em></h6>
                                <div class="row">

                                    <!-- 1st Row Starts -->
                                    <div class="col-md-12 align-content-between text-body-tertiary fw-semibold fs-6 ps-4 pe-3">
                                        <asp:RadioButton ID="manualRadio" runat="server" Text="" OnCheckedChanged="manualRadio_CheckedChanged" AutoPostBack="true" GroupName="entryMode" />
                                        <literal>Manual Item Entry</literal>
                                    </div>
                                    <!-- 1st Row Ends -->

                                    <%--<!-- 2nd Row Starts -->
                                    <div class="col-md-12 align-content-between text-body-tertiary fw-semibold fs-6 ps-4 pe-3">
                                        <asp:RadioButton ID="excelRadio" runat="server" Text="" OnCheckedChanged="excelRadio_CheckedChanged" AutoPostBack="true" GroupName="entryMode" />
                                        <literal>Through Excel</literal>
                                    </div>
                                    <!-- 2nd Row Ends -->--%>

                                    <!-- 3rd Row Starts -->
                                    <div class="col-md-12 align-content-between text-body-tertiary fw-semibold fs-6 ps-4 pe-3">
                                        <asp:RadioButton ID="EstimateRadio" runat="server" Text="" OnCheckedChanged="EstimateRadio_CheckedChanged" AutoPostBack="true" Checked="true" GroupName="entryMode" />
                                        <literal>Through Tender</literal>
                                    </div>
                                    <!-- 3rd Row Ends -->

                                </div>
                            </div>
                            <!-- Mode Of Entry Ends -->




                            <!-- Item Manual UI STarts -->
                            <div id="itemEnterManualDiv" runat="server" visible="false" class="mt-3">

                                <!-- 1st Row Starts -->
                                <div class="row mb-2">

                                    <!-- Item Category -->
                                    <div class="col-md-3 align-self-end">
                                        <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                            <asp:Literal ID="Literal16" Text="" runat="server">Item Category<em style="color: red">*</em></asp:Literal>
                                            <div>
                                                <asp:RequiredFieldValidator ID="rr6" ControlToValidate="ItemCategory" ValidationGroup="ItemSave" CssClass="invalid-feedback" InitialValue="0" runat="server" ErrorMessage="select item category" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <asp:DropDownList ID="ItemCategory" OnSelectedIndexChanged="ItemCategory_SelectedIndexChanged" runat="server" AutoPostBack="true" class="form-control is-invalid" CssClass=""></asp:DropDownList>
                                    </div>

                                    <!-- Item Sub Category -->
                                    <div class="col-md-3 align-self-end">
                                        <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                            <asp:Literal ID="Literal2" Text="" runat="server">Item Sub Category<em style="color: red">*</em></asp:Literal>
                                            <div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ItemSubCategory" ValidationGroup="ItemSave" CssClass="invalid-feedback" InitialValue="0" runat="server" ErrorMessage="select item sub category" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <asp:DropDownList ID="ItemSubCategory" OnSelectedIndexChanged="ItemSubCategory_SelectedIndexChanged" runat="server" AutoPostBack="true" class="form-control is-invalid" CssClass=""></asp:DropDownList>
                                    </div>

                                    <!-- Item Name -->
                                    <div class="col-md-3 align-self-end">
                                        <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                            <asp:Literal ID="Literal4" Text="" runat="server">Item Name</asp:Literal>
                                            <div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ItemName" ValidationGroup="ItemSave" CssClass="invalid-feedback" InitialValue="0" runat="server" ErrorMessage="select item name" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                            </div>
                                            <div>
                                                <asp:CustomValidator ID="ItemExistsCV" OnServerValidate="ItemExistsCV_ServerValidate" ValidationGroup="ItemSave"
                                                    ClientValidationFunction="checkItemExistence" Display="Dynamic" runat="server" CssClass="text-danger">
                                                </asp:CustomValidator>
                                            </div>
                                        </div>
                                        <asp:DropDownList ID="ItemName" OnSelectedIndexChanged="ItemName_SelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control is-invalid" CssClass=""></asp:DropDownList>
                                    </div>


                                </div>
                                <!-- 1st Row Ends -->


                                <!-- 2nd Row Starts -->
                                <div class="row mb-2">

                                    <!-- PO Quantity -->
                                    <div class="col-md-3 align-self-end">
                                        <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                            <asp:Literal ID="Literal20" Text="" runat="server">P.O. Quantity</asp:Literal>
                                            <div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="PoQuantity" ValidationGroup="ItemSave" CssClass="invalid-feedback" InitialValue="" runat="server" ErrorMessage="enter p.o. quantity" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <asp:TextBox ID="PoQuantity" runat="server" type="number" steps="0.01" onchange="calculateSubTotal()" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                                    </div>

                                    <!-- Item UOM -->
                                    <div class="col-md-3 align-self-end">
                                        <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                            <asp:Literal ID="Literal18" Text="" runat="server">Item UOM<em style="color: red">*</em></asp:Literal>
                                            <div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="ItemUOM" ValidationGroup="ItemSave" CssClass="invalid-feedback" InitialValue="0" runat="server" ErrorMessage="select uom" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <asp:DropDownList ID="ItemUOM" runat="server" AutoPostBack="false" class="form-control is-invalid" CssClass=""></asp:DropDownList>
                                    </div>

                                    <!-- Item Rate -->
                                    <div class="col-md-3 align-self-end">
                                        <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                            <asp:Literal ID="Literal19" Text="" runat="server">Item Rate</asp:Literal>
                                            <div>
                                                <asp:RequiredFieldValidator ID="rr9" ControlToValidate="ItemRate" ValidationGroup="ItemSave" CssClass="invalid-feedback" InitialValue="" runat="server" ErrorMessage="enter item rate / unit" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <asp:TextBox ID="ItemRate" runat="server" type="number" steps="0.01" onchange="calculateSubTotal()" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                                    </div>

                                    <!-- Item Sub Total (JS) -->
                                    <div class="col-md-3 align-self-end">
                                        <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                            <asp:Literal ID="Literal6" Text="" runat="server">Item Sub Total</asp:Literal>
                                            <div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="ItemSubTotalTxt" ValidationGroup="ItemSave" CssClass="invalid-feedback" InitialValue="" runat="server" ErrorMessage="enter item sub total" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <asp:TextBox ID="ItemSubTotalTxt" runat="server" type="number" steps="0.01" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                                    </div>

                                </div>
                                <!-- 2nd Row Ends -->


                                <!-- 3rd Row Starts -->
                                <div class="row mb-2">

                                    <!-- Item Description -->
                                    <div class="col-md-10 align-self-end mb-3">
                                        <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                            <asp:Literal ID="Literal7" Text="" runat="server">Item Description</asp:Literal>
                                        </div>
                                        <textarea id="ItemDescription" rows="2" cols="50" class="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1" runat="server"></textarea>
                                    </div>

                                    <!-- Add Button -->
                                    <div class="col-md-2 align-self-end  mb-3 text-start">
                                        <div class="pb-0 mb-0">
                                            <asp:Button ID="btnItemInsert" runat="server" Text="Add +" OnClick="btnItemInsert_Click" ValidationGroup="ItemSave" CssClass="btn btn-success text-white shadow mb-5 col-md-7 button-position" />
                                        </div>
                                    </div>

                                </div>
                                <!-- 3rd Row Ends -->

                            </div>
                            <!-- Item Manual UI Ends -->





                            <!-- Excel Upload UI Starts -->
                            <div id="excelUploadDiv" runat="server" visible="false" class="mb-3 mt-3 py-3">

                                <!-- Heading 2 Excel -->
                                <div class="border-top border-bottom border-secondary-subtle py-2 mt-4">
                                    <div class="fw-normal fs-5 fw-medium text-body-secondary">
                                        <asp:Literal Text="Upload P.O. Items Excel" runat="server"></asp:Literal>
                                    </div>
                                </div>

                                <!-- Excel Upload UI Starts-->
                                <div id="docDiv" runat="server" visible="true" class="row mt-4">

                                    <!-- Excel Sheet Name -->
                                    <div class="col-md-4 align-self-end">
                                        <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                            <asp:Literal ID="Literal1" Text="" runat="server">Sheet Name<em style="color: red">*</em></asp:Literal>
                                            <div>
                                                <asp:RequiredFieldValidator ID="rr3" ControlToValidate="SheetName" ValidationGroup="DocumentUpload" CssClass="invalid-feedback" InitialValue="" runat="server" ErrorMessage="enter sheet name" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <asp:TextBox runat="server" ID="SheetName" type="text" step="any" min="-Infinity" max="Infinity" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                                    </div>

                                    <!-- Excel Upload -->
                                    <div class="col-md-4 align-self-end">
                                        <h6 class="fw-lighter fs-6 text-secondary-subtle">User can upload excel with format .xlsx or .xls</h6>
                                        <div>
                                            <asp:RequiredFieldValidator ID="rr4" ControlToValidate="fileExcel" ValidationGroup="DocumentUpload" CssClass="invalid-feedback" InitialValue="" runat="server" ErrorMessage="choose excel file to upload" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="input-group has-validation">
                                            <asp:FileUpload ID="fileExcel" runat="server" CssClass="form-control" aria-describedby="inputGroupPrepend" />
                                            <asp:Button ID="btnDocUpload" Text="Upload" OnClick="btnDocUpload_Click" ValidationGroup="DocumentUpload" runat="server" AutoPost="true" CssClass="btn btn-custom btn-outline-secondary" />
                                        </div>
                                    </div>

                                    <!-- Download Reference Excel  -->
                                    <div class="col-md-4 align-self-end">
                                        <div class="mb-1 text-body-tertiary fw-semibold fs-6 mb-2">
                                            <asp:Literal ID="Literal8" Text="" runat="server">Download Excel File Format</asp:Literal>
                                        </div>
                                        <div class="input-group has-validation">
                                            <h6 class="fw-semibold fs-6 text-dark-subtle align-self-end">
                                                <asp:LinkButton ID="btnSample" OnClick="btnSample_Click" runat="server" CssClass="btn btn-success shadow">Purchase_Order_BOM.xlsx</asp:LinkButton>
                                            </h6>
                                        </div>
                                    </div>

                                    <!-- Download Reference Excel  -->
                                    <div class="col-md-4 align-self-end">
                                        <h6 class="fw-semibold fs-6 text-dark-subtle"></h6>
                                    </div>

                                </div>
                                <!-- Excel Upload UI Ends -->

                            </div>
                            <!-- Excel Upload UI Ends -->


                            <!-- Item GridView Starts -->
                            <div id="itemDiv" runat="server" visible="false" class="mt-3">

                                <hr class="border-bottom border-secondary-subtle mt-5 mb-5" />

                                <asp:GridView ShowHeaderWhenEmpty="true" ID="itemGrid" runat="server" AutoGenerateColumns="false" OnRowDeleting="Grid_RowDeleting"
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
                                            <ItemStyle CssClass="col-md-1 align-middle" />
                                            <ItemStyle Font-Size="15px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ItemCategoryText" HeaderText="Item Category" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />
                                        <asp:BoundField DataField="ItemSubCategoryText" HeaderText="Item Sub Category" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />
                                        <asp:BoundField DataField="ItemNameText" HeaderText="Item Name" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />
                                        <asp:BoundField DataField="PoQuantity" HeaderText="P.O. Qty" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />
                                        <asp:BoundField DataField="ItemUOMText" HeaderText="UOM" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />
                                        <asp:BoundField DataField="ItemRate" HeaderText="Rate" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />
                                        <asp:BoundField DataField="ItemSubTotal" HeaderText="Item Sub Total" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />
                                        <asp:BoundField DataField="ItemDescription" HeaderText="Description" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />

                                        <asp:TemplateField HeaderText="Action" ItemStyle-CssClass="align-middle">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" CommandArgument='<%# Container.DataItemIndex %>'>
                                                    <asp:Image runat="server" ImageUrl="../assests/img/modern-cross-fill.svg" AlternateText="Edit" style="width: 28px; height: 28px;"/>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>

                                <!-- Baisc Amount -->
                                <div class="row px-0">
                                    <div class="col-md-8">
                                    </div>
                                    <div class="col-md-4 align-self-end text-end">
                                        <asp:Literal ID="Literal15" Text="" runat="server">Total P.O. Basic Amount</asp:Literal>
                                        <div class="input-group">
                                            <span class="input-group-text fs-5 fw-semibold">₹</span>
                                            <asp:TextBox runat="server" ID="BasicAmount" CssClass="form-control fw-lighter border border-2" ReadOnly="true" placeholder="Total Item Amount"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <!-- Item GridView Ends -->


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

                                        <asp:TemplateField HeaderText="Select" ItemStyle-CssClass="align-middle">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckStatus" OnCheckedChanged="CheckStatus_CheckedChanged" AutoPostBack="true" Checked='<%# Eval("CheckStatus") %>' runat="server" CssClass="" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
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

                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnDocUpload" />
                            <asp:PostBackTrigger ControlID="btnSample" />
                        </Triggers>
                    </asp:UpdatePanel>

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

            // js function to do product of item qty & unit rate
            function calculateSubTotal() {
                var poQuantity = parseInt(document.getElementById('<%= PoQuantity.ClientID %>').value);
                var itemRate = parseInt(document.getElementById('<%= ItemRate.ClientID %>').value);
                var itemSubTotal = poQuantity * itemRate;
                document.getElementById('<%= ItemSubTotalTxt.ClientID %>').value = itemSubTotal; //itemSubTotal.toFixed(2); to roundup to 2 decimalsmals
            }

            // function to toggle select / de-select all checkboxes
            function toggleCheckBoxes(chkAll) {
                var gridView = document.getElementById('<%= itemGrid.ClientID %>');
                var checkBoxes = gridView.getElementsByTagName('input');

                for (var i = 0; i < checkBoxes.length; i++) {
                    if (checkBoxes[i].type == 'checkbox') {
                        checkBoxes[i].checked = chkAll.checked;
                    }
                }
            }

        </script>



    </form>
</body>
</html>
