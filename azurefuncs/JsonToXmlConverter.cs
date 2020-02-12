using System;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Glasswall.Redact.Api
{
    public class JsonToXmlConverter<T> : IJsonToXmlConverter<T>
    {
        public string Convert(string json)
        {
            var result = Deserialise(json);
            return GenerateXml(result);
        }

        private static string GenerateXml(T settings)
        {
            using (var stringWriter = new System.IO.StringWriter())
            {
                var serializer = new XmlSerializer(typeof(T));

                try
                {
                    serializer.Serialize(stringWriter, settings);
                }
                catch (Exception e)
                {
                    throw new XmlException($"Could not serialise {typeof(T)} to XML", e);
                }

                return stringWriter.ToString();
            }
        }

        private static T Deserialise(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (JsonSerializationException e)
            {
                throw new JsonSerializationException($"The supplied json could not be deserialised to type {typeof(T)}", e);
            }
        }
    }
}