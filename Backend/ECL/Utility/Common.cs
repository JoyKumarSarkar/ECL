using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace ECL.Utility
{
    public class Common
    {
        public static bool IsEmptyDatasetName(string? datasetNAme)
        {
            return !string.IsNullOrWhiteSpace(datasetNAme);
        }

        public static string GetEnumDescription<T>(int value) where T : Enum
        {
            T enumValue = (T)Enum.ToObject(typeof(T), value);

            var memberInfo = typeof(T).GetMember(enumValue.ToString()!).FirstOrDefault();
            if (memberInfo != null)
            {
                var descriptionAttribute = memberInfo.GetCustomAttribute<DescriptionAttribute>();
                if (descriptionAttribute != null)
                    return descriptionAttribute.Description;
            }

            return enumValue.ToString()!;
        }
    }

    public class Return<T>
    {
        public string? Message { get; set; }
        public T Data { get; set; }
        public bool Success { get; set; } = true;
        public object ExtraData { get; set; } = null!;
    }

}
