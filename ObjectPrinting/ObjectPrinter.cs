using System;
using System.Text;

namespace ObjectPrinting
{
    public class ObjectPrinter : IObjectPrinter
	{
        string IObjectPrinter.PrintToString(IPrintingConfig config, object obj, int height) => PrintToString(config, obj, height);

	    public static PrintingConfig<T> For<T>()
	    {
            return new PrintingConfig<T>();
        }

        private static string PrintToString(IPrintingConfig config, object obj, int nestingLevel)
        {
            if (obj == null)
                return "null" + Environment.NewLine;

            var type = obj.GetType();
            if (type.IsPrimitive || type.IsEnum || config.FinalTypes.Contains(type))
                return obj + Environment.NewLine;

            var identation = new string('\t', nestingLevel + 1);
            var sb = new StringBuilder();
            sb.AppendLine(type.Name);
            foreach (var propertyInfo in type.GetProperties())
            {
                if (!config.ExcludedTypes.Contains(propertyInfo.PropertyType) && !config.ExcludedFields.Contains(propertyInfo))
                {
                    var value = PrintToString(config, propertyInfo.GetValue(obj), nestingLevel + 1);
                    if (config.AlternativeSerializersByType.ContainsKey(propertyInfo.PropertyType))
                        value = config.AlternativeSerializersByType[propertyInfo.PropertyType](propertyInfo.GetValue(obj)) + Environment.NewLine;
                    if (config.AlternativeSerializersByName.ContainsKey(propertyInfo))
                        value = config.AlternativeSerializersByName[propertyInfo](propertyInfo.GetValue(obj)) + Environment.NewLine;
                    sb.Append(identation + propertyInfo.Name + " = " + value);
                }
            }
            return sb.ToString();
        }
    }
}