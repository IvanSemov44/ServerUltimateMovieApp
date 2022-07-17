using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class EmployeeForCreationDto
    {
        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }

        public string Position { get; set; } = string.Empty;
    }
}
