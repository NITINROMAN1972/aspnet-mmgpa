using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Numerics;
using System.Web.UI.HtmlControls;

public partial class Bills_And_Dispatch_VendorInvoiceNew : System.Web.UI.Page
{
    string connectionString = ConfigurationManager.ConnectionStrings["Ginie"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Project: MMGPA
        // Code: 757

        //Session["UserId"] = "10223"; // client - milind

        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");

                PoNo_DropDown();
                DocType_DropDown();

                // invoice current date
                InvoiceDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }
        else
        {
            getSweetHTML("Not Signed-In!", "Kindly <strong>Sign-In</strong> To Access The Porject <br/> By Clicking On <strong><i>Login</i></strong> Button Above");
            loginDiv.Visible = false;
        }
    }




    //=========================={ Paging & Alert }==========================
    private void alert(string mssg)
    {
        // alert pop - up with only message
        string message = mssg;
        string script = $"alert('{message}');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "messageScript", script, true);
    }




    //=========================={ Sweet Alert JS }==========================
    private void getSweetAlertWarningMandatory(string titles, string mssg)
    {
        string title = titles;
        string message = mssg;
        string icon = "warning";
        string confirmButtonText = "OK";
        string allowOutsideClick = "false"; // Prevent closing on outside click

        string sweetAlertScript =
        $@"<script>
            Swal.fire({{ 
                title: '{title}', 
                text: '{message}', 
                icon: '{icon}', 
                confirmButtonText: '{confirmButtonText}', 
                allowOutsideClick: {allowOutsideClick}
            }});
        </script>";
        ClientScript.RegisterStartupScript(this.GetType(), "sweetAlert", sweetAlertScript, false);
    }

    private void getSweetAlertSuccessRedirectMandatory(string titles, string mssg, string redirectUrl)
    {
        string title = titles;
        string message = mssg;
        string icon = "success";
        string confirmButtonText = "OK";
        string allowOutsideClick = "false"; // Prevent closing on outside click

        string sweetAlertScript =
        $@"<script>
            Swal.fire({{ 
                title: '{title}', 
                text: '{message}', 
                icon: '{icon}', 
                confirmButtonText: '{confirmButtonText}', 
                allowOutsideClick: {allowOutsideClick}
            }}).then((result) => {{
                if (result.isConfirmed) {{
                    window.location.href = '{redirectUrl}';
                }}
            }});
        </script>";
        ClientScript.RegisterStartupScript(this.GetType(), "sweetAlert", sweetAlertScript, false);
    }

    // sweet alert - error only block
    private void getSweetAlertErrorMandatory(string titles, string mssg)
    {
        string title = titles;
        string message = mssg;
        string icon = "error";
        string confirmButtonText = "OK";
        string allowOutsideClick = "false"; // Prevent closing on outside click

        string sweetAlertScript =
        $@"<script>
            Swal.fire({{ 
                title: '{title}', 
                text: '{message}', 
                icon: '{icon}', 
                confirmButtonText: '{confirmButtonText}', 
                allowOutsideClick: {allowOutsideClick}
            }});
        </script>";
        ClientScript.RegisterStartupScript(this.GetType(), "sweetAlert", sweetAlertScript, false);
    }

    // sweet alert - info
    private void getSweetAlertInfo(string titles, string mssg)
    {
        string title = titles;
        string message = mssg;
        string icon = "info";
        string confirmButtonText = "OK";
        string allowOutsideClick = "false"; // Prevent closing on outside click

        string sweetAlertScript =
        $@"<script>
            Swal.fire({{ 
                title: '{title}', 
                text: '{message}', 
                icon: '{icon}', 
                confirmButtonText: '{confirmButtonText}', 
                allowOutsideClick: {allowOutsideClick}
            }});
        </script>";
        ClientScript.RegisterStartupScript(this.GetType(), "sweetAlert", sweetAlertScript, false);
    }

    //sweet alert - html (no redirect)
    private void getSweetHTML(string titles, string mssg)
    {
        string title = titles;
        string message = mssg;
        string icon = "info";
        string confirmButtonText = "OK";
        string allowOutsideClick = "false"; // Prevent closing on outside click

        // Create a placeholder textarea for user input
        string sweetAlertScript = $@"
            <script>
                Swal.fire({{
                    title: '{title}',
                    html: '{message}',
                    icon: '{icon}',
                    confirmButtonText: '{confirmButtonText}',
                    allowOutsideClick: {allowOutsideClick}
                }})
            </script>";

        // Register the script
        ClientScript.RegisterStartupScript(this.GetType(), "sweetAlertWithTextarea", sweetAlertScript, false);
    }

    //sweet alert - html redirect
    private void getSweetHTMLRedirect(string titles, string mssg, string redirectUrl)
    {
        string title = titles;
        string message = mssg;
        string icon = "info";
        string confirmButtonText = "OK";
        string allowOutsideClick = "false"; // Prevent closing on outside click

        // Create a placeholder textarea for user input
        string sweetAlertScript = $@"
            <script>
                Swal.fire({{
                    title: '{title}',
                    html: '{message}',
                    icon: '{icon}',
                    confirmButtonText: '{confirmButtonText}',
                    allowOutsideClick: {allowOutsideClick}
                }}).then((result) => {{
                    if (result.isConfirmed) {{
                        window.location.href = '{redirectUrl}';
                    }}
                }});
            </script>";

        // Register the script
        ClientScript.RegisterStartupScript(this.GetType(), "sweetAlertWithTextarea", sweetAlertScript, false);
    }

    //sweet alert - html warning (no redirect)
    private void getSweetHTMLWzrning(string titles, string mssg)
    {
        string title = titles;
        string message = mssg;
        string icon = "warning";
        string confirmButtonText = "OK";
        string allowOutsideClick = "false"; // Prevent closing on outside click

        // Create a placeholder textarea for user input
        string sweetAlertScript = $@"
            <script>
                Swal.fire({{
                    title: '{title}',
                    html: '{message}',
                    icon: '{icon}',
                    confirmButtonText: '{confirmButtonText}',
                    allowOutsideClick: {allowOutsideClick}
                }})
            </script>";

        // Register the script
        ClientScript.RegisterStartupScript(this.GetType(), "sweetAlertWithTextarea", sweetAlertScript, false);
    }





    //=========================={ Binding Dropdowns }==========================
    private void PoNo_DropDown()
    {
        string userID = Session["UserId"].ToString();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = $@"select Distinct  pd.* 
                            from PoDetails757 as pd 
                            left join PoBom757 as pb on pb.PORefNo = pd.RefID";
            SqlCommand cmd = new SqlCommand(sql, con);
            //cmd.Parameters.AddWithValue("@SaveBy", userID);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            PoNo.DataSource = dt;
            PoNo.DataTextField = "PoNo";
            PoNo.DataValueField = "RefID";
            PoNo.DataBind();
            PoNo.Items.Insert(0, new ListItem("------Select P.O. No.------", "0"));
        }
    }

    private void DocType_DropDown()
    {
        string userID = Session["UserId"].ToString();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = $@"select RefNo, DocumentName, DocumentCode, DocumentType from DocumentType757 Where DocumentType = @DocumentType";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@DocumentType", "Invoice");
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            DocType.DataSource = dt;
            DocType.DataTextField = "DocumentName";
            DocType.DataValueField = "RefNo";
            DocType.DataBind();
            DocType.Items.Insert(0, new ListItem(" --  Select Document Type  -- ", "0"));
        }
    }



    //=========================={ Drop Down Event }==========================
    protected void PoNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        string poRefID = PoNo.SelectedValue;

        if (PoNo.SelectedValue != "0")
        {
            itemGrid.Visible = true;
            itemDiv.Visible = true;

            // auto-fill p.o. details and invoice details
            AutoFillHeader(poRefID);

            // auto-fill p.o. bom items 
            AutoFillPOItems(poRefID);

            // auto-fill taxs / account head
            //FillTaxHead();
        }
        else
        {
            DetailsDiv.Visible = false;
            itemGrid.Visible = false;
            itemDiv.Visible = false;
            GridTax.Visible = false;

            PoDate.Text = string.Empty;
            PoAmount.Text = string.Empty;
            VendorName.Text = string.Empty;

            InvoiceNo.Text = string.Empty;
            InvoiceDate.Text = string.Empty;

            itemGrid.DataSource = null;
            itemGrid.DataBind();

            ViewState["ItemDetails_VS"] = null;
            Session["ItemDetails"] = null;

            GridTax.DataSource = null;
            GridTax.DataBind();

            Session["AccountHeadDT"] = null;

            divTaxHead.Visible = false;

            BasicPOAmount.Text = string.Empty;
            txtTotalDeduct.Text = string.Empty;
            txtTotalAdd.Text = string.Empty;
            txtNetAmnt.Text = string.Empty;
        }
    }

    private void AutoFillHeader(string poRefID)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = $@"select pd.*, ven.VendorName 
                            from PoDetails757 as pd
                            inner join VendorMaster757 as ven on ven.RefID = pd.VendNme 
                            where pd.RefID = @RefID";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@RefID", poRefID);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            if (dt.Rows.Count > 0)
            {
                DateTime podate = DateTime.Parse(dt.Rows[0]["PoDate"].ToString());
                PoDate.Text = podate.ToString("yyyy-MM-dd");

                decimal poAmount = Convert.ToDecimal(dt.Rows[0]["PoAmt"]);
                PoAmount.Text = poAmount.ToString("N");

                VendorName.Text = dt.Rows[0]["VendorName"].ToString();
            }
        }
    }

    private void AutoFillPOItems(string poRefID)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = $@"select pb.*, cat.CategoryName as ItemCategoryText, subcat.SubCategory as ItemSubCategoryText, 
                            item.ItemName as ItemNameText, uom.UnitName as ItemUOMText 
                            from PoBom757 as pb 
                            inner join ItemCategory757 as cat on cat.RefID = pb.ItemCategory 
                            inner join ItemSubCategory757 as subcat on subcat.RefID = pb.ItemSubCategory 
                            inner join UnitOfMeasurement757 as uom on uom.RefID = pb.ItemUOM 
                            inner join ItemMaster757 as item on item.RefID = pb.ItemName 
                            where pb.PORefNo = @PORefNo AND pb.BalanceQty != 0 And DeleteFlag IS NULL";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@PORefNo", poRefID);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            if (dt.Rows.Count > 0)
            {
                if (!dt.Columns.Contains("CheckStatus"))
                {
                    // adding the new column with checkboxes
                    DataColumn CheckStatus = new DataColumn("CheckStatus", typeof(bool));
                    CheckStatus.DefaultValue = false;
                    dt.Columns.Add(CheckStatus);
                }

                // setting bill qty to 0
                foreach (DataRow row in dt.Rows)
                {
                    row["BillQty"] = 0;
                }

                DetailsDiv.Visible = true;

                itemGrid.DataSource = dt;
                itemGrid.DataBind();

                ViewState["ItemDetails_VS"] = dt;
                Session["ItemDetails"] = dt;

                double? totalBillAmount = dt.AsEnumerable().Sum(row => row["ItemSubTotal"] is DBNull ? (double?)null : Convert.ToDouble(row["ItemSubTotal"])) ?? 0.0;
                //BasicPOAmount.Text = totalBillAmount.HasValue ? totalBillAmount.Value.ToString("N2") : "0.00";

                Session["TotalBillAmount"] = totalBillAmount;
            }
        }
    }




    //=========================={ GridView RowDeleting }==========================
    protected void Grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridView gridView = (GridView)sender;

        // item gridview
        //if (gridView.ID == "itemGridxx")
        //{
        //    int rowIndex = e.RowIndex;

        //    DataTable dt = ViewState["ItemDetails_VS"] as DataTable;

        //    if (dt != null && dt.Rows.Count > rowIndex)
        //    {
        //        dt.Rows.RemoveAt(rowIndex);

        //        ViewState["ItemDetails_VS"] = dt;
        //        Session["ItemDetails"] = dt;

        //        itemGrid.DataSource = dt;
        //        itemGrid.DataBind();

        //        // re-calculating total amount n assigning back to textbox
        //        double? totalBillAmount = dt.AsEnumerable().Sum(row => row["Amount"] is DBNull ? (double?)null : Convert.ToDouble(row["Amount"])) ?? 0.0;
        //        txtBillAmount.Text = totalBillAmount.HasValue ? totalBillAmount.Value.ToString("N2") : "0.00";

        //        re - calculating taxes
        //        FillTaxHead();
        //    }
        //}

        // document gridview delete
        if (gridView.ID == "GridDocument")
        {
            int rowIndex = e.RowIndex;

            DataTable dt = ViewState["DocDetails_VS"] as DataTable;

            if (dt != null && dt.Rows.Count > rowIndex)
            {
                dt.Rows.RemoveAt(rowIndex);

                ViewState["DocDetails_VS"] = dt;
                Session["DocUploadDT"] = dt;

                GridDocument.DataSource = dt;
                GridDocument.DataBind();
            }
        }
    }





    //=========================={ Fetch Data }==========================
    private DataTable getAccountHeadNew()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from GLAccounts757 where GLGroupName = '1000005' AND IsTaxApplied = '1000001'";
            SqlCommand cmd = new SqlCommand(sql, con);
            //cmd.Parameters.AddWithValue("@WorkOrderRefNo", workOrderRefNo);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            return dt;
        }
    }

    private string GetInvoiceRefNo(SqlConnection con, SqlTransaction transaction)
    {
        string nextRefNo = "1000001";

        string sql = "SELECT ISNULL(MAX(CAST(RefNo AS INT)), 1000000) + 1 AS NextRefNo FROM InvoiceMaster757";
        SqlCommand cmd = new SqlCommand(sql, con, transaction);

        SqlDataAdapter ad = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        ad.Fill(dt);

        if (dt.Rows.Count > 0) return dt.Rows[0]["NextRefNo"].ToString();
        else return nextRefNo;
    }

    private string GetNewInvoiceGLTaxRefNo(SqlConnection con, SqlTransaction transaction)
    {
        string nextRefNo = "1000001";

        string sql = "SELECT ISNULL(MAX(CAST(RefNo AS INT)), 1000000) + 1 AS NextRefNo FROM InvoiceGLTax757";
        SqlCommand cmd = new SqlCommand(sql, con, transaction);

        SqlDataAdapter ad = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        ad.Fill(dt);

        if (dt.Rows.Count > 0) return dt.Rows[0]["NextRefNo"].ToString();
        else return nextRefNo;
    }

    private string GetNewInvoiveItemRefNo(SqlConnection con, SqlTransaction transaction)
    {
        string nextRefNo = "1000001";

        string sql = "SELECT ISNULL(MAX(CAST(RefNo AS INT)), 1000000) + 1 AS NextRefNo FROM InvoiceItems757";
        SqlCommand cmd = new SqlCommand(sql, con, transaction);

        SqlDataAdapter ad = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        ad.Fill(dt);

        if (dt.Rows.Count > 0) return dt.Rows[0]["NextRefNo"].ToString();
        else return nextRefNo;
    }

    private string GetNewDocumentRefNo(SqlConnection con, SqlTransaction transaction)
    {
        string nextRefNo = "1000001";

        string sql = "SELECT ISNULL(MAX(CAST(RefNo AS INT)), 1000000) + 1 AS NextRefNo FROM InvoiceDocuments757";
        SqlCommand cmd = new SqlCommand(sql, con, transaction);

        SqlDataAdapter ad = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        ad.Fill(dt);

        if (dt.Rows.Count > 0) return dt.Rows[0]["NextRefNo"].ToString();
        else return nextRefNo;
    }





    //=========================={ Checkbox Event }==========================
    protected void CheckStatus_CheckedChanged(object sender, EventArgs e)
    {
        DataTable itemDT = (DataTable)Session["ItemDetails"];

        decimal basicAmount = 0.00m;
        bool someChecked = false;

        foreach (GridViewRow itemRow in itemGrid.Rows)
        {
            int rowIndex = itemRow.RowIndex;

            decimal poQty = Convert.ToDecimal(itemDT.Rows[rowIndex]["PoQuantity"]);

            TextBox BillQtyTxt = itemRow.FindControl("BillQty") as TextBox;
            decimal billQty = Convert.ToDecimal(BillQtyTxt.Text);

            TextBox BalanceQtyTxt = itemRow.FindControl("BalanceQty") as TextBox;
            decimal balanceQty = Convert.ToDecimal(BalanceQtyTxt.Text);

            decimal rate = Convert.ToDecimal(itemDT.Rows[rowIndex]["ItemRate"]);

            decimal itemSubTotal = Convert.ToDecimal(itemDT.Rows[rowIndex]["ItemSubTotal"]);

            //string checkboxStatus = ((HtmlInputCheckBox)itemRow.FindControl("CheckStatus")).Checked.ToString();
            bool checkboxStatus = ((CheckBox)itemRow.FindControl("CheckStatus")).Checked;

            if (checkboxStatus)
            {
                basicAmount += (billQty * rate);

                if (balanceQty >= 0)
                {
                    //BalanceQtyTxt.Text = (balanceQty - billQty).ToString();
                }
                else
                {
                    //BalanceQtyTxt.Text = (poQty - billQty).ToString();
                }

                someChecked = true;
            }
            else
            {
                BillQtyTxt.Text = (0).ToString();
                //BalanceQtyTxt.Text = Session["BalanceQty"].ToString();
            }
        }

        if (someChecked)
        {
            Session["TotalBillAmount"] = basicAmount;

            if (basicAmount > 0)
            {
                BasicPOAmount.Text = basicAmount.ToString("N2");

                FillTaxHead();
            }
        }
        else
        {
            divTaxHead.Visible = false;

            BasicPOAmount.Text = string.Empty;
            txtTotalDeduct.Text = string.Empty;
            txtTotalAdd.Text = string.Empty;
            txtNetAmnt.Text = string.Empty;

            GridTax.DataSource = null;
            GridTax.DataBind();

            Session["AccountHeadDT"] = null;
        }
    }




    //=========================={ GridView Bill Quantity Validation }==========================
    protected void BillQtyValidator_ServerValidate(object source, ServerValidateEventArgs args)
    {
        GridViewRow row = (GridViewRow)((CustomValidator)source).NamingContainer;

        TextBox BillQtyTxt = (TextBox)row.FindControl("BillQty");
        TextBox BalanceQtyTxt = (TextBox)row.FindControl("BalanceQty");

        decimal billQty = Convert.ToDecimal(BillQtyTxt.Text);
        decimal balanceQty = Convert.ToDecimal(BalanceQtyTxt.Text);

        //args.IsValid = (billQty <= balanceQty);
        decimal netBalanceQty = (balanceQty - billQty);

        args.IsValid = (netBalanceQty >= 0);
    }





    //=========================={ Tax Head }==========================
    protected void GridTax_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) > 0)
        {
            // Set the row in edit mode
            e.Row.RowState = e.Row.RowState ^ DataControlRowState.Edit;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // fetching acount head or taxes
            DataTable accountHeadNewDT = getAccountHeadNew();

            //=================================={ Add/Less column }========================================
            DropDownList ddlAddLess = (DropDownList)e.Row.FindControl("AddLess");

            if (ddlAddLess != null)
            {
                string addLessValue = accountHeadNewDT.Rows[e.Row.RowIndex]["AddLess"].ToString();
                ddlAddLess.SelectedValue = addLessValue;
            }

            //=================================={ Percentage/Amount column }========================================
            DropDownList ddlPerOrAmnt = (DropDownList)e.Row.FindControl("Factor");

            if (ddlPerOrAmnt != null)
            {
                string perOrAmntValue = accountHeadNewDT.Rows[e.Row.RowIndex]["Factor"].ToString();
                ddlPerOrAmnt.SelectedValue = perOrAmntValue;
            }
        }
    }

    private void FillTaxHead()
    {
        DataTable accountHeadNewDT = getAccountHeadNew();

        decimal totalBillAmount = Convert.ToDecimal(Session["TotalBillAmount"]);

        Session["AccountHeadDT"] = accountHeadNewDT;
        autoFilltaxHeads(accountHeadNewDT, totalBillAmount);
    }

    private void autoFilltaxHeads(DataTable accountHeadDT, decimal bscAmnt)
    {
        decimal basicAmount = bscAmnt;
        decimal totalDeduction = 0.00m;
        decimal totalAddition = 0.00m;
        decimal netAmount = 0.00m;

        // adding dynamic column
        if (!accountHeadDT.Columns.Contains("TaxAmount"))
        {
            // adding the new column with checkboxes
            DataColumn checkboxColumn = new DataColumn("TaxAmount", typeof(int));
            accountHeadDT.Columns.Add(checkboxColumn);
        }

        foreach (DataRow row in accountHeadDT.Rows)
        {
            decimal percentage = Convert.ToDecimal(row["Value"]);

            decimal taxValue = (basicAmount * percentage) / 100;

            if (row["AddLess"].ToString() == "Add")
            {
                totalAddition = totalAddition + taxValue;
            }
            else
            {
                totalDeduction = totalDeduction + taxValue;
            }

            row["TaxAmount"] = taxValue;
        }


        divTaxHead.Visible = true;
        GridTax.Visible = true;

        GridTax.DataSource = accountHeadDT;
        GridTax.DataBind();

        // filling total deduction
        txtTotalDeduct.Text = Math.Abs(totalDeduction).ToString("N2");

        // filling total addition
        txtTotalAdd.Text = totalAddition.ToString("N2");

        // Net Amount after tax deductions or addition
        netAmount = (basicAmount + totalAddition) - Math.Abs(totalDeduction);
        txtNetAmnt.Text = netAmount.ToString("N2");
    }

    protected void btnReCalTax_Click(object sender, EventArgs e)
    {
        // Account Head DataTable
        DataTable dt = (DataTable)Session["AccountHeadDT"];

        // Perform calculations or other logic based on the updated values
        decimal totalBill = Convert.ToDecimal(BasicPOAmount.Text);
        decimal totalDeduction = 0.00m;
        decimal totalAddition = 0.00m;
        decimal netAmount = 0.00m;

        foreach (GridViewRow row in GridTax.Rows)
        {
            // Accessing the updated dropdown values from the grid
            string updatedAddLessValue = ((DropDownList)row.FindControl("AddLess")).SelectedValue;

            int rowIndex = row.RowIndex;

            // reading parameters from gridview
            TextBox AcHeadNameTxt = row.FindControl("GLAccountName") as TextBox;
            TextBox ValueTxt = row.FindControl("Value") as TextBox;
            DropDownList FactorDD = row.FindControl("Factor") as DropDownList;
            DropDownList AddLessDropown = row.FindControl("AddLess") as DropDownList;
            TextBox TaxAccountHeadAmount = row.FindControl("TaxAmount") as TextBox;

            string accountHeadName = (AcHeadNameTxt.Text).ToString();
            decimal taxValue = Convert.ToDecimal(ValueTxt.Text);
            string factor = FactorDD.SelectedValue;
            string addLess = AddLessDropown.SelectedValue;
            decimal taxAmount = Convert.ToDecimal(TaxAccountHeadAmount.Text);

            if (factor == "Amount")
            {
                taxAmount = taxValue;

                // setting tax head amount
                TaxAccountHeadAmount.Text = Math.Abs(taxAmount).ToString("");

                if (addLess == "Add")
                {
                    totalAddition = totalAddition + taxAmount;
                }
                else
                {
                    totalDeduction = totalDeduction + taxAmount;
                }
            }
            else
            {
                // tax amount (based on add or less)
                taxAmount = (totalBill * taxValue) / 100;

                // setting tax head amount
                TaxAccountHeadAmount.Text = Math.Abs(taxAmount).ToString("");

                if (addLess == "Add")
                {
                    totalAddition = totalAddition + taxAmount;
                }
                else
                {
                    totalDeduction = totalDeduction + taxAmount;
                }
            }
        }

        // setting total deduction
        txtTotalDeduct.Text = Math.Abs(totalDeduction).ToString("N2");

        // setting total addition
        txtTotalAdd.Text = totalAddition.ToString("N2");

        // Net Amount after tax deductions or addition
        netAmount = (totalBill + totalAddition) - Math.Abs(totalDeduction);
        txtNetAmnt.Text = netAmount.ToString("N2");
    }





    //----------============={ Upload Documents }=============----------
    protected void btnDocUpload_Click(object sender, EventArgs e)
    {
        // setting the file size in web.config file (web.config should not be read only)
        //settingHttpRuntimeForFileSize();

        if (fileDoc.HasFile)
        {
            string FileExtension = System.IO.Path.GetExtension(fileDoc.FileName);

            if (FileExtension == ".xlsx" || FileExtension == ".xls")
            {

            }

            // document type
            string documentType = DocType.SelectedValue;

            // document reference name / manually entered name
            string docRefName = DocRefName.Text.ToString();

            // file name
            string onlyFileNameWithExtn = fileDoc.FileName.ToString();

            // getting unique file name
            string strFileName = GenerateUniqueId(onlyFileNameWithExtn);

            // saving and getting file path
            string filePath = getServerFilePath(strFileName);

            // Retrieve DataTable from ViewState or create a new one
            DataTable dt = ViewState["DocDetails_VS"] as DataTable ?? CreateDocDetailsDataTable();

            // filling document details datatable
            AddRowToDocDetailsDataTable(dt, documentType, docRefName, onlyFileNameWithExtn, filePath);

            // Save DataTable to ViewState
            ViewState["DocDetails_VS"] = dt;
            Session["DocUploadDT"] = dt;

            if (dt.Rows.Count > 0)
            {
                docGrid.Visible = true;

                // binding document details gridview
                GridDocument.DataSource = dt;
                GridDocument.DataBind();

                // hiding the doctype id column, only showing doctype text column
                GridDocument.Columns[1].Visible = false;

                // clearing dioc reference name textbox
                DocType.SelectedValue = "0";
                DocRefName.Text = string.Empty;
            }
        }
    }

    private string GenerateUniqueId(string strFileName)
    {
        long timestamp = DateTime.Now.Ticks;
        //string guid = Guid.NewGuid().ToString("N"); //N to remove hypen "-" from GUIDs
        string guid = Guid.NewGuid().ToString();
        string uniqueID = timestamp + "_" + guid + "_" + strFileName;
        return uniqueID;
    }

    private string getServerFilePath(string strFileName)
    {
        string orgFilePath = Server.MapPath("~/Portal/Public/" + strFileName);

        // saving file
        fileDoc.SaveAs(orgFilePath);

        //string filePath = Server.MapPath("~/Portal/Public/" + strFileName);
        //file:///C:/HostingSpaces/PAWAN/cdsmis.in/wwwroot/Pms2/Portal/Public/638399011215544557_926f9320-275e-49ad-8f59-32ecb304a9f1_EMB%20Recording.pdf

        // replacing server-specific path with the desired URL
        string baseUrl = "http://101.53.144.92/mmgpa/Ginie/External?url=.."; // server
        //string baseUrl = "http://mpkv.in/Ginie/External?url=.."; // domain
        string relativePath = orgFilePath.Replace(Server.MapPath("~/Portal/Public/"), "Portal/Public/");

        // Full URL for the hyperlink
        string fullUrl = $"{baseUrl}/{relativePath}";

        return fullUrl;
    }

    private DataTable CreateDocDetailsDataTable()
    {
        DataTable dt = new DataTable();

        // Document Type - DD Value 
        DataColumn DocTypeText = new DataColumn("DocTypeText", typeof(string));
        dt.Columns.Add(DocTypeText);

        // Document Reference name
        DataColumn DocType = new DataColumn("DocType", typeof(string));
        dt.Columns.Add(DocType);

        // Document Reference name
        DataColumn DocRefName = new DataColumn("DocRefName", typeof(string));
        dt.Columns.Add(DocRefName);

        // file name
        DataColumn DocName = new DataColumn("DocName", typeof(string));
        dt.Columns.Add(DocName);

        // Doc uploaded path
        DataColumn DocPath = new DataColumn("DocPath", typeof(string));
        dt.Columns.Add(DocPath);

        return dt;
    }

    private void AddRowToDocDetailsDataTable(DataTable dt, string documentType, string docRefName, string onlyFileNameWithExtn, string filePath)
    {
        // Create a new row
        DataRow row = dt.NewRow();

        // Set values for the new row
        row["DocTypeText"] = DocType.SelectedItem.Text;
        row["DocType"] = documentType;
        row["DocRefName"] = docRefName;
        row["DocName"] = onlyFileNameWithExtn;
        row["DocPath"] = filePath;

        // Add the new row to the DataTable
        dt.Rows.Add(row);
    }





    //=========================={ Submit Button Click Event }==========================

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("VendorInvoice.aspx");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }

        bool someChecked = false;
        bool somePositiveBillQty = false;

        foreach (GridViewRow row in itemGrid.Rows)
        {
            bool checkStatus = ((CheckBox)row.FindControl("CheckStatus")).Checked;

            TextBox BillQtyTxt = row.FindControl("BillQty") as TextBox;
            decimal billQty = Convert.ToDecimal(BillQtyTxt.Text);

            if (checkStatus)
            {
                someChecked = true;

                if(billQty > 0)
                {
                    somePositiveBillQty = true;
                }
            }
        }

        if (someChecked)
        {
            if (somePositiveBillQty)
            {
                if (GridDocument.Rows.Count > 0)
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        SqlTransaction transaction = con.BeginTransaction();

                        try
                        {
                            // fetching new invoice / bill reference no
                            string invoiceRefNo = GetInvoiceRefNo(con, transaction);

                            // inserting header
                            InsertInvoiceHeader(con, transaction, invoiceRefNo);

                            // inserting items details
                            InsertItemDetails(con, transaction, invoiceRefNo);

                            // inserting invoice or tax heads
                            InsertTaxHead(con, transaction, invoiceRefNo);

                            // insert documents
                            InsertDocuments(con, transaction, invoiceRefNo);

                            if (transaction.Connection != null) transaction.Commit();

                            getSweetAlertSuccessRedirectMandatory("Invoice Generated!", $"The Invoice: {invoiceRefNo} Has Been Generated Successfully", "VendorInvoice.aspx");
                        }
                        catch (Exception ex)
                        {
                            getSweetAlertErrorMandatory("Oops!", $"{ex.Message}");
                            transaction.Rollback();
                        }
                        finally
                        {
                            con.Close();
                            transaction.Dispose();
                        }
                    }
                }
                else
                {
                    getSweetAlertErrorMandatory("No Documents Found!!", $"Kindly Add Minimum One Invoice Related Document To Proceed Further");
                }
            }
            else
            {
                getSweetHTMLWzrning("No Bill Quantity Found", "Kindly Enter Some Bill Quantity To Proceed Further");
            }
        }
        else
        {
            getSweetHTMLWzrning("No Items Are Checked", "Kindly Check Minimum 1 Item To Proceed Further");
        }
    }





    // insert header
    private void InsertInvoiceHeader(SqlConnection con, SqlTransaction transaction, string invoiceRefNo)
    {
        string userID = Session["UserId"].ToString();

        string poRefNo = PoNo.SelectedValue;
        DateTime poDate = DateTime.Parse(PoDate.Text);

        string invoiceNo = InvoiceNo.Text;
        DateTime invoiceDate = DateTime.Parse(InvoiceDate.Text);

        decimal totalDeduct = decimal.TryParse(txtTotalDeduct.Text, out decimal deductValue) ? deductValue : 0;
        decimal totalAdd = decimal.TryParse(txtTotalAdd.Text, out decimal addValue) ? addValue : 0;
        decimal basicAmount = decimal.TryParse(BasicPOAmount.Text, out decimal basicAmnt) ? basicAmnt : 0;
        decimal netAmount = decimal.TryParse(txtNetAmnt.Text, out decimal netValue) ? netValue : 0;

        string invoiceRemark = InvoiceRemark.Value;

        string sql = $@"insert into InvoiceMaster757 
                        (RefNo, PoRefNo, InvoiceNo, InvoiceDate, TotalDeduct, TotalAdd, BasicAmount, NetAmount, InvoiceRemark, SaveBy) 
                        Values 
                        (@RefNo, @PoRefNo, @InvoiceNo, @InvoiceDate, @TotalDeduct, @TotalAdd, @BasicAmount, @NetAmount, @InvoiceRemark, @SaveBy)";

        SqlCommand cmd = new SqlCommand(sql, con, transaction);
        cmd.Parameters.AddWithValue("@RefNo", invoiceRefNo);
        cmd.Parameters.AddWithValue("@PoRefNo", poRefNo);
        cmd.Parameters.AddWithValue("@InvoiceNo", invoiceNo);
        cmd.Parameters.AddWithValue("@InvoiceDate", invoiceDate);
        cmd.Parameters.AddWithValue("@TotalDeduct", totalDeduct);
        cmd.Parameters.AddWithValue("@TotalAdd", totalAdd);
        cmd.Parameters.AddWithValue("@BasicAmount", basicAmount);
        cmd.Parameters.AddWithValue("@NetAmount", netAmount);
        cmd.Parameters.AddWithValue("@InvoiceRemark", invoiceRemark);
        cmd.Parameters.AddWithValue("@SaveBy", userID);
        int k = cmd.ExecuteNonQuery();

        //SqlDataAdapter ad = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        //ad.Fill(dt);
    }

    // insert invoice items details
    private void InsertItemDetails(SqlConnection con, SqlTransaction transaction, string invoiceRefNo)
    {
        string userID = Session["UserId"].ToString();

        DataTable itemDT = (DataTable)Session["ItemDetails"];

        if (itemDT.Rows.Count > 0)
        {
            foreach (GridViewRow row in itemGrid.Rows)
            {
                int rowIndex = row.RowIndex;

                // checkbox check status
                bool checkStatus = ((CheckBox)row.FindControl("CheckStatus")).Checked;

                string poRefNo_Existing = itemDT.Rows[rowIndex]["RefNo"].ToString();

                string itemRefNo_New = GetNewInvoiveItemRefNo(con, transaction);
                string poRefNo = PoNo.SelectedValue;

                string itemCategory = itemDT.Rows[rowIndex]["ItemCategory"].ToString();
                string itemSubCategory = itemDT.Rows[rowIndex]["ItemSubCategory"].ToString();
                string itemName = itemDT.Rows[rowIndex]["ItemName"].ToString();
                string itemUOM = itemDT.Rows[rowIndex]["ItemUOM"].ToString();
                string itemRate = itemDT.Rows[rowIndex]["ItemRate"].ToString();
                string itemSUbTotal = itemDT.Rows[rowIndex]["ItemSubTotal"].ToString();

                decimal poQty = Convert.ToDecimal(itemDT.Rows[rowIndex]["PoQuantity"].ToString());

                TextBox BillQtyTxt = row.FindControl("BillQty") as TextBox;
                decimal billQty = Convert.ToDecimal(BillQtyTxt.Text);

                TextBox BalanceQtyTxt = row.FindControl("BalanceQty") as TextBox;
                decimal balanceQty = Convert.ToDecimal(BalanceQtyTxt.Text);

                decimal netBalanceQty = (balanceQty - billQty);

                //decimal balanceQty = (balanceQty - billQty);

                if (checkStatus)
                {
                    string sql = $@"Insert into InvoiceItems757  
                                (RefNo, InvoiceRefNo, PORefNo, ItemCategory, ItemSubCategory, ItemName, PoQuantity, BalanceQty, ItemUOM, ItemRate, ItemSubTotal, BillQty, SaveBy) 
                                Values 
                                (@RefNo, @InvoiceRefNo, @PORefNo, @ItemCategory, @ItemSubCategory, @ItemName, @PoQuantity, @BalanceQty, @ItemUOM, @ItemRate, @ItemSubTotal, @BillQty, @SaveBy)";

                    SqlCommand cmd = new SqlCommand(sql, con, transaction);
                    cmd.Parameters.AddWithValue("@RefNo", itemRefNo_New);
                    cmd.Parameters.AddWithValue("@InvoiceRefNo", invoiceRefNo);
                    cmd.Parameters.AddWithValue("@PORefNo", poRefNo);
                    cmd.Parameters.AddWithValue("@ItemCategory", itemCategory);
                    cmd.Parameters.AddWithValue("@ItemSubCategory", itemSubCategory);
                    cmd.Parameters.AddWithValue("@ItemName", itemName);
                    cmd.Parameters.AddWithValue("@PoQuantity", poQty);
                    cmd.Parameters.AddWithValue("@BalanceQty", balanceQty);
                    cmd.Parameters.AddWithValue("@ItemUOM", itemUOM);
                    cmd.Parameters.AddWithValue("@ItemRate", itemRate);
                    cmd.Parameters.AddWithValue("@ItemSubTotal", itemSUbTotal);
                    cmd.Parameters.AddWithValue("@BillQty", billQty);
                    cmd.Parameters.AddWithValue("@SaveBy", userID);
                    int k = cmd.ExecuteNonQuery();

                    if (k > 0)
                    {
                        // update balance & bill qty from PoBOM757 table
                        UpdatePoItems(con, transaction, billQty, netBalanceQty, poRefNo_Existing);
                    }
                }
            }
        }
    }

    private void UpdatePoItems(SqlConnection con, SqlTransaction transaction, decimal billQty, decimal netBalanceQty, string poRefNo_Existing)
    {
        string sql = $@"Update PoBom757 SET BalanceQty = @BalanceQty, BillQty = @BillQty WHERE RefNo = @RefNo";

        SqlCommand cmd = new SqlCommand(sql, con, transaction);
        cmd.Parameters.AddWithValue("@BillQty", billQty);
        cmd.Parameters.AddWithValue("@BalanceQty", netBalanceQty);
        cmd.Parameters.AddWithValue("@RefNo", poRefNo_Existing);
        int k = cmd.ExecuteNonQuery();
    }


    // insert Gl Tax Heads
    private void InsertTaxHead(SqlConnection con, SqlTransaction transaction, string invoiceRefNo)
    {
        string userID = Session["UserId"].ToString();

        DataTable dt = (DataTable)Session["AccountHeadDT"];

        if (dt != null)
        {
            foreach (GridViewRow row in GridTax.Rows)
            {
                int rowIndex = row.RowIndex;

                string purchaseOrderRefNo = PoNo.SelectedValue;

                string glAccountCode = dt.Rows[rowIndex]["GLAccountCode"].ToString();

                // Tax Head Grid Details
                TextBox GLAccountNameTxt = row.FindControl("GLAccountName") as TextBox;
                string glAccountName = (GLAccountNameTxt.Text).ToString();

                TextBox ValueTXT = row.FindControl("Value") as TextBox;
                decimal taxValue = Convert.ToDecimal(ValueTXT.Text);

                DropDownList FactorDD = row.FindControl("Factor") as DropDownList;
                string factor = FactorDD.SelectedValue;

                DropDownList AddLessDropown = row.FindControl("AddLess") as DropDownList;
                string addLess = AddLessDropown.SelectedValue;

                TextBox TaxAccountHeadAmount = row.FindControl("TaxAmount") as TextBox;
                decimal taxAmount = Convert.ToDecimal(TaxAccountHeadAmount.Text);

                // new invoice details reference no
                string newTaxRefNo = GetNewInvoiceGLTaxRefNo(con, transaction);

                string sql = $@"INSERT INTO InvoiceGLTax757  
                                (RefNo, InvoiceRefNo, PoRefNo, GLAccountName, GLAccountCode, Value, Factor, AddLess, TaxAmount, SaveBy) 
                                VALUES 
                                (@RefNo, @InvoiceRefNo, @PoRefNo, @GLAccountName, @GLAccountCode, @Value, @Factor, @AddLess, @TaxAmount, @SaveBy)";

                SqlCommand cmd = new SqlCommand(sql, con, transaction);
                cmd.Parameters.AddWithValue("@RefNO", newTaxRefNo);
                cmd.Parameters.AddWithValue("@InvoiceRefNo", invoiceRefNo);
                cmd.Parameters.AddWithValue("@PoRefNo", purchaseOrderRefNo);
                cmd.Parameters.AddWithValue("@GLAccountName", glAccountName);
                cmd.Parameters.AddWithValue("@GLAccountCode", glAccountCode);
                cmd.Parameters.AddWithValue("@Value", taxValue);
                cmd.Parameters.AddWithValue("@Factor", factor);
                cmd.Parameters.AddWithValue("@AddLess", addLess);
                cmd.Parameters.AddWithValue("@TaxAmount", taxAmount);
                cmd.Parameters.AddWithValue("@SaveBy", userID);
                cmd.ExecuteNonQuery();
            }
        }
    }

    // insert documents
    private void InsertDocuments(SqlConnection con, SqlTransaction transaction, string invoiceRefNo)
    {
        DataTable documentsDT = (DataTable)Session["DocUploadDT"];

        foreach (GridViewRow row in GridDocument.Rows)
        {
            // logged-in userID
            string userID = Session["UserID"].ToString();

            int rowIndex = row.RowIndex;

            string docType = documentsDT.Rows[rowIndex]["DocType"].ToString();
            //string docRefName = documentsDT.Rows[rowIndex]["DocRefName"].ToString();
            string docName = documentsDT.Rows[rowIndex]["DocName"].ToString();

            HyperLink hypDocPath = (HyperLink)row.FindControl("DocPath");
            string docPath = hypDocPath.NavigateUrl;

            string docRefNo_New = GetNewDocumentRefNo(con, transaction);


            string sql = $@"Insert Into InvoiceDocuments757 
                            (RefNo, InvoiceRefNo, DocType, DocName, DocPath, SaveBy) 
                            Values 
                            (@RefNo, @InvoiceRefNo, @DocType, @DocName, @DocPath, @SaveBy)";

            SqlCommand cmd = new SqlCommand(sql, con, transaction);
            cmd.Parameters.AddWithValue("@RefNo", docRefNo_New);
            cmd.Parameters.AddWithValue("@InvoiceRefNo", invoiceRefNo);
            cmd.Parameters.AddWithValue("@DocType", docType);
            cmd.Parameters.AddWithValue("@DocName", docName);
            cmd.Parameters.AddWithValue("@DocPath", docPath);
            cmd.Parameters.AddWithValue("@SaveBy", userID);
            cmd.ExecuteNonQuery();

            //SqlDataAdapter ad = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //ad.Fill(dt);
        }
    }


}