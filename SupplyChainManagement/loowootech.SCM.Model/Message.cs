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
            this.ContactTime = DateTime.Now;
        }
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// 文字
        /// </summary>
        public string Note { get; set; }
        /// <summary>
        /// 某年某月某日
        /// </summary>
        public DateTime ContactTime { get; set; }

        public int EnterpriseId { get; set; }

        public int ContactId { get; set; }
        [NotMapped]
        public Contact Contact { get; set; }

        public int OrderId { get; set; }
    }
}
