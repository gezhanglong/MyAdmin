using System;
using MyProject.Services.ORM;
namespace MyProject.Core.Entities
{
	/// <summary>
	///菜单表
	/// </summary>
	[TableName("AdminMenu")]
	[PrimaryKey("MenuId")]
	public class AdminMenu
	{
		/// <summary>
		/// 
		/// </summary>
		public int MenuId{ get;set;}
		
		/// <summary>
		/// 
		/// </summary>
		public string MenuName{ get;set;}
		
		/// <summary>
		/// 
		/// </summary>
		public string LinkUrl{ get;set;}
		
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateDate{ get;set;}
		
		/// <summary>
		/// 
		/// </summary>
		public int ParentId{ get;set;}
		
		/// <summary>
		/// 
		/// </summary>
		public int SortOrder{ get;set;}
		

	}
}
