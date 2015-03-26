using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace GoldenCityShop.Extentions
{
    public static class JsonHelper
    {
        public static string ToJson(this object data)
        {
            return new JavaScriptSerializer().Serialize(data);
        }
    }
}