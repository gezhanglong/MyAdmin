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
	public class AdminUserRoleDao : BaseDao<AdminUserRole>
	{
	    public List<AdminUserRole> GetAll()
	    {
	        var sql = Sql.Builder.Where("1=1");
	        return Query(sql).ToList();
	    }

	    public AdminUserRole GetByRoleId(int roleId)
	    {
	        var sql = Sql.Builder.Where("RoleId = @0", roleId);
	        return FirstOrDefault(sql);
	    }
	}
}

