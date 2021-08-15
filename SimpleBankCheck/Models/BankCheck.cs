using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBankCheck.Models
{
    public class BankCheck
    {
        public string PayeeName { get; private set; }
        public DateTime IssueDate { get; private set; }
        public Decimal Amount { get; private set; }

        public string AmountText()
        {
            return "not impl yet";
        }


        public BankCheck(string payeeName, DateTime issueDate, Decimal amount)
        {
            PayeeName = payeeName;
            IssueDate = issueDate;
            Amount = amount;
        }
    }
}