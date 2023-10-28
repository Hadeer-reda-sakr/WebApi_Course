namespace WebApi_Task.DTO
{
	public class TranieeDepartDTO
	{
		public int id {  get; set; }	

		public string name { get; set; }

		public string address { get; set; }
		public int Dept_id { get; set; }

		public string DepartmentName { get; set; }

		public List<string> CoursesName { get; set; } = new List<string>();


	}
}
