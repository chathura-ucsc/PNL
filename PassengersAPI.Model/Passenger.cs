using System.ComponentModel.DataAnnotations;

namespace PassengersAPI.Models
{
    public class Passenger
    {
        [Required(ErrorMessage = "Passenger Title is required.")]
        [RegularExpression("^(MR|MRS)$", ErrorMessage = "Invalid Passenger Title")]
        public string Title { set; get; }

        [Required(ErrorMessage = "Passenger First Name is required.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Passenger First Name should contain letters only.")]

        public string FirstName { set; get; }

        [Required(ErrorMessage = "Passenger Last Name is required.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Passenger Last Name should contain letters only.")]
        public string LastName { set; get; }

        [Required(ErrorMessage = "Locator name is required.")]
        [RegularExpression(@"(.{6})", ErrorMessage = "Locator name should contain six(6) characters.")]
        public string LocatorName { set; get; }

    }
}