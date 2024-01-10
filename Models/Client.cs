using System.ComponentModel.DataAnnotations;

namespace HomeAppStore.Models
{
    public class Client
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public int Product_cost1 { get; set; }
        public int Product_cost2 { get; set; }
        public int Product_cost3 { get; set; }
        public int Product_cost4 { get; set; }
        public int Product_cost5 { get; set; }
        public int Product_cost6 { get; set; }
        public int Discount { get; set; }
    }
}
