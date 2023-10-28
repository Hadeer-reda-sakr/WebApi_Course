namespace WebApi_Task.DTO
{
	public class DepartsTraineeDTO
	{
		public int DeptId {  get; set; }
		public string DepartName { get; set; }

		public List<string> TranieerName { get; set; }=new List<string>();
		public List<string> courses { get; set; } = new List<string>();

		public List<string> Instructors { get; set; } = new List<string>();

	}
}
