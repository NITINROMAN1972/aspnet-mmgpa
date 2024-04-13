using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Odbc;

public partial class Administrative_Approval_AdministrativeApprovalNew : System.Web.UI.Page
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
                // adminitrative approval & sanctioned date
                AADate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                SanctionDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

                Burreau_DropDown();
                DocumentType_DropDown();
                SourceOfBudget_DropDown();
                AAForItemCategory_DropDown();
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

    //sweet alert - success redirect
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



    //=========================={ GridView RowDeleting }==========================
    protected void Grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridView gridView = (GridView)sender;

        // item gridview
        if (gridView.ID == "itemGridxx")
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
                dt.Rows.RemoveAt(rowIndex);

                ViewState["DocDetails_VS"] = dt;
                Session["DocUploadDT"] = dt;

                GridDocument.DataSource = dt;
                GridDocument.DataBind();
            }
        }
    }




    //=========================={ Binding Search Dropdowns }==========================
    private void Burreau_DropDown()
    {
        string userID = Session["UserId"].ToString();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from Buro757";
            SqlCommand cmd = new SqlCommand(sql, con);
            //cmd.Parameters.AddWithValue("@SaveBy", userID);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            AABureau.DataSource = dt;
            AABureau.DataTextField = "Bureau";
            AABureau.DataValueField = "BuroID";
            AABureau.DataBind();
            AABureau.Items.Insert(0, new ListItem("------Select Bureau------", "0"));
        }
    }

    private void DocumentType_DropDown()
    {
        string userID = Session["UserId"].ToString();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from DocumentType757 Where DocumentType = 'AA'";
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
        string userID = Session["UserId"].ToString();

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
        string userID = Session["UserId"].ToString();

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
            //AAForItemCategory.Items.Insert(0, new ListItem("------Select Item Category------", "0"));

            //ListItem selectValuesItem = AAForItemCategory.Items.FindByValue("0");

            //if (selectValuesItem != null)
            //{
            //    selectValuesItem.Selected = true;
            //}
        }
    }



    //=========================={ Fetch Data }==========================
    private string GetAdministrativeApproveRefNo(SqlConnection con, SqlTransaction transaction)
    {
        string nextRefNo = "1000001";

        string sql = "SELECT ISNULL(MAX(CAST(RefNo AS INT)), 1000000) + 1 AS NextRefNo FROM AAMaster757";
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

        string sql = "SELECT ISNULL(MAX(CAST(RefNo AS INT)), 1000000) + 1 AS NextRefNo FROM AADocuments757";
        SqlCommand cmd = new SqlCommand(sql, con, transaction);

        SqlDataAdapter ad = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        ad.Fill(dt);

        if (dt.Rows.Count > 0) return dt.Rows[0]["NextRefNo"].ToString();
        else return nextRefNo;
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
                    // adminstrative approve number
                    string aaNo = AANumber.Text.ToString();

                    // inserting header
                    InsertAdministrativeApproval(con, transaction);

                    // inserting document
                    string AaHeaderRefNo = Session["AdminApproveRefNo"].ToString();
                    InsertDocuments(con, transaction, AaHeaderRefNo);

                    if (transaction.Connection != null) transaction.Commit();

                    getSweetAlertSuccessRedirectMandatory("Administrative Approval Created!", $"The Administrative Approval Number: {aaNo}, Created Successfully", "AdministrativeApproval.aspx");
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



    private void InsertAdministrativeApproval(SqlConnection con, SqlTransaction transaction)
    {
        // logged-in userID
        string userID = Session["UserID"].ToString();

        string adminapproveNo = AANumber.Text;
        DateTime adminapproveDate = DateTime.Parse(AADate.Text);
        string adminapproveTitle = AATitle.Text;

        double sanctionedAmount = Convert.ToDouble(SanctionAmount.Text);
        DateTime sanctionedDate = DateTime.Parse(SanctionDate.Text);
        string sourceOfBudget = SourceOfBudget.Text;

        string aaBureau = AABureau.SelectedValue;

        // fetching new administrative approve reference number
        string aaRefNo = GetAdministrativeApproveRefNo(con,transaction);
        Session["AdminApproveRefNo"] = aaRefNo;


        // combining selected category refIDs into comma seperated string

        //AAForItemCategory.Items[0].Selected = false;

        List<string> itemCategorylist = new List<string>();
        
        foreach (ListItem li in AAForItemCategory.Items)
        {
            if (li.Selected == true)
            {
                itemCategorylist.Add(li.Value);
            }
        }

        string selectedItemsCategoryRefIDs = string.Join(",", itemCategorylist);

        // SQL query
        string sql = $@"SP_InsertAAMaster";
        SqlCommand cmd = new SqlCommand(sql, con, transaction);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@RefNo", aaRefNo);
        cmd.Parameters.AddWithValue("@AANumber", adminapproveNo);
        cmd.Parameters.AddWithValue("@AADate", adminapproveDate);
        cmd.Parameters.AddWithValue("@AATitle", adminapproveTitle);
        cmd.Parameters.AddWithValue("@AAFor", selectedItemsCategoryRefIDs);
        cmd.Parameters.AddWithValue("@SanctionAmount", sanctionedAmount);
        cmd.Parameters.AddWithValue("@SanctionDate", sanctionedDate);
        cmd.Parameters.AddWithValue("@SourceOfBudget", sourceOfBudget);
        cmd.Parameters.AddWithValue("@AABureau", aaBureau);
        cmd.Parameters.AddWithValue("@SaveBy", userID);
        cmd.ExecuteNonQuery();

        //SqlDataAdapter ad = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        //ad.Fill(dt);
    }

    private void InsertDocuments(SqlConnection con, SqlTransaction transaction, string aaRefNo)
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


            // sp
            string sql = $@"SP_InsertAADocuments";

            SqlCommand cmd = new SqlCommand(sql, con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;

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