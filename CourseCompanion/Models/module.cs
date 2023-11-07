
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseCompanion.Models
{
    public class module
    {
        [Key]
        public int module_id { get; set; }

        [Column("semester")]
        public int semester { get; set; }

        [Column("name")]
        public string name { get; set; }

        [Column("code")]
        public string code { get; set; }

        [Column("credits")]
        public double credits { get; set; }

        [Column("weekly_hrs")]
        public double weekly_hrs { get; set; }

        [Column("num_weeks")]
        public int num_weeks { get; set; }

        [Column("hrs_left")]
        public double hrs_left { get; set; }

        [Column("user_id")]
        public int user_id { get; set; }

        [Column("selfstudy_hrs")]
        public double selfstudy_hrs { get; set; }

    }

}
