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
    /// 联系人
    /// </summary>
    [Table("contacts")]
    public class Contact
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int EnterpriseId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [DisplayName("姓名")]
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Column(TypeName = "int")]
        [DisplayName("性别")]
        public Gender Gender { get; set; }

        public string Mobile { get; set; }

        public string QQ { get; set; }

        public string Address { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }

    }

    public enum Gender
    {
        [Description("其他")]
        Others,
        [Description("男")]
        Male,
        [Description("女")]
        Female
    }

    
}
