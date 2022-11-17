using System.ConponentModel.DataAnnotaions;

namespace TranMinhDucBTH2.Models
{
    public class Student
    {
        [Key]

        public string StdID { get; set; }

        public string StdName { get; set; }

        public string FacultyID { get; set; }
        [ForeignKey("FacultyID")]

        public Faculty? Faculty { get; set; }

    }
}
