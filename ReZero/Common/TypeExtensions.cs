using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    public static class TypeExtensions
    {
        public static Type GetNonNullableType(this Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return type.GetGenericArguments()[0];
            }

            // 如果类型不是 Nullable<>，则直接返回原类型  
            return type;
        }
    }
}
