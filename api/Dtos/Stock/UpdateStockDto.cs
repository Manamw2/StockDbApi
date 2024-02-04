using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Stock
{
    public class UpdateStockDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Sympol can't exceed 10 characters")]
        public string Sympol { get; set; } = string.Empty;
        [Required]
        [MinLength(3, ErrorMessage = "Company's name must be atleast 3 characters")]
        [MaxLength(30, ErrorMessage = "Company's name can't exceed 30 characters")]
        public String CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(1, 1000000000, ErrorMessage = "Purchase must be value in between 1 and 1000000000")]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.001, 100, ErrorMessage = "LastDiv must be value in between 0.001 and 100")]
        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength(28, ErrorMessage = "Industry can't exceed 28 characters")]
        public String Industry { get; set; } = string.Empty;
        [Required]
        [Range(1, 9000000000, ErrorMessage = "MarketCap must be value in between 1 and 9000000000")]
        public long MarketCap { get; set; }
    }
}