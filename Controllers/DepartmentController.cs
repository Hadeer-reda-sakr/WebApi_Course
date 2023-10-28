using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_Task.Models;
using WebApi_Task.DTO;
using Microsoft.EntityFrameworkCore;

namespace WebApi_Task.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DepartmentController : ControllerBase
	{
		ITI_MVCContext context;
		public DepartmentController(ITI_MVCContext context)
		{
			this.context = context;
		}


		[HttpGet]
		public ActionResult GetAll()
		{
			List<Department> departments= context.Departments.ToList();
			
			List<DepartsTraineeDTO> departs = new List<DepartsTraineeDTO>();

            foreach (var item in departments)
            {
				DepartsTraineeDTO depart = new DepartsTraineeDTO()
				{
					DeptId = item.id,
					DepartName = item.name,
					Instructors = item.Instructors.Select(d => d.Name).ToList(),
					courses = item.Courses.Select(e=>e.Name).ToList(),
					TranieerName=item.Traniees.Select(e=>e.Name).ToList()
				};
				departs.Add(depart);
            }



            return Ok(departs);
		}

		[HttpGet("{id:int}")]
		public ActionResult<Department> GetById(int id)
		{
			Department d = context.Departments.Include(d=>d.Courses).Include(d=>d.Instructors).Include(d=>d.Traniees) .FirstOrDefault(y => y.id == id);

			DepartsTraineeDTO departsTrainee = new DepartsTraineeDTO()
			{
				DeptId=d.id,
				DepartName=d.name,
				courses = d.Courses.Select(d => d.Name).ToList(),
				Instructors = d.Instructors.Select(d => d.Name).ToList(),
				
			};

            foreach (Traniee t in d.Traniees)
            {
				departsTrainee.TranieerName.Add($"{t.Name} - {t.Grade}");
            }
            if (d != null)
			{
				return Ok(d);
			}
			else

				return NotFound();
		}

		[HttpPost]

		public ActionResult Add(Department d)
		{
			if (d== null) return BadRequest();
			if (ModelState.IsValid)
			{
				context.Departments.Add(d);
				try
				{
					context.SaveChanges();
					return Created("Added", context.Departments.ToList());

				}
				catch
				{
					return BadRequest(ModelState);
				}

			}
			else
				return BadRequest(ModelState);
		}



		[HttpPut]
		public ActionResult Edit(Department d)
		{
			if (ModelState.IsValid)
			{
				context.Entry(d).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
				context.SaveChanges();

				return NoContent();
			}
			else

				return BadRequest(ModelState);

		}

		[HttpDelete]
		public ActionResult delete(int id)
		{
			Department d = context.Departments.FirstOrDefault(u => u.id == id);

			if (d == null)
			{
				return NotFound();
			}
			else
			{
				context.Departments.Remove(d);
				context.SaveChanges();
				return Ok(d);
			}

		}
	}
}
