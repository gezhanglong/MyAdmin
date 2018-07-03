using System;
using MyProject.Services.ORM;
namespace MyProject.Core.Entities
{
	/// <summary>
	/// 角色表
	/// </summary>
    [TableName("AdminUserRole")]
	[PrimaryKey("RoleId")]
	public class AdminUserRole
	{
		/// <summary>
		/// 
		/// </summary>
		public int RoleId{ get;set;}
		
		/// <summary>
		/// 
		/// </summary>
		public string RoleName{ get;set;}
		

	}
}
