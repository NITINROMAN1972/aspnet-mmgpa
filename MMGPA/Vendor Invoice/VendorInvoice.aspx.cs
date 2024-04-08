using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlTypes;

public partial class Vendor_Invoice_VendorInvoice : System.Web.UI.Page
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

                SEARCH_InvoiceNumber_Bind_Dropdown();

            }
        }
        else
        {
            getSweetHTML("Not Signed-In!", "Kindly <strong>Sign-In</strong> To Access The Porject <br/> By Clicking On <strong><i>Login</i></strong> Button Above");
            loginDiv.Visible = false;
        }
    }




    //=========================={ Paging & Alert }==========================
    protected void gridSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //binding GridView to PageIndex object
        gridSearch.PageIndex = e.NewPageIndex;

        DataTable pagination = (DataTable)Session["PaginationDataSource"];

        gridSearch.DataSource = pagination;
        gridSearch.DataBind();
    }

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

    private void SEARCH_InvoiceNumber_Bind_Dropdown()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = $@"select Distinct im.InvoiceNo, im.RefNo
                            from InvoiceMaster757 as im
                            inner join InvoiceItems757 as ii on ii.InvoiceRefNo = im.RefNo";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            ddScInvoiceNo.DataSource = dt;
            ddScInvoiceNo.DataTextField = "InvoiceNo";
            ddScInvoiceNo.DataValueField = "RefNo";
            ddScInvoiceNo.DataBind();
            ddScInvoiceNo.Items.Insert(0, new ListItem("------- Select Invoice No. -------", "0"));
        }
    }

    private void PoNoTitle_DropDown()
    {
        string userID = Session["UserId"].ToString();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select RefID, PoNo from PoDetails757";
            SqlCommand cmd = new SqlCommand(sql, con);
            //cmd.Parameters.AddWithValue("@SaveBy", userID);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            //PoNo.DataSource = dt;
            //PoNo.DataTextField = "PoNo";
            //PoNo.DataValueField = "RefID";
            //PoNo.DataBind();
            //PoNo.Items.Insert(0, new ListItem("------Select P.O. Number------", "0"));
        }
    }






    //=========================={ Fetch Data }==========================
    private DataTable GetInvoiceDT(string invoiceReferenceNo)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from InvoiceMaster757 where RefNo = @RefNo";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@RefNo", invoiceReferenceNo);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            return dt;
        }
    }

    private string GetPoItemsRefNo(SqlConnection con, SqlTransaction transaction)
    {
        string nextRefNo = "1000001";

        string sql = "SELECT ISNULL(MAX(CAST(RefNo AS INT)), 1000000) + 1 AS NextRefNo FROM PoBom757";
        SqlCommand cmd = new SqlCommand(sql, con, transaction);

        SqlDataAdapter ad = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        ad.Fill(dt);

        if (dt.Rows.Count > 0) return dt.Rows[0]["NextRefNo"].ToString();
        else return nextRefNo;
    }

    private DataTable getAccountHeadNew(string invoiceRefNo)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from InvoiceGLTax757 where InvoiceRefNo = @InvoiceRefNo";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@InvoiceRefNo", invoiceRefNo);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            return dt;
        }
    }






    //=========================={ Search Button Event }==========================
    protected void btnNewBill_Click(object sender, EventArgs e)
    {
        Response.Redirect("VendorInvoiceNew.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGridView();
    }

    private void BindGridView()
    {
        searchGridDiv.Visible = true;

        string invoiceReferenceNo = ddScInvoiceNo.SelectedValue; // invoice ref no

        DateTime fromDate;
        DateTime toDate;

        if (!DateTime.TryParse(ScFromDate.Text, out fromDate)) { fromDate = SqlDateTime.MinValue.Value; }
        if (!DateTime.TryParse(ScToDate.Text, out toDate)) { toDate = SqlDateTime.MaxValue.Value; }

        // DTs
        DataTable invoiceDT = GetInvoiceDT(invoiceReferenceNo);

        // dt values
        string invoiceRefNo = (invoiceDT.Rows.Count > 0) ? invoiceDT.Rows[0]["RefNo"].ToString() : string.Empty;

        DataTable searchResultDT = SearchRecords(invoiceRefNo, fromDate, toDate);

        // binding the search grid
        gridSearch.DataSource = searchResultDT;
        gridSearch.DataBind();

        //Required for jQuery DataTables to work.
        gridSearch.UseAccessibleHeader = true;
        gridSearch.HeaderRow.TableSection = TableRowSection.TableHeader;
        //gridSearch.FooterRow.TableSection = TableRowSection.TableHeader;

        Session["PaginationDataSource"] = searchResultDT;
    }

    public DataTable SearchRecords(string invoiceRefNo, DateTime fromDate, DateTime toDate)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string sql = $@"select Distinct im.RefNo, im.InvoiceNo, im.InvoiceDate, Concat(N'₹ ', FORMAT(im.NetAmount, 'N0', 'en-IN')) AS NetAmount, 
                            pd.PoNo, Concat (N'₹ ', Format(pd.PoAmt, 'N0', 'en-IN')) as PoAmt, vm.VendorName, 
                            (select count(*) from InvoiceItems757 ii where ii.InvoiceRefNo = im.RefNo) as InvoiceItemsCount 
                            from InvoiceMaster757 as im 
                            inner join InvoiceItems757 as ii on ii.InvoiceRefNo = im.RefNo 
                            inner join PoDetails757 as pd on pd.RefID = im.PoRefNo 
                            inner join VendorMaster757 as vm on vm.RefID = pd.VendNme
                            WHERE 1=1";

            if (!string.IsNullOrEmpty(invoiceRefNo))
            {
                sql += " AND im.RefNo = @RefNo";
            }

            if (fromDate != null)
            {
                sql += " AND im.InvoiceDate >= @FromDate";
            }

            if (toDate != null)
            {
                sql += " AND im.InvoiceDate <= @ToDate";
            }

            sql += " ORDER BY im.RefNo DESC";
            //sql += " AND aa.RefNo=@AANumber ORDER BY RefNo DESC";




            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                if (!string.IsNullOrEmpty(invoiceRefNo))
                {
                    command.Parameters.AddWithValue("@RefNo", invoiceRefNo);
                }

                if (fromDate != null)
                {
                    command.Parameters.AddWithValue("@FromDate", fromDate);
                }

                if (toDate != null)
                {
                    command.Parameters.AddWithValue("@ToDate", toDate);
                }

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }
    }







    //=========================={ Update - Fill Bill & Item Details }==========================
    protected void gridSearch_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "lnkView")
        {
            string invoiceRefNo = (e.CommandArgument).ToString();
            Session["InvoiceRefNo"] = invoiceRefNo;

            searchGridDiv.Visible = false;
            divTopSearch.Visible = false;
            UpdateDiv.Visible = true;

            // fill header
            AutoFilHeader(invoiceRefNo);

            // fill items
            AutoFilItems(invoiceRefNo);

            // fill GL tax
            AutoFillTGLTaxs(invoiceRefNo);

            // fill invoice documents
            AutoFilDocuments(invoiceRefNo);
        }
    }

    private void AutoFilHeader(string invoiceRefNo)
    {
        itemDiv.Visible = true;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = $@"select im.RefNo, im.InvoiceNo, im.InvoiceDate, im.InvoiceRemark, pd.RefID, pd.PoNo, pd.PoDate, pd.PoAmt, vm.VendorName, 
                            im.TotalDeduct, im.TotalAdd, im.BasicAmount, im.NetAmount
                            from InvoiceMaster757 as im 
                            inner join InvoiceItems757 as ii on ii.InvoiceRefNo = im.RefNo 
                            inner join PoDetails757 as pd on pd.RefID = im.PoRefNo 
                            inner join VendorMaster757 as vm on vm.RefID = pd.VendNme
                            where im.RefNo = @RefNo";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@RefNo", invoiceRefNo);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            if (dt.Rows.Count > 0)
            {
                InvoiceNo.Text = dt.Rows[0]["InvoiceNo"].ToString();

                DateTime invoiceDate = DateTime.Parse(dt.Rows[0]["InvoiceDate"].ToString());
                InvoiceDate.Text = invoiceDate.ToString("yyyy-MM-dd");

                DateTime poDate = DateTime.Parse(dt.Rows[0]["PoDate"].ToString());
                PoDate.Text = poDate.ToString("yyyy-MM-dd");

                decimal poAmount = Convert.ToDecimal(dt.Rows[0]["PoAmt"]);
                PoAmount.Text = poAmount.ToString("N2");

                VendorName.Text = dt.Rows[0]["VendorName"].ToString();

                PoNo.DataSource = dt;
                PoNo.DataTextField = "PoNo";
                PoNo.DataValueField = "RefID";
                PoNo.DataBind();
                PoNo.Items.Insert(0, new ListItem("------Select P.O. Number------", "0"));

                if (PoNo.SelectedIndex < 2) PoNo.SelectedIndex = 1;


                // GL Tax Head
                decimal totalDeduct = Convert.ToDecimal(dt.Rows[0]["TotalDeduct"]);
                decimal totalAdd = Convert.ToDecimal(dt.Rows[0]["TotalAdd"]);
                decimal basicAmount = Convert.ToDecimal(dt.Rows[0]["BasicAmount"]);
                decimal netAmount = Convert.ToDecimal(dt.Rows[0]["NetAmount"]);

                BasicPOAmount.Text = basicAmount.ToString("N2");
                txtTotalDeduct.Text = totalDeduct.ToString("N2");
                txtTotalAdd.Text = totalAdd.ToString("N2");
                txtNetAmnt.Text = netAmount.ToString("N2");

                InvoiceRemark.Value = dt.Rows[0]["InvoiceRemark"].ToString();
            }
        }
    }

    private void AutoFilItems(string invoiceRefNo)
    {
        itemDiv.Visible = true;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = $@"select ii.*, cat.CategoryName as ItemCategoryText, subcat.SubCategory as ItemSubCategoryText, 
                            item.ItemName as ItemNameText, uom.UnitName as ItemUOMText
                            from InvoiceItems757 as ii 
                            inner join InvoiceMaster757 as im on im.RefNo = ii.InvoiceRefNo 
                            inner join ItemCategory757 as cat on cat.RefID = ii.ItemCategory 
                            inner join ItemSubCategory757 as subcat on subcat.RefID = ii.ItemSubCategory 
                            inner join UnitOfMeasurement757 as uom on uom.RefID = ii.ItemUOM 
                            inner join ItemMaster757 as item on item.RefID = ii.ItemName
                            where im.RefNo = @RefNo";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@RefNo", invoiceRefNo);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            if (dt.Rows.Count > 0)
            {
                itemGrid.DataSource = dt;
                itemGrid.DataBind();

                ViewState["ItemDetails_VS"] = dt;
                Session["ItemDetails"] = dt;

                double? totalBillAmount = dt.AsEnumerable().Sum(row => row["ItemSubTotal"] is DBNull ? (double?)null : Convert.ToDouble(row["ItemSubTotal"])) ?? 0.0;
                //txtBillAmount.Text = totalBillAmount.HasValue ? totalBillAmount.Value.ToString("N2") : "0.00";

                Session["TotalBillAmount"] = totalBillAmount;
            }
        }
    }

    private void AutoFillTGLTaxs(string invoiceRefNo)
    {
        itemDiv.Visible = true;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = $@"select  * from InvoiceGLTax757 where InvoiceRefNo = @InvoiceRefNo";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@InvoiceRefNo", invoiceRefNo);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            if (dt.Rows.Count > 0)
            {
                divTaxHead.Visible = true;
                GridTax.Visible = true;

                GridTax.DataSource = dt;
                GridTax.DataBind();

                Session["AccountHeadDT"] = dt;
            }
        }
    }

    protected void GridTax_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) > 0)
        {
            // Set the row in edit mode
            e.Row.RowState = e.Row.RowState ^ DataControlRowState.Edit;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string invoiceRefNo = Session["InvoiceRefNo"].ToString();

            // fetching acount head or taxes
            DataTable accountHeadNewDT = getAccountHeadNew(invoiceRefNo);

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



    private void AutoFilDocuments(string invoiceRefNo)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = $@"select id.RefNo, id.InvoiceRefNo , id.DocType, dt.DocumentName as DocTypeText, id.DocName, id.DocPath 
                            from InvoiceDocuments757 as id 
                            inner join DocumentType757 as dt on dt.RefNo = id.DocType
                            Where id.InvoiceRefNo = @InvoiceRefNo And id.DeleteFlag IS NULL";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@InvoiceRefNo", invoiceRefNo);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            if (dt.Rows.Count > 0)
            {
                docGrid.Visible = true;

                GridDocument.DataSource = dt;
                GridDocument.DataBind();

                // hiding the doctype id column, only showing doctype text column
                GridDocument.Columns[1].Visible = false;

                ViewState["DocDetails_VS"] = dt;
                Session["DocUploadDT"] = dt;
            }
        }
    }


    //=========================={ Submit Button Click Event }==========================
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("VendorInvoice.aspx");
    }



}