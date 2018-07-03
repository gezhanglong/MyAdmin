using System;
using System.Collections.Generic;
using System.Linq;
using MyProject.Services.ORM;
using MyProject.Core.Entities;
namespace MyProject.Data.Daos
{
	/// <summary>
	/// 
    /// </summary>
    [DbFactory("MyP")]
	public class AdminRolePowerDao : BaseDao<AdminRolePower>
	{
	    public List<AdminRolePower> GetListByRoleId(int roleId)
	    {
	        var sql = Sql.Builder.Where("RoleId = @0", roleId);
	        return Query(sql).ToList();
	    }

	    public void DeleteByRoleId(int roleId)
	    {
            var sql = Sql.Builder.Where("RoleId = @0", roleId);
	        Delete(sql);
	    }
	}
}

