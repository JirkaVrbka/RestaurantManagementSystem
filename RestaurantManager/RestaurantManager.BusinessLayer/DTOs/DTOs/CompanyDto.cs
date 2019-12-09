using System;
using System.ComponentModel.DataAnnotations;

namespace RestaurantManager.BusinessLayer.DTOs.DTOs
{
    public class CompanyDto : DtoBase
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Your company has to have an Ico")]
        public int Ico { get; set; }
        public DateTime? JoinDate { get; set; }

        public EmployeeDto Owner { get; set; }
    }
}
