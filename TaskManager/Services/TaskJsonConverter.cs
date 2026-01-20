using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using TaskManagerApp.Models;

namespace TaskManagerApp.Services
{
    public class TaskJsonConverter : JsonConverter<TaskItem>
    {
        public override TaskItem? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (var jsonDoc = JsonDocument.ParseValue(ref reader))
            {
                var root = jsonDoc.RootElement;

                if (!root.TryGetProperty("TaskType", out var typeProperty))
                    throw new JsonException("Missing TaskType property");

                string taskType = typeProperty.GetString() ?? "";

                TaskItem task = taskType switch
                {
                    "Personal" => JsonSerializer.Deserialize<PersonalTask>(root.GetRawText(), options)!,
                    "Work" => JsonSerializer.Deserialize<WorkTask>(root.GetRawText(), options)!,
                    _ => JsonSerializer.Deserialize<TaskItem>(root.GetRawText(), options)!
                };
                
                return task;
            }
        }

        public override void Write(Utf8JsonWriter writer, TaskItem value, JsonSerializerOptions options)
        {
            if (value is PersonalTask)
            {
                JsonSerializer.Serialize(writer, (PersonalTask)value, options);
            }
            else if (value is WorkTask)
            {
                JsonSerializer.Serialize(writer, (WorkTask)value, options);
            }
            else
            {
                JsonSerializer.Serialize(writer, value, options);
            }
        }
    }
}