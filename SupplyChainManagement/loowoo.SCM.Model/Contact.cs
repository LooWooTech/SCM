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
    /// 联系人
    /// </summary>
    [Table("contacts")]
    public class Contact
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [DisplayName("姓名")]
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Column(TypeName="int")]
        [DisplayName("性别")]
        public Sex sex { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        [DisplayName("联系电话")]
        public string TelPhone { get; set; }
        [DisplayName("QQ")]
        public string QQ { get; set; }
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        [DisplayName("邮箱")]
        public string Email { get; set; }
        [DisplayName("微信号")]
        public string WeChat { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [DisplayName("联系地址")]
        public string Address { get; set; }
        public int EID { get; set; }
    }

    public enum Sex
    {
        [Description("男")]
        male,
        [Description("女")]
        female
    }
}
