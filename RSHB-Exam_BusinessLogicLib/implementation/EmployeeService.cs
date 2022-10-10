using Microsoft.Extensions.Logging;
using RSHB_Exam_BusinessLogicLib.Exceptions;
using RSHB_Exam_DataAccessLib;
using RSHB_Exam_DataAccessLib.Exceptions;
using RSHB_Exam_ModelLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSHB_Exam_BusinessLogicLib.implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger _logger;

        public EmployeeService(IEmployeeRepository employeeRepository, ILogger<EmployeeService> logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public Employee ChangeEmployeeFullName(long id, string newFullName)
        {
            try
            {
                Employee employeeToUpdate = _employeeRepository.FindById(id);

                employeeToUpdate.FullName = newFullName;

                return _employeeRepository.Update(employeeToUpdate);

            }
            catch (UniqueConstraintException)
            {
                throw new RSHBExamAlreadyExistExeption(newFullName);
            }
            catch (RecordNotFoundException)
            {
                throw new RSHBExamNotFoundException(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogTrace(ex.StackTrace);

                throw new RSHBExamException(ex.Message);
            }
        }

        public Employee ChangeEmployeeFullName(string currentFullName, string newFullName)
        {
            try
            {
                Employee employeeToUpdate = _employeeRepository.FindByContaintFullName(currentFullName, 0, 0).First();

                employeeToUpdate.FullName = newFullName;

                return _employeeRepository.Update(employeeToUpdate);

            }
            catch (UniqueConstraintException)
            {
                throw new RSHBExamAlreadyExistExeption(newFullName);
            }
            catch (RecordNotFoundException)
            {
                throw new RSHBExamNotFoundException(currentFullName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogTrace(ex.StackTrace);

                throw new RSHBExamException(ex.Message);
            }
        }

        public Employee ChangeEmployeeJobTitle(long id, string newJobTitle)
        {
            try
            {
                Employee employeeToUpdate = _employeeRepository.FindById(id);

                employeeToUpdate.JobTitle = newJobTitle;

                return _employeeRepository.Update(employeeToUpdate);

            }
            catch (RecordNotFoundException)
            {
                throw new RSHBExamNotFoundException(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogTrace(ex.StackTrace);

                throw new RSHBExamException(ex.Message);
            }
        }

        public Employee ChangeEmployeeJobTitle(string currentFullName, string newJobTitle)
        {
            try
            {
                Employee employeeToUpdate = _employeeRepository.FindByContaintFullName(currentFullName, 0, 0).First();

                employeeToUpdate.JobTitle = newJobTitle;

                return _employeeRepository.Update(employeeToUpdate);

            }
            catch (RecordNotFoundException)
            {
                throw new RSHBExamNotFoundException(currentFullName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogTrace(ex.StackTrace);

                throw new RSHBExamException(ex.Message);
            }
        }

        public Employee DeleteEmployee(Employee employee)
        {
            try
            {
                Employee deleteinEmployee;

                if (employee.Id > 0)
                {
                    deleteinEmployee = _employeeRepository.FindById(employee.Id);

                    if (!deleteinEmployee.Equals(employee))
                    {
                        throw new RSHBExamException("Переданный объект не соответствует объекту в базе данных");
                    }

                    _employeeRepository.Delete(deleteinEmployee.Id);

                    _logger.LogDebug($"Employee: {deleteinEmployee} удалён");

                    deleteinEmployee.Id = 0;

                    return deleteinEmployee;
                }
                else
                {
                    throw new RSHBExamException("Переданный объект не содержи ID сотрудника");
                }

            }
            catch (RecordNotFoundException)
            {
                throw new RSHBExamNotFoundException(employee.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogTrace(ex.StackTrace);

                throw new RSHBExamException(ex.Message);
            }
        }

        public Employee DeleteEmployee(long id)
        {
            try
            {
                Employee deleteinEmployee;

                if (id > 0)
                {
                    deleteinEmployee = _employeeRepository.FindById(id);

                    _employeeRepository.Delete(id);

                    _logger.LogDebug($"Employee: {deleteinEmployee} удалён");
                    
                    deleteinEmployee.Id = 0;

                    return deleteinEmployee;
                }
                else
                {
                    throw new RSHBExamException("ID сотрудника должен быть больше 0");
                }

            }
            catch (RecordNotFoundException)
            {
                throw new RSHBExamNotFoundException(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogTrace(ex.StackTrace);

                throw new RSHBExamException(ex.Message);
            }
        }

        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                int i = 0;
                bool hasMore = true;
                

                while (hasMore)
                {
                    i++;

                    List<Employee> getEmployees = _employeeRepository.FindAll(i, IEmployeeRepository.MAX_PAGE_SIZE);

                    if(getEmployees.Count < IEmployeeRepository.MAX_PAGE_SIZE)
                    {
                        hasMore = false;
                    }

                    employees.AddRange(getEmployees);
                }

                return employees;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogTrace(ex.StackTrace);

                throw new RSHBExamException(ex.Message);
            }
        }

        public Employee GetEmployeeById(long id)
        {
            try
            {
                return _employeeRepository.FindById(id);
            }
            catch (RecordNotFoundException)
            {
                throw new RSHBExamNotFoundException(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogTrace(ex.StackTrace);

                throw new RSHBExamException(ex.Message);
            }
        }

        public Employee NewEmployee(Employee employee)
        {
            try
            {
                return _employeeRepository.Add(employee);
            }
            catch (UniqueConstraintException)
            {
                throw new RSHBExamAlreadyExistExeption(employee.FullName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogTrace(ex.StackTrace);

                throw new RSHBExamException(ex.Message);
            }
        }

        public Employee NewEmployee(string fullName, string jobTitle)
        {
            Employee employee = new Employee(fullName, jobTitle);

            try
            {
                return _employeeRepository.Add(employee);
            }
            catch (UniqueConstraintException)
            {
                throw new RSHBExamAlreadyExistExeption(employee.FullName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogTrace(ex.StackTrace);

                throw new RSHBExamException(ex.Message);
            }
        }

        public List<Employee> SaerchEmployeesByJobTitle(string jobTitle)
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                int i = 0;
                bool hasMore = true;

                while (hasMore)
                {
                    i++;

                    List<Employee> getEmployees = _employeeRepository.FindByContaintJobTitle(jobTitle, i, IEmployeeRepository.MAX_PAGE_SIZE);

                    if (getEmployees.Count < IEmployeeRepository.MAX_PAGE_SIZE)
                    {
                        hasMore = false;
                    }

                    employees.AddRange(getEmployees);
                }

                return employees;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogTrace(ex.StackTrace);

                throw new RSHBExamException(ex.Message);
            }
        }

        public List<Employee> SearchEmployeesByFullName(string fullName)
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                int i = 0;
                bool hasMore = true;

                while (hasMore)
                {
                    i++;

                    List<Employee> getEmployees = _employeeRepository.FindByContaintFullName(fullName, i, IEmployeeRepository.MAX_PAGE_SIZE);

                    if (getEmployees.Count < IEmployeeRepository.MAX_PAGE_SIZE)
                    {
                        hasMore = false;
                    }

                    employees.AddRange(getEmployees);
                }

                return employees;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogTrace(ex.StackTrace);

                throw new RSHBExamException(ex.Message);
            }
        }
    }
}
