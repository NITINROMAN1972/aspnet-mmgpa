﻿<%@ Page Language="C#" UnobtrusiveValidationMode="None" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="AdministrativeApproval.aspx.cs" Inherits="Administrative_Approval_AdministrativeApproval" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Administrative Approval</title>

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

    <script src="adminapproval.js"></script>
    <link rel="stylesheet" href="adminapproval.css" />


</head>
<body>
    <form id="form1" runat="server">


        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>


        <!-- top Searching div Starts -->
        <div id="divTopSearch" runat="server" visible="true">
            <div class="col-md-12 mx-auto">

                <!-- Heading Start -->
                <div class="justify-content-end d-flex px-0 text-body-secondary mb-0 mt-4">
                    <div class="col-md-6 px-0">
                        <div class="fw-semibold fs-3 text-dark">
                            <asp:Literal ID="Literal14" Text="Administrative Approvals List" runat="server"></asp:Literal>
                        </div>
                    </div>
                    <div class="col-md-6 text-end px-0">
                        <div class="fw-semibold fs-5">
                            <asp:Button ID="btnNewBill" runat="server" Text="New A.A. +" OnClick="btnNewBill_Click" CssClass="btn btn-custom text-white shadow" />
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
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddScAANo" runat="server" AutoPostBack="false" class="form-control is-invalid" CssClass=""></asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
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
                                    <asp:BoundField DataField="Bureau" HeaderText="Bureau" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" />
                                    <asp:BoundField DataField="VerificationStatus" HeaderText="Verification Status" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" />

                                    <asp:TemplateField HeaderText="Update">
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
            <div class="col-md-12 mx-auto fw-normal fs-3 fw-medium ps-0 pb-2 text-body-secondary mb-3">
                <asp:Literal Text="Administrative Approval Update" runat="server"></asp:Literal>
            </div>

            <!-- Update Header UI Starts -->
            <div class="card col-md-12 mx-auto mt-2 py-2 shadow rounded-3">
                <div class="card-body">

                    <!-- Heading -->
                    <div class="fw-normal fs-5 fw-medium text-body-secondary border-bottom pb-2 mb-4">
                        <asp:Literal Text="A.A. Details" runat="server"></asp:Literal>
                    </div>


                    <!-- 1st row Starts -->
                    <div class="row mb-2">

                        <!-- Administrative Approva Title -->
                        <div class="col-md-8 align-self-end">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal7" Text="" runat="server">A.A. Title<em style="color: red">*</em></asp:Literal>
                                <div>
                                    <asp:RequiredFieldValidator ID="rr3" ControlToValidate="AATitle" ValidationGroup="finalSubmit" CssClass="invalid-feedback" InitialValue="" runat="server" ErrorMessage="enter administrative approval date" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <asp:TextBox runat="server" ID="AATitle" type="text" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                        </div>

                        <!-- Bureau -->
                        <div class="col-md-4 align-self-end">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal5" Text="" runat="server">Bureau<em style="color: red">*</em></asp:Literal>
                                <div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="AABureau" ValidationGroup="finalSubmit" CssClass="invalid-feedback" InitialValue="0" runat="server" ErrorMessage="select aa bureau" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <asp:DropDownList ID="AABureau" AutoPostBack="false" runat="server" class="form-control is-invalid" CssClass=""></asp:DropDownList>
                        </div>

                    </div>
                    <!-- 1st row Ends -->

                    <!-- 2nd row Starts -->
                    <div class="row mb-2">

                        <!-- Administrative Approval No -->
                        <div class="col-md-4 align-self-end">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal6" Text="" runat="server">A.A. Number<em style="color: red">*</em></asp:Literal>
                                <div>
                                    <asp:RequiredFieldValidator ID="rr1" ControlToValidate="AANumber" ValidationGroup="finalSubmit" CssClass="invalid-feedback" InitialValue="" runat="server" ErrorMessage="enter administrative approval no." SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <asp:TextBox runat="server" ID="AANumber" type="text" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                        </div>

                        <!-- Administrative Approval Date -->
                        <div class="col-md-4 align-self-end">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal12" Text="" runat="server">A.A. Date<em style="color: red">*</em></asp:Literal>
                                <div>
                                    <asp:RequiredFieldValidator ID="rr2" ControlToValidate="AADate" ValidationGroup="finalSubmit" CssClass="invalid-feedback" InitialValue="" runat="server" ErrorMessage="select administrative approval date" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <asp:TextBox runat="server" ID="AADate" type="date" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                        </div>

                        <!-- AA For (Item Category) -->
                        <div class="col-md-4 align-self-end">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal4" Text="" runat="server">A.A. For: <em style="color: red">*</em></asp:Literal>
                            </div>
                            <asp:ListBox ID="AAForItemCategory" SelectionMode="Multiple" AutoPostBack="false" runat="server" ClientIDMode="Static" CssClass="form-control rounded-1 border-1 border-secondar-subtle"></asp:ListBox>
                        </div>

                    </div>
                    <!-- 2nd row Ends -->

                    <!-- 3rd row Starts -->
                    <div class="row mb-2">

                        <!-- Sanction Amount -->
                        <div class="col-md-4 align-self-end">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal1" Text="" runat="server">Sanction Amount<em style="color: red">*</em></asp:Literal>
                                <div>
                                    <asp:RequiredFieldValidator ID="rr4" ControlToValidate="SanctionAmount" ValidationGroup="finalSubmit" CssClass="invalid-feedback" InitialValue="" runat="server" ErrorMessage="enter sanction amount" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <asp:TextBox runat="server" ID="SanctionAmount" type="number" step="any" min="-Infinity" max="Infinity" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                        </div>

                        <!-- Sanction Date -->
                        <div class="col-md-4 align-self-end">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal2" Text="" runat="server">Sanction Date<em style="color: red">*</em></asp:Literal>
                                <div>
                                    <asp:RequiredFieldValidator ID="rr5" ControlToValidate="SanctionDate" ValidationGroup="finalSubmit" CssClass="invalid-feedback" InitialValue="" runat="server" ErrorMessage="select sanction date" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <asp:TextBox runat="server" ID="SanctionDate" type="date" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                        </div>

                        <!-- Source Of Budget -->
                        <div class="col-md-4 align-self-end">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal3" Text="" runat="server">Source Of Budget<em style="color: red">*</em></asp:Literal>
                                <div>
                                    <asp:RequiredFieldValidator ID="rr9" ControlToValidate="SourceOfBudget" ValidationGroup="finalSubmit" CssClass="invalid-feedback" InitialValue="0" runat="server" ErrorMessage="select source of budget" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <asp:DropDownList ID="SourceOfBudget" AutoPostBack="false" runat="server" class="form-control is-invalid" CssClass=""></asp:DropDownList>
                        </div>


                    </div>
                    <!-- 3rd row Ends -->



                    <!-- Documents Upload UI Starts -->
                    <div class="mb-3 mt-5 py-3">

                        <!-- Heading Document -->
                        <div class="border-top border-bottom border-secondary-subtle py-2 mt-4">
                            <div class="fw-normal fs-5 fw-medium text-body-secondary">
                                <asp:Literal Text="Administrative Approval Documents" runat="server"></asp:Literal>
                            </div>
                        </div>

                        <!-- Documents Upload -->
                        <div id="docDiv" runat="server" visible="true" class="row mt-4">

                            <!-- DD Document Name -->
                            <div id="docTypeDiv" runat="server" visible="true" class="col-md-3 align-self-start">
                                <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                    <asp:Literal ID="Literal16" Text="" runat="server">Document Type</asp:Literal>
                                </div>
                                <asp:DropDownList ID="DocType" AutoPostBack="false" runat="server" class="form-control is-invalid" CssClass=""></asp:DropDownList>
                                <div>
                                    <asp:RequiredFieldValidator ID="rr6" ControlToValidate="DocType" ValidationGroup="DocumentUpload" CssClass="invalid-feedback" InitialValue="0" runat="server" ErrorMessage="please document type" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <!-- Document Name -->
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
                            <div class="col-md-8 align-self-start">
                                <h6 class="fw-lighter fs-6 text-secondary-subtle">User can add administrative approval Documents !</h6>
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
            <!-- Update Header UI Ends -->


        </div>
        <!-- Update Div Ends -->





    </form>
</body>
</html>
