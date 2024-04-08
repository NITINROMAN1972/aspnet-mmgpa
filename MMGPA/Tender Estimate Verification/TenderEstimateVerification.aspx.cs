using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tender_Verification_TenderVerification : System.Web.UI.Page
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
                EstimateNo_Bind_Dropdown();
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

    private void EstimateNo_Bind_Dropdown()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = $@"select DISTINCT EstimateNo from TenderBOM757";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            ddScEstimateNo.DataSource = dt;
            ddScEstimateNo.DataTextField = "EstimateNo";
            ddScEstimateNo.DataValueField = "EstimateNo";
            ddScEstimateNo.DataBind();
            ddScEstimateNo.Items.Insert(0, new ListItem("------- Select Estimate No. -------", "0"));
        }
    }



    //=========================={ Fetch Data }==========================
    private DataTable GetAdminstrativeApprovalDT(string estimateNo)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select DISTINCT EstimateNo from TenderBOM757 where EstimateNo = @EstimateNo";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@EstimateNo", estimateNo);
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

        string sql = "SELECT ISNULL(MAX(CAST(RefNo AS INT)), 1000000) + 1 AS NextRefNo FROM EstimateVerification757";
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
        Response.Redirect("TenderEstimateVerification.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGridView();
    }

    private void BindGridView()
    {
        searchGridDiv.Visible = true;

        string estimateNoStr = ddScEstimateNo.SelectedValue; // estimate no

        DateTime fromDate;
        DateTime toDate;

        if (!DateTime.TryParse(ScFromDate.Text, out fromDate)) { fromDate = SqlDateTime.MinValue.Value; }
        if (!DateTime.TryParse(ScToDate.Text, out toDate)) { toDate = SqlDateTime.MaxValue.Value; }

        // DTs
        DataTable estimateDT = GetAdminstrativeApprovalDT(estimateNoStr);

        // dt values
        string estimateNo = (estimateDT.Rows.Count > 0) ? estimateDT.Rows[0]["EstimateNo"].ToString() : string.Empty;

        DataTable searchResultDT = SearchRecords(estimateNo, fromDate, toDate);

        // binding the search grid
        gridSearch.DataSource = searchResultDT;
        gridSearch.DataBind();

        //Required for jQuery DataTables to work.
        gridSearch.UseAccessibleHeader = true;
        gridSearch.HeaderRow.TableSection = TableRowSection.TableHeader;
        //gridSearch.FooterRow.TableSection = TableRowSection.TableHeader;

        Session["PaginationDataSource"] = searchResultDT;
    }

    public DataTable SearchRecords(string estimateNo, DateTime fromDate, DateTime toDate)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string sql = $@"WITH NumberedRows AS (
                                SELECT EstimateNo, EstimateDate, Concat(N'₹ ', (Format(BasicAmount, 'N', 'en-IN'))) as BasicAmount, 
                                Case When (select count(*) from EstimateVerification757 as est where est.EstimateNo = tb.EstimateNo) > 0 
                                Then 'Verified' Else 'Pending' end as EstimateVerified, 
	                            (select count(*) from TenderBOM757 as tb Where tb.EstimateNo = '{estimateNo}' AND tb.DeleteFlag IS NULL) as ItemCount, 
                                ROW_NUMBER() OVER (PARTITION BY EstimateNo ORDER BY EstimateNo) AS RowNum 
                                FROM TenderBOM757 as tb 
                            )
                            SELECT * 
                            FROM NumberedRows
                            WHERE 1=1";

            if (!string.IsNullOrEmpty(estimateNo))
            {
                sql += " AND EstimateNo = @EstimateNo";
            }

            if (fromDate != null)
            {
                sql += " AND EstimateDate >= @FromDate";
            }

            if (toDate != null)
            {
                sql += " AND EstimateDate <= @ToDate";
            }
            
            sql += " AND RowNum = 1 ORDER BY EstimateNo DESC";
            //sql += " AND aa.RefNo=@AANumber ORDER BY RefNo DESC";




            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                if (!string.IsNullOrEmpty(estimateNo))
                {
                    command.Parameters.AddWithValue("@EstimateNo", estimateNo);
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
            string estimateNo = (e.CommandArgument).ToString();
            Session["EstimateNo"] = estimateNo;

            searchGridDiv.Visible = false;
            divTopSearch.Visible = false;
            UpdateDiv.Visible = true;

            // verifying existing approved AA
            CheckApprovedAA(estimateNo);

            // fill header
            AutoFilHeader(estimateNo);

            // fill items
            AutoFilItems(estimateNo);
        }
    }

    private void CheckApprovedAA(string estimateNo)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = $@"select verify.* 
                            from EstimateVerification757 as verify
                            inner join TenderBOM757 as tb on tb.EstimateNo = verify.EstimateNo
                            where verify.EstimateNo = @EstimateNo";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@EstimateNo", estimateNo);
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

                    string estimateNoi = dt.Rows[0]["EstimateNo"].ToString();

                    getSweetAlertInfo("Approved!", $"The Tender Estimation: {estimateNoi} Has Been Already Verified");
                }
            }
            else
            {
                Session["ExistingVerification"] = null;
            }
        }
    }

    private void AutoFilHeader(string estimateNo)
    {
        itemDiv.Visible = true;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = $@"select TOP 1 EstimateNo, EstimateDate from TenderBOM757 where EstimateNo =  @EstimateNo";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@EstimateNo", estimateNo);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            if(dt.Rows.Count > 0)
            {
                EstimateNo.Text = dt.Rows[0]["EstimateNo"].ToString();

                DateTime estimateDate = DateTime.Parse(dt.Rows[0]["EstimateDate"].ToString());
                EstimateDate.Text = estimateDate.ToString("yyyy-MM-dd");
            }
        }
    }

    private void AutoFilItems(string estimateNo)
    {
        itemDiv.Visible = true;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = $@"select tb.*, cat.CategoryName as ItemCategoryText, subcat.SubCategory as ItemSubCategoryText, 
                            item.ItemName as ItemNameText, uom.UnitName as ItemUOMText
                            from TenderBOM757 as tb
                            inner join ItemCategory757 as cat on cat.RefID = tb.ItemCategory 
                            inner join ItemSubCategory757 as subcat on subcat.RefID = tb.ItemSubCategory 
                            inner join UnitOfMeasurement757 as uom on uom.RefID = tb.ItemUOM 
                            inner join ItemMaster757 as item on item.RefID = tb.ItemName 
                            where tb.EstimateNo = @EstimateNo AND tb.DeleteFlag IS NULL";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@EstimateNo", estimateNo);
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
        Response.Redirect("TenderEstimateVerification.aspx");
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
                        UpdatingEstimateVerificationStatus(con, transaction);
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


    private void UpdatingEstimateVerificationStatus(SqlConnection con, SqlTransaction transaction)
    {
        DataTable dt = (DataTable)Session["ItemDetails"];

        if (dt.Rows.Count > 0)
        {
            string approveStatus = ApprovalStatus.SelectedValue;


            foreach (DataRow row in dt.Rows)
            {
                int rowIndex = dt.Rows.IndexOf(row);

                string sql = $@"Update TenderBOM757 set IsVerified = @IsVerified Where RefNo = @RefNo";

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

        string sql = $@"UPDATE EstimateVerification757 SET VerificationRemark=@VerificationRemark, VerificationStatus=@VerificationStatus 
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
            getSweetAlertSuccessRedirectMandatory("A.A. Verification Done!", $"The Following Items Have Been Approved", "TenderEstimateVerification.aspx");
        }
    }

    private void InsertAAVerification(SqlConnection con, SqlTransaction transaction)
    {
        string userID = Session["UserId"].ToString();

        string verificationRefNo_New = GetVerificationRefNo(con, transaction);

        string estimateNo = Session["EstimateNo"].ToString();
        string verifyRemark = VerificationRemark.Value;
        string verifyStatus = ApprovalStatus.SelectedValue;

        string sql = $@"INSERT INTO EstimateVerification757  
                        (RefNo, EstimateNo, VerificationRemark, VerificationStatus, SaveBy) 
                        VALUES 
                        (@RefNo, @EstimateNo, @VerificationRemark, @VerificationStatus, @SaveBy)";

        SqlCommand cmd = new SqlCommand(sql, con, transaction);
        cmd.Parameters.AddWithValue("@RefNo", verificationRefNo_New);
        cmd.Parameters.AddWithValue("@EstimateNo", estimateNo);
        cmd.Parameters.AddWithValue("@VerificationRemark", verifyRemark);
        cmd.Parameters.AddWithValue("@VerificationStatus", verifyStatus);
        cmd.Parameters.AddWithValue("@SaveBy", userID);
        int k = cmd.ExecuteNonQuery();

        //SqlDataAdapter ad = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        //ad.Fill(dt);

        if (k > 0 && ApprovalStatus.SelectedValue == "TRUE")
        {
            getSweetAlertSuccessRedirectMandatory("Estimate Verification Done!", $"The Following Items Have Been Verified", "TenderEstimateVerification.aspx");
        }
        else
        {
            getSweetAlertSuccessRedirectMandatory("Estimate Verification Pending!", $"The Following Items Have Not Been Verified Yet", "TenderVerification.aspx");
        }
    }




}