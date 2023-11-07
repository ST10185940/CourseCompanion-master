using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseCompanion.Models
{
    public class semester
    {
        [Key]
        public int semester_id { get; set; }

        [Column("start_date")]
        public DateTime start_date { get; set; }

        [Column("num_weeks")]
        public int num_weeks { get; set; }

    }
}
