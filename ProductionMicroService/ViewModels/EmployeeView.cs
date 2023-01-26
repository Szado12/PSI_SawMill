namespace EmployeeMicroservice.ViewModels
{
    public class EmployeeView
    {
        public int EmployeeId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public bool IsBlocked { get; set; }

        public string? EmployeeType { get; set; }
    }
}
