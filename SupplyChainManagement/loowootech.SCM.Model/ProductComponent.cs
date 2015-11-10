using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace LoowooTech.SCM.Model
{
    [Table("product_components")]
    public class ProductComponent
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// 部件数量
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 对应的产品
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// 对应的部件
        /// </summary>
        public int ComponentId { get; set; }

        [NotMapped]
        public int StoreNumber { get; set; }

        [NotMapped]
        public Component Component { get; set; }
    }
}
