using System;
using System.Collections.Generic;
using System.Linq;
using MyProject.Core.Entities;
using MyProject.Data.Daos;
namespace MyProject.Tasks
{
    /// <summary>
    ///
    /// </summary>
    public class AdminUserRoleTask
    {
        private readonly AdminUserRoleDao _adimnUserRole = new AdminUserRoleDao();
        private readonly AdminRoleMenuDao _adminRoleMenuDao = new AdminRoleMenuDao();
        private readonly AdminRolePowerDao _adminRolePowerDao = new AdminRolePowerDao();

        public List<AdminUserRole> GetAll()
        {
            return _adimnUserRole.GetAll();
        }

        public AdminUserRole GetByRoleId(int roleId)
        {
            return _adimnUserRole.GetByRoleId(roleId);
        }

        public void Create(string roleName, List<int> menuIds, List<int> powerIds)
        {
            var adminUserRole = new AdminUserRole
                                    {
                                        RoleName = roleName,
                                    };
            var roleId = Convert.ToInt32(_adimnUserRole.Add(adminUserRole));

            var menuList = menuIds.Select(c => new AdminRoleMenu
                                               {
                                                   RoleId = roleId,
                                                   MenuId = c,
                                               }).ToList();
            var powerList = powerIds.Select(c => new AdminRolePower
                                                     {
                                                         RoleId = roleId,
                                                         PowerId = c,
                                                     }).ToList();
            foreach (var adminRoleMenu in menuList)
                _adminRoleMenuDao.Add(adminRoleMenu);
            foreach (var adminRolePower in powerList)
                _adminRolePowerDao.Add(adminRolePower);

        }

        public void Update(int roleId, string roleName, List<int> menuIds, List<int> powerIds)
        {
            var adminUserRole = _adimnUserRole.GetByRoleId(roleId);
            if (adminUserRole != null)
            {
                adminUserRole.RoleName = roleName;
                _adimnUserRole.Update(adminUserRole);

                _adminRolePowerDao.DeleteByRoleId(roleId);
                _adminRoleMenuDao.DeleteByRoleId(roleId);

                var menuList = menuIds.Select(c => new AdminRoleMenu
                                                       {
                                                           RoleId = roleId,
                                                           MenuId = c,
                                                       }).ToList();
                var powerList = powerIds.Select(c => new AdminRolePower
                                                         {
                                                             RoleId = roleId,
                                                             PowerId = c,
                                                         }).ToList();
                foreach (var adminRoleMenu in menuList)
                    _adminRoleMenuDao.Add(adminRoleMenu);
                foreach (var adminRolePower in powerList)
                    _adminRolePowerDao.Add(adminRolePower);
            }
        }
    }
}

