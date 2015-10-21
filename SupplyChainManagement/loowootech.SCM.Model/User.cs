using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace LoowooTech.SCM.Model
{
    /// <summary>
    /// 经销商
    /// </summary>
    [Table("users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int EnterpriseId { get; set; }

        public Role Role { get; set; }

        public DateTime? LastLoginTime { get; set; }

        public string LastLoginIp { get; set; }

        public bool Deleted { get; set; }
    }

    public enum Role
    {
        Guest,
        User,
        Admin
    }
}
