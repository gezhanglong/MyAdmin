using System;
using MyProject.Services.ORM;
namespace MyProject.Core.Entities
{
	/// <summary>
	/// 权限
	/// </summary>
	[TableName("AdminPower")]
	[PrimaryKey("PowerId")]
	public class AdminPower
	{
		/// <summary>
		/// 
		/// </summary>
		public int PowerId{ get;set;}
		
		/// <summary>
		/// 
		/// </summary>
		public string PowerName{ get;set;}
		
		/// <summary>
		/// 
		/// </summary>
		public string Action{ get;set;}
		
		/// <summary>
		/// 
		/// </summary>
		public string Controller{ get;set;}
		
		/// <summary>
		/// 
		/// </summary>
		public DateTime CreateDate{ get;set;}
		
		/// <summary>
		/// 
		/// </summary>
		public string PowerCode{ get;set;}
		
		/// <summary>
		/// 
		/// </summary>
		public int MenuId{ get;set;}
		

	}
}
