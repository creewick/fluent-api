using System;
using System.Text;

namespace ObjectPrinting
{
    public class ObjectPrinter
    {
        private static IPrintingConfig printingConfig;

        public ObjectPrinter(IPrintingConfig config)
        {
            printingConfig = config;
        }

        public static PrintingConfig<T> For<T>()
        {
            return new PrintingConfig<T>();
        }

        public string PrintToString(object obj)
        {
            return PrintToString(obj, 0);
        }

        private string PrintToString(object obj, int nestingLevel)
        {
            if (obj == null)
                return "null" + Environment.NewLine;

            var type = obj.GetType();
            if (type.IsPrimitive || type.IsEnum || printingConfig.FinalTypes.Contains(type))
                return obj + Environment.NewLine;

            var identation = new string('\t', nestingLevel + 1);
            var sb = new StringBuilder();
            sb.AppendLine(type.Name);
            foreach (var propertyInfo in type.GetProperties())
            {
                if (printingConfig.ExcludedTypes.Contains(propertyInfo.PropertyType) ||
                    printingConfig.ExcludedFields.Contains(propertyInfo)) continue;
                string value;
                if (printingConfig.AlternativeSerializersByType.ContainsKey(propertyInfo.PropertyType))
                    value = printingConfig.AlternativeSerializersByType[propertyInfo.PropertyType](propertyInfo.GetValue(obj)) + Environment.NewLine;
                else if (printingConfig.AlternativeSerializersByName.ContainsKey(propertyInfo))
                    value = printingConfig.AlternativeSerializersByName[propertyInfo](propertyInfo.GetValue(obj)) + Environment.NewLine;
                else
                    value = PrintToString(propertyInfo.GetValue(obj), nestingLevel + 1);
                sb.Append(identation + propertyInfo.Name + " = " + value);
            }
            return sb.ToString();
        }
}
}