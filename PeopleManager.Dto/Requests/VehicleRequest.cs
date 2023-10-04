using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleManager.Dto.Requests
{
    public class VehicleRequest
    {
        [Required]
        public required string LicensePlate { get; set; }
        public string? Brand { get; set; }
        public string? Type { get; set; }
        public int? ResponsiblePersonId { get; set; }
    }
}
