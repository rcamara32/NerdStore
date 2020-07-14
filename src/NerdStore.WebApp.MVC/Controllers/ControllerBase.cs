using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NerdStore.WebApp.MVC.Controllers
{
    public abstract class ControllerBase : Controller
    {

        protected Guid ClientId = Guid.Parse("1bf7dc4d-c7fc-4d8b-b724-b7302d3bb63c");


    }
}
