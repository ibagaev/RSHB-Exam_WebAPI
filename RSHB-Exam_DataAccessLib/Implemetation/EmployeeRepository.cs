using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RSHB_Exam_DataAccessLib.DataContext;
using RSHB_Exam_DataAccessLib.Exceptions;
using RSHB_Exam_ModelLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSHB_Exam_DataAccessLib.Implemetation
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDataContext _context;
        private readonly ILogger _logger;

       
        public EmployeeRepository(AppDataContext context, ILogger<EmployeeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Employee Add(Employee employee)
        {
            try
            {
                _context.employees.Add(employee);
                _context.SaveChanges();

                _logger.LogDebug($"Добавлен пользователь: {employee}");

                return employee;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.ToLower().Contains("unique constraint"))
                {
                    throw new UniqueConstraintException();
                }
                else
                {
                    _logger.LogError(ex.InnerException?.Message);
                    throw ex;
                }
            }
            catch (UniqueConstraintException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message);
                _logger?.LogTrace(ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public List<Employee> AddRange(List<Employee> employees)
        {
            try
            {
                _context.employees.AddRange(employees);
                _context.SaveChanges();

                employees.ForEach(e => { _logger.LogDebug($"Добавлен пользователь: {e}"); });

                return employees;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.ToLower().Contains("unique constraint"))
                {
                    _logger?.LogDebug(ex.Message);

                    throw new UniqueConstraintException();
                }
                else
                {
                    _logger?.LogError(ex.InnerException?.Message);
                    _logger?.LogTrace(ex.InnerException?.StackTrace);
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message);
                _logger?.LogTrace(ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void Delete(long id)
        {
            try
            {
                var removeEmployee = FindById(id);
                _context.employees.Remove(removeEmployee);
                _context.SaveChanges();
            }
            catch (RecordNotFoundException ex)
            {
                _logger?.LogDebug(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message);
                _logger?.LogTrace(ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public List<Employee> FindAll(int page, int pageSize)
        {
            
            if (page == 0)
            {
                page = 1;
            } 

            if(pageSize == 0 || pageSize > 1000)
            {
                pageSize = IEmployeeRepository.MAX_PAGE_SIZE;
            }

            var skip = (page - 1) * pageSize;
            
            try
            {
                var employees = _context.employees.Skip(skip).Take(pageSize).ToList();

                _logger.LogDebug($"Получено записей: {employees.Count}");

                return employees;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message);
                _logger?.LogTrace(ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public List<Employee> FindByContaintFullName(string query, int page, int pageSize)
        {
            if (page == 0)
            {
                page = 1;
            }

            if (pageSize == 0 || pageSize > 1000)
            {
                pageSize = IEmployeeRepository.MAX_PAGE_SIZE;
            }

            var skip = (page - 1) * pageSize;

            try
            {
                var employees = _context.employees.Where(e => e.FullName.Contains(query)).Skip(skip).Take(pageSize).ToList();

                _logger.LogDebug($"Получено записей: {employees.Count}");

                return employees;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message);
                _logger?.LogTrace(ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public List<Employee> FindByContaintJobTitle(string query, int page, int pageSize)
        {
            if (page == 0)
            {
                page = 1;
            }

            if (pageSize == 0 || pageSize > 1000)
            {
                pageSize = IEmployeeRepository.MAX_PAGE_SIZE;
            }

            var skip = (page - 1) * pageSize;

            try
            {
                var employees = _context.employees.Where(e => e.JobTitle.Contains(query)).Skip(skip).Take(pageSize).ToList();

                _logger.LogDebug($"Получено записей: {employees.Count}");

                return employees;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message);
                _logger?.LogTrace(ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public Employee FindById(long id)
        {
            try
            {
                var employee = _context.employees.Find(id);

                if(employee == null)
                {
                    throw new RecordNotFoundException(id);
                }

                _logger.LogDebug($"Найден сотрудник: {employee}");
                
                return employee;
            }
            catch (RecordNotFoundException ex)
            {
                _logger?.LogDebug(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message);
                _logger?.LogTrace(ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public Employee Update(Employee employee)
        {
            try
            {
                var updateEmployee = FindById(employee.Id);

                _context.employees.Update(employee);
                _context.SaveChanges();
                
                return employee;   
            }
            catch (RecordNotFoundException ex)
            {
                _logger?.LogDebug(ex.Message);
                throw;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.ToLower().Contains("unique constraint"))
                {
                    throw new UniqueConstraintException();
                }
                else
                {
                    _logger.LogError(ex.InnerException?.Message);
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message);
                _logger?.LogTrace(ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }
    }
}
