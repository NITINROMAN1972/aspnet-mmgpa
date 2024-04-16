<%@ Page Language="C#" UnobtrusiveValidationMode="None" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="TenderEstimateVerification.aspx.cs" Inherits="Tender_Verification_TenderVerification" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tender Verification</title>

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

    <script src="tenderverification.js"></script>
    <link rel="stylesheet" href="tenderverification.css" />


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
                            <asp:Literal ID="Literal14" Text="Tender Estimate Verification" runat="server"></asp:Literal>
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
                                            <asp:Literal ID="Literal15" Text="" runat="server">Estimate Number</asp:Literal>
                                        </div>
                                        <asp:DropDownList ID="ddScEstimateNo" runat="server" AutoPostBack="false" class="form-control is-invalid" CssClass=""></asp:DropDownList>
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
                                    <asp:BoundField DataField="EstimateNo" HeaderText="Estimation No." ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" />
                                    <asp:BoundField DataField="EstimateDate" HeaderText="Estimation Date" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField DataField="BasicAmount" HeaderText="Total Amount" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" />
                                    <asp:BoundField DataField="ItemCount" HeaderText="Item Count" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" />
                                    <asp:BoundField DataField="EstimateVerified" HeaderText="Verification Status" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" />

                                    <asp:TemplateField HeaderText="Update" ItemStyle-CssClass="align-middle">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="btnedit" CommandArgument='<%# Eval("EstimateNo") %>' CommandName="lnkView" ToolTip="Edit" CssClass="shadow-sm">
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
                <asp:Literal Text="Estimate Verification" runat="server"></asp:Literal>
            </div>

            <!-- Header UI Starts -->
            <div class="card col-md-12 mx-auto mt-1 py-2 shadow rounded-3">
                <div class="card-body">


                    <!-- Heading 1 -->
                    <div class="fw-normal fs-5 fw-medium text-body-secondary border-bottom pb-2 mb-4">
                        <asp:Literal Text="Estimation Details" runat="server"></asp:Literal>
                    </div>

                    <!-- 1st row Starts -->
                    <div class="row mb-2">

                        <!-- Estimate No -->
                        <div class="col-md-6 align-self-end">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal9" Text="" runat="server">Estimate Number<em style="color: red">*</em></asp:Literal>
                            </div>
                            <asp:TextBox runat="server" ID="EstimateNo" type="text" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                        </div>

                        <!-- Estimate Date -->
                        <div class="col-md-6 align-self-end">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal12" Text="" runat="server">Estimate Date<em style="color: red">*</em></asp:Literal>
                                <div>
                                    <asp:RequiredFieldValidator ID="rr2" ControlToValidate="EstimateDate" ValidationGroup="finalSubmit" CssClass="invalid-feedback" InitialValue="" runat="server" ErrorMessage="select estimate date" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <asp:TextBox runat="server" ID="EstimateDate" type="date" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                        </div>

                    </div>
                    <!-- 1st row Ends -->


                </div>
            </div>
            <!-- Header UI Ends -->




            <!-- Details UI Starts -->
            <div class="card col-md-12 mx-auto mt-5 mb-5 rounded-3 shadow">
                <div class="card-body">

                    <!-- Heading 2 -->
                    <div class="fw-normal fs-5 fw-medium text-body-secondary border-bottom px-0 pb-2 mb-4">
                        <asp:Literal Text="Estimate Items" runat="server"></asp:Literal>
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
                                <asp:BoundField DataField="TenderQuantity" HeaderText="Item Qty" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />
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
                        <div class="col-md-6 align-self-end">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal1" Text="" runat="server">Verification Status <em style="color: red">*</em></asp:Literal>
                                <div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="VerificationStatus" ValidationGroup="finalSubmit" CssClass="invalid-feedback" InitialValue="0" runat="server" ErrorMessage="select verification status" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <asp:DropDownList ID="VerificationStatus" AutoPostBack="false" runat="server" class="form-control is-invalid" CssClass=""></asp:DropDownList>
                        </div>

                    </div>
                    <!-- 1st row Ends -->


                    <!-- Verification Remark -->
                    <div class="col-md-12 mt-3 px-0 align-self-start">
                        <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                            <asp:Literal ID="Literal8" Text="" runat="server">Verification Remark</asp:Literal>
                            <div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="VerificationRemark" ValidationGroup="finalSubmit" CssClass="invalid-feedback" InitialValue="0" runat="server" ErrorMessage="kindlly enter verification remark" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <textarea id="VerificationRemark" rows="3" cols="50" class="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1" runat="server"></textarea>
                    </div>



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
