using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseCompanion.Models
{
    public class user
    {
        [Key]
        public int user_id { get; set; }

        [Column("username")]
        public string username { get; set; }

        [Column("password")]
        public string password { get; set; }


    }
}
