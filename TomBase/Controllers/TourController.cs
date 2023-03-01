using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TomBase.Controllers
{
    public class TourController : Controller
    {
        // GET: TourController
        public ActionResult Index()
        {
            return View();
        }

       }
}
