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
                //get all students in db
                var retrivedstudents = await _studentRepository.GetStudents();
                var studentViewModel = retrivedstudents.Select(x => new StudentViewModel 
                {
                    id = x.id,
                    Name = x.Name,
                    Gender = x.Gender,
                    Class = x.Class
                }).ToList();
               
                aResponse.Successful = true;
                aResponse.Message = "Retrived All Students";
                aResponse.Data = studentViewModel;

                
            } catch (Exception e)
            {
                aResponse.Successful = false;
                aResponse.Message = e.Message;
            }
            return Ok(aResponse);
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<StudentController>
        [HttpPost]
        public async Task<IActionResult> AddStudent(StudentViewModel studentViewModel)
        {
            //initialize AResponse
            AResponse<Student> aResponse = new AResponse<Student>();
            try
            {
                var student = new Student()
                {
                    Name = studentViewModel.Name,
                    Class = studentViewModel.Class,
                    Gender = studentViewModel.Gender,
                    Date = DateTime.Now
                };

                Student studentCreated = await _studentRepository.CreateStudent(student);

                if(studentCreated == null)
                {
                    throw new InvalidOperationException("could not add student to db");
                }
                aResponse.Message = "Successfully created Student";
                aResponse.Data = studentCreated;
                aResponse.Successful = true;

                return Ok(aResponse);
            }
            catch (Exception e)
            {
                aResponse.Successful = false;
                aResponse.Message = e.Message;

                return Ok(aResponse);
            }
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
