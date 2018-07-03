using System.Collections.Generic;
using System.Linq;
using MyProject.Core.Entities;
using MyProject.Services.ORM;

namespace MyProject.Data.Daos
{
	/// <summary>
	/// 
	/// </summary>
    [DbFactory("MyP")]
	public class AdminMenuDao : BaseDao<AdminMenu>
	{
	    public IEnumerable<AdminMenu> GetAll()
	    {
	        var sql = Sql.Builder.Where("1=1");
	        return Query(sql);
	    }

	    public List<AdminMenu> GetParentList()
	    {
	        var sql = Sql.Builder.Where("ParentId = 0");
	        return Query(sql).ToList();
	    }

	    public AdminMenu GetByMenuId(int menuid)
	    {
	        var sql = Sql.Builder.Where("MenuId = @0", menuid);
	        return FirstOrDefault(sql);
	    }

	    public void DeleteByMenuId(int menuId)
	    {
	        var sql = Sql.Builder.Where("MenuId = @0", menuId);
	        Delete(sql);
	    }
	}
}

