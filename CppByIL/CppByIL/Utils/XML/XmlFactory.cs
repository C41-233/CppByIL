using System.Collections;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace CppByIL.Utils.XML;

internal static class XmlFactory
{

    public static XmlDocument Create(object root, string? ns)
    {
        var doc = new XmlDocument();

        var element = CreateElement(doc, root, null, ns);
        doc.AppendChild(element);

        return doc;
    }

    private static XmlElement CreateElement(XmlDocument document, object obj, string? name, string? ns)
    {
        var type = obj.GetType();

        var element = document.CreateElement(name ?? type.Name, ns);

        if (type == typeof(string) || type.IsPrimitive)
        {
            element.InnerText = ToValue(obj);
            return element;
        }

        foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Instance))
        {
            if (field.GetCustomAttribute<XmlAttributeAttribute>() != null)
            {
                var value = field.GetValue(obj);
                if (value != null)
                {
                    element.SetAttribute(field.Name, ToValue(value));
                }
            }
            else if(field.GetCustomAttribute<XmlArrayAttribute>() != null)
            {
                var list = (IEnumerable?) field.GetValue(obj);
                if (list != null)
                {
                    foreach (var value in list)
                    {
                        var child = CreateElement(document, value, null, ns);
                        element.AppendChild(child);
                    }
                }
            }
            else
            {
                var value = field.GetValue(obj);
                if (value != null)
                {
                    var child = CreateElement(document, value, field.Name, ns);
                    element.AppendChild(child);
                }
            }
        }

        return element;
    }
    
    private static string ToValue(object value) 
    {
        if (value is bool b)
        {
            return b ? "true" : "false";
        }
        return value.ToString()!;
    }

}
