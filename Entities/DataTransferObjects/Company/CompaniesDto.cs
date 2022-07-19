namespace Entities.DataTransferObjects.Company
{
    public class CompaniesDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string FullAddress { get; set; } = string.Empty;
    }
}
