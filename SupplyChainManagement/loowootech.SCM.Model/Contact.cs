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
        public Sex sex { get; set; }
        public int EID { get; set; }
    }

    public enum Sex
    {
        [Description("其他")]
        Others,
        [Description("男")]
        male,
        [Description("女")]
        female
    }

    
}
