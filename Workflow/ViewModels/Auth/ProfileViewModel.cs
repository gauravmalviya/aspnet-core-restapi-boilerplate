namespace CWG.API.Workflow.ViewModels.Auth
{
    public class ProfileViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PictureUrl { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public  string Email { get; set; }

        public string FullName { get{
            return this.FirstName  + " " +this.LastName;
        }}
    }
}