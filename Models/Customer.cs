using System;
using System.ComponentModel.DataAnnotations;

namespace MinimalAPI.Models
{
    public class CustomerData
    {
        [Range(1, int.MaxValue, ErrorMessage = "ID must be greater than 0.")]
        public int ID { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "UserID must be greater than 0.")]
        public int UserID { get; set; }

        [Required(ErrorMessage = "EmployeeID is required.")]
        public required string EmployeeID { get; set; }

        [Required(ErrorMessage = "SiteName is required.")]
        public required string SiteName { get; set; }

        [Required(ErrorMessage = "BusinessUnitName is required.")]
        public required string BusinessUnitName { get; set; }

        [Required(ErrorMessage = "AccountName is required.")]
        public required string AccountName { get; set; }

        [Required(ErrorMessage = "GroupName is required.")]
        public required string GroupName { get; set; }

        [Required(ErrorMessage = "CategoryName is required.")]
        public required string CategoryName { get; set; }

        [Required(ErrorMessage = "TypeName is required.")]
        public required string TypeName { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Duration is required.")]
        public required string Duration { get; set; }

        public bool IsProcessed { get; set; }
    }
}
