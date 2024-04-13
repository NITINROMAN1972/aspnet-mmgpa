﻿using OfficeOpenXml;
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
using System.Drawing;
using System.Activities.Expressions;
using System.Data.Odbc;

public partial class Tendor_BOM_TendorBOM : System.Web.UI.Page
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

                AANoAndTitle_DropDown();
                ItemCategory_DropDown();
                UnitOfMeasurement_DropDown();

                EstimateNo.Text = "System Generated";
                EstimateDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

                // initially manual entry option
                itemEnterManualDiv.Visible = true;

                // closing item session
                ViewState["ItemDetails_VS"] = null;
                Session["ItemDetails"] = null;
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
    private void ItemCategory_DropDown()
    {
        string userID = Session["UserId"].ToString();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from ItemCategory757";
            SqlCommand cmd = new SqlCommand(sql, con);
            //cmd.Parameters.AddWithValue("@SaveBy", userID);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            ItemCategory.DataSource = dt;
            ItemCategory.DataTextField = "CategoryName";
            ItemCategory.DataValueField = "RefID";
            ItemCategory.DataBind();
            ItemCategory.Items.Insert(0, new ListItem("------Select Item Category------", "0"));
        }
    }

    private void ItemSubCategory_Bind_Dropdown(string itemCategoryID)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from ItemSubCategory757 where ItemCategory = @ItemCategory";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@ItemCategory", itemCategoryID);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            ItemSubCategory.DataSource = dt;
            ItemSubCategory.DataTextField = "SubCategory";
            ItemSubCategory.DataValueField = "RefID";
            ItemSubCategory.DataBind();
            ItemSubCategory.Items.Insert(0, new ListItem("------Select Item Sub Category------", "0"));
        }
    }

    private void Item_Bind_Dropdown(string itemSubCategoryID)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from ItemMaster757 where SubCategory = @SubCategory";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@SubCategory", itemSubCategoryID);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            ItemName.DataSource = dt;
            ItemName.DataTextField = "ItemName";
            ItemName.DataValueField = "RefID";
            ItemName.DataBind();
            ItemName.Items.Insert(0, new ListItem("------Select Item------", "0"));
        }
    }

    private void UnitOfMeasurement_DropDown()
    {
        string userID = Session["UserId"].ToString();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from UnitOfMeasurement757";
            SqlCommand cmd = new SqlCommand(sql, con);
            //cmd.Parameters.AddWithValue("@SaveBy", userID);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            ItemUOM.DataSource = dt;
            ItemUOM.DataTextField = "UnitName";
            ItemUOM.DataValueField = "RefID";
            ItemUOM.DataBind();
            ItemUOM.Items.Insert(0, new ListItem("------ Select U.O.M. ------", "0"));
        }
    }





    //=========================={ Drop Down Event }==========================
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

    protected void ItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        string itemCategoryID = ItemCategory.SelectedValue;

        if (ItemCategory.SelectedValue != "0")
        {
            ItemSubCategory_Bind_Dropdown(itemCategoryID);
        }
        else
        {
            // clearing the items in the dropdown
            ItemSubCategory.Items.Clear();
            ItemSubCategory.Items.Insert(0, new ListItem("------Select Item Sub Category------", "0"));

            ItemName.Items.Clear();
            ItemName.Items.Insert(0, new ListItem("------Select Item------", "0"));

            ItemUOM.ClearSelection();
        }
    }

    protected void ItemSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        string itemSubCategoryID = ItemSubCategory.SelectedValue;

        if (ItemSubCategory.SelectedValue != "0")
        {
            Item_Bind_Dropdown(itemSubCategoryID);
        }
        else
        {
            // clearing the items in the dropdown
            ItemName.Items.Clear();
            ItemName.Items.Insert(0, new ListItem("------Select Item------", "0"));

            ItemUOM.ClearSelection();
        }
    }




    protected void ItemName_SelectedIndexChanged(object sender, EventArgs e)
    {
        string itemRefID = ItemName.SelectedValue;

        if (ItemName.SelectedValue != "0")
        {
            BindItemDetails(itemRefID);
        }
        else
        {
            ItemUOM.ClearSelection();
        }
    }

    private void BindItemDetails(string itemRefID)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = $@"select im.ItemCode, im.UMO, uom.UnitName
                            from ItemMaster757 as im 
                            left join UnitOfMeasurement757 as uom on uom.RefID = im.UMO 
                            Where im.RefID = @RefID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@RefID", itemRefID);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            ItemUOM.ClearSelection();

            string itemUOMRefID = dt.Rows[0]["UMO"].ToString();

            foreach (ListItem item in ItemUOM.Items)
            {
                if (item.Value == itemUOMRefID)
                {
                    item.Selected = true;
                    break;
                }
            }
        }
    }





    //=========================={ Radio Button Event }==========================
    protected void manualRadio_CheckedChanged(object sender, EventArgs e)
    {
        itemEnterManualDiv.Visible = true;
        excelUploadDiv.Visible = false;
        AAMultiCheckDiv.Visible = false;
    }

    protected void excelRadio_CheckedChanged(object sender, EventArgs e)
    {
        excelUploadDiv.Visible = true;
        itemEnterManualDiv.Visible = false;
        AAMultiCheckDiv.Visible = false;
    }

    protected void throughAA_CheckedChanged(object sender, EventArgs e)
    {
        excelUploadDiv.Visible = false;
        itemEnterManualDiv.Visible = false;
        AAMultiCheckDiv.Visible = true;

        //manualRadio.Enabled = false;
        excelRadio.Enabled = false;

        // clearing existing item grid
        //itemDiv.Visible = false;

        // clearing items gridview
        //itemGrid.DataSource = null;
        //itemGrid.DataBind();

        // clering the items session
        //ViewState["ItemDetails_VS"] = null;
        //Session["ItemDetails"] = null;
    }





    //=========================={ Fetch Data }==========================
    private string GetTendorItemsRefNo(SqlConnection con, SqlTransaction transaction)
    {
        string nextRefNo = "1000001";

        string sql = "SELECT ISNULL(MAX(CAST(RefNo AS INT)), 1000000) + 1 AS NextRefNo FROM TenderBOM757";
        SqlCommand cmd = new SqlCommand(sql, con, transaction);

        SqlDataAdapter ad = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        ad.Fill(dt);

        if (dt.Rows.Count > 0) return dt.Rows[0]["NextRefNo"].ToString();
        else return nextRefNo;
    }

    private string GetEstimateNo(SqlConnection con, SqlTransaction transaction)
    {
        string nextRefNo = "1000001";

        string sql = "SELECT ISNULL(MAX(CAST(EstimateNo AS INT)), 1000000) + 1 AS NextRefNo FROM TenderBOM757";
        SqlCommand cmd = new SqlCommand(sql, con, transaction);

        SqlDataAdapter ad = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        ad.Fill(dt);

        if (dt.Rows.Count > 0) return dt.Rows[0]["NextRefNo"].ToString();
        else return nextRefNo;
    }







    //=========================={ Item Save Button Click Event }==========================
    protected void btnItemInsert_Click(object sender, EventArgs e)
    {
        insertItemDetails();
    }

    private void insertItemDetails()
    {
        if (!Page.IsValid)
        {
            return;
        }

        // item dont exists in gridview

        string itemCategory = ItemCategory.SelectedValue;
        string itemSubCategory = ItemSubCategory.SelectedValue;
        string itemName = ItemName.SelectedValue;
        double tendorQuantity = Convert.ToDouble(TendorQuantity.Text.ToString());
        string uom = ItemUOM.SelectedValue;
        double itemRate = Convert.ToDouble(ItemRate.Text.ToString());
        string itemDescription = ItemDescription.Value;
        double itemSubTotal = (tendorQuantity * itemRate);

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();

            // CTE wit row_number window function
            string sql = $@"WITH UniqueItems AS (

                                SELECT ai.*, cat.CategoryName AS ItemCategoryText, subcat.SubCategory AS ItemSubCategoryText, 
                                item.ItemName AS ItemNameText, uom.UnitName AS ItemUOMText, 
                                ROW_NUMBER() OVER (PARTITION BY ai.ItemName ORDER BY ai.ItemName) AS RowNum, 
                                (SELECT SUM(ItemQuantity) FROM AAItem757 WHERE ItemName = ai.ItemName AND DeleteBy IS NULL) AS AoTotalQty 

                                FROM AAItem757 AS ai 
                                LEFT JOIN AAMaster757 AS am ON am.RefNo = ai.AARefNo
                                LEFT JOIN ItemCategory757 AS cat ON cat.RefID = ai.ItemCategory 
                                LEFT JOIN ItemSubCategory757 AS subcat ON subcat.RefID = ai.ItemSubCategory 
                                LEFT JOIN UnitOfMeasurement757 AS uom ON uom.RefID = ai.ItemUOM 
                                LEFT JOIN ItemMaster757 AS item ON item.RefID = ai.ItemName 
                                WHERE ai.ItemName = @ItemName AND ai.DeleteFlag IS NULL 
                                AND ai.IsVerified = 'TRUE' 
                            )
                            SELECT 
                                UI.*,
                                (UI.AoTotalQty - ISNULL((SELECT SUM(TenderQuantity) FROM TenderBOM757 WHERE ItemName = UI.ItemName AND DeleteBy IS NULL), 0)) AS AoBalanceQty
                            FROM UniqueItems AS UI WHERE UI.RowNum = 1";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@ItemName", itemName);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);

            if (dt.Rows.Count > 0) // if item exists in the AAItems or not ?
            {
                con.Close();

                itemDiv.Visible = true;

                if (!dt.Columns.Contains("CheckStatus"))
                {
                    DataColumn checkboxColumn = new DataColumn("CheckStatus", typeof(bool));
                    checkboxColumn.DefaultValue = false;
                    dt.Columns.Add(checkboxColumn);
                }

                if (!dt.Columns.Contains("TenderQuantity"))
                {
                    DataColumn TenderQuantity = new DataColumn("TenderQuantity", typeof(int));
                    TenderQuantity.DefaultValue = 0;
                    dt.Columns.Add(TenderQuantity);
                }

                if (!dt.Columns.Contains("DataEntryMode"))
                {
                    DataColumn DataEntryMode = new DataColumn("DataEntryMode", typeof(string));
                    DataEntryMode.DefaultValue = "MANUAL";
                    dt.Columns.Add(DataEntryMode);
                }

                if (!dt.Columns.Contains("ItemSubTotal"))
                {
                    DataColumn ItemSubTotal = new DataColumn("ItemSubTotal", typeof(int));
                    ItemSubTotal.DefaultValue = "0";
                    dt.Columns.Add(ItemSubTotal);
                }

                DataTable finalDT;
                if (ViewState["ItemDetails_VS"] != null)
                {
                    finalDT = ViewState["ItemDetails_VS"] as DataTable;
                    finalDT.Merge(dt);
                }
                else
                {
                    finalDT = dt;
                }

                itemGrid.DataSource = finalDT;
                itemGrid.DataBind();

                // total item basic amount
                double? totalItemAMount = finalDT.AsEnumerable().Sum(row => row["ItemSubTotal"] is DBNull ? (double?)null : Convert.ToDouble(row["ItemSubTotal"])) ?? 0.0;
                //BasicAmount.Text = totalItemAMount.HasValue ? totalItemAMount.Value.ToString("N2") : "0.00";

                ViewState["ItemDetails_VS"] = finalDT;
                Session["ItemDetails"] = finalDT;
            }
            else
            {
                // item doesnt exists in AAItems, then just add it in gridview so that it will inserted in to the tenderBOM table

                string sqlNew = $@"WITH UniqueItems AS (
	                                SELECT item.RefID ,item.ItemName, item.ItemCode, cat.CategoryName AS ItemCategoryText, subcat.SubCategory AS ItemSubCategoryText, 
	                                item.ItemName AS ItemNameText, uom.UnitName AS ItemUOMText, 
	                                ISNULL((SELECT SUM(ItemQuantity) FROM AAItem757 WHERE ItemName = item.RefID AND DeleteBy IS NULL), 0) AS AoTotalQty

	                                FROM ItemMaster757 AS item 
	                                LEFT JOIN ItemCategory757 AS cat ON cat.RefID = item.CategoryName 
	                                LEFT JOIN ItemSubCategory757 AS subcat ON subcat.RefID = item.SubCategory 
	                                LEFT JOIN UnitOfMeasurement757 AS uom ON uom.RefID = item.UMO 
	                                WHERE item.RefID = @RefID 
                                )
                                SELECT 
                                    UI.*,
                                    (UI.AoTotalQty - ISNULL((SELECT SUM(TenderQuantity) FROM TenderBOM757 WHERE ItemName = UI.ItemName AND DeleteBy IS NULL), 0)) AS AoBalanceQty
                                FROM UniqueItems AS UI";

                SqlCommand cmdNew = new SqlCommand(sqlNew, con);
                cmdNew.Parameters.AddWithValue("@RefID", itemName);
                cmdNew.ExecuteNonQuery();

                SqlDataAdapter adNew = new SqlDataAdapter(cmdNew);
                DataTable dtNew = new DataTable();
                adNew.Fill(dtNew);

                con.Close();

                if (dtNew.Rows.Count > 0)
                {
                    itemDiv.Visible = true;

                    if (!dtNew.Columns.Contains("CheckStatus"))
                    {
                        // adding the new column with checkboxes
                        DataColumn checkboxColumn = new DataColumn("CheckStatus", typeof(bool));
                        checkboxColumn.DefaultValue = false;
                        dtNew.Columns.Add(checkboxColumn);
                    }

                    if (!dtNew.Columns.Contains("TenderQuantity"))
                    {
                        DataColumn TenderQuantity = new DataColumn("TenderQuantity", typeof(int));
                        TenderQuantity.DefaultValue = 0;
                        dtNew.Columns.Add(TenderQuantity);
                    }

                    if (!dtNew.Columns.Contains("DataEntryMode"))
                    {
                        DataColumn DataEntryMode = new DataColumn("DataEntryMode", typeof(string));
                        DataEntryMode.DefaultValue = "MANUAL";
                        dtNew.Columns.Add(DataEntryMode);
                    }

                    if (!dtNew.Columns.Contains("ItemSubTotal"))
                    {
                        DataColumn ItemSubTotal = new DataColumn("ItemSubTotal", typeof(int));
                        ItemSubTotal.DefaultValue = "0";
                        dtNew.Columns.Add(ItemSubTotal);
                    }

                    if (!dtNew.Columns.Contains("ItemRate"))
                    {
                        DataColumn ItemRate = new DataColumn("ItemRate", typeof(int));
                        ItemRate.DefaultValue = itemRate;
                        dtNew.Columns.Add(ItemRate);
                    }

                    if (!dtNew.Columns.Contains("ItemDescription"))
                    {
                        DataColumn ItemDescription = new DataColumn("ItemDescription", typeof(string));
                        ItemDescription.DefaultValue = itemDescription;
                        dtNew.Columns.Add(ItemDescription);
                    }

                    DataTable finalDT;
                    if (ViewState["ItemDetails_VS"] != null)
                    {
                        finalDT = ViewState["ItemDetails_VS"] as DataTable;
                        finalDT.Merge(dtNew);
                    }
                    else
                    {
                        finalDT = dtNew;
                    }

                    itemGrid.DataSource = finalDT;
                    itemGrid.DataBind();

                    ViewState["ItemDetails_VS"] = finalDT;
                    Session["ItemDetails"] = finalDT;

                    // total item basic amount
                    double? totalItemAMount = finalDT.AsEnumerable().Sum(row => row["ItemSubTotal"] is DBNull ? (double?)null : Convert.ToDouble(row["ItemSubTotal"])) ?? 0.0;
                    //BasicAmount.Text = totalItemAMount.HasValue ? totalItemAMount.Value.ToString("N2") : "0.00";

                    Session["TotalBillAmount"] = totalItemAMount;
                }
            }

            // clearing input elements
            ItemCategory.SelectedIndex = 0;
            ItemSubCategory.SelectedIndex = 0;
            ItemUOM.SelectedIndex = 0;
            ItemName.SelectedIndex = 0;
            ReqBalanceQty.Text = string.Empty;
            TendorQuantity.Text = string.Empty;
            ItemRate.Text = string.Empty;
            ItemSubTotalTxt.Text = string.Empty;
            ItemDescription.Value = string.Empty;
        }

        if (itemRate >= 0.00 && tendorQuantity >= 0)
        {

        }
        else
        {
            string title = "Negative Values";
            string message = "please add positive values";
            getSweetAlertErrorMandatory(title, message);
        }
    }





    //----------============={ Excel Upload }=============----------

    protected void btnSample_Click(object sender, EventArgs e)
    {
        // the file has to be there, at the source

        string filename = "~/Portal/Reference/Tendor_BOM.xlsx"; // excel file
        //string filename = "~/Portal/Samples/Milind Khamkar Infosys Offer Letter.pdf"; // pdf file

        if (filename != "")
        {
            string path = Server.MapPath(filename);
            System.IO.FileInfo file = new System.IO.FileInfo(path);
            if (file.Exists)
            {
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.WriteFile(file.FullName);
                Response.End();
            }
            else
            {
                getSweetAlertErrorMandatory("Oops!", "Reference Excel File Don't Exsits");
            }
        }
        else
        {
            getSweetAlertErrorMandatory("Oops!", "Reference Excel File Don't Exsits");
        }
    } // reference excel file download

    protected void btnDocUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (fileExcel.HasFile)
            {
                string FileExtension = System.IO.Path.GetExtension(fileExcel.FileName);

                if (FileExtension == ".xlsx" || FileExtension == ".xls")
                {
                    string strFileName = DateTime.Now.Day.ToString() + '_' + DateTime.Now.Month.ToString() + '_' + DateTime.Now.Year.ToString() + '_' + DateTime.Now.Hour.ToString() + '_' +
                                         DateTime.Now.Minute.ToString() + '_' + fileExcel.FileName.ToString();

                    string filePath = Server.MapPath("~/Portal/Public/" + strFileName);
                    fileExcel.SaveAs(filePath);

                    using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
                    {
                        // Licence for Non-Commercial applications
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                        // Assuming the data is in the first worksheet
                        //ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                        ExcelWorksheet worksheet = null;

                        string sheetName = SheetName.Text.Trim();

                        if (!string.IsNullOrEmpty(sheetName))
                        {
                            worksheet = package.Workbook.Worksheets.FirstOrDefault(sheet => sheet.Name == sheetName);
                        }

                        if (worksheet != null)
                        {
                            // Access data from the worksheet
                            int rowCount = worksheet.Dimension.Rows;
                            int colCount = worksheet.Dimension.Columns;

                            DataTable dt = new DataTable();

                            // Assuming the first row contains column headers
                            for (int col = 1; col <= colCount; col++)
                            {
                                dt.Columns.Add(worksheet.Cells[1, col].Text);
                            }

                            // Starting from the second row to skip headers
                            for (int row = 2; row <= rowCount; row++)
                            {
                                DataRow dataRow = dt.NewRow();

                                for (int col = 1; col <= colCount; col++)
                                {
                                    dataRow[col - 1] = worksheet.Cells[row, col].Text;
                                }

                                dt.Rows.Add(dataRow);
                            }

                            if (dt.Rows.Count > 0)
                            {
                                // Checking column names present in excel sheet or not
                                if (dt.Columns[0].ColumnName.Trim() == "ItemCategory" && dt.Columns[1].ColumnName.Trim() == "ItemSubCategory" &&
                                dt.Columns[2].ColumnName.Trim() == "ItemName" && dt.Columns[3].ColumnName.Trim() == "TendorQuantity"
                                && dt.Columns[4].ColumnName.Trim() == "ItemUOM" && dt.Columns[5].ColumnName.Trim() == "ItemRate" && dt.Columns[6].ColumnName.Trim() == "ItemDescription")
                                {
                                    // Method 1: delete data from Temp Table
                                    // Method 2: inserting data into temp table if needed

                                    // removing empty records from excel DT data
                                    for (int i = dt.Rows.Count - 1; i >= 0; i--)
                                    {
                                        if (string.IsNullOrWhiteSpace(dt.Rows[i]["ItemName"].ToString()))
                                        {
                                            dt.Rows.RemoveAt(i);
                                        }
                                    }

                                    InsertBOMToGrid(dt);

                                    // Method 3: To display the inserted data from Temp or Main Table
                                }
                                else
                                {
                                    getSweetHTMLWzrning("Wrong Excel Columns!", "The Excel Format Has Not Matched, <br/> Please Download The Correct Excel File Format");
                                }
                            }
                        }
                        else
                        {
                            getSweetHTMLWzrning("Invalid Worksheet Name!", "The specified worksheet name was not found in the excel file. <br/> Please check the excel file properly.");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            getSweetHTMLWzrning("Ops!", ex.Message);

            itemGrid.DataSource = null;
            itemGrid.DataBind();

            ViewState["ItemDetails_VS"] = null;
            Session["ItemDetails"] = null;

            SheetName.Text = string.Empty;
        }
    }

    public void InsertBOMToGrid(DataTable dtItem)
    {
        string itemNames = string.Join(",", dtItem.AsEnumerable().Select(row => row.Field<string>("ItemName")));
        string itemCategories = string.Join(",", dtItem.AsEnumerable().Select(row => row.Field<string>("ItemCategory")));
        string itemSubCategories = string.Join(",", dtItem.AsEnumerable().Select(row => row.Field<string>("ItemSubCategory")));
        string itemUOM = string.Join(",", dtItem.AsEnumerable().Select(row => row.Field<string>("ItemUOM")));
        string itemRate = string.Join(",", dtItem.AsEnumerable().Select(row => row.Field<string>("ItemRate")));
        string itemDescriptions = string.Join(",", dtItem.AsEnumerable().Select(row => row.Field<string>("ItemDescription")));

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();

            string sql = "SP_TenderEstimation_ExcelUplod";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ItemNames", itemNames);
            cmd.Parameters.AddWithValue("@ItemCategories", itemCategories);
            cmd.Parameters.AddWithValue("@ItemSubCategories", itemSubCategories);
            cmd.Parameters.AddWithValue("@ItemUOMs", itemUOM);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);

            con.Close();

            foreach (DataRow row in dt.Rows)
            {
                int rowIndex = dt.Rows.IndexOf(row);

                // attaching item rate and description dynamically to empty columns from SP resultset
                if (row["ItemNameText"].ToString() == dtItem.Rows[rowIndex]["ItemName"].ToString())
                {
                    row["ItemRate"] = dtItem.Rows[rowIndex]["ItemRate"].ToString();
                    row["ItemDescription"] = dtItem.Rows[rowIndex]["ItemDescription"].ToString();
                }
            }

            // getting all items from result set from dt
            HashSet<string> existingItemNames = new HashSet<string>(dt.AsEnumerable().Select(row => row.Field<string>("ItemNameText")));

            // iterating the result data to check if item exists or not
            foreach (DataRow sourceRow in dtItem.Rows)
            {
                string sourceItemName = sourceRow.Field<string>("ItemName");

                if (!existingItemNames.Contains(sourceItemName))
                {
                    DataRow newRow = dt.NewRow();

                    newRow["ItemNameText"] = sourceItemName;
                    newRow["ItemCategoryText"] = sourceRow.Field<string>("ItemCategory");
                    newRow["ItemSubCategoryText"] = sourceRow.Field<string>("ItemSubCategory");
                    newRow["ItemUOMText"] = sourceRow.Field<string>("ItemUOM");
                    newRow["ItemRate"] = sourceRow.Field<string>("ItemRate");
                    newRow["ItemDescription"] = sourceRow.Field<string>("ItemDescription");
                    newRow["AoTotalQty"] = 0;
                    newRow["AoBalanceQty"] = 0;

                    dt.Rows.Add(newRow);
                }
            }

            if (dt.Rows.Count > 0)
            {
                itemDiv.Visible = true;

                if (!dt.Columns.Contains("CheckStatus"))
                {
                    // adding the new column with checkboxes
                    DataColumn checkboxColumn = new DataColumn("CheckStatus", typeof(bool));
                    checkboxColumn.DefaultValue = false;
                    dt.Columns.Add(checkboxColumn);
                }

                if (!dt.Columns.Contains("TenderQuantity"))
                {
                    DataColumn TenderQuantity = new DataColumn("TenderQuantity", typeof(int));
                    TenderQuantity.DefaultValue = 0;
                    dt.Columns.Add(TenderQuantity);
                }

                if (!dt.Columns.Contains("ItemSubTotal"))
                {
                    DataColumn ItemSubTotal = new DataColumn("ItemSubTotal", typeof(int));
                    ItemSubTotal.DefaultValue = 0;
                    dt.Columns.Add(ItemSubTotal);
                }

                if (!dt.Columns.Contains("DataEntryMode"))
                {
                    DataColumn DataEntryMode = new DataColumn("DataEntryMode", typeof(string));
                    DataEntryMode.DefaultValue = "EXCEL";
                    dt.Columns.Add(DataEntryMode);
                }

                itemGrid.DataSource = dt;
                itemGrid.DataBind();

                // total item basic amount
                //double? totalItemAMount = dt.AsEnumerable().Sum(row => row["ItemSubTotal"] is DBNull ? (double?)null : Convert.ToDouble(row["ItemSubTotal"])) ?? 0.0;
                //BasicAmount.Text = totalItemAMount.HasValue ? totalItemAMount.Value.ToString("N2") : "0.00";

                ViewState["ItemDetails_VS"] = dt;
                Session["ItemDetails"] = dt;

                SheetName.Text = string.Empty;
            }
        }
    }





    //=========================={ AA Through Multi-Check Listbox Event }==========================
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
                                        ROW_NUMBER() OVER (PARTITION BY ai.ItemName ORDER BY ai.ItemName) AS RowNum, 
	                                    (SELECT SUM(ItemQuantity) FROM AAItem757 WHERE ItemName = ai.ItemName AND DeleteBy IS NULL) AS AoTotalQty 
                                        FROM AAItem757 AS ai 
                                        LEFT JOIN AAMaster757 AS am ON am.RefNo = ai.AARefNo
                                        LEFT JOIN ItemCategory757 AS cat ON cat.RefID = ai.ItemCategory 
                                        LEFT JOIN ItemSubCategory757 AS subcat ON subcat.RefID = ai.ItemSubCategory 
                                        LEFT JOIN UnitOfMeasurement757 AS uom ON uom.RefID = ai.ItemUOM 
                                        LEFT JOIN ItemMaster757 AS item ON item.RefID = ai.ItemName 
                                        WHERE am.RefNo IN ({string.Join(",", selectedAARefNo.Select(AARefNo => $"'{AARefNo}'"))}) AND ai.DeleteFlag IS NULL 
                                        AND ai.IsVerified = 'TRUE' 
                                    )
                                    SELECT 
                                        UI.*,
                                        (UI.AoTotalQty - COALESCE((SELECT SUM(TenderQuantity) FROM TenderBOM757 WHERE ItemName = UI.ItemName AND DeleteBy IS NULL), 0)) AS AoBalanceQty
                                    FROM UniqueItems AS UI WHERE UI.RowNum = 1";


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
                            DataColumn checkboxColumn = new DataColumn("CheckStatus", typeof(bool));
                            checkboxColumn.DefaultValue = false;
                            dt.Columns.Add(checkboxColumn);
                        }

                        if (!dt.Columns.Contains("TenderQuantity"))
                        {
                            DataColumn TenderQuantity = new DataColumn("TenderQuantity", typeof(int));
                            TenderQuantity.DefaultValue = 0;
                            dt.Columns.Add(TenderQuantity);
                        }

                        foreach (DataRow row in dt.Rows)
                        {
                            row["DataEntryMode"] = "AAO";
                            row["ItemSubTotal"] = "0";
                        }

                        DataTable finalDT;
                        if (ViewState["ItemDetails_VS"] != null)
                        {
                            finalDT = ViewState["ItemDetails_VS"] as DataTable;
                            finalDT.Merge(dt);
                        }
                        else
                        {
                            finalDT = dt;
                        }

                        itemGrid.DataSource = dt;
                        itemGrid.DataBind();

                        // total item basic amount
                        double? totalItemAMount = dt.AsEnumerable().Sum(row => row["ItemSubTotal"] is DBNull ? (double?)null : Convert.ToDouble(row["ItemSubTotal"])) ?? 0.0;
                        //BasicAmount.Text = totalItemAMount.HasValue ? totalItemAMount.Value.ToString("N2") : "0.00";

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

            itemDiv.Visible = false;

            // clearing items gridview
            itemGrid.DataSource = null;
            itemGrid.DataBind();

            // clering the items session
            ViewState["ItemDetails_VS"] = null;
            Session["ItemDetails"] = null;
        }
    }






    //=========================={ Checkbox Event }==========================
    protected void CheckStatus_CheckedChanged(object sender, EventArgs e)
    {
        decimal basicAmount = 0.00m;

        foreach (GridViewRow itemRow in itemGrid.Rows)
        {
            int rowIndex = itemRow.RowIndex;

            TextBox TenderQtyTxt = itemRow.FindControl("TenderQuantity") as TextBox;
            decimal tenderQty = Convert.ToDecimal(TenderQtyTxt.Text);

            TextBox ItemRateTxt = itemRow.FindControl("ItemRate") as TextBox;
            decimal itemRate = Convert.ToDecimal(ItemRateTxt.Text);

            TextBox ItemSubTotalTxt = itemRow.FindControl("ItemSubTotal") as TextBox;

            //string checkboxStatus = ((HtmlInputCheckBox)itemRow.FindControl("CheckStatus")).Checked.ToString();
            bool checkboxStatus = ((CheckBox)itemRow.FindControl("CheckStatus")).Checked;

            if (checkboxStatus)
            {
                decimal itemSubTotal = (tenderQty * itemRate);

                ItemSubTotalTxt.Text = itemSubTotal.ToString();
                basicAmount += itemSubTotal;
            }
            else
            {
                //ItemSubTotalTxt.Text = (0).ToString();
            }
        }

        BasicAmount.Text = basicAmount.ToString();
    }

    protected void ItemSubTotal_TextChanged(object sender, EventArgs e)
    {
        decimal basicAmount = 0.00m;

        foreach (GridViewRow itemRow in itemGrid.Rows)
        {
            int rowIndex = itemRow.RowIndex;

            TextBox TenderQtyTxt = itemRow.FindControl("TenderQuantity") as TextBox;
            decimal tenderQty = Convert.ToDecimal(TenderQtyTxt.Text);

            TextBox ItemRateTxt = itemRow.FindControl("ItemRate") as TextBox;
            decimal itemRate = Convert.ToDecimal(ItemRateTxt.Text);

            TextBox ItemSubTotalTxt = itemRow.FindControl("ItemSubTotal") as TextBox;

            //string checkboxStatus = ((HtmlInputCheckBox)itemRow.FindControl("CheckStatus")).Checked.ToString();
            bool checkboxStatus = ((CheckBox)itemRow.FindControl("CheckStatus")).Checked;

            if (checkboxStatus)
            {
                decimal itemSubTotal = (tenderQty * itemRate);

                ItemSubTotalTxt.Text = itemSubTotal.ToString();
                basicAmount += itemSubTotal;
            }
            else
            {
                //ItemSubTotalTxt.Text = (0).ToString();
            }
        }

        BasicAmount.Text = basicAmount.ToString();
    }




    //=========================={ Custom Validation }==========================
    protected void ItemExistsCV_ServerValidate(object source, ServerValidateEventArgs args)
    {
        string itemName = ItemName.SelectedValue;

        if (Session["ItemDetails"] != null)
        {
            DataTable itemDT = (DataTable)Session["ItemDetails"];

            if (itemDT.Rows.Count > 0)
            {
                foreach (DataRow row in itemDT.Rows)
                {
                    string itemNameDT = row["ItemName"].ToString();

                    if (itemNameDT == itemName)
                    {
                        args.IsValid = false;
                        ItemExistsCV.ErrorMessage = "The selected item already exists";
                        return;
                    }
                }
            }
        }

        args.IsValid = true;
    }

    protected void TenderQtyGridCV_ServerValidate(object source, ServerValidateEventArgs args)
    {
        GridViewRow row = (GridViewRow)((CustomValidator)source).NamingContainer;

        TextBox TenderQtyTxt = (TextBox)row.FindControl("TenderQuantity");
        decimal tenderQty = Convert.ToDecimal(TenderQtyTxt.Text);

        CheckBox CheckStatusTxt = (CheckBox)row.FindControl("CheckStatus");

        if (CheckStatusTxt.Checked)
        {
            args.IsValid = (tenderQty > 0);

            if (!args.IsValid) ((CustomValidator)source).ErrorMessage = "enter tender qty";
        }
    }









    //=========================={ Bind Item Gridview }==========================
    private DataTable createItemDatatable()
    {
        DataTable dt = new DataTable();

        // DD text values

        // item category text
        DataColumn ItemCategoryText = new DataColumn("ItemCategoryText", typeof(string));
        dt.Columns.Add(ItemCategoryText);

        // item sub category text
        DataColumn ItemSubCategoryText = new DataColumn("ItemSubCategoryText", typeof(string));
        dt.Columns.Add(ItemSubCategoryText);

        // item uom text
        DataColumn ItemUOMText = new DataColumn("ItemUOMText", typeof(string));
        dt.Columns.Add(ItemUOMText);

        // item uom text
        DataColumn ItemNameText = new DataColumn("ItemNameText", typeof(string));
        dt.Columns.Add(ItemNameText);

        //__________________________________________________________________________

        // item category
        DataColumn ItemCategory = new DataColumn("ItemCategory", typeof(string));
        dt.Columns.Add(ItemCategory);

        // item sub category
        DataColumn ItemSubCategory = new DataColumn("ItemSubCategory", typeof(string));
        dt.Columns.Add(ItemSubCategory);

        // item name
        DataColumn ItemName = new DataColumn("ItemName", typeof(string));
        dt.Columns.Add(ItemName);

        // ao total qty
        DataColumn AoTotalQty = new DataColumn("AoTotalQty", typeof(string));
        dt.Columns.Add(AoTotalQty);

        // ao balance qty
        DataColumn AoBalanceQty = new DataColumn("AoBalanceQty", typeof(string));
        dt.Columns.Add(AoBalanceQty);

        // tendor qty
        DataColumn TenderQty = new DataColumn("TenderQty", typeof(string));
        dt.Columns.Add(TenderQty);

        // item uom
        DataColumn UOM = new DataColumn("ItemUOM", typeof(string));
        dt.Columns.Add(UOM);

        // item rate
        DataColumn ItemRate = new DataColumn("ItemRate", typeof(double));
        dt.Columns.Add(ItemRate);

        // item description
        DataColumn ItemDescription = new DataColumn("ItemDescription", typeof(string));
        dt.Columns.Add(ItemDescription);

        // item subtotal
        DataColumn ItemSubTotal = new DataColumn("ItemSubTotal", typeof(double));
        //ItemSubTotal.Expression = "ItemQuantity * ItemRate";
        dt.Columns.Add(ItemSubTotal);


        //___________________________Entry Mode Check__________________________________________

        // entry mode
        DataColumn DataEntryMode = new DataColumn("DataEntryMode", typeof(string));
        dt.Columns.Add(DataEntryMode);


        return dt;
    }

    private void AddRowToItemDataTable(DataTable dt, string itemCategory, string itemSubCategory, string itemName, double tendorQuantity, string uom, double itemRate, string description, string enteryMode)
    {
        // Create a new row
        DataRow row = dt.NewRow();

        // calculating item sub total price dynamically
        double itemSubTotal = Convert.ToDouble(tendorQuantity) * Convert.ToDouble(itemRate);

        //if (manualRadio.Checked)

        if (enteryMode == "MANUAL")
        {
            // in manual mode, need to have seperate DD refIDs and text values
            row["ItemCategoryText"] = ItemCategory.SelectedItem.Text;
            row["ItemSubCategoryText"] = ItemSubCategory.SelectedItem.Text;
            row["ItemNameText"] = ItemName.SelectedItem.Text;
            row["ItemUOMText"] = ItemUOM.SelectedItem.Text;
        }
        else
        {
            row["ItemCategoryText"] = itemCategory;
            row["ItemSubCategoryText"] = itemSubCategory;
            row["ItemNameText"] = itemName;
            row["ItemUOMText"] = uom;
        }

        // Set values for the new row
        //row["RefNo"] = refNo;
        row["ItemCategory"] = itemCategory;
        row["ItemSubCategory"] = itemSubCategory;
        row["ItemName"] = itemName;
        row["TendorQuantity"] = tendorQuantity;
        row["ItemUOM"] = uom;
        row["ItemRate"] = itemRate;
        row["ItemDescription"] = description;
        row["ItemSubTotal"] = itemSubTotal;

        // checking for data entry mode
        row["DataEntryMode"] = enteryMode;

        // Add the new row to the DataTable
        dt.Rows.Add(row);
    }






    //=========================={ Submit Button Click Event }==========================
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TenderEstimation.aspx");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }

        bool someChecked = false;

        foreach (GridViewRow row in itemGrid.Rows)
        {
            bool checkStatus = ((CheckBox)row.FindControl("CheckStatus")).Checked;

            if (checkStatus)
            {
                someChecked = true;
            }
        }

        if (someChecked)
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
                        InserTendorItems(con, transaction);

                        if (transaction.Connection != null) transaction.Commit();

                        string estimateNo = Session["EstimateNo"].ToString();

                        getSweetAlertSuccessRedirectMandatory("Success!", $"The Tendor Estimation: {estimateNo} Successfully Created", "TenderEstimation.aspx");
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
        else
        {
            getSweetHTMLWzrning("No Items Are Checked", "Kindly Check Minimum 1 Item To Proceed Further");
        }
    }




    private void InserTendorItems(SqlConnection con, SqlTransaction transaction)
    {
        DataTable dt = (DataTable)Session["ItemDetails"];

        if (dt.Rows.Count > 0)
        {
            string userID = Session["UserId"].ToString();

            string estimateNo = GetEstimateNo(con, transaction);
            Session["EstimateNo"] = estimateNo;

            decimal basicAmount = Convert.ToDecimal(BasicAmount.Text);

            foreach (DataRow row in dt.Rows)
            {
                int rowIndex = dt.Rows.IndexOf(row);

                CheckBox chkStatus = (CheckBox)itemGrid.Rows[rowIndex].FindControl("CheckStatus");

                // checking if all fields in the row have data
                if (row.ItemArray.All(field => !string.IsNullOrEmpty(field.ToString())))
                {

                }

                // only checked items will be inserted
                if (chkStatus != null && chkStatus.Checked)
                {
                    TextBox TenderQtyTxt = (TextBox)itemGrid.Rows[rowIndex].FindControl("TenderQuantity");
                    decimal tenderQty = Convert.ToDecimal(TenderQtyTxt.Text);

                    TextBox ItemRateTxt = (TextBox)itemGrid.Rows[rowIndex].FindControl("ItemRate");
                    decimal itemRate = Convert.ToDecimal(ItemRateTxt.Text);

                    // new item refernennce no
                    string itemRefNo_New = GetTendorItemsRefNo(con, transaction);

                    if (row["DataEntryMode"].ToString().ToUpper().Trim() == "EXCEL".ToUpper().Trim())
                    {
                        // fetching existing refids for following fields
                        string itemCategoryId = GetReferenceId_ItemCategory(con, transaction, row["ItemCategory"].ToString());
                        string itemSubCategoryId = GetReferenceId_ItemSubCategory(con, transaction, row["ItemSubCategory"].ToString(), itemCategoryId);
                        string itemUomId = GetReferenceId_UOM(con, transaction, row["ItemUOM"].ToString());
                        string itemId = GetReferenceId_ItemMaster(con, transaction, row["ItemName"].ToString(), itemCategoryId, itemSubCategoryId, itemUomId);

                        //excel entry
                        string sql = $@"insert into TenderBOM757 
                                        (RefNo, EstimateNo, EstimateDate, ItemCategory, ItemSubCategory, ItemName, AoTotalQty, AoBalanceQty, TendorQuantity, ItemUOM, ItemRate, ItemSubTotal, ItemDescription, DataEntryMode, BasicAmount, SaveBy) 
                                        values 
                                        (@RefNo, @EstimateNo, @EstimateDate, @ItemCategory, @ItemSubCategory, @ItemName, @AoTotalQty, @AoBalanceQty, @TendorQuantity, @ItemUOM, @ItemRate, @ItemSubTotal, @ItemDescription, @DataEntryMode, @BasicAmount, @SaveBy)";

                        SqlCommand cmd = new SqlCommand(sql, con, transaction);
                        cmd.Parameters.AddWithValue("@RefNo", itemRefNo_New);
                        cmd.Parameters.AddWithValue("@EstimateNo", estimateNo);
                        cmd.Parameters.AddWithValue("@EstimateDate", EstimateDate.Text);
                        cmd.Parameters.AddWithValue("@ItemCategory", itemCategoryId);
                        cmd.Parameters.AddWithValue("@ItemSubCategory", itemSubCategoryId);
                        cmd.Parameters.AddWithValue("@ItemName", itemId);
                        cmd.Parameters.AddWithValue("@AoTotalQty", row["AoTotalQty"]);
                        cmd.Parameters.AddWithValue("@AoBalanceQty", row["AoBalanceQty"]);
                        cmd.Parameters.AddWithValue("@TendorQuantity", tenderQty);
                        cmd.Parameters.AddWithValue("@ItemUOM", itemUomId);
                        cmd.Parameters.AddWithValue("@ItemRate", itemRate);
                        cmd.Parameters.AddWithValue("@ItemSubTotal", row["ItemSubTotal"]);
                        cmd.Parameters.AddWithValue("@ItemDescription", row["ItemDescription"]);
                        cmd.Parameters.AddWithValue("@DataEntryMode", row["DataEntryMode"]);
                        cmd.Parameters.AddWithValue("@BasicAmount", basicAmount);
                        cmd.Parameters.AddWithValue("@SaveBy", userID);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        // manual entry OR A.A. items (existing items)
                        string sql = $@"insert into TenderBOM757 
                                        (RefNo, EstimateNo, EstimateDate, ItemCategory, ItemSubCategory, ItemName, AoTotalQty, AoBalanceQty, TenderQuantity, ItemUOM, ItemRate, ItemSubTotal, ItemDescription, DataEntryMode, BasicAmount, SaveBy) 
                                        values 
                                        (@RefNo, @EstimateNo, @EstimateDate, @ItemCategory, @ItemSubCategory, @ItemName, @AoTotalQty, @AoBalanceQty, @TenderQuantity, @ItemUOM, @ItemRate, @ItemSubTotal, @ItemDescription, @DataEntryMode, @BasicAmount, @SaveBy)";

                        SqlCommand cmd = new SqlCommand(sql, con, transaction);
                        cmd.Parameters.AddWithValue("@RefNo", itemRefNo_New);
                        cmd.Parameters.AddWithValue("@EstimateNo", estimateNo);
                        cmd.Parameters.AddWithValue("@EstimateDate", EstimateDate.Text);
                        cmd.Parameters.AddWithValue("@ItemCategory", row["ItemCategory"]);
                        cmd.Parameters.AddWithValue("@ItemSubCategory", row["ItemSubCategory"]);
                        cmd.Parameters.AddWithValue("@ItemName", row["ItemName"]);
                        cmd.Parameters.AddWithValue("@AoTotalQty", row["AoTotalQty"]);
                        cmd.Parameters.AddWithValue("@AoBalanceQty", row["AoBalanceQty"]);
                        cmd.Parameters.AddWithValue("@TenderQuantity", tenderQty);
                        cmd.Parameters.AddWithValue("@ItemUOM", row["ItemUOM"]);
                        cmd.Parameters.AddWithValue("@ItemRate", itemRate);
                        cmd.Parameters.AddWithValue("@ItemSubTotal", row["ItemSubTotal"]);
                        cmd.Parameters.AddWithValue("@ItemDescription", row["ItemDescription"]);
                        cmd.Parameters.AddWithValue("@DataEntryMode", row["DataEntryMode"]);
                        cmd.Parameters.AddWithValue("@BasicAmount", basicAmount);
                        cmd.Parameters.AddWithValue("@SaveBy", userID);
                        cmd.ExecuteNonQuery();


                        //SqlDataAdapter ad = new SqlDataAdapter(cmd);
                        //DataTable dt = new DataTable();
                        //ad.Fill(dt);
                    }
                }
            }
        }
    }




    private string GetReferenceId_ItemCategory(SqlConnection con, SqlTransaction transaction, string textValue)
    {
        string userID = Session["UserId"].ToString();

        string sql = $@"SELECT RefID FROM ItemCategory757 WHERE CategoryName = @CategoryName";
        SqlCommand cmd = new SqlCommand(sql, con, transaction);
        cmd.Parameters.AddWithValue("@CategoryName", textValue);
        cmd.ExecuteNonQuery();

        SqlDataAdapter ad = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        ad.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            // return existing reference IDs
            if (dt.Columns.Contains("RefID")) return dt.Rows[0]["RefID"].ToString();
            else return dt.Rows[0]["RefNo"].ToString();
        }
        else
        {
            // insert and create new record and then return reference IDs

            string NewRefID = GetNewRefIDForExcelData(con, transaction, "ItemCategory757");

            string sqli = $@"INSERT INTO ItemCategory757 (RefID, CategoryName, SaveBy) VALUES (@RefID, @CategoryName, @SaveBy)";
            SqlCommand cmdi = new SqlCommand(sqli, con, transaction);
            cmdi.Parameters.Clear();
            cmdi.Parameters.AddWithValue("@RefID", NewRefID);
            cmdi.Parameters.AddWithValue("@CategoryName", textValue);
            cmdi.Parameters.AddWithValue("@SaveBy", userID);
            int k = cmdi.ExecuteNonQuery();

            if (k > 0) return NewRefID;
            else return NewRefID;
        }
    }

    private string GetReferenceId_ItemSubCategory(SqlConnection con, SqlTransaction transaction, string textValue, string itemCategoryId)
    {
        string userID = Session["UserId"].ToString();


        string sql = $@"SELECT RefID FROM ItemSubCategory757 WHERE SubCategory = @SubCategory";
        SqlCommand cmd = new SqlCommand(sql, con, transaction);
        cmd.Parameters.AddWithValue("@SubCategory", textValue);
        cmd.ExecuteNonQuery();

        SqlDataAdapter ad = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        ad.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            // return existing reference IDs
            if (dt.Columns.Contains("RefID")) return dt.Rows[0]["RefID"].ToString();
            else return dt.Rows[0]["RefNo"].ToString();
        }
        else
        {
            // insert and create new record and then return reference IDs

            string NewRefID = GetNewRefIDForExcelData(con, transaction, "ItemSubCategory757");

            string sqli = $@"INSERT INTO ItemSubCategory757 (RefID, SubCategory, ItemCategory, SaveBy) VALUES (@RefID, @SubCategory, @ItemCategory, @SaveBy)";
            SqlCommand cmdi = new SqlCommand(sqli, con, transaction);
            cmdi.Parameters.Clear();
            cmdi.Parameters.AddWithValue("@RefID", NewRefID);
            cmdi.Parameters.AddWithValue("@SubCategory", textValue);
            cmdi.Parameters.AddWithValue("@ItemCategory", itemCategoryId);
            cmdi.Parameters.AddWithValue("@SaveBy", userID);
            int k = cmdi.ExecuteNonQuery();

            if (k > 0) return NewRefID;
            else return NewRefID;
        }
    }

    private string GetReferenceId_UOM(SqlConnection con, SqlTransaction transaction, string textValue)
    {
        string userID = Session["UserId"].ToString();


        string sql = $@"SELECT RefID FROM UnitOfMeasurement757 WHERE UnitName = @UnitName";
        SqlCommand cmd = new SqlCommand(sql, con, transaction);
        cmd.Parameters.AddWithValue("@UnitName", textValue);
        cmd.ExecuteNonQuery();

        SqlDataAdapter ad = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        ad.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            // return existing reference IDs
            if (dt.Columns.Contains("RefID")) return dt.Rows[0]["RefID"].ToString();
            else return dt.Rows[0]["RefNo"].ToString();
        }
        else
        {
            // insert and create new record and then return reference IDs

            string NewRefID = GetNewRefIDForExcelData(con, transaction, "UnitOfMeasurement757");

            string sqli = $@"INSERT INTO UnitOfMeasurement757 (RefID, UnitName, SaveBy) VALUES (@RefID, @UnitName, @SaveBy)";
            SqlCommand cmdi = new SqlCommand(sqli, con, transaction);
            cmdi.Parameters.Clear();
            cmdi.Parameters.AddWithValue("@RefID", NewRefID);
            cmdi.Parameters.AddWithValue("@UnitName", textValue);
            cmdi.Parameters.AddWithValue("@SaveBy", userID);
            int k = cmdi.ExecuteNonQuery();

            if (k > 0) return NewRefID;
            else return NewRefID;
        }
    }

    private string GetReferenceId_ItemMaster(SqlConnection con, SqlTransaction transaction, string textValue, string itemCategoryId, string itemSubCategoryId, string itemUomId)
    {
        string userID = Session["UserId"].ToString();


        string sql = $@"SELECT RefID FROM ItemMaster757 WHERE ItemName = @ItemName";
        SqlCommand cmd = new SqlCommand(sql, con, transaction);
        cmd.Parameters.AddWithValue("@ItemName", textValue);
        cmd.ExecuteNonQuery();

        SqlDataAdapter ad = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        ad.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            // return existing reference IDs
            if (dt.Columns.Contains("RefID")) return dt.Rows[0]["RefID"].ToString();
            else return dt.Rows[0]["RefNo"].ToString();
        }
        else
        {
            // insert and create new record and then return reference IDs

            string NewRefID = GetNewRefIDForExcelData(con, transaction, "ItemMaster757");

            string sqli = $@"INSERT INTO ItemMaster757 (RefID, CategoryName, SubCategory, UMO, ItemName, SaveBy) VALUES (@RefID, @CategoryName, @SubCategory, @UMO, @ItemName, @SaveBy)";
            SqlCommand cmdi = new SqlCommand(sqli, con, transaction);
            cmdi.Parameters.Clear();
            cmdi.Parameters.AddWithValue("@RefID", NewRefID);
            cmdi.Parameters.AddWithValue("@CategoryName", itemCategoryId);
            cmdi.Parameters.AddWithValue("@SubCategory", itemSubCategoryId);
            cmdi.Parameters.AddWithValue("@UMO", itemUomId);
            cmdi.Parameters.AddWithValue("@ItemName", textValue);
            cmdi.Parameters.AddWithValue("@SaveBy", userID);
            int k = cmdi.ExecuteNonQuery();

            if (k > 0) return NewRefID;
            else return NewRefID;
        }
    }




    private string GetNewRefIDForExcelData(SqlConnection con, SqlTransaction transaction, string tableName)
    {
        string nextRefNo = "1000001";

        string sql = $@"SELECT ISNULL(MAX(CAST(RefID AS INT)), 1000000) + 1 AS NextRefNo FROM {tableName}";
        SqlCommand cmd = new SqlCommand(sql, con, transaction);

        SqlDataAdapter ad = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        ad.Fill(dt);

        if (dt.Rows.Count > 0) return dt.Rows[0]["NextRefNo"].ToString();
        else return nextRefNo;
    }


}