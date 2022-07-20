namespace Entities.DataTransferObjects.Employee
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }

        public string Position { get; set; } = string.Empty;

    }
}
