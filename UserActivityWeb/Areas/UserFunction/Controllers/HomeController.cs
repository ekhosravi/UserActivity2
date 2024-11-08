using UserActivity.DataAccess.Repository.IRepository;
using UserActivity.Models;
using UserActivity.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;

namespace UserActivity.Controllers;
[Area("UserFunction")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger )
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
          return View();
    }

    public IActionResult Details()
    { 
        return View();
    }
     
    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Error()
    {
        return View();
    }
}
