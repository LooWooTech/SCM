using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [Column(TypeName="int")]
        public OrderType Type { get; set; }
        [Column(TypeName="int")]
        public State State { get; set; }
        [NotMapped]
        public Enterprise Enterprise { get; set; }
        [NotMapped]
        public Remittance Remittance { get; set; }
        
    }


    public enum State
    {
        [Description("订单生成")]
        place,
        [Description("发货")]
        shipping,
        [Description("完成")]
        Done,
        [Description("退货")]
        turn,
        [Description("已汇款")]
        payment
    }

    public enum OrderType
    {
        [Description("进货")]
        bought,
        [Description("出货")]
        shipment
    }
}
