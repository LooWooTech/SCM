using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace LoowooTech.SCM.Model
{
    /// <summary>
    /// 部件进货订单
    /// </summary>
    [Table("orders")]
    public class Order
    {
        public Order()
        {
            this.CreateTime = DateTime.Now;
        }
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 快递ID
        /// </summary>
        public int Express { get; set; }

        /// <summary>
        /// 快递单号
        /// </summary>
        public string ExpressNo { get; set; }
        /// <summary>
        /// 是否有合同文件
        /// </summary>
        public bool Indenture { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public int EnterpriseId { get; set; }

        public bool Deleted { get; set; }

        [Column(TypeName = "int")]
        public OrderType Type { get; set; }

        public int State { get; set; }

        /// <summary>
        /// 是否付款
        /// </summary>
        public bool Payment { get; set; }

        [NotMapped]
        public Enterprise Enterprise { get; set; }

        [NotMapped]
        public Remittance Remittance { get; set; }

        [NotMapped]
        public List<OrderItem> Items { get; set; }

        [NotMapped]
        public List<OrderItem> Products { get; set; }

    }

    public enum SellOrderState
    {
        [Description("创建订单")]
        Created,
        [Description("正在配货")]
        Prepare,
        [Description("配送途中")]
        Delivery,
        [Description("已收货")]
        Receive,
        [Description("合同备案")]
        Contract,
        [Description("完成")]
        Done,
        [Description("关闭订单")]
        Closed

    }

    public enum BuyOrderState
    {
        [Description("联系卖家")]
        Contact,
        [Description("配货")]
        Place,
        [Description("发货")]
        Shipping,
        [Description("完成")]
        Done,
        [Description("收货入库")]
        Receive,
        [Description("汇款")]
        Payment,
        [Description("填写合同")]
        Contract,
    }

    public enum OrderType
    {
        [Description("进货")]
        Bought,
        [Description("出货")]
        Shipment
    }
}
