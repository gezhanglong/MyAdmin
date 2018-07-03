using System;
using MyProject.Services.ORM;
namespace MyProject.Core.Entities
{
	/// <summary>
	///角色菜单关联表
	/// </summary>
	[TableName("AdminRoleMenu")]
	public class AdminRoleMenu
	{
		/// <summary>
		/// 
		/// </summary>
		public int RoleId{ get;set;}
		
		/// <summary>
		/// 
		/// </summary>
		public int MenuId{ get;set;}
		

	}
}
