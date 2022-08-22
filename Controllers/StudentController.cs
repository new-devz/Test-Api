using API_Aplication.Generics;
using API_Aplication.Model;
using API_Aplication.Model.ViewModel;
using API_Aplication.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Aplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        // GET: api/<StudentController>
        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetAllStudents()
        {
            AResponse<List<StudentViewModel>> aResponse = new AResponse<List<StudentViewModel>>();
            try
            {

                //get all Students form the database
                List<Student> Student = await _studentRepository.GetStudents();
                aResponse.Successful = true;
                aResponse.Message = "";
                aResponse.Data = Student;

                return Ok(aResponse);
            }
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<StudentController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
