using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace LoowooTech.SCM.Model
{
    /// <summary>
    /// 价格记录（进货清单）
    /// </summary>
    [Table("quotations")]
    public class Quotation
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 指定的部件
        /// </summary>
        public int CID { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public int OID { get; set; }

        [NotMapped]
        public Components Components { get; set; }
    }
}
