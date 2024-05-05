using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using SqlSugar;
using System.Reflection.Emit;
namespace ReZero.SuperAPI
{
    public class AttibuteInterfaceInitializerService
    {
        internal static ZeroInterfaceList GetZeroInterfaceItem(Type type, MethodInfo method)
        {
            var classAttribute = type.GetCustomAttribute<ApiAttribute>();
            var methodAttribute = method.GetCustomAttribute<ApiMethodAttribute>();
            var groupName = methodAttribute.GroupName ?? classAttribute.GroupName ?? type.Name;
            var url = methodAttribute.Url??$"/api/{classAttribute.InterfaceCategoryId}/{type.Name?.ToLower()}/{method.Name?.ToLower()}";
            var methodDesc = methodAttribute.Description ?? string.Empty;
            ZeroInterfaceList it = new ZeroInterfaceList();
            it.HttpMethod = HttpRequestMethod.All.ToString();
            it.Id = SnowFlakeSingle.Instance.NextId();
            it.GroupName = groupName;
            it.InterfaceCategoryId = classAttribute.InterfaceCategoryId;
            it.Name = methodDesc;
            it.Url = url;
            it.IsInitialized = false;
            it.IsAttributeMethod = true;
            it.DataModel = new DataModel()
            {
                TableId = EntityInfoInitializerProvider.Id_ZeroInterfaceList,
                ActionType = ActionType.MethodGeneratorAPI,
                MyMethodInfo = new MyMethodInfo()
                {
                    MethodArgsCount = method.GetParameters().Count(),
                    MethodClassFullName = type.FullName,
                    MethodName = method.Name
                }
            };
            it.DataModel.DefaultParameters = new List<DataModelDefaultParameter>();
            foreach (var item in method.GetParameters())
            {
                DataModelDefaultParameter dataModelDefaultParameter = new DataModelDefaultParameter();
                dataModelDefaultParameter.Name= item.Name;
                if (IsDefaultType(item.ParameterType))
                {
                    dataModelDefaultParameter.ValueType = item.ParameterType.Name;
                }
                else if (item.ParameterType == typeof(byte[]))
                {
                    dataModelDefaultParameter.ValueType = "Byte[]";
                }
                else if (IsObject(item.ParameterType))
                {
                    dataModelDefaultParameter.ValueType = "Json";
                    object obj = Activator.CreateInstance(item.ParameterType);
                    dataModelDefaultParameter.Value = new SerializeService().SerializeObject(obj);
                }
                else if (method.GetParameters().Count() == 1)
                {
                    var paramters = item.ParameterType.GetProperties();
                    foreach (var p in paramters)
                    {

                    }
                }
                else 
                {
                    dataModelDefaultParameter.ValueType = "Json";
                    object obj = Activator.CreateInstance(item.ParameterType);
                    dataModelDefaultParameter.Value = new SerializeService().SerializeObject(obj);
                }
                it.DataModel.DefaultParameters.Add(dataModelDefaultParameter);
            }
            return it;
        }

        private static bool IsDefaultType(Type type)
        {
            return type.IsValueType || type == typeof(string);
        }

        internal static void InitDynamicAttributeApi(List<Type>? types)
        {
            types = AttibuteInterfaceInitializerService.GetTypesWithDynamicApiAttribute(types ?? new List<Type>());
            List<ZeroInterfaceList> zeroInterfaceLists = new List<ZeroInterfaceList>();
            foreach (var type in types)
            {
                var methods = AttibuteInterfaceInitializerService.GetMethodsWithDynamicMethodAttribute(type);
                if (methods.Any())
                {
                    foreach (var method in methods)
                    {
                        var addItem = AttibuteInterfaceInitializerService.GetZeroInterfaceItem(type, method);
                        zeroInterfaceLists.Add(addItem);
                    }
                }
            }
            var db = App.PreStartupDb;
            db!.QueryFilter.ClearAndBackup();
            db.Deleteable<ZeroInterfaceList>().Where(it => it.IsAttributeMethod == true).ExecuteCommand();
            db.Insertable(zeroInterfaceLists).ExecuteCommand();
            db!.QueryFilter.Restore();
        }

        /// <summary>
        /// Get the list of types with the DynamicApiAttribute
        /// </summary>
        /// <param name="types">The list of types</param>
        /// <returns>The list of types with the DynamicApiAttribute</returns>
        public static List<Type> GetTypesWithDynamicApiAttribute(List<Type> types)
        {
            List<Type> typesWithDynamicApiAttribute = new List<Type>();

            foreach (var type in types)
            {
                // Check if the type has the DynamicApiAttribute
                if (type.GetCustomAttributes(typeof(ApiAttribute), true).Length > 0)
                {
                    typesWithDynamicApiAttribute.Add(type);
                }
            }

            return typesWithDynamicApiAttribute;
        }


        /// <summary>
        /// Get the list of methods with the DynamicMethodAttribute for a given type
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>The list of methods with the DynamicMethodAttribute</returns>
        public static List<MethodInfo> GetMethodsWithDynamicMethodAttribute(Type type)
        {
            List<MethodInfo> methodsWithDynamicMethodAttribute = new List<MethodInfo>();

            MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var method in methods)
            {
                if (method.GetCustomAttributes(typeof(ApiMethodAttribute), true).Length > 0)
                {
                    methodsWithDynamicMethodAttribute.Add(method);
                }
            }

            return methodsWithDynamicMethodAttribute;
        }

        private static bool IsObject(Type type)
        {
            return (type.IsArray || type.FullName.StartsWith("System.Collections.Generic.List"));
        }
    }
}
