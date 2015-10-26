using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace LoowooTech.SCM.Model
{
    [Table("order_products")]
    public class OrderProduct
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int OrderID { get; set; }

        public int ProductID { get; set; }

        public double Price { get; set; }

        public int Number { get; set; }

        public double? DealPrice { get; set; }

        public int? DealNumber { get; set; }

        [NotMapped]
        public Product Product { get; set; }

        public ProductStatus Status { get; set; }
    }

    public enum ProductStatus
    {
        [Description("开始生产")]
        Producing,
        [Description("生产完成")]
        Completed
    }

}
