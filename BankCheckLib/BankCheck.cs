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
            int dollars = Convert.ToInt32(decimal.Truncate(Amount));
            int cents = Convert.ToInt32(Amount.ToString("#.00").Split('.')[1]);
            return $"{FormatDollars(dollars)} and {FormatCents(cents)}";
        }

        private string FormatDollars(int dollars)
        {
            if (dollars == 0)
            {
                return "No Dollars";
            }

            string textDollars = TranslateNumbersToText(dollars); //digits.Aggregate<int, string>("", (a,b) => $"{a}{DigitTextMap[b]}-{PlaceText(b)}");
            return $"{textDollars} Dollars";
        }

        private string FormatCents(int cents)
        {
            //return $"{DigitTextMap[cents]} Cents";
            return "notimpl Cents";
        }

        private string TranslateNumbersToText(int dollars)
        {
            //IEnumerable<int> digits = dollars.ToString().ToCharArray().ToList().Select(x => Convert.ToInt32(x.ToString())).Reverse();
            if (dollars == 0)
            {
                return "";
            }

            if (dollars < 20)
            {
                return MapTo19[dollars];
            }

            if (dollars < 100)
            {
                return $"{MapTensFrom20To90[dollars / 10]}-{MapTo19[dollars % 10]}";
            }
            if (dollars < 1000)
            {
                return $"{MapTo19[dollars / 100]} {MapPowers[2]} {TranslateNumbersToText(dollars % 100)}";
            }
            if (dollars < 10000)
            {
                return $"{MapTo19[dollars / 1000]} {MapPowers[3]} {TranslateNumbersToText(dollars % 1000)}";
            }
            if (dollars < 100000)
            {
                return $"{TranslateNumbersToText(dollars / 1000)} {MapPowers[3]} {TranslateNumbersToText(dollars % 1000)}";
            }
            // 10^0: 00000 - 00009 - Amount < 10                    n = Take1 : MapTo19[n] 
            // 10^1: 00010 - 00019 - Amount > 10 && Amount < 20     n = Take2 : MapTo19[n]
            // 10^1: 00020 - 00099 - Amount > 19 && Amount < 100    n = Take2 : MapTensFrom20To90[m / 10] + "-" + Map19[n mod 10]
            // 10^2: 00100 - 00999 - Amount > 99 && Amount < 1000   n = Take3 : Map19[n / 100] + MapPowers[2] + " " + (handle like Amount < 100)
            // 10^3: 01000 - 09999 - Amount > 999 && Amount < 10000 n = Take4 : Map19[Take2] + MapPowers[3] + " " + (handle like Amount < 1000)
            // 10^4: 10000 - 99999 -  
            // 10000 - 99999 - 
            return "Too Many";
        }

        // MapTo19 - {{ 0, ""}, {1, "One"}, {2, "Two"}, ... {19, "Nineteen"}}
        private Dictionary<int, string> MapTo19 = new Dictionary<int, string>(20)
        {
            { 0, "" },
            { 1, "One" },
            { 2, "Two" },
            { 3, "Three" },
            { 4, "Four" },
            { 5, "Five" },
            { 6, "Six" },
            { 7, "Seven" },
            { 8, "Eight" },
            { 9, "Nine" },
            { 10, "Ten" },
            { 11, "Eleven" },
            { 12, "Twelve" },
            { 13, "Thirteen" },
            { 14, "Fourteen" },
            { 15, "Fifteen" },
            { 16, "Sixteen" },
            { 17, "Seventeen" },
            { 18, "Eighteen" },
            { 19, "Nineteen" },
        };

        // MapTensFrom20To90 {{2, "Twenty"}, {3, "Thirty"}, ... {9, "Ninety"}}
        private Dictionary<int, string> MapTensFrom20To90 = new Dictionary<int, string>(20)
        {
            { 2, "Twenty" },
            { 3, "Thirty" },
            { 4, "Fourty" },
            { 5, "Fifty" },
            { 6, "Sixty" },
            { 7, "Seventy" },
            { 8, "Eighty" },
            { 9, "Ninety" },
        };

        // MapPowers - {{2, " Hundred"}, {3, " Thousand"}, {6, "Million"}, {9, "Billion"}, {12, "Trillion"}}
        private Dictionary<int, string> MapPowers = new Dictionary<int, string>(5)
        {
            {2, " Hundred"},
            {3, " Thousand"},
            {6, "Million"},
            {9, "Billion"},
            {12, "Trillion"}
        };

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
