using System;

namespace DodoApp.Contracts.V1.Responses
{
    public class ReadCurrencySummaryPerDayDto
    {
        public DateTime Day { get; set; }
        public int TotalFundChange { get; set; }
        public int TotalProfitChange { get; set; }
        public int FundAmount { get; set; }
        public int ProfitAmount { get; set; }
    }
}