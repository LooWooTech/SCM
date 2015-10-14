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
    /// 汇款信息
    /// </summary>
    [Table("remittances")]
    public class Remittance
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public double Money { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 银行（支付宝）账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 开户行
        /// </summary>
        public string Bank { get; set; }
        /// <summary>
        /// 账号类型
        /// </summary>
        [Column(TypeName = "int")]
        public Payment Pay { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }
        
        public int OrderId { get; set; }
    }
    public enum Payment
    {
        [Description("支付宝")]
        Alipay,
        [Description("银联")]
        Unionpay,
        [Description("微信支付")]
        WeChat
    }
}
