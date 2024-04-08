using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using System.Data.SqlTypes;
using System.IO;

public partial class AA_Approval_AAVerification : System.Web.UI.Page
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

                AANoAndTitle_DropDown();
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




    //=========================={ Binding Dropdowns }==========================

    private void AdministrativeAprovalNo_Bind_Dropdown()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = $@"select Distinct mas.AANumber, mas.RefNo, CONCAT(mas.AANumber, ' - ', mas.AATitle) as AANoTitle 
                            from AAMaster757 as mas
                            inner join AAItem757 as item on item.AARefNo = mas.RefNo";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            ddScAANo.DataSource = dt;
            ddScAANo.DataTextField = "AANoTitle";
            ddScAANo.DataValueField = "RefNo";
            ddScAANo.DataBind();
            ddScAANo.Items.Insert(0, new ListItem("------- Select A.A. No. - Title -------", "0"));
        }
    }

    private void AANoAndTitle_DropDown()
    {
        string userID = Session["UserId"].ToString();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select RefNo, AANumber, AATitle, CONCAT(AANumber, '  -  ', AATitle) AS AAnoTitle from AAMaster757";
            SqlCommand cmd = new SqlCommand(sql, con);
            //cmd.Parameters.AddWithValue("@SaveBy", userID);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            AdminApproveNo.DataSource = dt;
            AdminApproveNo.DataTextField = "AAnoTitle";
            AdminApproveNo.DataValueField = "RefNo";
            AdminApproveNo.DataBind();
            AdminApproveNo.Items.Insert(0, new ListItem("------Select A.A. Number & A.A. Title------", "0"));
        }
    }



    //=========================={ Fetch Data }==========================
    private DataTable GetAdminstrativeApprovalDT(string adminApprovalRefNo)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from AAMaster757 where RefNo = @RefNo";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@RefNo", adminApprovalRefNo);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            return dt;
        }
    }

    private string GetAAItemsRefNo(SqlConnection con, SqlTransaction transaction)
    {
        string nextRefNo = "1000001";

        string sql = "SELECT ISNULL(MAX(CAST(RefNo AS INT)), 1000000) + 1 AS NextRefNo FROM AAItem757";
        SqlCommand cmd = new SqlCommand(sql, con, transaction);

        SqlDataAdapter ad = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        ad.Fill(dt);

        if (dt.Rows.Count > 0) return dt.Rows[0]["NextRefNo"].ToString();
        else return nextRefNo;
    }

    private string GetVerificationRefNo(SqlConnection con, SqlTransaction transaction)
    {
        string nextRefNo = "1000001";

        string sql = "SELECT ISNULL(MAX(CAST(RefNo AS INT)), 1000000) + 1 AS NextRefNo FROM AAVerification757";
        SqlCommand cmd = new SqlCommand(sql, con, transaction);

        SqlDataAdapter ad = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        ad.Fill(dt);

        if (dt.Rows.Count > 0) return dt.Rows[0]["NextRefNo"].ToString();
        else return nextRefNo;
    }





    //=========================={ Search Button Event }==========================
    protected void btnNewBill_Click(object sender, EventArgs e)
    {
        Response.Redirect("AAItemsNew.aspx");
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
        string aaRefNo = (adminApprovalDT.Rows.Count > 0) ? adminApprovalDT.Rows[0]["RefNo"].ToString() : string.Empty;

        DataTable searchResultDT = SearchRecords(aaRefNo, fromDate, toDate);

        // binding the search grid
        gridSearch.DataSource = searchResultDT;
        gridSearch.DataBind();

        //Required for jQuery DataTables to work.
        gridSearch.UseAccessibleHeader = true;
        gridSearch.HeaderRow.TableSection = TableRowSection.TableHeader;
        //gridSearch.FooterRow.TableSection = TableRowSection.TableHeader;

        Session["PaginationDataSource"] = searchResultDT;
    }

    public DataTable SearchRecords(string aaRefNo, DateTime fromDate, DateTime toDate)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string sql = $@"select Distinct aa.RefNo , aa.AANumber, aa.AADate, aa.AATitle, aa.SanctionAmount, aa.SanctionDate, budget.BudgetName, 
                            (select count(*) from AAItem757 as item where item.AARefNo = aa.RefNo AND item.DeleteFlag IS NULL) as AAItemCount, 
                            case when (verify.VerificationStatus = 'TRUE') then 'Approved' else 'Pending' end as verifyStatus 
                            from AAMaster757 as aa 
                            inner join AAItem757 as item on item.AARefNo = aa.RefNo 
                            left join SourceOfBudget757 budget on budget.refid = aa.SourceOfBudget 
                            left join ItemCategory757 as ic on ic.RefId = aa.AAFor 
                            left join AAVerification757 as verify on verify.AARefNo = aa.RefNo 
                            WHERE 1=1";

            if (!string.IsNullOrEmpty(aaRefNo))
            {
                sql += " AND aa.RefNo = @RefNo";
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
                if (!string.IsNullOrEmpty(aaRefNo))
                {
                    command.Parameters.AddWithValue("@RefNo", aaRefNo);
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

            // verifying existing approved AA
            CheckApprovedAA(aaRefNo);

            // fill header
            AutoFilHeader(aaRefNo);

            // fill items
            AutoFilItems(aaRefNo);
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

            //ApprovalStatus.DataSource = dt;
            //ApprovalStatus.DataTextField = "AAnoTitle";
            //ApprovalStatus.DataValueField = "VerificationStatus";
            //ApprovalStatus.DataBind();
            ApprovalStatus.Items.Insert(0, new ListItem("------Select Approval Status------", "0"));

            if (dt.Rows.Count > 0)
            {
                Session["ExistingVerification"] = dt;

                VerificationRemark.Value = dt.Rows[0]["VerificationRemark"].ToString();

                // clearing existing selections
                ApprovalStatus.ClearSelection();

                string verifyStatus = dt.Rows[0]["VerificationStatus"].ToString();

                foreach (ListItem item in ApprovalStatus.Items)
                {
                    if (item.Value == verifyStatus)
                    {
                        item.Selected = true;
                        break;
                    }
                }

                if (dt.Rows[0]["VerificationStatus"].ToString() == "TRUE")
                {
                    VerificationRemark.Disabled = true;
                    btnSubmit.Visible = false;

                    string aaNo = dt.Rows[0]["AANumber"].ToString();

                    getSweetAlertInfo("Approved!", $"The A.A.: {aaNo} Has Been Already Approved");
                }
            }
            else
            {
                Session["ExistingVerification"] = null;
            }
        }
    }

    private void AutoFilHeader(string aaRefNo)
    {
        itemDiv.Visible = true;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select RefNo, AANumber, AATitle, CONCAT(AANumber, '  -  ', AATitle) AS AAnoTitle from AAMaster757 where RefNo = @RefNo";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@RefNo", aaRefNo);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            AdminApproveNo.DataSource = dt;
            AdminApproveNo.DataTextField = "AAnoTitle";
            AdminApproveNo.DataValueField = "RefNo";
            AdminApproveNo.DataBind();
            AdminApproveNo.Items.Insert(0, new ListItem("------Select A.A. Number & A.A. Title------", "0"));

            if (AdminApproveNo.SelectedIndex < 2) AdminApproveNo.SelectedIndex = 1;
        }
    }

    private void AutoFilItems(string aaRefNo)
    {
        itemDiv.Visible = true;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = $@"select aa.*, cat.CategoryName as ItemCategoryText, subcat.SubCategory as ItemSubCategoryText, 
                            item.ItemName as ItemNameText, uom.UnitName as ItemUOMText
                            from AAItem757 as aa
                            inner join ItemCategory757 as cat on cat.RefID = aa.ItemCategory 
                            inner join ItemSubCategory757 as subcat on subcat.RefID = aa.ItemSubCategory 
                            inner join UnitOfMeasurement757 as uom on uom.RefID = aa.ItemUOM 
                            inner join ItemMaster757 as item on item.RefID = aa.ItemName 
                            where aa.AARefNo = @AARefNo AND aa.DeleteFlag IS NULL";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@AARefNo", aaRefNo);
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



    //=========================={ Submit Button Click Event }==========================
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("AAVerification.aspx");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (itemGrid.Rows.Count > 0)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();

                try
                {
                    DataTable exisintingVerificationDT = (DataTable)Session["ExistingVerification"];

                    string approveStatus = ApprovalStatus.SelectedValue;
                    if (approveStatus == "TRUE")
                    {
                        // updating aa items approval status
                        UpdatingAAItemsApprovalStatus(con, transaction);
                    }

                    if (exisintingVerificationDT != null)
                    {
                        // there is existing record, update
                        UpdateVerificationRecord(con, transaction);
                    }
                    else
                    {
                        // no existing verification record, insert
                        InsertAAVerification(con, transaction);
                    }


                    if (transaction.Connection != null) transaction.Commit();

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
            getSweetAlertErrorMandatory("No Items Found!", "Kindly Add / Upload Some Items To Proceed Further");
        }
    }


    private void UpdatingAAItemsApprovalStatus(SqlConnection con, SqlTransaction transaction)
    {
        DataTable dt = (DataTable)Session["ItemDetails"];

        if (dt.Rows.Count > 0)
        {
            // administrative approval (DD) ref no
            string adminApproveRefNo = AdminApproveNo.SelectedValue;
            string approveStatus = ApprovalStatus.SelectedValue;


            foreach (DataRow row in dt.Rows)
            {
                int rowIndex = dt.Rows.IndexOf(row);

                string sql = $@"Update AAItem757 set IsVerified = @IsVerified Where RefNo = @RefNo";

                SqlCommand cmd = new SqlCommand(sql, con, transaction);
                cmd.Parameters.AddWithValue("@IsVerified", approveStatus);
                cmd.Parameters.AddWithValue("@RefNo", row["RefNo"]);
                cmd.ExecuteNonQuery();

                //SqlDataAdapter ad = new SqlDataAdapter(cmd);
                //DataTable dt = new DataTable();
                //ad.Fill(dt);
            }
        }
    }

    private void UpdateVerificationRecord(SqlConnection con, SqlTransaction transaction)
    {
        DataTable exisintingVerificationDT = (DataTable)Session["ExistingVerification"];

        string refNo = exisintingVerificationDT.Rows[0]["RefNo"].ToString();

        string verifyRemark = VerificationRemark.Value;
        string verifyStatus = ApprovalStatus.SelectedValue;

        string sql = $@"UPDATE AAVerification757 SET VerificationRemark=@VerificationRemark, VerificationStatus=@VerificationStatus 
                        Where RefNo=@RefNo";

        SqlCommand cmd = new SqlCommand(sql, con, transaction);
        cmd.Parameters.AddWithValue("@VerificationRemark", verifyRemark);
        cmd.Parameters.AddWithValue("@VerificationStatus", verifyStatus);
        cmd.Parameters.AddWithValue("@RefNo", refNo);
        int k = cmd.ExecuteNonQuery();

        //SqlDataAdapter ad = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        //ad.Fill(dt);

        if (k > 0)
        {
            getSweetAlertSuccessRedirectMandatory("A.A. Verification Done!", $"The Following Items Have Been Approved", "AAVerification.aspx");
        }
    }

    private void InsertAAVerification(SqlConnection con, SqlTransaction transaction)
    {
        string userID = Session["UserId"].ToString();

        string verificationRefNo_New = GetVerificationRefNo(con, transaction);

        string aaRefNo = Session["AdminApproveRefNo"].ToString();
        string verifyRemark = VerificationRemark.Value;
        string verifyStatus = ApprovalStatus.SelectedValue;

        string sql = $@"INSERT INTO AAVerification757 
                        (RefNo, AARefNo, VerificationRemark, VerificationStatus, SaveBy) 
                        VALUES 
                        (@RefNo, @AARefNo, @VerificationRemark, @VerificationStatus, @SaveBy)";

        SqlCommand cmd = new SqlCommand(sql, con, transaction);
        cmd.Parameters.AddWithValue("@RefNo", verificationRefNo_New);
        cmd.Parameters.AddWithValue("@AARefNo", aaRefNo);
        cmd.Parameters.AddWithValue("@VerificationRemark", verifyRemark);
        cmd.Parameters.AddWithValue("@VerificationStatus", verifyStatus);
        cmd.Parameters.AddWithValue("@SaveBy", userID);
        int k = cmd.ExecuteNonQuery();

        //SqlDataAdapter ad = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        //ad.Fill(dt);

        if (k > 0 && ApprovalStatus.SelectedValue == "TRUE")
        {
            getSweetAlertSuccessRedirectMandatory("A.A. Verification Done!", $"The Following Items Have Been Approved", "AAVerification.aspx");
        }
        else
        {
            getSweetAlertSuccessRedirectMandatory("A.A. Verification Pending!", $"The Following Items Have Not Been Approved Yet", "AAVerification.aspx");
        }
    }



}