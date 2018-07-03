using System;
using System.Collections.Generic;
using MyProject.Core.Entities;
using MyProject.Data.Daos;
namespace MyProject.Tasks
{
	/// <summary>
	///
	/// </summary>
	public class AdminRoleMenuTask
	{
		private readonly AdminRoleMenuDao _adminRoleMenu = new AdminRoleMenuDao();

	    public List<AdminRoleMenu> GetListByRoleId(int roleId)
	    {
	        return _adminRoleMenu.GetListByRoleId(roleId);
	    }

	    public List<AdminMenu> GetMenuLstByRoleId(int roleId)
	    {
	        return _adminRoleMenu.GetMenuLstByRoleId(roleId);
	    }
	}
}

