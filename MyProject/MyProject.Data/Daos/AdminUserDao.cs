using System;
using MyProject.Services.ORM;
using MyProject.Services.MvcPager;
using MyProject.Core.Dtos;
using MyProject.Core.Entities;

namespace MyProject.Data.Daos
{
    /// <summary>
    /// 
    /// </summary>
    [DbFactory("MyP")]
    public class AdminUserDao : BaseDao<AdminUser>
    {
        public void Delete(int id)
        {
            var sql = Sql.Builder.Where("AdminUserId = @0", id);
            Delete(sql);
        }

        public bool ExistsPower(string userName, int powerId)
        {
            var sql = Sql.Builder
                .Append("SELECT COUNT(1)")
                .Append("FROM AdminUser a JOIN AdminRolePower b ON a.RoleId = b.RoleId")
                .Append("WHERE a.UserName = @0 AND b.PowerId = @1", userName, powerId);
            return FirstOrDefault<int>(sql) > 0;
        }

        public AdminUser GetById(int id)
        {
            var sql = Sql.Builder.Where("AdminUserId = @0", id);
            return FirstOrDefault(sql);
        }

        public AdminUser GetByUserName(string userName)
        {
            var sql = Sql.Builder.Where("UserName = @0", userName);
            return FirstOrDefault(sql);
        }

        public PagedList<AdminUserDto> GetPagedList(string account,int? userRole,int pageIndex,int pageSize)
        {
            var sql = Sql.Builder
                .Select("au.AdminUserId,au.UserName,au.IsLock,aur.RoleName")
                .From("AdminUser au")
                .LeftJoin("AdminUserRole aur")
                .On("au.RoleId = aur.RoleId");

            if (!string.IsNullOrEmpty(account))
                sql.Where("au.UserName LIKE @0", string.Format("%{0}%", account));
            if (userRole != null)
                sql.Where("au.RoleId = @0", (int) userRole);

            return PagedList<AdminUserDto>(pageIndex, pageSize, sql);
        }
    }
}

