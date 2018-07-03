using System;
using System.Collections.Generic;
using System.Linq;
using MyProject.Services.ORM;
using MyProject.Core.Entities;
using MyProject.Core.Dtos;
namespace MyProject.Data.Daos
{
	/// <summary>
	/// 
    /// </summary> 
    [DbFactory("MyP")]
	public class AdminPowerDao : BaseDao<AdminPower>
	{
        //public AdminPower Get(string controllerName, string actionName)
        //{
        //    var sql = Sql.Builder
        //        .Where("Controller =@0 AND Action = @1", controllerName, actionName);
        //    return FirstOrDefault(sql);
        //}

       
        public List<PermDto> GetPermList(int roleId, string controller)
        {
            var sql = Sql.Builder.Append("select p.Action from AdminPower as p left join AdminRolePower as rp on p.PowerId=rp.PowerId where rp.RoleId=@0 and p.Controller=@1",roleId,controller);
            return Query<PermDto>(sql).ToList();
        }
	    public List<AdminPower> GetListByMenuId(int menuId)
	    {
	        var sql = Sql.Builder.Where("MenuId = @0", menuId);
	        return Query(sql).ToList();
	    }

	    public void DeleteByPowerId(int powerId)
	    {
	        var sql = Sql.Builder.Where("PowerId = @0", powerId);
	        Delete(sql);
	    }

	    public List<AdminPower> GetAll()
	    {
	        var sql = Sql.Builder.Where("1=1");
	        return Query(sql).ToList();
	    }
	}
}

