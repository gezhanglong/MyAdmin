using System;
using MyProject.Services.ORM;
namespace MyProject.Core.Entities
{
	/// <summary>
	/// 用户表
	/// </summary>
	[TableName("AdminUser")]
	[PrimaryKey("AdminUserId")]
	public class AdminUser
	{
		/// <summary>
		/// 
		/// </summary>
		public int AdminUserId{ get;set;}
		
		/// <summary>
		/// 
		/// </summary>
		public string UserName{ get;set;}
		
		/// <summary>
		/// 
		/// </summary>
		public string Password{ get;set;}
		
		/// <summary>
		/// 
		/// </summary>
		public bool IsLock{ get;set;}
		
		/// <summary>
		/// 
		/// </summary>
		public int RoleId{ get;set;}

        /// <summary>
        /// email
        /// </summary>
        public string Email { get; set; }
	}
}
