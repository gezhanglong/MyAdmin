using System;
using MyProject.Services.MvcPager;
using MyProject.Data.Daos;
using MyProject.Core.Dtos;
using MyProject.Core.Entities;

namespace MyProject.Tasks
{
	/// <summary>
	///
	/// </summary>
	public class AdminUserTask
	{
		private readonly AdminUserDao _adminUser = new AdminUserDao();

        public void Add(AdminUser model)
        {
            _adminUser.Add(model);
        }

        public void Update(AdminUser model)
        {
            _adminUser.Update(model);
        }

        public void Delete(int id)
        {
            _adminUser.Delete(id);
        }

	    public bool ExistsPower(string userName, int powerId)
	    {
	        return _adminUser.ExistsPower(userName, powerId);
	    }

        public AdminUser GetById(int id)
        {
            return _adminUser.GetById(id);
        }

	    public AdminUser GetByUserName(string userName)
	    {
	        return _adminUser.GetByUserName(userName);
	    }

        public PagedList<AdminUserDto> GetPagedList(string account, int? userRole, int pageIndex, int pageSize)
        {
            return _adminUser.GetPagedList(account, userRole, pageIndex, pageSize);
        }
	}
}

