using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodelineStore.Data.Model
{
    public class ProductReport
    {
        [Key]
        public int ReportId { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [ForeignKey(nameof(Client))]
        public int ClientId { get; set; }
        public Client Client { get; set; }

        [Required(ErrorMessage = "Reason for reporting is required.")]
        public string Reason { get; set; }

        [Required(ErrorMessage = "Report date is required.")]
        public DateTime ReportDate { get; set; }
    }
}

