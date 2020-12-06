using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Newtonsoft.Json;
using System.IO;
namespace TestTracer
{
    class JsonSeril : ISeril
    {
        public JsonSeril(List<Tracer.Tracer.TracerLog> tL)
        {
            TracerLog = tL;
        }
        public override string Save()
        {
            string pathToJson = DateTime.UtcNow.ToFileTimeUtc().ToString(CultureInfo.InvariantCulture) + ".json";
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartObject();
                writer.WritePropertyName("threads");
                writer.WriteStartArray();
                foreach (Tracer.Tracer.TracerLog tL in TracerLog)
                {
                    writer.WriteStartObject();
                    writer.WritePropertyName("id");
                    writer.WriteValue(tL.TraceChilds[0].Info.ThreadId.ToString(CultureInfo.InvariantCulture));
                    writer.WritePropertyName("methods");
                    writer.WriteStartArray();
                    
                    CreateJsonTree(writer, tL.TraceChilds);
                 
                    writer.WriteEnd();
                    writer.WriteEndObject();
                }
                writer.WriteEnd();
                writer.WriteEndObject();
            }

            using (StreamWriter swtofile = new StreamWriter(pathToJson, true, System.Text.Encoding.Default))
            {
                swtofile.Write(sb.ToString());
            }

                return pathToJson;
        }
        private void CreateJsonTree(JsonWriter writer, List<Tracer.Tracer.TracerLog> curTracerLog)
        {
            foreach (Tracer.Tracer.TracerLog trace in curTracerLog)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("name");
                writer.WriteValue(trace.Info.MethodName);
                writer.WritePropertyName("class");
                writer.WriteValue(trace.Info.ClassName);
                writer.WritePropertyName("time");
                writer.WriteValue(trace.Info.DeltaTime.ToString(CultureInfo.InvariantCulture));

                writer.WritePropertyName("methods");
                writer.WriteStartArray();

                if (trace.TraceChilds.Count <= 0) { writer.WriteEnd(); writer.WriteEndObject(); continue; }
                CreateJsonTree(writer, trace.TraceChilds);
                writer.WriteEnd();
                writer.WriteEndObject();
            }
        }
    }
}
