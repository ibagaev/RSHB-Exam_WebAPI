using RSHB_Exam_ModelLib;
using System.ComponentModel.DataAnnotations;

namespace RSHB_Exam_WebAPI.APIModel
{
    public class EmployeeRequest
    {
        [Required]
        [RegularExpression(@"^((\w+[-]?)*\s(\w+[-]?)*\s(\w+[-]?)*)+$", ErrorMessage = "Необходимо указать ФИО полностью!")]
        public string FullName { get; set; }
        [Required]
        public string JobTitle { get; set; }

        public EmployeeRequest()
        {

        }

        public EmployeeRequest(string fullName, string jobTitle)
        {
            FullName = fullName;
            JobTitle = jobTitle;
        }

        public Employee covertToEntityModel()
        {
            return new Employee(FullName, JobTitle);
        }
    }
}
