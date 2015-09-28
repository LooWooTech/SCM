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
    /// 企业
    /// </summary>
    [Table("enterprises")]
    public class Enterprise
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// 企业名称
        /// </summary>
        [DisplayName("企业名称")]
        public string Name { get; set; }
        /// <summary>
        /// 企业地址
        /// </summary>
        [DisplayName("企业地址")]
        public string Address { get; set; }

        [Column(TypeName = "int")]
        public Business Business { get; set; }
    }

    public enum Business
    {
        [Description("供应商")]
        Supplier,
        [Description("销售商")]
        Seller
    }
}
