<%@ Page Language="C#" UnobtrusiveValidationMode="None" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="AAVerification.aspx.cs" Inherits="AA_Approval_AAVerification" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AA Verification</title>

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

    <script src="aaverification.js"></script>
    <link rel="stylesheet" href="aaverification.css" />


</head>
<body>
    <form id="form1" runat="server">


        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>


        <!-- top Searching div Starts -->
        <div id="divTopSearch" runat="server" visible="true">
            <div class="col-md-12 mx-auto">

                <!-- Heading Start -->
                <div class="justify-content-end px-0 text-body-secondary mb-0 mt-4">
                    <div class="col-md-6 px-0">
                        <div class="fw-semibold fs-3 text-dark">
                            <asp:Literal ID="Literal14" Text="Administrative Approval Verification" runat="server"></asp:Literal>
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
                                            <asp:Literal ID="Literal15" Text="" runat="server">Administrative Approval No.</asp:Literal>
                                        </div>
                                        <asp:DropDownList ID="ddScAANo" runat="server" AutoPostBack="false" class="form-control is-invalid" CssClass=""></asp:DropDownList>
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
                                    <asp:BoundField DataField="AANumber" HeaderText="A.A. No" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" />
                                    <asp:BoundField DataField="AADate" HeaderText="A.A. Date" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField DataField="AATitle" HeaderText="A.A. Title" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" />
                                    <asp:BoundField DataField="SanctionAmount" HeaderText="Sanctioned Amount (₹)" ItemStyle-CssClass="col-xs-2 align-middle text-start fw-light" />
                                    <asp:BoundField DataField="SanctionDate" HeaderText="Sanctioned Date" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField DataField="BudgetName" HeaderText="Source Of Budget" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" />
                                    <asp:BoundField DataField="AAItemCount" HeaderText="A.A. Items Count" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" />
                                    <asp:BoundField DataField="Bureau" HeaderText="Bureau" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" />
                                    <asp:BoundField DataField="verifyStatus" HeaderText="Verification Status" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" />

                                    <asp:TemplateField HeaderText="Update" ItemStyle-CssClass="align-middle">
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
                <asp:Literal Text="A.A. Verification" runat="server"></asp:Literal>
            </div>

            <!-- AA Header UI Starts -->
            <div class="card col-md-12 mx-auto mt-1 py-2 shadow rounded-3">
                <div class="card-body">


                    <!-- Heading 2 -->
                    <div class="fw-normal fs-5 fw-medium text-body-secondary border-bottom pb-2 mb-4">
                        <asp:Literal Text="A.A. Details" runat="server"></asp:Literal>
                    </div>

                    <!-- 1st row Starts -->
                    <div class="row mb-2">

                        <!-- AA No & Title -->
                        <div class="col-md-6 align-self-end">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal2" Text="" runat="server">A.A. Number & Title</asp:Literal>
                                <div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="AdminApproveNo" ValidationGroup="finalSubmit" CssClass="invalid-feedback" InitialValue="0" runat="server" ErrorMessage="select administrative approval number and title" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <asp:DropDownList ID="AdminApproveNo" AutoPostBack="false" runat="server" class="form-control is-invalid" CssClass=""></asp:DropDownList>
                        </div>

                        <!-- AA Date -->
                        <div class="col-md-6 align-self-end">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal12" Text="" runat="server">A.A. Date</asp:Literal>
                            </div>
                            <asp:TextBox runat="server" ID="AADate" type="date" ReadOnly="true" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                        </div>

                    </div>
                    <!-- 1st row Ends -->

                    <!-- 2nd row Starts -->
                    <div class="row mb-2">

                        <!-- Sanction Amount -->
                        <div class="col-md-6 align-self-end">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal5" Text="" runat="server">Sanction Amount</asp:Literal>
                            </div>
                            <asp:TextBox runat="server" ID="SanctionAmount" type="text" ReadOnly="true" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                        </div>

                        <!-- Sanction Date -->
                        <div class="col-md-6 align-self-end">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal4" Text="" runat="server">Sanction Date</asp:Literal>
                            </div>
                            <asp:TextBox runat="server" ID="SanctionDate" type="date" ReadOnly="true" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                        </div>

                    </div>
                    <!-- 2nd row Ends -->

                    <!-- 3rd row Starts -->
                    <div class="row mb-2">

                        <!-- Source Of Budget -->
                        <div class="col-md-6 align-self-end">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal3" Text="" runat="server">Source Of Budget</asp:Literal>
                            </div>
                            <asp:DropDownList ID="SourceOfBudget" AutoPostBack="false" runat="server" class="form-control is-invalid" CssClass=""></asp:DropDownList>
                        </div>

                        <!-- Bureau -->
                        <div class="col-md-6 align-self-end">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal6" Text="" runat="server">Bureau</asp:Literal>
                            </div>
                            <asp:DropDownList ID="Bureau" AutoPostBack="false" runat="server" class="form-control is-invalid" CssClass=""></asp:DropDownList>
                        </div>

                    </div>
                    <!-- 3rd row Ends -->


                </div>
            </div>
            <!-- Header UI Ends -->




            <!-- Details UI Starts -->
            <div class="card col-md-12 mx-auto mt-5 mb-5 rounded-3 shadow">
                <div class="card-body">

                    <!-- Heading 2 -->
                    <div class="fw-normal fs-5 fw-medium text-body-secondary border-bottom px-0 pb-2 mb-4">
                        <asp:Literal Text="A.A. Added Item" runat="server"></asp:Literal>
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
                                <asp:BoundField DataField="ItemCode" HeaderText="Item Code" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />
                                <asp:BoundField DataField="ItemQuantity" HeaderText="Item Qty" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />
                                <asp:BoundField DataField="ItemUOMText" HeaderText="UOM" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />
                                <asp:BoundField DataField="ItemRate" HeaderText="Rate" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />
                                <asp:BoundField DataField="ItemSubTotal" HeaderText="Item Sub Total" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />
                                <asp:BoundField DataField="ItemDescription" HeaderText="Description" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <!-- Item GridView Ends -->


                    <!-- DD Aproval Status -->
                    <div class="row mb-2">

                        <!-- AA No & Title -->
                        <div class="col-md-6 align-self-start">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal1" Text="" runat="server">Verification Status <em style="color: red">*</em></asp:Literal>
                                <div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="VerificationStatus" ValidationGroup="finalSubmit" CssClass="invalid-feedback" InitialValue="0" runat="server" ErrorMessage="select verification status" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <asp:DropDownList ID="VerificationStatus" AutoPostBack="false" runat="server" class="form-control is-invalid" CssClass=""></asp:DropDownList>
                        </div>

                        <!-- Verification Remark -->
                        <div class="col-md-6 px-0 align-self-start">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal8" Text="" runat="server">Verification Remark</asp:Literal>
                            </div>
                            <textarea id="VerificationRemark" rows="2" cols="50" class="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1" runat="server"></textarea>
                        </div>

                    </div>
                    <!-- 1st row Ends -->


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
        <!-- Update Div Ends -->


    </form>
</body>
</html>
