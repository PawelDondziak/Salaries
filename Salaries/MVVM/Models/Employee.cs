using CsvHelper;
using CsvHelper.Configuration.Attributes;

namespace Salaries.MVVM.Models
{
    public class Employee
    {
        [Name("id")]
        public int Id { get; set; }

        [Name("firstname")]
        public string FirstName { get; set; } = string.Empty;

        [Name("lastname")]
        public string LastName { get; set; } = string.Empty;

        [Name("email")]
        public string Email { get; set; } = string.Empty;

        [Name("department")]
        public string Department { get; set; } = string.Empty;

        [Name("salary")]
        public int Salary { get; set; }

    }
}
