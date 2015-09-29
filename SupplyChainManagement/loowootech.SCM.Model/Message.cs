using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace LoowooTech.SCM.Model
{
    /// <summary>
    /// 与供应商 销售商互动信息
    /// </summary>
    [Table("messages")]
    public class Message
    {
        public Message()
        {
            this.Time = DateTime.Now;
        }
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// 文字
        /// </summary>
        public string Word { get; set; }
        /// <summary>
        /// 某年某月某日
        /// </summary>
        public DateTime Time { get; set; }

        public int EnterpriseId { get; set; }

        public int ContactId { get; set; }
        [NotMapped]
        public Contact Contact { get; set; }
    }
}
