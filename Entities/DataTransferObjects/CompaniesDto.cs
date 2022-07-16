using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class CompaniesDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; } = string.Empty;

        public string FullAddress { get; set; } = string.Empty;
    }
}
