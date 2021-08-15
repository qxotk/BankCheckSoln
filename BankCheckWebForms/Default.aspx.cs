using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using BankCheckLib;

namespace BankCheckWebForms
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitForm();
            }
        }

        protected void btnViewCheck_Click(object sender, EventArgs e)
        {
            ClearAlert();
            List<string> errorMessages = ValidateForm();
            if (errorMessages.Count > 0)
            {
                lblAlert.Text = errorMessages.Aggregate((a, b) => a + "<br/>" + b);
                pnlAlert.Visible = true;
                return;
            }

            string payee = txtCheckPayee.Text.Trim();
            DateTime issueDate = Convert.ToDateTime(txtCheckIssueDate.Text.Trim());
            decimal amount = Convert.ToDecimal(txtCheckAmount.Text.Trim());
            BankCheck bankCheck = new BankCheck(payee, issueDate, amount);
            ShowPrintedCheck(bankCheck);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            InitForm();
        }

        protected void InitForm()
        {
            txtCheckPayee.Text = "";
            txtCheckIssueDate.Text = "";
            txtCheckIssueDate.Attributes["min"] = DateTime.Today.Date.ToString("yyyy-MM-dd");
            txtCheckAmount.Text = "";
            pnlCheckResult.Visible = false;
            ClearAlert();
        }

        protected void ShowPrintedCheck(BankCheck check)
        {
            lblDateIssued.Text = check.IssueDate.ToString("d");
            lblPayee.Text = check.PayeeName;
            lblNumericAmount.Text = check.Amount.ToString("c");
            lblTextAmount.Text = check.GetTextAmount();
            pnlCheckResult.Visible = true;
        }

        protected void ClearAlert()
        {
            lblAlert.Text = "";
            pnlAlert.Visible = false;
        }

        protected List<string> ValidateForm()
        {
            bool isValid = true;
            List<string> messages = new List<string>();
            isValid &= !String.IsNullOrEmpty(txtCheckPayee.Text.Trim());
            if (!isValid)
            {
                messages.Add("Payee field is required.");
            }
            isValid &= !String.IsNullOrEmpty(txtCheckIssueDate.Text.Trim());
            if (!isValid)
            {
                messages.Add("Date is required.");
            }
            DateTime issueDate;
            isValid &= DateTime.TryParse(txtCheckIssueDate.Text.Trim(), out issueDate);
            if (!isValid)
            {
                messages.Add("Date is not a valid date.");
            }
            isValid &= (isValid && issueDate >= DateTime.Today);
            if (!isValid)
            {
                messages.Add("Date must not be in the past.");
            }
            decimal amount;
            isValid = Decimal.TryParse(txtCheckAmount.Text.Trim(), out amount);
            if (!isValid)
            {
                messages.Add("Amount is not a valid amount of money.");
            }
            isValid = (isValid && amount >= 0.01M);
            if (!isValid)
            {
                messages.Add("Amount must be more than $0.01.");
            }
            return messages;
        }
    }
}