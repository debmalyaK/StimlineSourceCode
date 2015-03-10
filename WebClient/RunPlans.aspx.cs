using ServiceReference;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RunPlans : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (UIRepository.Instance.NeedToRefreshUICache)
        {
            UIRepository.Instance.FetchInitialDataFromServer();
            UIRepository.Instance.NeedToRefreshUICache = false;
        }
        BindInitialData();
    }

    private void BindInitialData()
    {
        try
        {
            ///Bind all run plans
            var items = from run in UIRepository.Instance.AllRunPlans
                        select new { Name = run.Name, Task = run.RunTask };
            dgvRunPlans.DataSource = items.ToList();
            dgvRunPlans.DataBind();

            ///Bind all units
            ListItem[] units = (from run in UIRepository.Instance.AllRunPlans
                                select new ListItem { Value = run.UnitId }).ToArray();
            cmbUnit.Items.Clear();
            cmbUnit.Items.Add(new ListItem("--Select--"));
            cmbUnit.Items.AddRange(units);

            ///Bind all projects
            ListItem[] projects = (from run in UIRepository.Instance.AllRunPlans
                                   select new ListItem { Value = run.ProjectName }).ToArray();
            cmbSharedProject.Items.Clear();
            cmbSharedProject.Items.Add(new ListItem("--Select--"));
            cmbSharedProject.Items.AddRange(projects);
        }
        catch (Exception ex)
        {

        }
    }

    protected void OnRowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(dgvRunPlans, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to view run plan details.";
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void dgvRunPlans_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in dgvRunPlans.Rows)
        {
            try
            {
                if (row.RowIndex == dgvRunPlans.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    row.ToolTip = string.Empty;
                    PopulateRunplanDetails(row.Cells[0].Text);
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "Click to view run plan details..";
                }
            }
            catch (Exception ex)
            {

            }
        }
    }

    private void PopulateRunplanDetails(string name)
    {
        try
        {
            ///Bind selected run plan details
            RunPlanInfo plan = UIRepository.Instance.AllRunPlans.First(x => x.Name.Equals(name));
            txtName.Text = plan.Name;
            //txtRunTask.Text = plan.RunTask;
            cmbUnit.SelectedIndex = cmbUnit.Items.IndexOf(cmbUnit.Items.FindByText(plan.UnitId));
            cmbSharedProject.SelectedIndex = cmbSharedProject.Items.IndexOf(cmbSharedProject.Items.FindByText(plan.ProjectName));
            txtEstimatedDuration.Text = plan.EstimatedDuration.ToString();
            txtCustomer.Text = plan.CustomerName;
            txtWell.Text = plan.WellName;

            ///Bind runs for the selected run plan
            if (UIRepository.Instance.AllRuns != null)
            {
                List<RunInfo> runs = UIRepository.Instance.AllRuns.Where(x => x.RunPlanId == plan.Id).ToList();
                dgvRuns.DataSource = runs;
                dgvRuns.DataBind();
                lblRunCount.Text = "Found " + runs.Count + " rows";
            }
        }
        catch (Exception ex)
        {

        }
    }
}