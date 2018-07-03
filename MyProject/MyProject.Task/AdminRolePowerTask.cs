using System;
using System.Collections.Generic;
using MyProject.Core.Entities;
using MyProject.Data.Daos;
namespace MyProject.Tasks
{
	/// <summary>
	///
	/// </summary>
	public class AdminRolePowerTask
	{
		private readonly AdminRolePowerDao _adminRolePower = new AdminRolePowerDao();

	    public List<AdminRolePower> GetListByRoleId(int roleId)
	    {
	        return _adminRolePower.GetListByRoleId(roleId);
	    }
	}
}

