using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administrative_Approval_AdministrativeApproval : System.Web.UI.Page
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
                // searcha DD
                AdministrativeAprovalNo_Bind_Dropdown();
            }
        }
        else
        {
            getSweetHTML("Not Signed-In!", "Kindly <strong>Sign-In</strong> To Access The Porject <br/> By Clicking On <strong><i>Login</i></strong> Button Above");
            divTopSearch.Visible = false;
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

    // success mandatory redirect
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






    //=========================={ Dropdown Bind }==========================
    private void AdministrativeAprovalNo_Bind_Dropdown()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from AAMaster757";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            ddScAANo.DataSource = dt;
            ddScAANo.DataTextField = "AANumber";
            ddScAANo.DataValueField = "AANumber";
            ddScAANo.DataBind();
            ddScAANo.Items.Insert(0, new ListItem("------- Select A.A. Number -------", "0"));
        }
    }

    private void DocumentType_DropDown()
    {
        //string userID = Session["UserId"].ToString();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from DocumentType757";
            SqlCommand cmd = new SqlCommand(sql, con);
            //cmd.Parameters.AddWithValue("@SaveBy", userID);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            DocType.DataSource = dt;
            DocType.DataTextField = "DocumentName";
            DocType.DataValueField = "RefNo";
            DocType.DataBind();
            DocType.Items.Insert(0, new ListItem("------Select Document Type------", "0"));
        }
    }

    private void SourceOfBudget_DropDown()
    {
        //string userID = Session["UserId"].ToString();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from SourceOfBudget757";
            SqlCommand cmd = new SqlCommand(sql, con);
            //cmd.Parameters.AddWithValue("@SaveBy", userID);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            SourceOfBudget.DataSource = dt;
            SourceOfBudget.DataTextField = "BudgetName";
            SourceOfBudget.DataValueField = "RefID";
            SourceOfBudget.DataBind();
            SourceOfBudget.Items.Insert(0, new ListItem("------Select Source Of Budget------", "0"));
        }
    }

    private void AAForItemCategory_DropDown()
    {
        //string userID = Session["UserId"].ToString();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from ItemCategory757";
            SqlCommand cmd = new SqlCommand(sql, con);
            //cmd.Parameters.AddWithValue("@Unit", unitOrOfficeCode);

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            AAForItemCategory.DataSource = dt;
            AAForItemCategory.DataTextField = "CategoryName";
            AAForItemCategory.DataValueField = "RefID";
            AAForItemCategory.DataBind();
            AAForItemCategory.Items.Insert(0, new ListItem("------Select Item Category------", "0"));

            ListItem selectValuesItem = AAForItemCategory.Items.FindByValue("0");

            if (selectValuesItem != null)
            {
                selectValuesItem.Selected = true;
            }
        }
    }




    //=========================={ Fetch Data }==========================
    private DataTable GetAdminstrativeApprovalDT(string adminApprovalNo)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from AAMaster757 where AANumber = @AANumber";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@AANumber", adminApprovalNo);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            return dt;
        }
    }

    private bool checkForDocuUploadedExist(string docRefNo, SqlConnection con, SqlTransaction transaction)
    {
        string sql = "SELECT * FROM AADocuments757 WHERE RefNo=@RefNo";

        SqlCommand cmd = new SqlCommand(sql, con, transaction);
        cmd.Parameters.AddWithValue("@RefNo", docRefNo);
        cmd.ExecuteNonQuery();

        SqlDataAdapter ad = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        ad.Fill(dt);

        if (dt.Rows.Count > 0) return true;
        else return false;
    }

    private string GetNewDocumentRefNo(SqlConnection con, SqlTransaction transaction)
    {
        string nextRefNo = "1000001";

        string sql = "SELECT ISNULL(MAX(CAST(RefNo AS INT)), 1000000) + 1 AS NextRefNo FROM AADocuments757";
        SqlCommand cmd = new SqlCommand(sql, con, transaction);

        SqlDataAdapter ad = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        ad.Fill(dt);

        if (dt.Rows.Count > 0) return dt.Rows[0]["NextRefNo"].ToString();
        else return nextRefNo;
    }

    private DataTable GetAADT(string adminApprovalNo, SqlConnection con, SqlTransaction transaction)
    {
        string sql = "select * from AAMaster757 where AANumber = @AANumber";
        SqlCommand cmd = new SqlCommand(sql, con, transaction);
        cmd.Parameters.AddWithValue("@AANumber", adminApprovalNo);
        cmd.ExecuteNonQuery();

        SqlDataAdapter ad = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        ad.Fill(dt);

        return dt;
    }





    //=========================={ Search Button Event }==========================
    protected void btnNewBill_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdministrativeApprovalNew.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGridView();
    }

    private void BindGridView()
    {
        searchGridDiv.Visible = true;

        string adminApprovalNo = ddScAANo.SelectedValue; // A.A. ref no

        DateTime fromDate;
        DateTime toDate;

        if (!DateTime.TryParse(ScFromDate.Text, out fromDate)) { fromDate = SqlDateTime.MinValue.Value; }
        if (!DateTime.TryParse(ScToDate.Text, out toDate)) { toDate = SqlDateTime.MaxValue.Value; }

        // DTs
        DataTable adminApprovalDT = GetAdminstrativeApprovalDT(adminApprovalNo);

        // dt values
        string aaNo = (adminApprovalDT.Rows.Count > 0) ? adminApprovalDT.Rows[0]["AANumber"].ToString() : string.Empty;

        DataTable searchResultDT = SearchRecords(aaNo, fromDate, toDate);

        // binding the search grid
        gridSearch.DataSource = searchResultDT;
        gridSearch.DataBind();

        //Required for jQuery DataTables to work.
        gridSearch.UseAccessibleHeader = true;
        gridSearch.HeaderRow.TableSection = TableRowSection.TableHeader;
        //gridSearch.FooterRow.TableSection = TableRowSection.TableHeader;

        Session["PaginationDataSource"] = searchResultDT;
    }

    public DataTable SearchRecords(string aaNo, DateTime fromDate, DateTime toDate)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string sql = $@"select aa.RefNo , aa.AANumber, aa.AADate, aa.AATitle, aa.AAFor, aa.SanctionAmount, aa.SanctionDate, aa.SourceOfBudget, budget.BudgetName, 
                            LEN(aa.AAFor) - LEN(REPLACE(aa.AAFor, ',', '')) + 1 AS NumberOfItems, 
                            case when (select count (*) from AAVerification757 as verify where verify.AARefNo = aa.RefNo) > 0 then 'Verified' else 'Pending' end as VerificationStatus
                            from AAMaster757 as aa 
                            left join SourceOfBudget757 budget on budget.refid = aa.SourceOfBudget 
                            left join ItemCategory757 as ic on ic.RefId = aa.AAFor
                            left join AAVerification757 as verify on verify.AARefNo = aa.RefNo
                            WHERE 1=1";

            if (!string.IsNullOrEmpty(aaNo))
            {
                sql += " AND aa.AANumber = @AANumber";
            }

            if (fromDate != null)
            {
                sql += " AND aa.AADate >= @FromDate";
            }

            if (toDate != null)
            {
                sql += " AND aa.AADate <= @ToDate";
            }

            sql += " ORDER BY aa.RefNo DESC";
            //sql += " AND aa.RefNo=@AANumber ORDER BY RefNo DESC";




            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                if (!string.IsNullOrEmpty(aaNo))
                {
                    command.Parameters.AddWithValue("@AANumber", aaNo);
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
            string aaRefNo = (e.CommandArgument).ToString();
            Session["AdminApproveRefNo"] = aaRefNo;

            searchGridDiv.Visible = false;
            divTopSearch.Visible = false;
            UpdateDiv.Visible = true;

            // binding existing header dropdowns
            DocumentType_DropDown();
            SourceOfBudget_DropDown();
            AAForItemCategory_DropDown();

            // checkinf for approved AA Number
            CheckApprovedAA(aaRefNo);

            // fill header
            AutoFillHeader(aaRefNo);

            // fill documents
            AutoFillDocuments(aaRefNo);
        }
    }


    private void CheckApprovedAA(string aaRefNo)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = $@"select verify.*, aamas.AANumber 
                            from AAVerification757 as verify
                            inner join AAMaster757 as aamas on aamas.RefNo = verify.AARefNo
                            where verify.AARefNo = @AARefNo";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@AARefNo", aaRefNo);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["VerificationStatus"].ToString() == "TRUE")
                {
                    docDiv.Visible = false;
                    btnSubmit.Visible = false;

                    // hide document gridview action column
                    GridView docGridView = GridDocument;

                    DataControlField column = docGridView.Columns.Cast<DataControlField>().FirstOrDefault(col => col.HeaderText == "Actions");
                    if (column != null)
                    {
                        int columnIndex = docGridView.Columns.IndexOf(column);
                        docGridView.Columns[columnIndex].Visible = false;
                    }

                    getSweetAlertInfo("A.A. Verified!", $"The A.A. Has Been Already Verified, Hence Can Not Edit");
                }
            }
        }
    }

    private void AutoFillHeader(string aaRefNo)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = $@"select aa.*, budget.RefID as budgetRefId , budget.BudgetName 
                            from AAMaster757 as aa 
                            inner join SourceOfBudget757 as budget on budget.RefID = aa.SourceOfBudget 
                            where aa.RefNo=@RefNo";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@RefNo", aaRefNo);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            Session["HeaderDataTable"] = dt;

            AANumber.Text = dt.Rows[0]["AANumber"].ToString();
            AATitle.Text = dt.Rows[0]["AATitle"].ToString();
            SanctionAmount.Text = dt.Rows[0]["SanctionAmount"].ToString();

            DateTime aaDate = DateTime.Parse(dt.Rows[0]["AADate"].ToString());
            DateTime sanctionDate = DateTime.Parse(dt.Rows[0]["AADate"].ToString());

            AADate.Text = aaDate.ToString("yyyy-MM-dd");
            SanctionDate.Text = sanctionDate.ToString("yyyy-MM-dd");



            // clearing existing selections
            SourceOfBudget.ClearSelection();

            // source of budget DD refid
            string sourceOfBudgetRefID = dt.Rows[0]["budgetRefId"].ToString();

            foreach (ListItem item in SourceOfBudget.Items)
            {
                if (item.Value == sourceOfBudgetRefID)
                {
                    item.Selected = true;
                    break;
                }
            }
        }
    }

    private void AutoFillDocuments(string aaRefNo)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = $@"select aa.RefNo, aa.AANumber, doc.DocType, dt.DocumentName as DocTypeText, doc.DocName, doc.DocPath, dt.RefNo 
                            from AAMaster757 as aa 
                            inner join AADocuments757 as doc on doc.AARefNo = aa.RefNo 
                            inner join DocumentType757 as dt on dt.RefNo = doc.DocType 
                            where aa.RefNo=@RefNo AND doc.DeleteFlag IS NULL";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@RefNo", aaRefNo);
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
        //row["DocRefName"] = docRefName;
        row["DocName"] = onlyFileNameWithExtn;
        row["DocPath"] = filePath;

        // Add the new row to the DataTable
        dt.Rows.Add(row);
    }






    //=========================={ GridView RowDeleting }==========================
    protected void Grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridView gridView = (GridView)sender;

        // item gridview
        if (gridView.ID == "itemGridxxx")
        {
            int rowIndex = e.RowIndex;

            DataTable dt = ViewState["ItemDetails_VS"] as DataTable;

            if (dt != null && dt.Rows.Count > rowIndex)
            {
                dt.Rows.RemoveAt(rowIndex);

                ViewState["ItemDetails_VS"] = dt;
                Session["ItemDetails"] = dt;

                //itemGrid.DataSource = dt;
                //itemGrid.DataBind();

                //// re-calculating total amount n assigning back to textbox
                //double? totalBillAmount = dt.AsEnumerable().Sum(row => row["Amount"] is DBNull ? (double?)null : Convert.ToDouble(row["Amount"])) ?? 0.0;
                //txtBillAmount.Text = totalBillAmount.HasValue ? totalBillAmount.Value.ToString("N2") : "0.00";

                // re-calculating taxes
                //FillTaxHead();
            }
        }

        // document gridview delete
        if (gridView.ID == "GridDocument")
        {
            int rowIndex = e.RowIndex;

            DataTable dt = ViewState["DocDetails_VS"] as DataTable;

            if (dt != null && dt.Rows.Count > rowIndex)
            {
                string userID = Session["UserID"].ToString();

                // updating record to have column: DeleteFlag == TRUE
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "UPDATE AADocuments757 SET DeleteFlag=@DeleteFlag, DeleteDate=@DeleteDate, DeleteBy=@DeleteBy where RefNo = @RefNo";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@DeleteFlag", "TRUE");
                    cmd.Parameters.AddWithValue("@DeleteDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DeleteBy", userID);
                    cmd.Parameters.AddWithValue("@RefNo", dt.Rows[rowIndex]["RefNo"].ToString());
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                // removing record from the gridview
                dt.Rows.RemoveAt(rowIndex);

                ViewState["DocDetails_VS"] = dt;
                Session["DocUploadDT"] = dt;

                GridDocument.DataSource = dt;
                GridDocument.DataBind();
            }
        }
    }






    //=========================={ Submit Button Click Event }==========================
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdministrativeApproval.aspx");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (GridDocument.Rows.Count > 0)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();

                try
                {
                    string aaRefNo = Session["AdminApproveRefNo"].ToString();

                    // update header
                    UpdateHeader(con, transaction, aaRefNo);

                    // update documents
                    UpdateDocument(con, transaction, aaRefNo);

                    if (transaction.Connection != null) transaction.Commit();

                    getSweetAlertSuccessRedirectMandatory("Updated!", $"The Administrative Approval Updated Successfully", "AdministrativeApproval.aspx");
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
            getSweetAlertErrorMandatory("No Document Found", $"Kindly Minimum 1 Payment Related Document");
        }
    }

    private void UpdateHeader(SqlConnection con, SqlTransaction transaction, string aaRefNo)
    {
        // logged-in userID
        string userID = Session["UserID"].ToString();

        string adminapproveNo = AANumber.Text;
        DateTime adminapproveDate = DateTime.Parse(AADate.Text);
        string adminapproveTitle = AATitle.Text;

        double sanctionedAmount = Convert.ToDouble(SanctionAmount.Text);
        DateTime sanctionedDate = DateTime.Parse(SanctionDate.Text);
        string sourceOfBudget = SourceOfBudget.Text;



        // combining selected category refIDs into comma seperated string
        AAForItemCategory.Items[0].Selected = false;

        List<string> itemCategorylist = new List<string>();

        foreach (ListItem li in AAForItemCategory.Items)
        {
            if (li.Selected == true)
            {
                itemCategorylist.Add(li.Value);
            }
        }

        string selectedItemsCategoryRefIDs = string.Join(",", itemCategorylist);



        // SQL update query
        string sql = $@"Update AAMaster757 SET 
                        AANumber=@AANumber, AADate=@AADate, AATitle=@AATitle, AAFor=@AAFor, SanctionAmount=@SanctionAmount, 
                        SanctionDate=@SanctionDate, SourceOfBudget=@SourceOfBudget
                        WHERE RefNo=@RefNo";
        SqlCommand cmd = new SqlCommand(sql, con, transaction);

        cmd.Parameters.AddWithValue("@AANumber", adminapproveNo);
        cmd.Parameters.AddWithValue("@AADate", adminapproveDate);
        cmd.Parameters.AddWithValue("@AATitle", adminapproveTitle);
        cmd.Parameters.AddWithValue("@AAFor", selectedItemsCategoryRefIDs);
        cmd.Parameters.AddWithValue("@SanctionAmount", sanctionedAmount);
        cmd.Parameters.AddWithValue("@SanctionDate", sanctionedDate);
        cmd.Parameters.AddWithValue("@SourceOfBudget", sourceOfBudget);
        cmd.Parameters.AddWithValue("@RefNo", aaRefNo);
        cmd.ExecuteNonQuery();

        //SqlDataAdapter ad = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        //ad.Fill(dt);
    }

    private void UpdateDocument(SqlConnection con, SqlTransaction transaction, string aaRefNo)
    {
        string userID = Session["UserId"].ToString();

        DataTable documentsDT = (DataTable)Session["DocUploadDT"];

        foreach (GridViewRow row in GridDocument.Rows)
        {
            int rowIndex = row.RowIndex;

            string docType = documentsDT.Rows[rowIndex]["DocType"].ToString();
            //string docRefName = documentsDT.Rows[rowIndex]["DocRefName"].ToString();
            string docName = documentsDT.Rows[rowIndex]["DocName"].ToString();

            HyperLink hypDocPath = (HyperLink)row.FindControl("DocPath");
            string docPath = hypDocPath.NavigateUrl;

            // checking for documnet existance
            string docRefNo = documentsDT.Rows[rowIndex]["RefNo"].ToString();

            bool isDocExist = checkForDocuUploadedExist(docRefNo, con, transaction);

            // getting aaNo refNo
            //DataTable AdministrativeApprovalDT = GetAADT(aaNo, con, transaction);
            //string aaRefNo = AdministrativeApprovalDT.Rows[0]["RefNo"].ToString();

            if (!isDocExist) // insert
            {
                string docRefNo_New = GetNewDocumentRefNo(con, transaction);
                // sp
                //string sql = $@"SP_InsertAADocuments";
                string sql = $@"Insert into AADocuments757 
                                (RefNo, AARefNo, DocType, DocName, DocPath, SaveBy) 
                                Values 
                                (@RefNo, @AARefNo, @DocType, @DocName, @DocPath, @SaveBy)";

                SqlCommand cmd = new SqlCommand(sql, con, transaction);

                cmd.Parameters.AddWithValue("@RefNo", docRefNo_New);
                cmd.Parameters.AddWithValue("@AARefNo", aaRefNo);
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



}