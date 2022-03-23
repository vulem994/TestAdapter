using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestAdapter.Common
{
    public class XMLSerializer
    {
        public static T Deserialize<T>(string input) where T : class
        {
            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(T));

            string cleanXml = Regex.Replace(input, @"<[a-zA-Z].[^(><.)]+/>",
                                     new MatchEvaluator(RemoveText));

            using (StringReader sr = new StringReader(cleanXml))
            {           
                return (T)ser.Deserialize(sr);
            }
        }

        static string RemoveText(Match m) { return ""; }

        public static string Serialize<T>(T ObjectToSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(ObjectToSerialize.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, ObjectToSerialize);
                return textWriter.ToString();
            }
        }
    }//[Class]
}//[Namespace]
