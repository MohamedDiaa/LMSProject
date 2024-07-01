using LMS.api.Model;
using System.ComponentModel.DataAnnotations;

namespace LMS.api.Validations
{
    public class CheckOverlappingDates : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            //const string errorMessage = $"{nameof(Module)} dates overlap!";

            var module = (ModuleDTO)validationContext.ObjectInstance;

            //var modulesList = _moduleRequestService.GetModulesByCourseIdAsync(module.CourseID);

            //foreach (var m in modulesList)
            //{
            //    if (module.CheckIfDateOverlaps(m.Start2, m.End2))
            //    {
            return ValidationResult.Success;
            //    }
            //}

            //return ValidationResult.Success;
        }
    }
}
