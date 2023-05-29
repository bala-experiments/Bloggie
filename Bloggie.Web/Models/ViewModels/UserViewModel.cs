namespace Bloggie.Web.Models.ViewModels
{
    public class UserViewModel
    { 

        public List<User> UsersList { get; set; }
    }

    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
    }
}
