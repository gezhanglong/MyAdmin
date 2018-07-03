using System;
using System.Collections.Generic;
using MyProject.Core.Entities;
using MyProject.Data.Daos;
using MyProject.Core.Dtos;
namespace MyProject.Tasks
{
	/// <summary>
	///
	/// </summary>
	public class AdminPowerTask
	{
		private readonly AdminPowerDao _adminPower = new AdminPowerDao();

        //public AdminPower Get(string controllerName, string actionName)
        //{
        //    return _adminPower.Get(controllerName, actionName);
        //}

        /// <summary>
        /// 获得用户在控制器上所有的权限
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="controller">控制器</param>
        /// <returns></returns>
        public List<PermDto> GetPermList(int roleId, string controller)
        {
            return _adminPower.GetPermList(roleId, controller);
        }

	    public List<AdminPower> GetListByMenuId(int menuId)
	    {
	        return _adminPower.GetListByMenuId(menuId);
	    }

	    public void Add(AdminPower info)
	    {
	        _adminPower.Add(info);
	    }

	    public void DeleteByPowerId(int powerId)
	    {
	        _adminPower.DeleteByPowerId(powerId);
	    }

	    public List<AdminPower> GetAll()
	    {
	        return _adminPower.GetAll();
	    }
	}
}

