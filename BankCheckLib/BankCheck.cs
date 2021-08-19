using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankCheckLib
{
    public class BankCheck
    {
        public const Decimal MIN_VALID_AMOUNT = 0.01M; // One Cent
        public const Decimal MAX_VALID_AMOUNT = 999999999999999.99M; // 999 Trillion, 999 Billion, 999 Million, 999 Thousand, 999 Dollars and 99 cents.

        public string PayeeName { get; private set; }
        public DateTime IssueDate { get; private set; }
        public decimal Amount { get; private set; }

        public string GetTextAmount()
        {
            int cents = Convert.ToInt32(Amount.ToString("#.00").Split('.')[1]);
            long dollars = Convert.ToInt64(decimal.Truncate(Amount));
            return $"{FormatDollars(dollars)} and {FormatCents(cents)}";
        }

        public static BankCheck CreateValidBankCheck(string payeeName, DateTime issueDate, Decimal amount)
        {
            List<string> errorMessage = ValidateAttributes(payeeName, issueDate, amount);
            return errorMessage.Count == 0 ? new BankCheck(payeeName, issueDate, amount) : null;
        }

        public static List<string> ValidateAttributes(string payeeName, DateTime issueDate, Decimal amount)
        {
            List<string> errorMessages = new List<string>(3);
            if (String.IsNullOrEmpty(payeeName))
            {
                errorMessages.Add("Payee Name must not be blank.");
            }

            if (issueDate < DateTime.Today)
            {
                errorMessages.Add("Check date may not be in the past.");
            }

            if (amount < MIN_VALID_AMOUNT)
            {
                errorMessages.Add("Check amount must be at least $0.01.");
            }

            if (amount > MAX_VALID_AMOUNT)
            {
                errorMessages.Add("Check amount exceeds the maximum amount of $999,999,999,999,999.99");
            }

            return errorMessages;
        }

        private string FormatDollars(long dollars)
        {
            if (dollars == 0)
            {
                return "No Dollars";
            }

            string textDollars = TranslateNumbersToText(dollars);
            return $"{textDollars} Dollars";
        }

        private string FormatCents(int cents)
        {
            if (cents == 0)
            {
                return "No Cents";
            }

            if (cents < 20)
            {
                return $"{MapTo19[cents]} Cents";
            }

            if (cents < 100)
            {
                string centsOnesPlace = (cents % 10) > 0 ? $"-{MapTo19[cents % 10]}" : "";
                return $"{MapTensFrom20To99[cents / 10]}{centsOnesPlace} Cents";
            }

            return "Too many cents";
        }

        private string TranslateNumbersToText(long dollars)
        {
            // 0 - 19
            if (dollars < 20L)
            {
                return MapTo19[(int)dollars];
            }
            // 20 - 99
            if (dollars < 100L)
            {
                string dollarsOnesPlace = (dollars % 10 > 0) ? $"-{MapTo19[(int)(dollars % 10L)]}" : "";
                return $"{MapTensFrom20To99[(int)(dollars / 10L)]}{dollarsOnesPlace}";
            }
            // 100 - 999
            if (dollars < 1000L)
            {
                return $"{MapTo19[(int)(dollars / 100L)]} {MapPowers[2]} {TranslateNumbersToText(dollars % 100L)}";
            }
            // 1,000 - 9,999
            if (dollars < 10000L)
            {
                return $"{MapTo19[(int)(dollars / 1000L)]} {MapPowers[3]} {TranslateNumbersToText(dollars % 1000L)}";
            }
            // 10,000 - 99,000
            if (dollars < 100000L)
            {
                return $"{TranslateNumbersToText(dollars / 1000L)} {MapPowers[4]} {TranslateNumbersToText(dollars % 1000L)}";
            }
            // 100,000 - 999,000
            if (dollars < 1000000L)
            {
                return $"{TranslateNumbersToText(dollars / 1000L)} {MapPowers[5]} {TranslateNumbersToText(dollars % 1000L)}";
            }
            // 1,000,000 - 19,000,000 : 1MM - 19MM
            if (dollars < 20000000L)
            {
                return $"{MapTo19[(int)(dollars / 1000000L)]} {MapPowers[6]} {TranslateNumbersToText(dollars % 1000000L)}";
            }
            // 20,000,000 - 99,000,000 : 20MM - 99MM
            if (dollars < 100000000L)
            {
                return $"{TranslateNumbersToText(dollars / 1000000L)} {MapPowers[7]} {TranslateNumbersToText(dollars % 1000000L)}";
            }
            // 100,000,000 - 999,000,000 : 100MM - 999MM
            if (dollars < 1000000000L)
            {
                return $"{TranslateNumbersToText(dollars / 1000000L)} {MapPowers[8]} {TranslateNumbersToText(dollars % 1000000L)}";
            }
            // 1,000,000,000 - 19,000,000,000 : 1BB - 19BB
            if (dollars < 20000000000L)
            {
                return $"{MapTo19[(int)(dollars / 1000000000L)]} {MapPowers[9]} {TranslateNumbersToText(dollars % 1000000000L)}";
            }
            // 20,000,000,000 - 99,000,000,000 : 20BB - 99BB
            if (dollars < 100000000000L)
            {
                return $"{TranslateNumbersToText(dollars / 1000000000L)} {MapPowers[10]} {TranslateNumbersToText(dollars % 1000000000L)}";
            }
            // 100,000,000,000 - 999,000,000,000 : 100BB - 999BB
            if (dollars < 1000000000000L)
            {
                return $"{TranslateNumbersToText(dollars / 1000000000L)} {MapPowers[11]} {TranslateNumbersToText(dollars % 1000000000L)}";
            }
            // 1,000,000,000,000 - 19,000,000,000,000 : 1TT - 19TT
            if (dollars < 20000000000000L)
            {
                return $"{MapTo19[(int)(dollars / 1000000000000L)]} {MapPowers[12]} {TranslateNumbersToText(dollars % 1000000000000L)}";
            }
            // 20,000,000,000,000 - 99,000,000,000,000 : 20TT - 99TT
            if (dollars < 100000000000000L)
            {
                return $"{TranslateNumbersToText(dollars / 1000000000000L)} {MapPowers[13]} {TranslateNumbersToText(dollars % 1000000000000L)}";
            }
            // 100,000,000,000,000 - 999,000,000,000,000 : 100TT - 999TT
            if (dollars < 1000000000000000L)
            {
                return $"{TranslateNumbersToText(dollars / 1000000000000L)} {MapPowers[13]} {TranslateNumbersToText(dollars % 1000000000000L)}";
            }
            // 1,000,000,000,000,000 and Up - too  many dollars for us.
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
        private Dictionary<int, string> MapTensFrom20To99 = new Dictionary<int, string>(20)
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

        // MapPowers - {{2, " Hundred"}, {3, " Thousand"}, ... {6, "Million"}, ... {9, "Billion"}, ... {12, "Trillion"}}
        private Dictionary<int, string> MapPowers = new Dictionary<int, string>(5)
        {
            {2, "Hundred"},
            {3, "Thousand"},
            {4, "Thousand" },
            {5, "Thousand" },
            {6, "Million"},
            {7, "Million" },
            {8, "Million" },
            {9, "Billion"},
            {10, "Billion"},
            {11, "Billion" },
            {12, "Trillion"},
            {13, "Trillion" },
            {14, "Trillion" }
        };

        // Private Contstructors - use public factory method so that properties can be validated on back end.
        private BankCheck(string payeeName, DateTime issueDate, Decimal amount)
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
