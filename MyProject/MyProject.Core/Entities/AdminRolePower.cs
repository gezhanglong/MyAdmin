using System;
using MyProject.Services.ORM;
namespace MyProject.Core.Entities
{
	/// <summary>
	/// 角色权限关联表
	/// </summary>
	[TableName("AdminRolePower")]
	public class AdminRolePower
	{
		/// <summary>
		/// 
		/// </summary>
		public int RoleId{ get;set;}
		
		/// <summary>
		/// 
		/// </summary>
		public int PowerId{ get;set;}
		

	}
}
