namespace MyProject.Matrix.Controllers.SysManage.ViewModels
{
    public class RoleMenuModel
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public int ParentId { get; set; }
        public bool IsSelected { get; set; }
    }
}