using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Purchase_Order_PurchaseOrderBOM : System.Web.UI.Page
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

                Tender_Estimation_NoAndTitle_DropDown();
                Vender_Name_DropDown();

                ItemCategory_DropDown();
                UnitOfMeasurement_DropDown();

                DateTime poDate = DateTime.Now;
                PODate.Text = poDate.ToString("yyyy-MM-dd");

                DateTime estimateDate = DateTime.Now;
                EstimationDate.Text = estimateDate.ToString("yyyy-MM-dd");
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

    private void Tender_Estimation_NoAndTitle_DropDown()
    {
        string userID = Session["UserId"].ToString();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = $@"select td.RefID, ev.EstimateNo, td.TenNo, td.TenDate, CONCAT(td.TenNo, ' - ', td.TenTitle) as TenderNoNTitle
                            from TenderDetails757 as td 
                            inner join EstimateVerification757 ev on ev.EstimateNo = td.EstimatNo";
            SqlCommand cmd = new SqlCommand(sql, con);
            //cmd.Parameters.AddWithValue("@SaveBy", userID);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            TenderNoNTitle.DataSource = dt;
            TenderNoNTitle.DataTextField = "TenderNoNTitle";
            TenderNoNTitle.DataValueField = "EstimateNo";
            TenderNoNTitle.DataBind();
            TenderNoNTitle.Items.Insert(0, new ListItem("------Select Tender Number & Title------", "0"));
        }
    }

    private void Vender_Name_DropDown()
    {
        string userID = Session["UserId"].ToString();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = $@"select * from VendorMaster757";
            SqlCommand cmd = new SqlCommand(sql, con);
            //cmd.Parameters.AddWithValue("@SaveBy", userID);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            VenderName.DataSource = dt;
            VenderName.DataTextField = "VendorName";
            VenderName.DataValueField = "RefID";
            VenderName.DataBind();
            VenderName.Items.Insert(0, new ListItem("------Select Vendor Name------", "0"));
        }
    }

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

    protected void TenderNoNTitle_SelectedIndexChanged(object sender, EventArgs e)
    {
        string estimationNo = TenderNoNTitle.SelectedValue;

        if (TenderNoNTitle.SelectedValue != "0")
        {
            // fill header
            EstimationDetails_Bind_Dropdown(estimationNo); 
            // fill details
            EstimationItems_Bind_Dropdown(estimationNo);
        }
        else
        {
            EstimationNo.Text = string.Empty;
            EstimationDate.Text = string.Empty;

            itemDiv.Visible = false;

            itemGrid.DataSource = null;
            itemGrid.DataBind();

            ViewState["ItemDetails_VS"] = null;
            Session["ItemDetails"] = null;
        }
    }

    private void EstimationDetails_Bind_Dropdown(string estimationNo)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = $@"select distinct EstimateNo, EstimateDate from TenderBOM757 where EstimateNo = @EstimateNo";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@EstimateNo", estimationNo);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            if (dt.Rows.Count > 0)
            {
                EstimationNo.Text = dt.Rows[0]["EstimateNo"].ToString();

                DateTime estimateDate = DateTime.Parse(dt.Rows[0]["EstimateDate"].ToString());
                EstimationDate.Text = estimateDate.ToString("yyyy-MM-dd");
            }
        }
    }

    private void EstimationItems_Bind_Dropdown(string estimationNo)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = $@"select distinct tb.*, cat.CategoryName as ItemCategoryText, subcat.SubCategory as ItemSubCategoryText, 
                            item.ItemName as ItemNameText, uom.UnitName as ItemUOMText, tb.TenderQuantity as PoQuantity 
                            from TenderBOM757 as td
                            inner join TenderBOM757 as tb on tb.EstimateNo = td.EstimateNo 
                            inner join ItemCategory757 as cat on cat.RefID = tb.ItemCategory 
                            inner join ItemSubCategory757 as subcat on subcat.RefID = tb.ItemSubCategory 
                            inner join UnitOfMeasurement757 as uom on uom.RefID = tb.ItemUOM 
                            inner join ItemMaster757 as item on item.RefID = tb.ItemName
                            where td.EstimateNo = @EstimateNo";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@EstimateNo", estimationNo);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            if (dt.Rows.Count > 0)
            {
                itemDiv.Visible = true;

                itemGrid.DataSource = dt;
                itemGrid.DataBind();

                ViewState["ItemDetails_VS"] = dt;
                Session["ItemDetails"] = dt;

                // total item basic amount
                double? totalItemAMount = dt.AsEnumerable().Sum(row => row["ItemSubTotal"] is DBNull ? (double?)null : Convert.ToDouble(row["ItemSubTotal"])) ?? 0.0;
                BasicAmount.Text = totalItemAMount.HasValue ? totalItemAMount.Value.ToString("N2") : "0.00";

                Session["TotalBillAmount"] = totalItemAMount;

                // recalculating tax head
                FillTaxHead();
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
    }

    protected void excelRadio_CheckedChanged(object sender, EventArgs e)
    {
        excelUploadDiv.Visible = true;
        itemEnterManualDiv.Visible = false;
    }

    protected void EstimateRadio_CheckedChanged(object sender, EventArgs e)
    {
        excelUploadDiv.Visible = false;
        itemEnterManualDiv.Visible = false;
    }





    //=========================={ Fetch Data }==========================

    private string GetNewPOReferenceNo(SqlConnection con, SqlTransaction transaction)
    {
        string nextRefNo = "1000001";

        string sql = "SELECT ISNULL(MAX(CAST(RefID AS INT)), 1000000) + 1 AS NextRefNo FROM PoDetails757";
        SqlCommand cmd = new SqlCommand(sql, con, transaction);

        SqlDataAdapter ad = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        ad.Fill(dt);

        if (dt.Rows.Count > 0) return dt.Rows[0]["NextRefNo"].ToString();
        else return nextRefNo;
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

    private string GetNewGLTaxRefNo(SqlConnection con, SqlTransaction transaction)
    {
        string nextRefNo = "1000001";

        string sql = "SELECT ISNULL(MAX(CAST(RefNo AS INT)), 1000000) + 1 AS NextRefNo FROM PurchaseOrderTax757";
        SqlCommand cmd = new SqlCommand(sql, con, transaction);

        SqlDataAdapter ad = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        ad.Fill(dt);

        if (dt.Rows.Count > 0) return dt.Rows[0]["NextRefNo"].ToString();
        else return nextRefNo;
    }




    //=========================={ GridView RowDeleting }==========================
    protected void Grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridView gridView = (GridView)sender;

        // item gridview
        if (gridView.ID == "itemGrid")
        {
            int rowIndex = e.RowIndex;

            DataTable dt = ViewState["ItemDetails_VS"] as DataTable;

            if (dt != null && dt.Rows.Count > rowIndex)
            {
                // removing record from the gridview
                dt.Rows.RemoveAt(rowIndex);

                ViewState["ItemDetails_VS"] = dt;
                Session["ItemDetails"] = dt;

                itemGrid.DataSource = dt;
                itemGrid.DataBind();

                // re-calculating total amount n assigning back to textbox
                double? totalBillAmount = dt.AsEnumerable().Sum(row => row["ItemSubTotal"] is DBNull ? (double?)null : Convert.ToDouble(row["ItemSubTotal"])) ?? 0.0;
                BasicAmount.Text = totalBillAmount.HasValue ? totalBillAmount.Value.ToString("N2") : "0.00";

                Session["TotalBillAmount"] = totalBillAmount;

                // re-calculating taxes
                //FillTaxHead();
            }
        }
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

        string itemCategory = ItemCategory.SelectedValue;
        string itemSubCategory = ItemSubCategory.SelectedValue;
        string itemName = ItemName.SelectedValue;
        double poQuantity = Convert.ToDouble(PoQuantity.Text.ToString());
        string uom = ItemUOM.SelectedValue;
        double itemRate = Convert.ToDouble(ItemRate.Text.ToString());
        string itemDescription = ItemDescription.Value;
        double itemSubTotal = (poQuantity * itemRate);


        if (itemRate >= 0.00 && poQuantity >= 0)
        {
            DataTable dt = ViewState["ItemDetails_VS"] as DataTable ?? createItemDatatable();

            AddRowToItemDataTable(dt, itemCategory, itemSubCategory, itemName, poQuantity, uom, itemRate, itemDescription, "MANUAL");

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

                itemGrid.DataSource = dt;
                itemGrid.DataBind();

                ViewState["ItemDetails_VS"] = dt;
                Session["ItemDetails"] = dt;

                // total item basic amount
                double? totalItemAMount = dt.AsEnumerable().Sum(row => row["ItemSubTotal"] is DBNull ? (double?)null : Convert.ToDouble(row["ItemSubTotal"])) ?? 0.0;
                BasicAmount.Text = totalItemAMount.HasValue ? totalItemAMount.Value.ToString("N2") : "0.00";

                Session["TotalBillAmount"] = totalItemAMount;

                // recalculating tax head
                FillTaxHead();

                // clearing input elements
                ItemCategory.SelectedIndex = 0;
                ItemSubCategory.SelectedIndex = 0;
                ItemUOM.SelectedIndex = 0;
                ItemName.SelectedIndex = 0;
                PoQuantity.Text = string.Empty;
                ItemRate.Text = string.Empty;
                ItemSubTotalTxt.Text = string.Empty;
                ItemDescription.Value = string.Empty;


            }
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

        string filename = "~/Portal/Reference/Purchase_Order_BOM.xlsx"; // excel file
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
                                dt.Columns[2].ColumnName.Trim() == "ItemName" && dt.Columns[3].ColumnName.Trim() == "PoQuantity"
                                && dt.Columns[4].ColumnName.Trim() == "ItemUOM" && dt.Columns[5].ColumnName.Trim() == "ItemRate" && dt.Columns[6].ColumnName.Trim() == "ItemDescription")
                                {

                                    // Method 1: delete data from Temp Table
                                    // Method 2: inserting data into temp table if needed

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
        }
    }

    public void InsertBOMToGrid(DataTable dtItem)
    {
        DataTable dt = ViewState["ItemDetails_VS"] as DataTable ?? createItemDatatable();

        foreach (DataRow row in dtItem.Rows)
        {
            //int rowIndex = dt.Rows.IndexOf(row);
            //dt.Rows.RemoveAt(rowIndex);

            // checking if all fields in the row have data
            if (row.ItemArray.All(field => !string.IsNullOrEmpty(field.ToString())))
            {
                string itemCategory = row["ItemCategory"].ToString();
                string itemSubCategory = row["ItemSubCategory"].ToString();
                string itemName = row["ItemName"].ToString();
                double poQuantity = Convert.ToDouble(row["PoQuantity"].ToString());
                string uom = row["ItemUOM"].ToString();
                double itemRate = Convert.ToDouble(row["ItemRate"]);
                string description = row["ItemDescription"].ToString();

                AddRowToItemDataTable(dt, itemCategory, itemSubCategory, itemName, poQuantity, uom, itemRate, description, "EXCEL");
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

            itemGrid.DataSource = dt;
            itemGrid.DataBind();

            ViewState["ItemDetails_VS"] = dt;
            Session["ItemDetails"] = dt;

            // recalculating tax head
            FillTaxHead();

            SheetName.Text = string.Empty;
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

        // po qty
        DataColumn PoQuantity = new DataColumn("PoQuantity", typeof(string));
        dt.Columns.Add(PoQuantity);

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

    private void AddRowToItemDataTable(DataTable dt, string itemCategory, string itemSubCategory, string itemName, double poQuantity, string uom, double itemRate, string description, string enteryMode)
    {
        // Create a new row
        DataRow row = dt.NewRow();

        // calculating item sub total price dynamically
        double itemSubTotal = Convert.ToDouble(poQuantity) * Convert.ToDouble(itemRate);

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
        row["PoQuantity"] = poQuantity;
        row["ItemUOM"] = uom;
        row["ItemRate"] = itemRate;
        row["ItemDescription"] = description;
        row["ItemSubTotal"] = itemSubTotal;

        // checking for data entry mode
        row["DataEntryMode"] = enteryMode;

        // Add the new row to the DataTable
        dt.Rows.Add(row);
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

        decimal totalBillAmount = Convert.ToDecimal(BasicAmount.Text);

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
            DataColumn checkboxColumn = new DataColumn("TaxAmount", typeof(decimal));
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

        if (!accountHeadDT.Columns.Contains("CheckStatus"))
        {
            DataColumn CheckStatus = new DataColumn("CheckStatus", typeof(bool));
            CheckStatus.DefaultValue = true;
            accountHeadDT.Columns.Add(CheckStatus);
        }

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
        RecalculateTaxHead();
    }

    private void RecalculateTaxHead()
    {
        // Account Head DataTable
        DataTable dt = (DataTable)Session["AccountHeadDT"];

        // Perform calculations or other logic based on the updated values
        decimal totalBill = Convert.ToDecimal(BasicAmount.Text);
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

            bool checkboxStatus = ((CheckBox)row.FindControl("CheckStatus")).Checked;

            if (checkboxStatus)
            {
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
        }

        // setting total deduction
        txtTotalDeduct.Text = Math.Abs(totalDeduction).ToString("N2");

        // setting total addition
        txtTotalAdd.Text = totalAddition.ToString("N2");

        // Net Amount after tax deductions or addition
        netAmount = (totalBill + totalAddition) - Math.Abs(totalDeduction);
        txtNetAmnt.Text = netAmount.ToString("N2");
    }



    //=========================={ Checkbox Event }==========================
    protected void CheckStatus_CheckedChanged(object sender, EventArgs e)
    {
        RecalculateTaxHead();
    }






    //=========================={ Submit Button Click Event }==========================

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PurchaseOrderBOM.aspx");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        bool someChecked = false;

        foreach (GridViewRow row in GridTax.Rows)
        {
            bool CheckStatus = ((CheckBox)row.FindControl("CheckStatus")).Checked;

            if (CheckStatus)
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
                        string poRefNo_New = GetNewPOReferenceNo(con, transaction);

                        // insert po header
                        InsertPOHeader(con, transaction, poRefNo_New);

                        // inserting po items
                        InsertPOItems(con, transaction, poRefNo_New);

                        // inserting invoice or tax heads
                        InsertTaxHead(con, transaction, poRefNo_New);

                        if (transaction.Connection != null) transaction.Commit();

                        getSweetAlertSuccessRedirectMandatory("Success!", $"The P.O. Successfully Created", "PurchaseOrderBOM.aspx");
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
            getSweetAlertErrorMandatory("No GL Tax Checked", "Kindly Check Some GL Tax Heads To Proceed Further");
        }
    }




    private void InsertPOHeader(SqlConnection con, SqlTransaction transaction, string poRefNo_New)
    {
        string userID = Session["UserId"].ToString();

        string tenderEstimateNo = TenderNoNTitle.SelectedValue;
        string estimateNumber = EstimationNo.Text;
        DateTime estimateDate = DateTime.Parse(EstimationDate.Text);

        string poNumber = PONumber.Text;
        DateTime poDate = DateTime.Parse(PODate.Text);
        string venderName = VenderName.SelectedValue;
        decimal poAmount = Convert.ToDecimal(POAmount.Text);
        string poRemark = PORemark.Value;

        decimal aoBasicAmount = Convert.ToDecimal(BasicAmount.Text);
        decimal totalDeduct = decimal.TryParse(txtTotalDeduct.Text, out decimal deductValue) ? deductValue : 0;
        decimal totalAdd = decimal.TryParse(txtTotalAdd.Text, out decimal addValue) ? addValue : 0;
        decimal netAmount = decimal.TryParse(txtNetAmnt.Text, out decimal netValue) ? netValue : 0;

        string sql = $@"insert into PoDetails757 
                        (RefID, PoNo, PoDate, TenTitle, EstimationNo, EstimationDate, VendNme, PoAmt, Remark, BasicAmount, TotalDeduct, TotalAdd, NetAmount, SaveBy) 
                        values 
                        (@RefID, @PoNo, @PoDate, @TenTitle, @EstimationNo, @EstimationDate, @VendNme, @PoAmt, @Remark, @BasicAmount, @TotalDeduct, @TotalAdd, @NetAmount, @SaveBy)";

        SqlCommand cmd = new SqlCommand(sql, con, transaction);
        cmd.Parameters.AddWithValue("@RefID", poRefNo_New);
        cmd.Parameters.AddWithValue("@PoNo", poNumber);
        cmd.Parameters.AddWithValue("@PoDate", poDate);
        cmd.Parameters.AddWithValue("@TenTitle", tenderEstimateNo);
        cmd.Parameters.AddWithValue("@EstimationNo", estimateNumber);
        cmd.Parameters.AddWithValue("@EstimationDate", estimateDate);
        cmd.Parameters.AddWithValue("@VendNme", venderName);
        cmd.Parameters.AddWithValue("@PoAmt", poAmount);
        cmd.Parameters.AddWithValue("@Remark", poRemark);
        cmd.Parameters.AddWithValue("@BasicAmount", aoBasicAmount);
        cmd.Parameters.AddWithValue("@TotalDeduct", totalDeduct);
        cmd.Parameters.AddWithValue("@TotalAdd", totalAdd);
        cmd.Parameters.AddWithValue("@NetAmount", netAmount);
        cmd.Parameters.AddWithValue("@SaveBy", userID);
        cmd.ExecuteNonQuery();
    }

    private void InsertPOItems(SqlConnection con, SqlTransaction transaction, string poRefNo)
    {
        DataTable dt = (DataTable)Session["ItemDetails"];

        if (dt.Rows.Count > 0)
        {
            string userID = Session["UserId"].ToString();

            foreach (DataRow row in dt.Rows)
            {
                int rowIndex = dt.Rows.IndexOf(row);

                // new item refernennce no
                string itemRefNo_New = GetPoItemsRefNo(con, transaction);

                if (row["DataEntryMode"].ToString().ToUpper().Trim() == "EXCEL".ToUpper().Trim())
                {
                    // fetching existing refids for following fields
                    string itemCategoryId = GetReferenceId_ItemCategory(con, transaction, row["ItemCategory"].ToString());
                    string itemSubCategoryId = GetReferenceId_ItemSubCategory(con, transaction, row["ItemSubCategory"].ToString(), itemCategoryId);
                    string itemUomId = GetReferenceId_UOM(con, transaction, row["ItemUOM"].ToString());
                    string itemId = GetReferenceId_ItemMaster(con, transaction, row["ItemName"].ToString(), itemCategoryId, itemSubCategoryId, itemUomId);

                    //excel entry
                    string sql = $@"insert into PoBom757 
                                        (RefNo, PORefNo, ItemCategory, ItemSubCategory, ItemName, PoQuantity, ItemUOM, ItemRate, ItemSubTotal, ItemDescription, DataEntryMode, BalanceQty, BillQty, SaveBy) 
                                        values 
                                        (@RefNo, @PORefNo, @ItemCategory, @ItemSubCategory, @ItemName, @PoQuantity, @ItemUOM, @ItemRate, @ItemSubTotal, @ItemDescription, @DataEntryMode, @BalanceQty, @BillQty, @SaveBy)";

                    SqlCommand cmd = new SqlCommand(sql, con, transaction);
                    cmd.Parameters.AddWithValue("@RefNo", itemRefNo_New);
                    cmd.Parameters.AddWithValue("@PORefNo", poRefNo);
                    cmd.Parameters.AddWithValue("@ItemCategory", itemCategoryId);
                    cmd.Parameters.AddWithValue("@ItemSubCategory", itemSubCategoryId);
                    cmd.Parameters.AddWithValue("@ItemName", itemId);
                    cmd.Parameters.AddWithValue("@PoQuantity", row["PoQuantity"]);
                    cmd.Parameters.AddWithValue("@ItemUOM", itemUomId);
                    cmd.Parameters.AddWithValue("@ItemRate", row["ItemRate"]);
                    cmd.Parameters.AddWithValue("@ItemSubTotal", row["ItemSubTotal"]);
                    cmd.Parameters.AddWithValue("@ItemDescription", row["ItemDescription"]);
                    cmd.Parameters.AddWithValue("@DataEntryMode", row["DataEntryMode"]);
                    cmd.Parameters.AddWithValue("@BalanceQty", row["PoQuantity"]);
                    cmd.Parameters.AddWithValue("@BillQty", (0).ToString());
                    cmd.Parameters.AddWithValue("@SaveBy", userID);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    // manual entry
                    string sql = $@"insert into PoBom757 
                                        (RefNo, PORefNo, ItemCategory, ItemSubCategory, ItemName, PoQuantity, ItemUOM, ItemRate, ItemSubTotal, ItemDescription, DataEntryMode, BalanceQty, BillQty, SaveBy) 
                                        values 
                                        (@RefNo, @PORefNo, @ItemCategory, @ItemSubCategory, @ItemName, @PoQuantity, @ItemUOM, @ItemRate, @ItemSubTotal, @ItemDescription, @DataEntryMode, @BalanceQty, @BillQty, @SaveBy)";

                    SqlCommand cmd = new SqlCommand(sql, con, transaction);
                    cmd.Parameters.AddWithValue("@RefNo", itemRefNo_New);
                    cmd.Parameters.AddWithValue("@PORefNo", poRefNo);
                    cmd.Parameters.AddWithValue("@ItemCategory", row["ItemCategory"]);
                    cmd.Parameters.AddWithValue("@ItemSubCategory", row["ItemSubCategory"]);
                    cmd.Parameters.AddWithValue("@ItemName", row["ItemName"]);
                    cmd.Parameters.AddWithValue("@PoQuantity", row["PoQuantity"]);
                    cmd.Parameters.AddWithValue("@ItemUOM", row["ItemUOM"]);
                    cmd.Parameters.AddWithValue("@ItemRate", row["ItemRate"]);
                    cmd.Parameters.AddWithValue("@ItemSubTotal", row["ItemSubTotal"]);
                    cmd.Parameters.AddWithValue("@ItemDescription", row["ItemDescription"]);
                    cmd.Parameters.AddWithValue("@DataEntryMode", row["DataEntryMode"]);
                    cmd.Parameters.AddWithValue("@BalanceQty", row["PoQuantity"]);
                    cmd.Parameters.AddWithValue("@BillQty", (0).ToString());
                    cmd.Parameters.AddWithValue("@SaveBy", userID);
                    cmd.ExecuteNonQuery();

                    //SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    //DataTable dt = new DataTable();
                    //ad.Fill(dt);
                }
            }
        }
    }

    private void InsertTaxHead(SqlConnection con, SqlTransaction transaction, string poRefNo_New)
    {
        string userID = Session["UserId"].ToString();

        DataTable dt = (DataTable)Session["AccountHeadDT"];

        if (dt != null)
        {
            foreach (GridViewRow row in GridTax.Rows)
            {
                int rowIndex = row.RowIndex;

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

                string CheckStatus = ((CheckBox)row.FindControl("CheckStatus")).Checked.ToString();



                string newTaxRefNo = GetNewGLTaxRefNo(con, transaction);

                string sql = $@"INSERT INTO PurchaseOrderTax757 
                                (RefNo, PORefNo, GLAccountName, GLAccountCode, Value, Factor, AddLess, TaxAmount, CheckStatus, SaveBy) 
                                VALUES 
                                (@RefNo, @PORefNo, @GLAccountName, @GLAccountCode, @Value, @Factor, @AddLess, @TaxAmount, @CheckStatus, @SaveBy)";

                SqlCommand cmd = new SqlCommand(sql, con, transaction);
                cmd.Parameters.AddWithValue("@RefNO", newTaxRefNo);
                cmd.Parameters.AddWithValue("@PORefNo", poRefNo_New);
                cmd.Parameters.AddWithValue("@GLAccountName", glAccountName);
                cmd.Parameters.AddWithValue("@GLAccountCode", glAccountCode);
                cmd.Parameters.AddWithValue("@Value", taxValue);
                cmd.Parameters.AddWithValue("@Factor", factor);
                cmd.Parameters.AddWithValue("@AddLess", addLess);
                cmd.Parameters.AddWithValue("@TaxAmount", taxAmount);
                cmd.Parameters.AddWithValue("@CheckStatus", CheckStatus);
                cmd.Parameters.AddWithValue("@SaveBy", userID);
                cmd.ExecuteNonQuery();
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