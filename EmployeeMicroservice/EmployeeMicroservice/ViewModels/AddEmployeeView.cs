namespace EmployeeMicroservice.ViewModels
{
    public class AddEmployeeView
    {
        public int? EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int EmployeeTypeId { get; set; }
        public bool IsBlocked { get; set; }
    }
}
