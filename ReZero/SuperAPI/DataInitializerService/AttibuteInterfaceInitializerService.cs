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
            var urlParametersAttribute = method.GetCustomAttribute<UrlParametersAttribute>();
            var isUrlParameters = urlParametersAttribute != null;
            var groupName = methodAttribute.GroupName ?? classAttribute.GroupName ?? type.Name;
            string url = GetUrl(type, method, classAttribute, methodAttribute);
            var oldUrl = isUrlParameters ? url : null;
            var methodDesc = methodAttribute.Description ?? string.Empty;
            ZeroInterfaceList it = new ZeroInterfaceList();
            it.HttpMethod = methodAttribute.HttpMethod.ToString();
            it.Id = SnowFlakeSingle.Instance.NextId();
            it.GroupName = groupName;
            it.InterfaceCategoryId = classAttribute.InterfaceCategoryId;
            it.Name = methodDesc;
            it.Url = url;
            it.OriginalUrl = oldUrl;
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
            var isAdd = true;
            foreach (var item in method.GetParameters())
            {
                var nonNullableType = item.ParameterType.GetNonNullableType();
                it.Url = GetUrl(type, isUrlParameters, it.Url, item, nonNullableType);
                DataModelDefaultParameter dataModelDefaultParameter = new DataModelDefaultParameter();
                dataModelDefaultParameter.Name = item.Name;
                if (IsDefaultType(item.ParameterType))
                {
                    dataModelDefaultParameter.ValueType = item.ParameterType.GetNonNullableType().Name;
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
                    isAdd = false;
                    it.DataModel.MyMethodInfo.ArgsTypes = new Type[] { typeof(SingleModel) };
                    var paramters = item.ParameterType.GetProperties();
                    AddSingleClassParameters(it, paramters);
                }
                else
                {
                    dataModelDefaultParameter.ValueType = "Json";
                    object obj = Activator.CreateInstance(item.ParameterType);
                    dataModelDefaultParameter.Value = new SerializeService().SerializeObject(obj);
                }
                if (isAdd)
                    it.DataModel.DefaultParameters.Add(dataModelDefaultParameter);
            }
            return it;
        }

        private static string GetUrl(Type type, bool isUrlParameters, string url, ParameterInfo item, Type nonNullableType)
        {
            if (isUrlParameters && !(nonNullableType.IsValueType || nonNullableType == typeof(string)))
            {
                throw new Exception(TextHandler.GetCommonText($"{type.FullName}中的{item.Name}方法使用[UrlParameters] 只能是基础类型参数。{nonNullableType.Name}类型不支持", $"The {item.Name} method in {type.FullName} uses [UrlParameters] as a base type parameter only. The {nonNullableType.Name} type is not supported"));
            }
            else if (isUrlParameters)
            {
                url += "/{" + item.Name + "}";
            } 
            return url;
        }

        private static string GetUrl(Type type, MethodInfo method, ApiAttribute classAttribute, ApiMethodAttribute methodAttribute)
        {
            if (string.IsNullOrEmpty(methodAttribute.Url)&& !string.IsNullOrEmpty(classAttribute.Url)) 
            {
                return methodAttribute.Url ?? $"/{classAttribute.Url.TrimStart('/')}/{method.Name?.ToLower()}";
            }
            return methodAttribute.Url ?? $"/api/{classAttribute.InterfaceCategoryId}/{type.Name?.ToLower()}/{method.Name?.ToLower()}";
        }

        private static void AddSingleClassParameters(ZeroInterfaceList it, PropertyInfo[] paramters)
        {
            foreach (var p in paramters)
            {
                DataModelDefaultParameter addItem = new DataModelDefaultParameter();
                addItem.Name = p.Name;
                if (IsDefaultType(p.PropertyType))
                {
                    addItem.ValueType = p.PropertyType.GetNonNullableType().Name;
                }
                else if (p.PropertyType == typeof(byte[]))
                {
                    addItem.ValueType = "Byte[]";
                }
                else
                {
                    addItem.ValueType = "Json";
                    object obj = Activator.CreateInstance(p.PropertyType);
                    addItem.Value = new SerializeService().SerializeObject(obj);
                }
                it!.DataModel!.DefaultParameters!.Add(addItem);
            }
        }

        private static bool IsDefaultType(Type type)
        {
            return type.GetNonNullableType().IsValueType || type.GetNonNullableType() == typeof(string);
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
                        if (addItem.Url == PubConst.InitApi_SystemSaveConfig)
                        {
                            addItem.IsInitialized = true; 
                            addItem.Id = InterfaceListInitializerProvider.SaveConfigId;
                        }
                        if (addItem.Url == PubConst.InitApi_SystemGetInitConfig)
                        {
                            addItem.IsInitialized = true;
                            addItem.Id = InterfaceListInitializerProvider.GetInitConfigId;
                        }
                        if (addItem.Url == PubConst.InitApi_VerifyCode) 
                        {
                            addItem.IsInitialized = true;
                            addItem.Id = InterfaceListInitializerProvider.VerifyCodeId;
                        }
                        if (addItem.Url == PubConst.InitApi_SaveUser) 
                        {
                            addItem.IsInitialized = true;
                            addItem.Id = InterfaceListInitializerProvider.SaveUserId;
                        }
                        if (addItem.Url == PubConst.InitApi_GetUserById)
                        {
                            addItem.IsInitialized = true;
                            addItem.Id = InterfaceListInitializerProvider.GetUserById_Id;
                        }
                        if (addItem.Url == PubConst.InitApi_DeleteUserById) 
                        {
                            addItem.IsInitialized = true;
                            addItem.Id = InterfaceListInitializerProvider.DeleteUserById_Id;
                        }
                        if (addItem.Url == PubConst.InitApi_GetCurrentUser) 
                        {
                            addItem.IsInitialized = true;
                            addItem.Id = InterfaceListInitializerProvider.GetCurrentUserId;
                        }
                        if (addItem.Url == PubConst.InitApi_GetBizUsers) 
                        {
                            addItem.IsInitialized = true;
                            addItem.Id = InterfaceListInitializerProvider.GetBizUsersId;
                        }
                        if (addItem.Url == PubConst.InitApi_ViewTemplate) 
                        {
                            addItem.IsInitialized = true;
                            addItem.Id = InterfaceListInitializerProvider.ViewTemplateId;
                        }
                        zeroInterfaceLists.Add(addItem);
                    }
                }
            }
            var db = App.PreStartupDb;
            if (db != null)
            {
                db!.QueryFilter.ClearAndBackup();
                db.Deleteable<ZeroInterfaceList>().Where(it => it.IsAttributeMethod == true).ExecuteCommand();
                db.Insertable(zeroInterfaceLists).ExecuteCommand();
                db!.QueryFilter.Restore();
            }
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
