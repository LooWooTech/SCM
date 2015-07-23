using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace loowootech.SCM.Model
{
    /// <summary>
    /// 部件进货订单
    /// </summary>
    [Table("orders")]
    public class Order
    {
        public Order()
        {
            this.Time = DateTime.Now;
        }
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public DateTime Time { get; set; }
        /// <summary>
        /// 快递单
        /// </summary>
        public string Express { get; set; }
        /// <summary>
        /// 合同
        /// </summary>
        public string Indenture { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public int EID { get; set; }
        [NotMapped]
        public Enterprise Enterprise { get; set; }
        
    }
}
