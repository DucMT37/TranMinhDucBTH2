using System.ComponentModel.DataAnnotiona;

namespace TranMinhDucBTH2.Models

{
    public class Faculty
    {
        [key]
        public string FacultyID { get; set; }
        public string FacultyName { get; set; }
    }
}