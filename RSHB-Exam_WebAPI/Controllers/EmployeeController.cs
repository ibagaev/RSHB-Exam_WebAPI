using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSHB_Exam_BusinessLogicLib;
using RSHB_Exam_BusinessLogicLib.Exceptions;
using RSHB_Exam_ModelLib;
using RSHB_Exam_WebAPI.APIModel;

namespace RSHB_Exam_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            try
            {
                List<Employee> findEmploees = _employeeService.GetAllEmployees();

                if (findEmploees == null || findEmploees.Count == 0)
                {
                    return NotFound("В базе данных отсутствуют записи");
                }
                else
                {
                    return Ok(findEmploees);
                }
            }
            catch (RSHBExamException ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogTrace(ex.StackTrace);
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(long id)
        {
            try
            {
                return Ok(_employeeService.GetEmployeeById(id));
            }
            catch (RSHBExamNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (RSHBExamException ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogTrace(ex.StackTrace);
                return StatusCode(500);
            }
        }

        [HttpGet("fullname")]
        public IActionResult GetEmployeesByFullName([FromQuery] string query)
        {
            try
            {
                List<Employee> findEmploees = _employeeService.SearchEmployeesByFullName(query);
                
                if(findEmploees == null || findEmploees.Count == 0)
                {
                    return NotFound("Совпадения не найдены");
                }
                else
                {
                    return Ok(findEmploees);
                }
                
            }
            catch (RSHBExamException ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogTrace(ex.StackTrace);
                return StatusCode(500);
            }
        }

        [HttpGet("jobtitle")]
        public IActionResult GetEmployeesByJobTitle([FromQuery] string query)
        {
            try
            {
                List<Employee> findEmploees = _employeeService.SaerchEmployeesByJobTitle(query);

                if (findEmploees == null || findEmploees.Count == 0)
                {
                    return NotFound("Совпадения не найдены");
                }
                else
                {
                    return Ok(findEmploees);
                }

            }
            catch (RSHBExamException ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogTrace(ex.StackTrace);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public IActionResult NewEmployee([FromBody] EmployeeRequest employeeRequest)
        {
            try
            {
                return Created("", _employeeService.NewEmployee(employeeRequest.covertToEntityModel()));
            }
            catch (RSHBExamAlreadyExistExeption ex)
            {
                return BadRequest(ex.Message);
            }
            catch (RSHBExamException ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogTrace(ex.StackTrace);
                return StatusCode(500);
            }
        }

        [HttpPut("changefullname/{id}")]
        public IActionResult ChangeEmployeeFullName(long id, string newFullName)
        {
            try
            {
                EmployeeRequest employeeRequest = new EmployeeRequest()
                {
                    FullName = newFullName
                };

                return Ok(_employeeService.ChangeEmployeeFullName(id, employeeRequest.FullName));
            }
            catch (RSHBExamAlreadyExistExeption ex)
            {
                return BadRequest(ex.Message);
            }
            catch (RSHBExamNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (RSHBExamException ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogTrace(ex.StackTrace);
                return StatusCode(500);
            }
        }

        [HttpPut("changefullname")]
        public IActionResult ChangeEmployeeFullName(string currentFullName, string newFullName)
        {

            try
            {
                EmployeeRequest employeeRequest = new EmployeeRequest()
                {
                    FullName = newFullName
                };

                return Ok(_employeeService.ChangeEmployeeFullName(currentFullName, employeeRequest.FullName));
            }
            catch (RSHBExamAlreadyExistExeption ex)
            {
                return BadRequest(ex.Message);
            }
            catch (RSHBExamNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (RSHBExamException ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogTrace(ex.StackTrace);
                return StatusCode(500);
            }
        }

        [HttpPut("changejobtitle/{id}")]
        public IActionResult ChangeEmployeeJobTitle(long id, string newJobTitle)
        {
           
            try
            {
                return Ok(_employeeService.ChangeEmployeeJobTitle(id, newJobTitle));
            }
            catch (RSHBExamAlreadyExistExeption ex)
            {
                return BadRequest(ex.Message);
            }
            catch (RSHBExamNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (RSHBExamException ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogTrace(ex.StackTrace);
                return StatusCode(500);
            }
        }

        [HttpPut("changejobtitle")]
        public IActionResult ChangeEmployeeJobTitle(string currentFullName, string newJobTitle)
        {

            try
            {
                return Ok(_employeeService.ChangeEmployeeJobTitle(currentFullName, newJobTitle));
            }
            catch (RSHBExamNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (RSHBExamException ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogTrace(ex.StackTrace);
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployeeById(long id)
        {
            try
            {
                return Ok(_employeeService.DeleteEmployee(id));
            }
            catch (RSHBExamNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (RSHBExamException ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogTrace(ex.StackTrace);
                return StatusCode(500);
            }
        }
    }
}
