using System.ComponentModel.DataAnnotations;

namespace Services.CouponAPI.Dtos
{
    public class CouponDto
    {
        [Required]
        public string? CouponCode { get; set; }
        [Required]
        public double DiscountAmount { get; set; }
        public int MinAmount { get; set; }
    }

}
