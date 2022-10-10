using RSHB_Exam_ModelLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSHB_Exam_DataAccessLib
{
    public interface IEmployeeRepository
    {
        public const int MAX_PAGE_SIZE = 1000;

        /// <summary>
        /// Получение списка сотрудников
        /// </summary>
        /// <param name="page">Номер страницы</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <returns>Список объектов Employee из БД</returns>
        public List<Employee> FindAll(int page, int pageSize);

        /// <summary>
        /// Получение сотрудника по Id
        /// </summary>
        /// <param name="id">Уникальный идентификатор сотрудника</param>
        /// <returns>Объект Employee из БД</returns>
        public Employee FindById(long id);

        /// <summary>
        /// Получение списка сотрудников по содержанию ФИО
        /// </summary>
        /// <param name="fullName">Полное ФИО сотрудника</param>
        /// <param name="page">Номер страницы</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <returns>Объект Employee из БД</returns>
        public List<Employee> FindByContaintFullName(string query, int page, int pageSize);

        /// <summary>
        /// Получение списка сотрудников по названию должности
        /// </summary>
        /// <param name="jobTitle">Название должности</param>
        /// <param name="page">Номер страницы</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <returns>Список объектов Employee из БД</returns>
        public List<Employee> FindByContaintJobTitle(string query, int page, int pageSize);

        /// <summary>
        /// Добавление новго сотрудника
        /// </summary>
        /// <param name="employee">Объект Employee</param>
        /// <returns>Объект Employee из БД</returns>
        public Employee Add(Employee employee);

        /// <summary>
        /// Добавление списка сотрудников
        /// </summary>
        /// <param name="employees">Список объектов Employee</param>
        /// <returns>Список объектов Employee из БД</returns>
        public List<Employee> AddRange(List<Employee> employees);

        /// <summary>
        /// Обновление информации о сотруднике
        /// </summary>
        /// <param name="employee">Объект Employee</param>
        /// <returns>Объект Employee из БД</returns>
        public Employee Update(Employee employee);

        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Уникальный идентификатор сотрудника</returns>
        public void Delete(long id);
    }
}
