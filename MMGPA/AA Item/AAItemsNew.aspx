﻿<%@ Page Language="C#" UnobtrusiveValidationMode="None" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="AAItemsNew.aspx.cs" Inherits="AA_Item_AAItems" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AA Items New</title>

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

    <script src="aaitems.js"></script>
    <link rel="stylesheet" href="aaitems.css" />


</head>
<body>
    <form id="form1" runat="server" class="bg-light">


        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>

        <div id="loginDiv" runat="server" visible="true">

            <!-- Heading -->
            <div class="col-md-12 mx-auto fw-normal fs-3 fw-medium ps-0 pb-2 text-dark-emphasis mt-1 mb-1">
                <asp:Literal Text="A.A. Items Upload" runat="server"></asp:Literal>
            </div>

            <!-- Header UI Starts -->
            <div class="card col-md-12 mx-auto mt-1 py-2 shadow-sm rounded-3">
                <div class="card-body">


                    <!-- Heading 1 -->
                    <div class="fw-normal fs-5 fw-medium text-body-secondary border-bottom pb-2 mb-4">
                        <asp:Literal Text="A.A. Details" runat="server"></asp:Literal>
                    </div>

                    <!-- 1st row Starts -->
                    <div class="row mb-2">

                        <!-- AA No & Title -->
                        <div class="col-md-6 align-self-end">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal3" Text="" runat="server">A.A. Number & Title <em style="color: red">*</em></asp:Literal>
                                <div>
                                    <asp:RequiredFieldValidator ID="rr1" ControlToValidate="AdminApproveNo" ValidationGroup="finalSubmit" CssClass="invalid-feedback" InitialValue="0" runat="server" ErrorMessage="select administrative approval number and title" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <asp:DropDownList ID="AdminApproveNo" AutoPostBack="false" runat="server" class="form-control is-invalid" CssClass=""></asp:DropDownList>
                        </div>

                    </div>
                    <!-- 1st row Ends -->


                </div>
            </div>
            <!-- Header UI Ends -->

            <!-- Details UI Starts -->
            <div class="card col-md-12 mx-auto mt-5 rounded-3 shadow-sm">
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
                                        <asp:RadioButton ID="manualRadio" runat="server" Text="" OnCheckedChanged="manualRadio_CheckedChanged" AutoPostBack="true" Checked="true" GroupName="entryMode" />
                                        <literal>Manual Item Entry</literal>
                                    </div>
                                    <!-- 1st Row Ends -->

                                    <!-- 2nd Row Starts -->
                                    <div class="col-md-12 align-content-between text-body-tertiary fw-semibold fs-6 ps-4 pe-3">
                                        <asp:RadioButton ID="excelRadio" runat="server" Text="" OnCheckedChanged="excelRadio_CheckedChanged" AutoPostBack="true" GroupName="entryMode" />
                                        <literal>Through Excel</literal>
                                    </div>
                                    <!-- 2nd Row Ends -->

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
                                        </div>
                                        <asp:DropDownList ID="ItemName" runat="server" AutoPostBack="false" class="form-control is-invalid" CssClass=""></asp:DropDownList>
                                    </div>

                                    <!-- Item Code -->
                                    <div class="col-md-3 align-self-end">
                                        <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                            <asp:Literal ID="Literal5" Text="" runat="server">Item Code</asp:Literal>
                                            <div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ItemCode" ValidationGroup="ItemSave" CssClass="invalid-feedback" InitialValue="" runat="server" ErrorMessage="enter item code" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <asp:TextBox ID="ItemCode" runat="server" type="text" steps="0.01" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                                    </div>

                                </div>
                                <!-- 1st Row Ends -->


                                <!-- 2nd Row Starts -->
                                <div class="row mb-2">

                                    <!-- Item Quantity -->
                                    <div class="col-md-3 align-self-end">
                                        <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                            <asp:Literal ID="Literal20" Text="" runat="server">Item Quantity</asp:Literal>
                                            <div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="ItemQuantity" ValidationGroup="ItemSave" CssClass="invalid-feedback" InitialValue="" runat="server" ErrorMessage="enter item quantity" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <asp:TextBox ID="ItemQuantity" runat="server" type="number" steps="0.01" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
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
                                        <asp:TextBox ID="ItemRate" runat="server" type="number" steps="0.01" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                                    </div>

                                </div>
                                <!-- 2nd Row Ends -->


                                <!-- 3rd Row Starts -->
                                <div class="row mb-2">

                                    <!-- Item Description -->
                                    <div class="col-md-10 align-self-end mb-3">
                                        <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                            <asp:Literal ID="Literal7" Text="" runat="server">Item Description</asp:Literal>
                                            <div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ItemDescription" ValidationGroup="ItemSave" CssClass="invalid-feedback" InitialValue="" runat="server" ErrorMessage="enter item description" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                            </div>
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
                                        <asp:Literal Text="Upload A.A. Items Excel" runat="server"></asp:Literal>
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
                                                <asp:LinkButton ID="btnSample" OnClick="btnSample_Click" runat="server" CssClass="btn btn-success shadow">AAItems.xlsx</asp:LinkButton>
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
                                        <asp:BoundField DataField="ItemCode" HeaderText="Item Code" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />
                                        <asp:BoundField DataField="ItemQuantity" HeaderText="Item Qty" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />
                                        <asp:BoundField DataField="ItemUOMText" HeaderText="UOM" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />
                                        <asp:BoundField DataField="ItemRate" HeaderText="Rate/Unit" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />

                                        <asp:TemplateField HeaderText="Item Sub Total" ItemStyle-Font-Size="15px" ItemStyle-CssClass="col-md-1 align-middle">
                                            <ItemTemplate>
                                                <asp:TextBox ID="ItemSubTotal" Text='<%# Bind("ItemSubTotal") %>' type="number" step="0.01" title="can edit the item sub total" runat="server" Enabled="true" CssClass="col-md-12 fw-light border border-secondary-subtle shadow-sm rounded-1 py-1 px-2"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="ItemDescription" HeaderText="Description" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />

                                        <asp:TemplateField HeaderText="Add" ItemStyle-CssClass="align-middle">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkSelectAll" runat="server" Text="Select All" onclick="toggleCheckBoxes(this);" CssClass="" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckStatus" runat="server" AutoPostBack="false" Checked='<%# Eval("CheckStatus") %>' CssClass="" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>

                                <%--<!-- Total Item Amount -->
                    <div class="row px-0">
                        <div class="col-md-8">
                        </div>
                        <div class="col-md-4 align-self-end text-end">
                            <asp:Literal ID="Literal6" Text="" runat="server">Total Item Basic Amount</asp:Literal>
                            <div class="input-group">
                                <span class="input-group-text fs-5 fw-semibold">₹</span>
                                <asp:TextBox runat="server" ID="TotalItemAmount" CssClass="form-control fw-lighter border border-2" ReadOnly="true" placeholder="Total Service Amount"></asp:TextBox>
                            </div>
                        </div>
                    </div>--%>
                            </div>
                            <!-- Item GridView Ends -->

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
