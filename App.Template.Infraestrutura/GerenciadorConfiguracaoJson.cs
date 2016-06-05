using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace App.Template.Infraestrutura
{
    public static class GerenciadorConfiguracaoJson
    {
        private const string ConfigFileName = "config.json";
        private const string ConfigVariablesFileName = "config.variables.json";

        private static IEnumerable<KeyValuePair<string, string>> EnvironmentVariables
        {
            get { return Environment.GetEnvironmentVariables().OfType<DictionaryEntry>().Select(e => new KeyValuePair<string, string>(e.Key as string, e.Value as string)); }
        }

        public static T Load<T>() where T : new()
        {
            var exceptions = new[] {ConfigFileName, ConfigVariablesFileName}
                .Where(f => !File.Exists(f) && !File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, f))).ToList()
                	.Select(f => new InvalidOperationException($"{f} not found"))
                .OfType<Exception>().ToArray();

            if (exceptions.Any()) throw new AggregateException(exceptions);

            var variables = GetConfigVariables(EnvironmentVariables).Concat(EnvironmentVariables).ToArray();
            
            var configContent = ReplaceEnvironmentConfigVariables(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigFileName)), variables);

            var result = new T();

            JsonConvert.PopulateObject(configContent, result,
                new JsonSerializerSettings
                {
                    Converters = new List<JsonConverter> {new CustomCreationConverter(variables)}
                });
            return result;
        }

        private static string ReplaceEnvironmentConfigVariables(string readAllText, KeyValuePair<string, string>[] variables)
        {
            var replacedString = readAllText;
            foreach (var variable in variables)
            {
            	replacedString = replacedString.Replace($"${{{variable.Key}}}", variable.Value);
            }
            return replacedString;
        }

        private static IEnumerable<KeyValuePair<string, string>> GetConfigVariables(IEnumerable<KeyValuePair<string, string>> enviromentVariables)
        {
        	var environment = Environment.GetEnvironmentVariable("Environment") != null ? Environment.GetEnvironmentVariable("Environment").ToLower() : "local";

            var jsonVariables = JObject.Parse(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigVariablesFileName)))[environment] as JObject;

            if (jsonVariables == null) return null;

            var configVariables = new List<KeyValuePair<string, string>>();

            var keyValuePairs = enviromentVariables as KeyValuePair<string, string>[] ?? enviromentVariables.ToArray();
            foreach (var jsonValue in jsonVariables)
            {
                var replacedValue = jsonValue.Value.ToString();
                replacedValue = keyValuePairs.Aggregate(replacedValue, (current, environmentVariable) => current.Replace($"%{environmentVariable.Key}%", environmentVariable.Value));
                configVariables.Add(new KeyValuePair<string, string>(jsonValue.Key, replacedValue));
            }

            // local then consider dev enviroment for the future config replaces
            if (environment == "local" && configVariables.All(n => n.Key != "Environment"))
            {
                configVariables.Add(new KeyValuePair<string, string>("Environment", Environment.MachineName));
            }

            return configVariables;
        }

        private class CustomCreationConverter : JsonConverter
        {
            private readonly KeyValuePair<string, string>[] variables;

            public CustomCreationConverter(KeyValuePair<string, string>[] variables)
            {
                this.variables = variables;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotSupportedException();
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                if (objectType.IsPrimitive || objectType == typeof (string))
                {
                    return Convert.ChangeType(reader.Value, objectType);
                }

                if (objectType.IsEnum)
                {
                    return Convert.ChangeType(Enum.Parse(objectType, reader.Value.ToString()), objectType);
                }

                if (reader.TokenType == JsonToken.StartObject)
                {
                    var jObject = JObject.Load(reader);

                    if (jObject.Property("$ref") != null)
                    {
                        var variable = variables.Where(n => n.Key == jObject.Property("$ref").Value.ToString()).Select(n => n.Value).FirstOrDefault();
                        if (variable == null)
                        {
                        	throw new Exception(string.Format("Variable {jObject.Property('{0}').Value} not found", "$ref"));
                        }

                        if (string.IsNullOrEmpty(variable) || variable == "null")
                        {
                            return Activator.CreateInstance(objectType);
                        }

                        var child = JToken.Parse(variable);

                        if (child is JArray)
                        {
                            var list = Activator.CreateInstance(objectType);
                            serializer.Populate((child as JArray).CreateReader(), list);
                            return list;
                        }

                        var finalJObject = child as JObject;
                        if (finalJObject != null)
                        {
                            foreach (var property in finalJObject.Properties())
                            {
                                jObject.Add(property.Name, property.Value);
                            }
                        }
                    }


                    reader = jObject.CreateReader();
                }

                var typedObject = Activator.CreateInstance(objectType);
                serializer.Populate(reader, typedObject);
                return typedObject;
            }

            public override bool CanConvert(Type objectType)
            {
                return true;
            }
        }
    }
}