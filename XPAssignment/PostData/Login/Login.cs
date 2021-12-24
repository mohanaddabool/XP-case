using System.ComponentModel.DataAnnotations;

namespace XPAssignment.PostData.Login
{
    public class Login
    {
        [EmailAddress]
        public string? EmailAddress { get; set; }
        public string? Password { get; set; }
    }
}
