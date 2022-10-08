using System.ComponentModel.DataAnnotations;
using RSHB_Exam_ModelLib.Base;

namespace RSHB_Exam_ModelLib
{
    /// <summary>
    /// Сотрудник организации
    /// </summary>
    public class Employee : BaseEntity
    {
        /// <summary>
        /// Полное ФИО сотрудника
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Название должности сотрудника
        /// </summary>
        public string JobTitle { get; set; }

        public Employee(string fullName,  string jobTitle)
        {
            FullName = fullName;
            JobTitle = jobTitle;
        }

        public Employee(long id, string fullName, string jobTitle)
        {
            Id = id;
            FullName = fullName;
            JobTitle = jobTitle;
        }

        public Employee()
        {

        }

        public override string ToString()
        {
            return $"Id: [{Id}]; FullName: [{FullName}]; JobTitle: [{JobTitle}];";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, FullName, JobTitle);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Employee);
        }

        public bool Equals(Employee employee)
        {
            if(employee != null)
            {
                if(GetHashCode() == employee.GetHashCode())
                {
                    return true;
                } 
            }
            
            return false;
        }
    }
}
