using ECL.Models;

namespace ECL.Entity
{
    public class RoleMenuMapping
    {
        public int RoleMenuMappingId { get; set; }
        public int MenuId { get; set; }
        public string? MenuItem { get; set; }
        public string? MenuTag { get; set; }
        public int IsChecked { get; set; }
        public RoleMenuMapping[] Children { get; set; } = [];
    }

    public class SaveRoleMenuMappingByRoleId
    {
        public int RoleId { get; set; }
        public RoleMenuMapping[] RoleMenuMappings { get; set; } = [];
    }

    public class Admin : CommonResponse
    {
        public RoleMenuMapping[] RoleMenuMappingDetails { get; set; } = [];
    }
}
