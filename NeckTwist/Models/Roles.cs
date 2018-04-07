namespace NeckTwist.Models{
    public enum Role
    {
        Admin,
        Role1,
        Role2
    }

    public static class RoleExtensions
    {
        public static string GetRoleName(this Role role)
        {
            return role.ToString();
        }
    }
}