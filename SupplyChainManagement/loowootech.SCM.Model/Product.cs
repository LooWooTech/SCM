using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace LoowooTech.SCM.Model
{
    /// <summary>
    /// 产品
    /// </summary>
    [Table("products")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 售价
        /// </summary>
        public double Price { get; set; }
        [NotMapped]
        public List<Item> Items { get; set; }
        
        
    }
}
