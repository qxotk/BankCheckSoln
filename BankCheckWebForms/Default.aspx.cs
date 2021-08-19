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

        protected void btnDisplay_Click(object sender, EventArgs e)
        {
            ClearAlert();

            // Validate form input
            string inputPayee = txtCheckPayee.Text.Trim();
            string inputDate = txtCheckIssueDate.Text.Trim();
            string inputAmount = txtCheckAmount.Text.Trim();
            List<string> errorMessages = ValidateForm(inputPayee, inputDate, inputAmount);
            if (errorMessages.Count > 0)
            {
                lblAlert.Text = errorMessages.Aggregate((a, b) => a + "<br/>" + b);
                pnlAlert.Visible = true;
                return;
            }

            // Validate input values
            string payee = inputPayee;
            DateTime issueDate = DateTime.Parse(inputDate);
            Decimal amount = decimal.Parse(inputAmount);
            errorMessages = BankCheck.ValidateAttributes(payee, issueDate, amount);
            if (errorMessages.Count > 0)
            {
                lblAlert.Text = errorMessages.Aggregate((a, b) => a + "<br/>" + b);
                pnlAlert.Visible = true;
                return;
            }

            // Acquire a new BankCheck
            BankCheck bankCheck = BankCheck.CreateValidBankCheck(payee, issueDate, amount);
            if (bankCheck == null)
            {
                lblAlert.Text = "Not able to create a bank check with the given input values.";
                pnlAlert.Visible = true;
                return;
            }

            // Render the bank check to the page.
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
            pnlBankCheckDisplay.Visible = false;
            ClearAlert();
        }

        protected void ShowPrintedCheck(BankCheck check)
        {
            lblDateIssued.Text = check.IssueDate.ToString("d");
            lblPayee.Text = check.PayeeName;
            lblNumericAmount.Text = check.Amount.ToString("c");
            lblTextAmount.Text = check.GetTextAmount();
            pnlBankCheckDisplay.Visible = true;
        }

        protected void ClearAlert()
        {
            lblAlert.Text = "";
            pnlAlert.Visible = false;
        }

        protected List<string> ValidateForm(string inputPayee, string inputDate, string inputAmount)
        {
            bool isValid = true;

            List<string> messages = new List<string>();

            // Validate Payee field.
            isValid &= !String.IsNullOrEmpty(inputPayee);
            if (!isValid)
            {
                messages.Add("Payee field is required.");
            }

            // Validate Date field
            isValid &= !String.IsNullOrEmpty(inputDate);
            if (!isValid)
            {
                messages.Add("Date is required.");
            }

            DateTime issueDate;
            isValid &= DateTime.TryParse(inputDate, out issueDate);
            if (!isValid)
            {
                messages.Add("Date is not a valid date.");
            }

            // Validate Amount field
            isValid &= !String.IsNullOrEmpty(inputAmount);
            if (!isValid)
            {
                messages.Add("Amount is required.");
            }

            isValid &= (isValid && Decimal.TryParse(inputAmount, out _));
            if (!isValid)
            {
                messages.Add("Amount is not a valid decimal number.");
            }

            int countDecimalPlaces = (inputAmount.Contains(".") ? inputAmount.Split('.')[1].Length : 0);
            isValid &= (isValid && countDecimalPlaces == 2);
            if (!isValid)
            {
                messages.Add("Amount must be in Dollars and Cents, to exaclty 2 decimal places.");
            }

            return messages;
        }
    }
}