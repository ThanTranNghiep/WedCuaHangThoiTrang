using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Web.Http.Results;
using System.Web.Mvc;
using KNT_SHOP.Models;

namespace KNT_SHOP.Controllers;

public class InvoiceController : Controller
{
    // GET
    public ActionResult Index()
    {
        return View();
    }
}