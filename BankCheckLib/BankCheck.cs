using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankCheckLib
{
    public class BankCheck
    {
        public string PayeeName { get; private set; }
        public DateTime IssueDate { get; private set; }
        public decimal Amount { get; private set; }

        public string GetTextAmount()
        {
            decimal dollars = decimal.Truncate(Amount);
            decimal cents = decimal.Subtract(Amount, dollars);
            return $"{FormatDollars(dollars)} and {FormatCents(cents)}";
        }

        private string FormatDollars(decimal dollars)
        {
            return $"{dollars.ToString("some dollars")} Dollars";
        }

        private string FormatCents(decimal cents)
        {
            return $"{cents:00} / 100 cents";
        }

        public BankCheck(string payeeName, DateTime issueDate, Decimal amount)
        {
            PayeeName = payeeName;
            IssueDate = issueDate;
            Amount = amount;
        }

        private BankCheck()
        {
            // hide default constructor
        }
    }
}
