using BusinessLogic;
using DataCommunicator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administrative_MedicationCountsPopup : System.Web.UI.Page
{

    protected void Page_Init(object sender, EventArgs e)
    {
        UCtrSchedule.SetMsg += new EventHandler(UCtrSchedule_SetMsg);
        UCtrSchedule.ResetMsg += new EventHandler(UCtrSchedule_ResetMsg);
        UCtrSchedule.ResetErrorMsg += new EventHandler(UCtrSchedule_ResetErrorMsg);
        UCtrSchedule.SetReadings += new EventHandler(UCtrSchedule_SetReadings);
        UCtrSchedule.SetErrorMsg += new EventHandler(UCtrSchedule_SetErrorMsg);

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        ControlUtility.IsSessionExpired();
        int ServiceId = 0;
        if (Request.QueryString["ServiceId"] != null)
            ServiceId = Convert.ToInt32(Request.QueryString["ServiceId"]);
        int IsMedication = 0;
        if (Request.QueryString["IsMedication"] != null)
            IsMedication = Convert.ToInt32(Request.QueryString["IsMedication"]);
        UCtrSchedule.MedId = ServiceId;
        UCtrSchedule.MedicationType = IsMedication;
        UCtrSchedule.IsCountSchedulePage = true;
        if (!IsPostBack)
        {
            CommonSetUp();
            txtStartDate.Text = CommonUtilityBL.GetEmployeeCurrentDateTime(Session[Constants.S_CONSTANT_TIME_ZONE].ToString()).ToShortDateString();
            TxtStartTime.Text = CommonUtilityBL.GetEmployeeCurrentDateTime(Session[Constants.S_CONSTANT_TIME_ZONE].ToString()).ToString("hh:mm tt");
            rdbtnPrn.Checked = IsMedication == 2 ? true : false;
            rdbtnScheduled.Checked = IsMedication == 1 ? true : false;
            FillManageScheduleCountDetails();
        }
    }

    protected void UCtrSchedule_SetReadings(object sender, EventArgs e)
    {
        try
        {
            ////UCtrMedInvtry.ResetPhysicianOrderPanels();
        }
        catch (Exception ex)
        {
            AddExceptionToErrorLog(ex, MethodBase.GetCurrentMethod().Name);
        }
    }

    /// <summary>
    /// This event is used tm set error message.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void UCtrSchedule_SetMsg(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            AddExceptionToErrorLog(ex, MethodBase.GetCurrentMethod().Name);
        }
    }


    protected void UCtrSchedule_ResetMsg(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            AddExceptionToErrorLog(ex, MethodBase.GetCurrentMethod().Name);
        }
    }

    /// <summary>
    /// This method is used to empty errormessage label.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void UCtrSchedule_ResetErrorMsg(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            AddExceptionToErrorLog(ex, MethodBase.GetCurrentMethod().Name);
        }
    }

    /// <summary>
    /// This event is used tm set error message.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void UCtrSchedule_SetErrorMsg(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            AddExceptionToErrorLog(ex, MethodBase.GetCurrentMethod().Name);
        }
    }

    protected void btnSaveSchedule_Click(object sender, EventArgs e)
    {
        MedicationBL oCountScheduleBL = new MedicationBL();
        bool valid = false;
        int IsMedication = 0;
        if (Request.QueryString["IsMedication"] != null && Convert.ToInt32((Request.QueryString["IsMedication"])) == 2)
        {
            btnSaveSchedule.ValidationGroup = "Med";
            IsMedication = 2;
        }
        

        DateTime odt = Convert.ToDateTime(Constants.S_DEFAULT_DATE_2);
        if (txtStartDate.Text != "" && TxtStartTime.Text != "")
        {
            odt = Convert.ToDateTime(txtStartDate.Text + " " + TxtStartTime.Text);
            valid = true;
        }
        

        int serviceId = 0;
        string Caption = "Update";
        if (Request.QueryString["ServiceId"] != null)
            serviceId = Convert.ToInt32(Request.QueryString["ServiceId"]);
        UCtrSchedule.Client_ID = "4020";//Client_ID;

      
                                    //Start-Added by Sparsh(Finoit)-4ML-DYZ-6TDN
        if (IsCountasAdmin.Checked)
        {
            if (hidIsAdminCount.Value.ToString() == "As Admin")
            {
                string scriptstring = "alert('Only one Count as Admin is Allowed!');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", scriptstring, true);
                valid = false;
                hidIsAdminCount.Value = "";
            }
        } //End

        if (UCtrSchedule.CheckValidationForTime()) //--this function checks the Schedule option selected and the value of Time 1 . A particular time of the day should be entered by user as per the selected Schedule option.
            return;
        if (UCtrSchedule.CauseOtherTimeValidation()) //-- this function checks that if Other Time is selected in Schedule options then Time 1 should not be blank.
            return;
        if (UCtrSchedule.CountErrorMsg() && valid)
        {
            UCtrSchedule.StartSchCountDateTime = odt;
            oCountScheduleBL = UCtrSchedule.PopulateCountMedScheduleing(oCountScheduleBL, Caption, serviceId);
            oCountScheduleBL.SaveCountMedSchedule("Add", Constants.I_ONE, serviceId, 0, null);
            lblSavedMsg.Visible = true;
        }
        TxtStartTime.Text = txtStartDate.Text = string.Empty;
        FillManageScheduleCountDetails();
        UCtrSchedule.ResetManageScheduleCountsControls();
        UCtrSchedule.EnableControls(true);
    }

    protected void grdMedCountSchedule_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int iRowIndex = Convert.ToInt32(e.CommandArgument.ToString());
            int iMedCountSchedulingId = Convert.ToInt32(grdMedCountSchedule.DataKeys[iRowIndex]["MedCountSchedulingId"]);
            DateTime dtCurrentDateTime = Convert.ToDateTime(CommonUtilityBL.GetEmployeeCurrentDateTime(System.Web.HttpContext.Current.Session[Constants.S_CONSTANT_TIME_ZONE].ToString()));
            if (e.CommandName == "EditDetails")
            {
                //UCtrSchedule.SetCountScheduleControl(true, iMedCountSchedulingId);
            }
            if (e.CommandName == "DeleteDetails")
            {
                DeleteCountRecord(iMedCountSchedulingId);
                string scriptstring = "alert('Deleted Successfully');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", scriptstring, true);
                hidIsAdminCount.Value = "";
                FillManageScheduleCountDetails();
                UCtrSchedule.ResetManageScheduleCountsControls();
                UCtrSchedule.EnableControls(true);
            }

        }
        catch (Exception ex)
        {
            AddExceptionToErrorLog(ex, MethodBase.GetCurrentMethod().Name);
        }
    }

    /// <summary>
    /// This method id used to delete the selected count schedule record
    /// </summary>
    /// <param name="iMedCountSchedulingId"></param>
    private void DeleteCountRecord(int iMedCountSchedulingId)
    {
        try
        {
            MedicationBL oMedicationBL = new MedicationBL();
            oMedicationBL.DeleteCountRecord(iMedCountSchedulingId, Convert.ToInt32(Session[Constants.S_SESSION_EMPLOYEE_ID]), Convert.ToDateTime(CommonUtilityBL.GetEmployeeCurrentDateTime(System.Web.HttpContext.Current.Session[Constants.S_CONSTANT_TIME_ZONE].ToString())));
        }
        catch (Exception ex)
        {
            AddExceptionToErrorLog(ex, MethodBase.GetCurrentMethod().Name);
        }
    }


    private void CommonSetUp()
    {
        try
        {
            UCtrSchedule.SetValidationGroupForTime("Med");
            UCtrSchedule.ResetMedicationTimeControls();
            UCtrSchedule.ResetDaysControls();
            UCtrSchedule.CheckUnCheckedOtherTimeOptionBtn(false);
            UCtrSchedule.SetScheduleForRoutineMedicationOptBtn(true);
            UCtrSchedule.EnableControls(false);
        }
        catch (Exception ex)
        {
            AddExceptionToErrorLog(ex, MethodBase.GetCurrentMethod().Name);
        }
    }

    /// <summary>
    /// This method id used to fill the details of Schedule count.
    /// </summary>
    private void FillManageScheduleCountDetails()
    {
        try
        {
            MedicationBL oMedicationBL = new MedicationBL();
            int ServiceId = 0;
            if (Request.QueryString["ServiceId"] != null)
                ServiceId = Convert.ToInt32(Request.QueryString["ServiceId"]);
            oMedicationBL.Id = ServiceId;
            DataTable OdtCountSchDetails = oMedicationBL.GetMedicationScheduleCountDetails();
            txtMedicationName.Text = OdtCountSchDetails.Rows[0]["Medication"].ToString();
            if (OdtCountSchDetails.Rows[0]["MedCountSchedulingId"].ToString() == "0")
                OdtCountSchDetails = null;
            UCtrSchedule.IsMedication = true;
            grdMedCountSchedule.DataSource = OdtCountSchDetails;
            grdMedCountSchedule.DataBind();
            //UCtrSchedule.SetScheduleControls(true, true, ServiceId);


        }
        catch (Exception ex)
        {
            AddExceptionToErrorLog(ex, MethodBase.GetCurrentMethod().Name);
        }
    }


    /// <summary>
    /// This method is used to add exception to error log table in database.
    /// </summary>
    /// <param name="ex"></param>
    /// <param name="asMethodName"></param>
    private void AddExceptionToErrorLog(Exception ex, string asMethodName)
    {
        BusinessLogic.Exceptions.ExceptionHandler.WriteExceptionToErrorLog
          (ex.Message, ex.StackTrace,
            MethodBase.GetCurrentMethod().DeclaringType.FullName + '.' + asMethodName,
            Convert.ToInt32(Session["Company_Id"]));
    }


    protected void grdMedCountSchedule_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Added by Sparsh(Finoit)--
              if(e.Row.Cells[0].Text == "As Admin")    
                hidIsAdminCount.Value = e.Row.Cells[0].Text;
              

                //ImageButton imgDelete =   e.Row.Cells[0].Controls[0] as LinkButton;
                //del.Attributes.Add("onclick","return confirm('Are you sure you want to delete this event?');");
            }
        }
        catch (Exception ex)
        {
            AddExceptionToErrorLog(ex, MethodBase.GetCurrentMethod().Name);
        }
    }
}