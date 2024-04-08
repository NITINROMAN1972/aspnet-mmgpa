using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tendor_BOM_UploadTendorBOM : System.Web.UI.Page
{
    string connectionString = ConfigurationManager.ConnectionStrings["Ginie"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        // Project: MMGPA
        // Code: 757

        Session["UserId"] = "10223"; // client - milind

        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                TendorAndTitle_DropDown();
                AANoAndTitle_DropDown();
            }
        }
        else
        {
            getSweetHTML("Not Signed-In!", "Kindly <strong>Sign-In</strong> To Access The Porject <br/> By Clicking On The <strong><i>Login</i></strong> Button Above");
            loginDiv.Visible = false;
        }
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

    private void TendorAndTitle_DropDown()
    {
        string userID = Session["UserId"].ToString();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = $@"select RefID, TenNo, TenTitle, CONCAT(TenNo, '  -  ', TenTitle) AS TendorNoTitle from TenderDetails757";

            SqlCommand cmd = new SqlCommand(sql, con);
            //cmd.Parameters.AddWithValue("@SaveBy", userID);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            TendorNo.DataSource = dt;
            TendorNo.DataTextField = "TendorNoTitle";
            TendorNo.DataValueField = "RefID";
            TendorNo.DataBind();
            TendorNo.Items.Insert(0, new ListItem("------Select Tendor Number & Title------", "0"));


        }
    }

    private void AANoAndTitle_DropDown()
    {
        string userID = Session["UserId"].ToString();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = $@"select Distinct am.RefNo, am.AANumber, am.AATitle, CONCAT(am.AANumber, '  -  ', am.AATitle) AS AAnoTitle 
                            from AAMaster757 as am 
                            inner join AAItem757 as ai on ai.AARefNo = am.RefNo";

            SqlCommand cmd = new SqlCommand(sql, con);
            //cmd.Parameters.AddWithValue("@SaveBy", userID);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            AANo.DataSource = dt;
            AANo.DataTextField = "AAnoTitle";
            AANo.DataValueField = "RefNo";
            AANo.DataBind();
            //AANo.Items.Insert(0, new ListItem("------Select A.A. Number & A.A. Title------", "0"));

            ListItem selectValuesItem = AANo.Items.FindByValue("0");

            if (selectValuesItem != null)
            {
                selectValuesItem.Selected = true;
            }
        }
    }





    //=========================={ Drop Down Event }==========================
    protected void AANo_SelectedIndexChanged(object sender, EventArgs e)
    {
        // de-selecting initial heading list item
        //AANo.Items[0].Selected = false;

        // creating list for storing items
        List<string> selectedAARefNo = new List<string>();

        foreach (ListItem li in AANo.Items)
        {
            if (li.Selected == true)
            {
                selectedAARefNo.Add(li.Value);
            }
        }

        try
        {
            if (AANo.SelectedIndex == -1)
            {
                // no list is selected or selected

                itemDiv.Visible = false;

                // clearing items gridview
                itemGrid.DataSource = null;
                itemGrid.DataBind();

                // clering the items session
                ViewState["ItemDetails_VS"] = null;
                Session["ItemDetails"] = null;
            }
            else
            {
                // some list are checked or selected

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // CTE wit row_number window function
                    string sql = $@"WITH UniqueItems AS (
	                                    SELECT ai.*, cat.CategoryName AS ItemCategoryText, subcat.SubCategory AS ItemSubCategoryText, 
	                                    item.ItemName AS ItemNameText, uom.UnitName AS ItemUOMText, 
	                                    ROW_NUMBER() OVER (PARTITION BY ai.ItemCategory, ai.ItemSubCategory, ai.ItemName, ai.ItemUOM ORDER BY ai.AARefNo) AS RowNum 
	                                    FROM AAItem757 AS ai 
	                                    INNER JOIN AAMaster757 AS am ON am.RefNo = ai.AARefNo
	                                    INNER JOIN ItemCategory757 AS cat ON cat.RefID = ai.ItemCategory 
	                                    INNER JOIN ItemSubCategory757 AS subcat ON subcat.RefID = ai.ItemSubCategory 
	                                    INNER JOIN UnitOfMeasurement757 AS uom ON uom.RefID = ai.ItemUOM 
	                                    INNER JOIN ItemMaster757 AS item ON item.RefID = ai.ItemName 
	                                    WHERE am.RefNo IN ({string.Join(",", selectedAARefNo.Select(AARefNo => $"'{AARefNo}'"))}) AND ai.DeleteFlag IS NULL 
                                    )
                                    SELECT * FROM UniqueItems WHERE RowNum = 1";


                    SqlCommand cmd = new SqlCommand(sql, con);
                    //cmd.Parameters.AddWithValue("@RefNo", billNo);
                    cmd.ExecuteNonQuery();

                    SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);

                    con.Close();

                    if (dt.Rows.Count > 0)
                    {
                        itemDiv.Visible = true;

                        if (!dt.Columns.Contains("CheckStatus"))
                        {
                            // adding the new column with checkboxes
                            DataColumn checkboxColumn = new DataColumn("CheckStatus", typeof(bool));
                            checkboxColumn.DefaultValue = true;
                            dt.Columns.Add(checkboxColumn);
                        }

                        if (!dt.Columns.Contains("CheckQty"))
                        {
                            // adding the new column 
                            DataColumn CheckQty = new DataColumn("CheckQty", typeof(string));
                            dt.Columns.Add(CheckQty);
                        }

                        itemGrid.DataSource = dt;
                        itemGrid.DataBind();

                        ViewState["ItemDetails_VS"] = dt;
                        Session["ItemDetails"] = dt;

                        // sum of total bill amount
                        //double? totalBillAmount = dt.AsEnumerable().Sum(row => row["NetAmount"] is DBNull ? (double?)null : Convert.ToDouble(row["NetAmount"])) ?? 0.0;
                        //txtBillAmount.Text = totalBillAmount.HasValue ? totalBillAmount.Value.ToString("N2") : "0.00";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            getSweetAlertErrorMandatory("Oops!", $"{ex.Message}");
        }
    }


    //=========================={ Check Available Qty Event }==========================
    protected void CheckQty_Click(object sender, EventArgs e)
    {

    }








    //=========================={ Submit Button Click Event }==========================
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("UploadTendorBOM.aspx");
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
                    // inserting header
                    //InsertAAItems(con, transaction);

                    if (transaction.Connection != null) transaction.Commit();

                    getSweetAlertSuccessRedirectMandatory("Item Uploaded Successfully!", $"The Following Items Successfully Uploaded For Administrative Approval:", "UploadTendorBOM.aspx");
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



}