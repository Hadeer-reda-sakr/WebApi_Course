using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi_Task.Models;
using WebApi_Task.DTO;

namespace WebApi_Task.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TraineeController : ControllerBase
	{
		ITI_MVCContext context;
		public TraineeController(ITI_MVCContext context)
		{
			this.context = context;
		}
		[HttpGet]
		public ActionResult GetAll()
		{
			List<Traniee> traniees =  context.Traniees.Include(d=>d.Dept).Include(c=>c.CrsResults).ToList();

			List<TranieeDepartDTO> dTOs = new List<TranieeDepartDTO>();

            foreach (var item in traniees)
            {
				TranieeDepartDTO dTO = new TranieeDepartDTO()
				{
					id = item.Id,
					name = item.Name,
					CoursesName = item.CrsResults.Select(d => d.course.Name).ToList(),
					DepartmentName = item.Dept.name
				};
				dTOs.Add(dTO);
            }

			return Ok(dTOs);
		}


		[HttpGet("{id:int}")]
		
		public ActionResult<Traniee> GetById(int id)
		{
			Traniee e= context.Traniees.Include(d=>d.Dept).FirstOrDefault(y => y.Id == id);


			TranieeDepartDTO tranieeDepart = new TranieeDepartDTO()
			{
				id = e.Id,
				name = e.Name,
				address=e.Address,
				CoursesName = e.CrsResults.Select(d => d.course.Name).ToList(),

				DepartmentName = e.Dept.name
			};


			if (e != null)
			{
				return Ok(e);
			}
			else

				return NotFound();
		}

		[HttpPost]

		public ActionResult Add(Traniee e)
		{
			if (e == null) return BadRequest();
			if (ModelState.IsValid)
			{
				context.Traniees.Add(e);
				try
				{
					context.SaveChanges();
					return Created("Added", context.Traniees.ToList());

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
		public ActionResult Edit(Traniee e )
		{
			if (ModelState.IsValid)
			{
				context.Entry(e).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
				context.SaveChanges();

				return NoContent();
			}
			else
			
				return BadRequest(ModelState);
			
		}



		[HttpDelete]
		public ActionResult delete(int id)
		{
			Traniee e = context.Traniees.FirstOrDefault(u => u.Id == id);

			if (e == null)
			{
				return NotFound();
			}
			else
			{
				context.Traniees.Remove(e);
				context.SaveChanges();
				return Ok(e);
			}

		}


	}
}
