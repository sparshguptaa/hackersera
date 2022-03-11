using BusinessLogic;
using DataCommunicator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;

public partial class Administrative_ServiceConfiguration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ControlUtility.IsSessionExpired();
            if (!IsPostBack)
            {
                //FillProgressNoteDropDown();
                FillGridManageProcedureCode();
                FillProgessNoteandUnitHours();      //Added by Anandd(Finoit)
            }
        }
        catch (Exception ex)
        {
            AddExceptionToErrorLog(ex, MethodBase.GetCurrentMethod().Name);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            CompanyDetailsBL oCompanyDetailsBL = new CompanyDetailsBL();
            OneToOneServiceConfiguration oServiceConfiguration = PopulateServiceConfiguration();
            string sXMLObject = CommonUtilityDC.GetDataXML(oServiceConfiguration);
            bool IsSaved = false;
            if (hdnServiceConfigurationId.Value == "")
                IsSaved = oCompanyDetailsBL.InsertProcedureConfiguration(sXMLObject);
            else
                IsSaved = oCompanyDetailsBL.UpdateProcedureConfiguration(sXMLObject);
            FillGridManageProcedureCode();
            ResetControls();
        }
        catch (Exception ex)
        {
            AddExceptionToErrorLog(ex, MethodBase.GetCurrentMethod().Name);
        }
    }

    //Added by Anand(Finoit)
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            CompanyDetailsBL oCompanyDetailsBL = new CompanyDetailsBL();
            int iCodeId = Convert.ToInt32(hdnServiceConfigurationId.Value);
            oCompanyDetailsBL.Company_Id = Convert.ToInt32(Session[Constants.S_SESSION_COMPANY_ID]);
            if (oCompanyDetailsBL.CheckProcedureCodeExist(iCodeId))
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('This procedure code has already been used.');", true);
            else
            {
                hdnIsDeleted.Value = "true";
                OneToOneServiceConfiguration oServiceConfiguration = oServiceConfiguration = PopulateServiceConfiguration();
                string sXmlObject = CommonUtilityDC.GetDataXML(oServiceConfiguration);
                oCompanyDetailsBL.DeleteProcedureCode(sXmlObject);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Procedure Code deleted successfully.');", true);
                FillGridManageProcedureCode();
                ResetControls();
            }
        }
        catch (Exception ex)
        {
            AddExceptionToErrorLog(ex, MethodBase.GetCurrentMethod().Name);
        }
    }

    protected void imgbtnNew_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ResetControls();
        }
        catch (Exception ex)
        {
            AddExceptionToErrorLog(ex, MethodBase.GetCurrentMethod().Name);
        }
    }

    protected void gvProcedureCode_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Added by Anand(Finoit) --Start
                TableCell OTtablecell = e.Row.Cells[8];
                if (OTtablecell.Text == "True")
                    OTtablecell.Text = "Y";
                else
                    OTtablecell.Text = "N";
                OTtablecell = e.Row.Cells[11];
                if (OTtablecell.Text == "0")
                    OTtablecell.Text = string.Empty;
                //Added by Anand(Finoit) --End
                //Commented by Anand(Finoit) --Start
                //TableCell otablecell = e.Row.Cells[6];
                //if (otablecell.Text == "0")
                //    otablecell.Text = "";
                //else
                //    otablecell.Text += " min";
                //Added by Anand(Finoit) --End
            }
        }
        catch (Exception ex)
        {
            AddExceptionToErrorLog(ex, MethodBase.GetCurrentMethod().Name);
        }


    }


    protected void gvProcedureCode_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            SetSortDirection();
            CompanyDetailsBL oCompanyDetailsBL = new CompanyDetailsBL();
            oCompanyDetailsBL.Company_Id = Convert.ToInt32(Session[Constants.S_SESSION_COMPANY_ID]);
            DataSet odtproclist = oCompanyDetailsBL.GetProcedureCodeList();
            if (odtproclist != null)
            {
                odtproclist.Tables[0].DefaultView.Sort = e.SortExpression + " " + hidSortDirection.Value;
                gvProcedureCode.DataSource = odtproclist;
                gvProcedureCode.DataBind();
            }
        }
        catch (Exception ex)
        {
            AddExceptionToErrorLog(ex, MethodBase.GetCurrentMethod().Name);
        }

    }

    protected void gvMileageRate_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int RowIndex = Convert.ToInt32(e.CommandArgument.ToString());
            int iId = Convert.ToInt32(gvMileageRate.DataKeys[RowIndex]["id"].ToString());
            if (e.CommandName == "EditDetails")
            {
                hdnServiceMileageRateId.Value = iId.ToString();
                txtMileage.Text = gvMileageRate.DataKeys[RowIndex]["Rate"].ToString();
                txtValidMileage.Text = gvMileageRate.DataKeys[RowIndex]["Date"].ToString();
                chkMileageRate.Checked = gvMileageRate.DataKeys[RowIndex]["Status"].ToString() == "Active" ? true : false;
            }
        }
        catch (Exception ex)
        {
            AddExceptionToErrorLog(ex, MethodBase.GetCurrentMethod().Name);
        }
    }

    protected void gvTravelRateList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int RowIndex = Convert.ToInt32(e.CommandArgument.ToString());
            int iId = Convert.ToInt32(gvTravelRateList.DataKeys[RowIndex]["id"].ToString());
            if (e.CommandName == "EditDetails")
            {
                hdnServiceTravelRateId.Value = iId.ToString();
                txtReimbRate.Text = gvTravelRateList.DataKeys[RowIndex]["Rate"].ToString();
                txtFrom.Text = gvTravelRateList.DataKeys[RowIndex]["Date"].ToString();
                chkReimbRate.Checked = gvTravelRateList.DataKeys[RowIndex]["Status"].ToString() == "Active" ? true : false;
            }
        }
        catch (Exception ex)
        {
            AddExceptionToErrorLog(ex, MethodBase.GetCurrentMethod().Name);
        }
    }

    protected void gvProcedureCode_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName != "Sort")
            {
                int RowIndex = Convert.ToInt32(e.CommandArgument.ToString());
                int iId = Convert.ToInt32(gvProcedureCode.DataKeys[RowIndex]["CodeId"].ToString());
                if (e.CommandName == "EditDetails")
                {
                    hdnServiceConfigurationId.Value = iId.ToString();
                    txtProcCode.Text = gvProcedureCode.DataKeys[RowIndex]["Code"].ToString();
                    txtMOD.Text = gvProcedureCode.DataKeys[RowIndex]["MODCode"].ToString();
                    txtMOD2.Text = gvProcedureCode.DataKeys[RowIndex]["MODCode2"].ToString();
                    txtMOD3.Text = gvProcedureCode.DataKeys[RowIndex]["MODCode3"].ToString();
                    txtMOD4.Text = gvProcedureCode.DataKeys[RowIndex]["MODCode4"].ToString();
                    //ddlType.SelectedValue = gvProcedureCode.DataKeys[RowIndex]["CodeType"].ToString() == "Service" ? "0" : "1";       //Commented by Anand(Finoit)
                    //Added by Anand(Finoit) --Start
                    if (gvProcedureCode.DataKeys[RowIndex]["CodeType"].ToString() == "Service")
                        ddlType.SelectedValue = "0";
                    else if (gvProcedureCode.DataKeys[RowIndex]["CodeType"].ToString() == "Day")
                        ddlType.SelectedValue = "1";
                    else
                        ddlType.SelectedValue = "2";
                    //Added by Anand(Fionit) --end
                    txtMinutes.Text = gvProcedureCode.DataKeys[RowIndex]["ServiceMinutes"].ToString();
                    txtNextunit.Text = gvProcedureCode.DataKeys[RowIndex]["RoundToNext"].ToString();
                    chkOvertime.Checked = Convert.ToBoolean(gvProcedureCode.DataKeys[RowIndex]["OT"]);
                    txtAccount.Text = gvProcedureCode.DataKeys[RowIndex]["Account"].ToString();
                    ddlUnitHour.SelectedValue = gvProcedureCode.DataKeys[RowIndex]["Unit_Hour"].ToString();
                    if (ddlProgressNote.Items.FindByValue(gvProcedureCode.DataKeys[RowIndex]["ProgressNoteId"].ToString()) != null)
                        ddlProgressNote.SelectedValue = gvProcedureCode.DataKeys[RowIndex]["ProgressNoteId"].ToString();
                    else
                        ddlProgressNote.SelectedValue = "0";
                    if (ddlType.SelectedValue == "1")
                    {
                        trUnits.Style.Add("display", "none");
                        txtMinutes.Text = "";
                        txtNextunit.Text = "";
                        trProgressNotes.Style.Add("display", "none");
                        ddlProgressNote.SelectedValue = "0";
                        // trchkOverTime.Style.Add("display", "none"); 
                        // chkOvertime.Checked = false; // commented By Sparsh(finoit)
                        trchkOverTime.Style.Remove("display");// added by Sparsh(Finoit)--BDL-ZN4-3SX6--(All-Fields-were-not-displayed-while-clicked-to-Edit)
                        trHours.Style.Add("display", "none");
                        ddlUnitHour.SelectedValue = "0";
                    }
                    else
                    {
                        trUnits.Style.Remove("display");
                        trchkOverTime.Style.Remove("display");//Start-- added by Sparsh(Finoit)--BDL-ZN4-3SX6--(All-Fields-were-not-displayed-while-clicked-to-Edit)
                    }
                    txtService.Text = gvProcedureCode.DataKeys[RowIndex]["Description"].ToString();
                    // ddlProgressNote.SelectedValue = gvProcedureCode.DataKeys[RowIndex]["ProgressNoteId"].ToString();
                    //if (ddlProgressNote.Items.FindByValue(gvProcedureCode.DataKeys[RowIndex]["ProgressNoteId"].ToString()) != null)
                    //    ddlProgressNote.SelectedValue = gvProcedureCode.DataKeys[RowIndex]["ProgressNoteId"].ToString();
                    //else
                    //    ddlProgressNote.SelectedValue = "0";
                    ddlStatus.SelectedValue = gvProcedureCode.DataKeys[RowIndex]["Status"].ToString() == "Active" ? "0" : "1";
                    btnDelete.Visible = true;
                    txtPOS.Text = gvProcedureCode.DataKeys[RowIndex]["POS"].ToString() == "0" ? string.Empty : gvProcedureCode.DataKeys[RowIndex]["POS"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            AddExceptionToErrorLog(ex, MethodBase.GetCurrentMethod().Name);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ResetControls();
        }
        catch (Exception ex)
        {
            AddExceptionToErrorLog(ex, MethodBase.GetCurrentMethod().Name);
        }
    }

    protected void gvTravelRateList_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void gvMileageRate_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void btnTravelSave_Click(object sender, EventArgs e)
    {
        try
        {
            ServiceRateDetails oServiceRateDetails = PopulateServiceTravelRateDetails();
            CompanyDetailsBL oCompanyDetailsBL = new CompanyDetailsBL();
            string sXMLObject = CommonUtilityDC.GetDataXML(oServiceRateDetails);
            bool IsSaved = false;
            if (hdnServiceTravelRateId.Value == "")
                IsSaved = oCompanyDetailsBL.InsertServiceTravelRate(sXMLObject);
            else
                IsSaved = oCompanyDetailsBL.UpdateServiceTravelRate(sXMLObject);
            FillGridManageProcedureCode();
            ResetControls();
        }
        catch (Exception ex)
        {
            AddExceptionToErrorLog(ex, MethodBase.GetCurrentMethod().Name);
        }

    }

    protected void btnMileage_Click(object sender, EventArgs e)
    {
        try
        {
            ServiceRateDetails oServiceRateDetails = PopulateServiceMileageRateDetails();
            CompanyDetailsBL oCompanyDetailsBL = new CompanyDetailsBL();
            string sXMLObject = CommonUtilityDC.GetDataXML(oServiceRateDetails);
            bool IsSaved = false;
            if (hdnServiceMileageRateId.Value == "")
                IsSaved = oCompanyDetailsBL.InsertServiceMileageRate(sXMLObject);
            else
                IsSaved = oCompanyDetailsBL.UpdateServiceMileageRate(sXMLObject);
            FillGridManageProcedureCode();
        }
        catch (Exception ex)
        {
            AddExceptionToErrorLog(ex, MethodBase.GetCurrentMethod().Name);
        }
    }

    private void ResetControls()
    {
        txtProcCode.Text = "";
        txtMOD.Text = "";
        txtMOD2.Text = "";
        txtMOD3.Text = "";
        txtMOD4.Text = "";
        txtService.Text = "";
        txtNextunit.Text = "";
        txtMinutes.Text = "";
        ddlType.SelectedValue = "0";
        ddlProgressNote.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;
        btnDelete.Visible = false;
        txtFrom.Text = txtMileage.Text = txtReimbRate.Text = txtValidMileage.Text = "";
        chkMileageRate.Checked = chkReimbRate.Checked = true;
        hdnServiceConfigurationId.Value = hdnServiceMileageRateId.Value = hdnIsDeleted.Value = hdnServiceTravelRateId.Value = "";
        txtAccount.Text = string.Empty;
        ddlUnitHour.SelectedValue = "0";
        txtPOS.Text = string.Empty;

    }

    private void FillGridManageProcedureCode()
    {
        try
        {
            CompanyDetailsBL oCompanyDetailsBL = new CompanyDetailsBL();
            oCompanyDetailsBL.Company_Id = Convert.ToInt32(Session[Constants.S_SESSION_COMPANY_ID]);
            DataSet odtproclist = oCompanyDetailsBL.GetProcedureCodeList();
            if (odtproclist != null)
            {
                gvProcedureCode.DataSource = odtproclist.Tables[0];
                gvProcedureCode.DataBind();

                gvTravelRateList.DataSource = odtproclist.Tables[1];
                gvTravelRateList.DataBind();

                gvMileageRate.DataSource = odtproclist.Tables[2];
                gvMileageRate.DataBind();
            }
        }
        catch (Exception ex)
        {
            AddExceptionToErrorLog(ex, MethodBase.GetCurrentMethod().Name);
        }
    }

    private void FillProgessNoteandUnitHours()
    {
        try
        {
            CompanyDetailsBL oCompanyDetailsBL = new CompanyDetailsBL();
            oCompanyDetailsBL.Company_Id = Convert.ToInt32(Session[Constants.S_SESSION_COMPANY_ID]);

            DataSet oDS = oCompanyDetailsBL.GetProgressNoteAndUnitHoursInBillingCode();
            ControlUtility.FillDropDownList(oDS.Tables[0], ref ddlUnitHour, "UnitValue", "UnitText", "--Select--");
            ControlUtility.FillDropDownList(oDS.Tables[1], ref ddlProgressNote, "ProgressNoteTypeId", "ProgressNoteType", "--Select--");
        }
        catch (Exception ex)
        {
            AddExceptionToErrorLog(ex, MethodBase.GetCurrentMethod().Name);
        }
    }


    private OneToOneServiceConfiguration PopulateServiceConfiguration()
    {

        OneToOneServiceConfiguration oServiceConfiguration = new OneToOneServiceConfiguration();
        oServiceConfiguration.Code = txtProcCode.Text.ToString();
        oServiceConfiguration.Company_Id = Convert.ToInt32(Session[Constants.S_SESSION_COMPANY_ID]);
        oServiceConfiguration.ModCode = txtMOD.Text.ToString();
        oServiceConfiguration.ModCode2 = txtMOD2.Text.ToString();
        oServiceConfiguration.ModCode3 = txtMOD3.Text.ToString();
        oServiceConfiguration.ModCode4 = txtMOD4.Text.ToString();
        oServiceConfiguration.POS = txtPOS.Text == String.Empty ? 0 : Convert.ToInt32(txtPOS.Text);
        if (txtMOD.Text == "")
            oServiceConfiguration.ModCode = " ";
        else
            oServiceConfiguration.ModCode = txtMOD.Text.ToString();
        oServiceConfiguration.Description = txtService.Text.ToString();
        oServiceConfiguration.Type = Convert.ToInt32(ddlType.SelectedValue);
        //oServiceConfiguration.OverTime = chkOvertime.Checked;     //Commented by Anand(Finoit)
        if (txtAccount.Text == "")
            oServiceConfiguration.Account = "0";
        else
            oServiceConfiguration.Account = txtAccount.Text.ToString();
        //oServiceConfiguration.Unit_Hours = Convert.ToInt32(ddlUnitHour.SelectedValue);  //Commentd by Anand(Finoit)
        if (ddlType.SelectedValue == "1")
        {
            oServiceConfiguration.Minutes_Unit = 0;
            oServiceConfiguration.RoundNextUnit = 0;
            oServiceConfiguration.OverTime = chkOvertime.Checked;// added by Sparsh(Finoit)--BDL-ZN4-3SX6
            oServiceConfiguration.progressCodeId = 0;
            oServiceConfiguration.Unit_Hours = 0;
        }
        else
        {
            oServiceConfiguration.Minutes_Unit = txtMinutes.Text != "" ? Convert.ToInt32(txtMinutes.Text) : 0;
            oServiceConfiguration.RoundNextUnit = txtNextunit.Text != "" ? Convert.ToInt32(txtNextunit.Text) : 0;
            //Added by Anand(Finoit) -- Start
            oServiceConfiguration.OverTime = chkOvertime.Checked;
            oServiceConfiguration.progressCodeId = Convert.ToInt32(ddlProgressNote.SelectedValue);
            oServiceConfiguration.Unit_Hours = Convert.ToInt32(ddlUnitHour.SelectedValue);
            //Added by Anand(Finoit) --End
        }
        //oServiceConfiguration.progressCodeId = Convert.ToInt32(ddlProgressNote.SelectedValue);    //Commented by Anand(Finoit)
        oServiceConfiguration.Status = ddlStatus.SelectedValue == "0" ? true : false;
        if (hdnServiceConfigurationId.Value == "")
        {
            oServiceConfiguration.CreatedBy = Convert.ToInt32(Session[Constants.S_SESSION_EMPLOYEE_ID]);
            oServiceConfiguration.CreatedOn = CommonUtilityBL.GetEmployeeCurrentDateTime(Session[Constants.S_CONSTANT_TIME_ZONE].ToString());
        }
        else
        {
            //Added by Anand(Finoit) Start
            if (hdnIsDeleted.Value == "true")
            {
                oServiceConfiguration.IsDiscontinue = Convert.ToBoolean(Constants.I_ONE);
                oServiceConfiguration.DiscontinuedBy = Convert.ToInt32(Session[Constants.S_SESSION_EMPLOYEE_ID]);
                oServiceConfiguration.DiscontinuedOn = Convert.ToDateTime(CommonUtilityBL.GetEmployeeCurrentDateTime(Session[Constants.S_CONSTANT_TIME_ZONE].ToString()));
            }
            //Added by Anand(Finoit) End
            oServiceConfiguration.ServiceConfigurationId = Convert.ToInt32(hdnServiceConfigurationId.Value);
            oServiceConfiguration.UpdatedBy = Convert.ToInt32(Session[Constants.S_SESSION_EMPLOYEE_ID]);
            oServiceConfiguration.UpdatedOn = CommonUtilityBL.GetEmployeeCurrentDateTime(Session[Constants.S_CONSTANT_TIME_ZONE].ToString());
        }
        return oServiceConfiguration;
    }

    //private void FillProgressNoteDropDown()
    //{
    //    try
    //    {
    //        ProgressNotesBL oProgressNotesBL = new ProgressNotesBL();
    //        int iCompanyId = Convert.ToInt32(Session[Constants.S_SESSION_COMPANY_ID]);
    //        DataTable oDSProgressNoteType = oProgressNotesBL.FetchProgressNoteTypeFromDatabase(iCompanyId);
    //        ControlUtility.FillDropDownList(oDSProgressNoteType, ref ddlProgressNote, "ProgressNoteTypeId", "ProgressNoteType", "--Select--");
    //    }
    //    catch (Exception ex)
    //    {
    //        AddExceptionToErrorLog(ex, MethodBase.GetCurrentMethod().Name);
    //    }
    //}



    private void SetSortDirection()
    {
        if (hidSortDirection.Value == Constants.S_DESCENDING)
            hidSortDirection.Value = Constants.S_ASCENDING;
        else
            hidSortDirection.Value = Constants.S_DESCENDING;
    }

    private ServiceRateDetails PopulateServiceTravelRateDetails()
    {
        ServiceRateDetails oServiceRateDetails = new ServiceRateDetails();
        oServiceRateDetails.CompanyId = Convert.ToInt32(Session[Constants.S_SESSION_COMPANY_ID]);
        oServiceRateDetails.Rate = Convert.ToDecimal(txtReimbRate.Text);
        oServiceRateDetails.ValidThrough = Convert.ToDateTime(txtFrom.Text);
        oServiceRateDetails.Status = chkReimbRate.Checked;
        if (hdnServiceTravelRateId.Value == "")
        {
            oServiceRateDetails.CreatedBy = Convert.ToInt32(Session[Constants.S_SESSION_EMPLOYEE_ID]);
            oServiceRateDetails.CreatedOn = CommonUtilityBL.GetEmployeeCurrentDateTime(Session[Constants.S_CONSTANT_TIME_ZONE].ToString());
        }
        else
        {
            oServiceRateDetails.ServiceRateId = Convert.ToInt32(hdnServiceTravelRateId.Value);
            oServiceRateDetails.UpdatedBy = Convert.ToInt32(Session[Constants.S_SESSION_EMPLOYEE_ID]);
            oServiceRateDetails.UpdatedOn = CommonUtilityBL.GetEmployeeCurrentDateTime(Session[Constants.S_CONSTANT_TIME_ZONE].ToString());
        }
        return oServiceRateDetails;
    }


    private ServiceRateDetails PopulateServiceMileageRateDetails()
    {
        ServiceRateDetails oServiceRateDetails = new ServiceRateDetails();
        oServiceRateDetails.CompanyId = Convert.ToInt32(Session[Constants.S_SESSION_COMPANY_ID]);
        oServiceRateDetails.Rate = Convert.ToDecimal(txtMileage.Text);
        oServiceRateDetails.ValidThrough = Convert.ToDateTime(txtValidMileage.Text);
        oServiceRateDetails.Status = chkMileageRate.Checked;
        if (hdnServiceMileageRateId.Value == "")
        {
            oServiceRateDetails.CreatedBy = Convert.ToInt32(Session[Constants.S_SESSION_EMPLOYEE_ID]);
            oServiceRateDetails.CreatedOn = CommonUtilityBL.GetEmployeeCurrentDateTime(Session[Constants.S_CONSTANT_TIME_ZONE].ToString());
        }
        else
        {
            oServiceRateDetails.ServiceRateId = Convert.ToInt32(hdnServiceMileageRateId.Value);
            oServiceRateDetails.UpdatedBy = Convert.ToInt32(Session[Constants.S_SESSION_EMPLOYEE_ID]);
            oServiceRateDetails.UpdatedOn = CommonUtilityBL.GetEmployeeCurrentDateTime(Session[Constants.S_CONSTANT_TIME_ZONE].ToString());
        }
        return oServiceRateDetails;
    }

    private void AddExceptionToErrorLog(Exception ex, string asMethodName)
    {
        BusinessLogic.Exceptions.ExceptionHandler.WriteExceptionToErrorLog
          (ex.Message, ex.StackTrace,
            MethodBase.GetCurrentMethod().DeclaringType.FullName + '.' + asMethodName,
            Convert.ToInt32(Session[Constants.S_SESSION_COMPANY_ID]));
    }           
    
    
}