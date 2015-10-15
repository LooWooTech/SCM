using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace LoowooTech.SCM.Model
{
    /// <summary>
    /// 价格记录
    /// </summary>
    [Table("product_price_logs")]
    public class ProductPriceLog
    {
        public ProductPriceLog()
        {
            this.CreateTime = DateTime.Now;
        }
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public DateTime CreateTime { get; set; }
        public double Price { get; set; }
        public int ProductId { get; set; }
    }
}
