using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace LoowooTech.SCM.Model
{
    [Table("order_items")]
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int OrderID { get; set; }

        public int ItemID { get; set; }

        public OrderItemType ItemType { get; set; }

        public double Price { get; set; }

        public int Number { get; set; }

        public double? DealPrice { get; set; }

        public int? DealNumber { get; set; }

        [NotMapped]
        public int StoreNumber { get; set; }

        [NotMapped]
        public string ItemName { get; set; }

        /// <summary>
        /// Product的Components
        /// </summary>
        [NotMapped]
        public List<ProductComponent> Items { get; set; }

        public OrderItemStatus Status { get; set; }
    }

    public enum OrderItemType
    {
        Component,
        Product
    }

    public enum OrderItemStatus
    {
        [Description("库存充足")]
        Normal,
        [Description("库存不足")]
        Shortage,
        [Description("开始生产")]
        Producing
    }

}
