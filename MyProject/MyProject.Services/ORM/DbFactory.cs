using System;

namespace MyProject.Services.ORM
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DbFactory : Attribute
    {
        public readonly string ConnectionName;

        public DbFactory(string connectionName)
        {
            ConnectionName = connectionName;
        }

        public static string GetKeyFrom(object target)
        {
            var objectType = target.GetType();
            var attributes = objectType.GetCustomAttributes(typeof(DbFactory), true);
            if (attributes.Length > 0)
            {
                var attribute = (DbFactory)attributes[0];
                return attribute.ConnectionName;
            }

            return "DefaultDB";
        }
    }
}