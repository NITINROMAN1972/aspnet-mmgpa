<%@ Page Language="C#" UnobtrusiveValidationMode="None" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="AdministrativeApprovalNew.aspx.cs" Inherits="Administrative_Approval_AdministrativeApprovalNew" %>

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
    <form id="form1" runat="server" class="bg-light">

        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>

        <div id="loginDiv" runat="server" visible="true">

            <!-- Heading -->
            <div class="col-md-12 mx-auto fw-normal fs-3 fw-medium ps-0 pb-2 text-dark-emphasis mt-1 mb-1">
                <asp:Literal Text="Administrative Approval" runat="server"></asp:Literal>
            </div>

            <!-- Header UI Starts -->
            <div class="card col-md-12 mx-auto mt-1 py-2 shadow rounded-3">
                <div class="card-body">

                    <!-- Heading -->
                    <div class="fw-normal fs-5 fw-medium text-body-secondary border-bottom pb-2 mb-4">
                        <asp:Literal Text="AA Details" runat="server"></asp:Literal>
                    </div>

                    <!-- 1st row Starts -->
                    <div class="row mb-2">

                        <!-- Administrative Approva Title -->
                        <div class="col-md-12 align-self-end">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal7" Text="" runat="server">A.A. Title<em style="color: red">*</em></asp:Literal>
                                <div>
                                    <asp:RequiredFieldValidator ID="rr3" ControlToValidate="AATitle" ValidationGroup="finalSubmit" CssClass="invalid-feedback" InitialValue="" runat="server" ErrorMessage="enter administrative approval date" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <asp:TextBox runat="server" ID="AATitle" type="text" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
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
                                <div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="AAForItemCategory" ValidationGroup="finalSubmit" CssClass="invalid-feedback" InitialValue="" runat="server" ErrorMessage="select item categories for this A.A." SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                </div>
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
                                <asp:Literal Text="Add Administrative Approval Documents" runat="server"></asp:Literal>
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
                                    <asp:RequiredFieldValidator ID="rr6" ControlToValidate="DocType" ValidationGroup="DocumentUpload" CssClass="invalid-feedback" InitialValue="0" runat="server" ErrorMessage="please document type" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
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
            <!-- Header UI Ends -->

        </div>


    </form>
</body>
</html>
