using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace LoowooTech.SCM.Model
{
    public class Components
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string Specification { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 填单机类型
        /// </summary>
        [Column(TypeName = "int")]
        public UnitType Type { get; set; }
    }

    public enum UnitType
    {
        [Description("CPU")]
        CPU,
        [Description("机壳")]
        Crust,
        [Description("票据打印机")]
        BillPrinter,
        [Description("显示屏")]
        Display,
        [Description("主板")]
        MainBoard
    }
}
