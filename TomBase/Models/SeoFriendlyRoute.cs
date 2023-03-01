//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Routing;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;

//namespace TomBase.Models
//{

//    public class SeoFriendlyRoute : Route
//    {
//        public SeoFriendlyRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler) : base(url, defaults, routeHandler)
//        {
//        }

//        public override RouteData RouteData(HttpContext httpContext)
//        {
//            var routeData = base.RouteData(httpContext);

//            if (routeData != null)
//            {
//                if (routeData.Values.ContainsKey("id"))
//                    routeData.Values["id"] = GetIdValue(routeData.Values["id"]);
//            }

//            return routeData;
//        }

//        private object GetIdValue(object id)
//        {
//            if (id != null)
//            {
//                string idValue = id.ToString();

//                var regex = new Regex(@"^(?<id>\d+).*$");
//                var match = regex.Match(idValue);

//                if (match.Success)
//                {
//                    return match.Groups["id"].Value;
//                }
//            }

//            return id;
//        }
//    }
//}
