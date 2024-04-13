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
using OfficeOpenXml;
using System.IO;

public partial class AA_Item_AAItems : System.Web.UI.Page
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

                // initially manual entry option
                itemEnterManualDiv.Visible = true;
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

    protected void AdminApproveNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        string aaRefNo = AdminApproveNo.SelectedValue;

        if (AdminApproveNo.SelectedValue != "0")
        {
            FillAADetails(aaRefNo);
        }
        else
        {
            AADate.Text = string.Empty;
            SanctionAmount.Text = string.Empty;
            SanctionDate.Text = string.Empty;

            SourceOfBudget.ClearSelection();
            Bureau.ClearSelection();
        }
    }

    private void FillAADetails(string aaRefNo)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = $@"select am.RefNo, am.AANumber, am.AATitle, CONCAT(am.AANumber, '  -  ', am.AATitle) AS AAnoTitle, am.AADate, 
                            Concat(N'₹ ',Format(am.SanctionAmount, 'N', 'en-IN')) as SanctionAmount, SanctionDate, 
                            sb.BudgetName, b.Bureau
                            from AAMaster757 as am 
                            inner join SourceOfBudget757 as sb on sb.RefID = am.SourceOfBudget 
                            left join Buro757 as b on b.BuroID = am.AABureau
                            where RefNo = @RefNo";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@RefNo", aaRefNo);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            if (dt.Rows.Count > 0)
            {
                DateTime aaDate = DateTime.Parse(dt.Rows[0]["AADate"].ToString());
                AADate.Text = aaDate.ToString("yyyy-MM-dd");

                DateTime sanctionDate = DateTime.Parse(dt.Rows[0]["SanctionDate"].ToString());
                SanctionDate.Text = sanctionDate.ToString("yyyy-MM-dd");

                SanctionAmount.Text = dt.Rows[0]["SanctionAmount"].ToString();

                SourceOfBudget.DataSource = dt;
                SourceOfBudget.DataTextField = "BudgetName";
                SourceOfBudget.DataValueField = "BudgetName";
                SourceOfBudget.DataBind();
                SourceOfBudget.Items.Insert(0, new ListItem("------Select Source Of Budget------", "0"));

                if (SourceOfBudget.SelectedIndex < 2) SourceOfBudget.SelectedIndex = 1;

                Bureau.DataSource = dt;
                Bureau.DataTextField = "Bureau";
                Bureau.DataValueField = "Bureau";
                Bureau.DataBind();
                Bureau.Items.Insert(0, new ListItem("------Select Bureau------", "0"));

                if (Bureau.SelectedIndex < 2) Bureau.SelectedIndex = 1;
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

            ItemCode.Text = string.Empty;
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

            ItemCode.Text = string.Empty;
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
            ItemCode.Text = string.Empty;
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

            ItemCode.Text = dt.Rows[0]["ItemCode"].ToString();




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





    //=========================={ Fetch Data }==========================
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
                //double? totalBillAmount = dt.AsEnumerable().Sum(row => row["ItemSubTotal"] is DBNull ? (double?)null : Convert.ToDouble(row["ItemSubTotal"])) ?? 0.0;
                //txtBillAmount.Text = totalBillAmount.HasValue ? totalBillAmount.Value.ToString("N2") : "0.00";

                // re-calculating taxes
                //FillTaxHead();
            }
        }
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
        string itemCode = ItemCode.Text;
        double itemQty = Convert.ToDouble(ItemQuantity.Text.ToString());
        string uom = ItemUOM.SelectedValue;
        double itemRate = Convert.ToDouble(ItemRate.Text.ToString());
        double itemSubTotal = Convert.ToDouble(ItemSubTotalTxt.Text.ToString());
        string itemDescription = ItemDescription.Value;


        if (itemRate >= 0.00 && itemQty >= 0)
        {
            DataTable dt = ViewState["ItemDetails_VS"] as DataTable ?? createItemDatatable();

            AddRowToItemDataTable(dt, itemCategory, itemSubCategory, itemName, itemCode, itemQty, uom, itemRate, itemSubTotal, itemDescription, "MANUAL");

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
                //TotalItemAmount.Text = totalItemAMount.HasValue ? totalItemAMount.Value.ToString("N2") : "0.00";

                Session["TotalBillAmount"] = totalItemAMount;

                // clearing input elements
                ItemCategory.SelectedIndex = 0;
                ItemSubCategory.SelectedIndex = 0;
                ItemUOM.SelectedIndex = 0;
                ItemName.SelectedIndex = 0;
                ItemCode.Text = string.Empty;
                ItemQuantity.Text = string.Empty;
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

        string filename = "~/Portal/Reference/AA_BOM.xlsx"; // excel file
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
                                dt.Columns[2].ColumnName.Trim() == "ItemName" && dt.Columns[3].ColumnName.Trim() == "ItemCode" && dt.Columns[4].ColumnName.Trim() == "ItemQuantity"
                                && dt.Columns[5].ColumnName.Trim() == "ItemUOM" && dt.Columns[6].ColumnName.Trim() == "ItemRate" && dt.Columns[7].ColumnName.Trim() == "ItemDescription")
                                {

                                    // Method 1: delete data from Temp Table
                                    // Method 2: inserting data into temp table if needed

                                    InsertAAItemsToGrid(dt);

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

    public void InsertAAItemsToGrid(DataTable dtItem)
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
                string itemCode = row["ItemCode"].ToString();
                double itemQty = Convert.ToDouble(row["ItemQuantity"].ToString());
                string uom = row["ItemUOM"].ToString();
                double itemRate = Convert.ToDouble(row["ItemRate"]);
                string description = row["ItemDescription"].ToString();

                double itemSubTotal = (itemQty * itemRate);

                AddRowToItemDataTable(dt, itemCategory, itemSubCategory, itemName, itemCode, itemQty, uom, itemRate, itemSubTotal, description, "EXCEL");
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

        // item code
        DataColumn ItemCode = new DataColumn("ItemCode", typeof(string));
        dt.Columns.Add(ItemCode);

        // item qty
        DataColumn ItemQuantity = new DataColumn("ItemQuantity", typeof(string));
        dt.Columns.Add(ItemQuantity);

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

    private void AddRowToItemDataTable(DataTable dt, string itemCategory, string itemSubCategory, string itemName, string itemCode, double itemQty, string uom, double itemRate, double itemSubTotal, string description, string enteryMode)
    {
        // Create a new row
        DataRow row = dt.NewRow();

        // calculating item sub total price dynamically
        //double itemSubTotal = Convert.ToDouble(itemQty) * Convert.ToDouble(itemRate);

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
        row["ItemCode"] = itemCode;
        row["ItemQuantity"] = itemQty;
        row["ItemUOM"] = uom;
        row["ItemRate"] = itemRate;
        row["ItemSubTotal"] = itemSubTotal;
        row["ItemDescription"] = description;

        // checking for data entry mode
        row["DataEntryMode"] = enteryMode;

        // Add the new row to the DataTable
        dt.Rows.Add(row);
    }





    //=========================={ Custom Validation }==========================
    protected void ItemExistsCV_ServerValidate(object source, ServerValidateEventArgs args)
    {
        string itemName = ItemName.SelectedItem.Text;

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
                        ItemExistsCV.ErrorMessage = "item already exists";
                        return;
                    }
                }
            }
        }

        args.IsValid = true;
    }






    //=========================={ Submit Button Click Event }==========================
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("AAItems.aspx");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
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
                        string adminApproveRefNo = AdminApproveNo.SelectedValue;

                        // inserting header
                        InsertAAItems(con, transaction);

                        if (transaction.Connection != null) transaction.Commit();

                        string adminapprovalNo = AdminApproveNo.SelectedItem.Text;
                        getSweetAlertSuccessRedirectMandatory("Item Uploaded Successfully!", $"The Following Items Successfully Uploaded For Administrative Approval: {adminapprovalNo}", "AAItems.aspx");
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






    private void InsertAAItems(SqlConnection con, SqlTransaction transaction)
    {
        DataTable dt = (DataTable)Session["ItemDetails"];

        if (dt.Rows.Count > 0)
        {
            string userID = Session["UserId"].ToString();

            // administrative approval (DD) ref no
            string adminApproveRefNo = AdminApproveNo.SelectedValue;

            foreach (DataRow row in dt.Rows)
            {
                int rowIndex = dt.Rows.IndexOf(row);

                CheckBox chkStatus = (CheckBox)itemGrid.Rows[rowIndex].FindControl("CheckStatus");

                // checking if all fields in the row have data
                if (row.ItemArray.All(field => !string.IsNullOrEmpty(field.ToString())))
                {
                    // only checked items will be inserted
                    if (chkStatus != null && chkStatus.Checked)
                    {
                        // new item refernennce no
                        string itemRefNo_New = GetAAItemsRefNo(con, transaction);

                        //if (excelRadio.Checked) // mapping refids from text values


                        if (row["DataEntryMode"].ToString().ToUpper().Trim() == "EXCEL".ToUpper().Trim())
                        {
                            // fetching existing refids for following fields
                            string itemCategoryId = GetReferenceId_ItemCategory(con, transaction, row["ItemCategory"].ToString());
                            string itemSubCategoryId = GetReferenceId_ItemSubCategory(con, transaction, row["ItemSubCategory"].ToString(), itemCategoryId);
                            string itemUomId = GetReferenceId_UOM(con, transaction, row["ItemUOM"].ToString());
                            string itemId = GetReferenceId_ItemMaster(con, transaction, row["ItemName"].ToString(), itemCategoryId, itemSubCategoryId, itemUomId);

                            //excel entry
                            string sql = $@"insert into AAItem757 
                                        (RefNo, AARefNo, ItemCategory, ItemSubCategory, ItemName, ItemCode, ItemQuantity, ItemUOM, ItemRate, ItemSubTotal, ItemDescription, DataEntryMode, SaveBy) 
                                        values 
                                        (@RefNo, @AARefNo, @ItemCategory, @ItemSubCategory, @ItemName, @ItemCode, @ItemQuantity, @ItemUOM, @ItemRate, @ItemSubTotal, @ItemDescription, @DataEntryMode, @SaveBy)";

                            SqlCommand cmd = new SqlCommand(sql, con, transaction);
                            cmd.Parameters.AddWithValue("@RefNo", itemRefNo_New);
                            cmd.Parameters.AddWithValue("@AARefNo", adminApproveRefNo);
                            cmd.Parameters.AddWithValue("@ItemCategory", itemCategoryId);
                            cmd.Parameters.AddWithValue("@ItemSubCategory", itemSubCategoryId);
                            cmd.Parameters.AddWithValue("@ItemName", itemId);
                            cmd.Parameters.AddWithValue("@ItemCode", row["ItemCode"]);
                            cmd.Parameters.AddWithValue("@ItemQuantity", row["ItemQuantity"]);
                            cmd.Parameters.AddWithValue("@ItemUOM", itemUomId);
                            cmd.Parameters.AddWithValue("@ItemRate", row["ItemRate"]);
                            cmd.Parameters.AddWithValue("@ItemSubTotal", row["ItemSubTotal"]);
                            cmd.Parameters.AddWithValue("@ItemDescription", row["ItemDescription"]);
                            cmd.Parameters.AddWithValue("@DataEntryMode", row["DataEntryMode"]);
                            cmd.Parameters.AddWithValue("@SaveBy", userID);
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            // manual entry
                            string sql = $@"insert into AAItem757 
                                        (RefNo, AARefNo, ItemCategory, ItemSubCategory, ItemName, ItemCode, ItemQuantity, ItemUOM, ItemRate, ItemSubTotal, ItemDescription, DataEntryMode, SaveBy) 
                                        values 
                                        (@RefNo, @AARefNo, @ItemCategory, @ItemSubCategory, @ItemName, @ItemCode, @ItemQuantity, @ItemUOM, @ItemRate, @ItemSubTotal, @ItemDescription, @DataEntryMode, @SaveBy)";

                            SqlCommand cmd = new SqlCommand(sql, con, transaction);
                            cmd.Parameters.AddWithValue("@RefNo", itemRefNo_New);
                            cmd.Parameters.AddWithValue("@AARefNo", adminApproveRefNo);
                            cmd.Parameters.AddWithValue("@ItemCategory", row["ItemCategory"]);
                            cmd.Parameters.AddWithValue("@ItemSubCategory", row["ItemSubCategory"]);
                            cmd.Parameters.AddWithValue("@ItemName", row["ItemName"]);
                            cmd.Parameters.AddWithValue("@ItemCode", row["ItemCode"]);
                            cmd.Parameters.AddWithValue("@ItemQuantity", row["ItemQuantity"]);
                            cmd.Parameters.AddWithValue("@ItemUOM", row["ItemUOM"]);
                            cmd.Parameters.AddWithValue("@ItemRate", row["ItemRate"]);
                            cmd.Parameters.AddWithValue("@ItemSubTotal", row["ItemSubTotal"]);
                            cmd.Parameters.AddWithValue("@ItemDescription", row["ItemDescription"]);
                            cmd.Parameters.AddWithValue("@DataEntryMode", row["DataEntryMode"]);
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