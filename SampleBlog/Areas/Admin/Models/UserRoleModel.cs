namespace SB.Areas.Admin.Models
{
    public class UserRoleModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public bool Writer { get; set; }

        public bool Admin { get; set; }
    }
}