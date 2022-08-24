using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace Web.Application.Utils
{
    public static class JsonHelper
    {
        public static string SerializeObject(this object? obj)
        {
            var options =
                new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,// 小驼峰
                    WriteIndented = true // 美化
                };
            return JsonSerializer.Serialize(obj, options);
        }
    }
}