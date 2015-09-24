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
    /// 通讯录
    /// </summary>
    [Table("addresslist")]
    public class AddressList
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Column(TypeName="int")]
        public ContactWay way { get; set; }
        public string Value { get; set; }
        public int CID { get; set; }
    }

    public enum ContactWay
    {
        [Description("QQ")]
        QQ,
        [Description("电话")]
        TelPhone,
        [Description("邮箱")]
        Email,
        [Description("微信")]
        WeChat,
        [Description("地址")]
        Address
    }
}
