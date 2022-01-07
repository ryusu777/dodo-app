using System;
using System.ComponentModel.DataAnnotations;

namespace DodoApp.Contracts.V1.Requests
{
    public class GetCurrencySummaryDto
    {
        [Required]
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}