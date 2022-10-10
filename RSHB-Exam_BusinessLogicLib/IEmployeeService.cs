using RSHB_Exam_ModelLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSHB_Exam_BusinessLogicLib
{
    public interface IEmployeeService
    {
        public List<Employee> GetAllEmployees();
        public Employee GetEmployeeById(long id);
        public List<Employee> SearchEmployeesByFullName(string fullName);
        public List<Employee> SaerchEmployeesByJobTitle(string jobTitle);

        public Employee NewEmployee(Employee employee);
        public Employee NewEmployee(string fullName, string jobTitle);

        public Employee ChangeEmployeeFullName(long id, string newFullName);
        public Employee ChangeEmployeeFullName(string currentFullName, string newFullName);
        public Employee ChangeEmployeeJobTitle(long id, string newJobTitle);
        public Employee ChangeEmployeeJobTitle(string currentFullName, string newJobTitle);

        public Employee DeleteEmployee(Employee employee);
        public Employee DeleteEmployee(long id);
    }
}
