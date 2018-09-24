using MyProject.Core.Entities;
using MyProject.Task;
using Quartz;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Matrix.Controllers.Job
{
    public class JobController : Controller
    { 
        public ActionResult Index()
        { 
            return Content("OK");
        }

    }
   
}
