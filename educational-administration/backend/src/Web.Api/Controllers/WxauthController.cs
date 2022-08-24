using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Web.Api.Controllers
{
    [Route("[controller]")]
    public class WxauthController : ControllerBase
    {
        private readonly ILogger<WxauthController> _logger;

        public WxauthController(ILogger<WxauthController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public dynamic GetCode(string json_code)
        {
            string serviceAddress = "https://api.weixin.qq.com/sns/jscode2session?appid=" + "wx250f9dda5d5aab2f" + "&secret="
                + "851fa63d9c32acf88304b6d53d634e99" + "&js_code=" + json_code + "&grant_type=authorization_code";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
            request.Method = "GET";
            request.ContentType = "text/html;charset=utf-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, System.Text.Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            var obj = new
            {
                data = retString,
                Success = true
            };
            Formatting microsoftDataFormatSettings = default(Formatting);
            string result = JsonConvert.SerializeObject(obj, microsoftDataFormatSettings);
            // Response.Write(result);
            // 删除 session_key ，因为用不上
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);

            var tmp = jo["data"].ToString();

            JObject jo2 = (JObject)JsonConvert.DeserializeObject(tmp);

            jo2.Remove("session_key");

            return jo2.ToString();
        }
    }
}