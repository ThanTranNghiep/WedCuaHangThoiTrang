using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using KNT_Shop.Models;
using KNT_Shop.Models.ViewModel;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using NonActionAttribute = System.Web.Http.NonActionAttribute;

namespace KNT_Shop.Controllers;

public class InvoiceController : Controller
{
    // GET
    public ActionResult Index()
    {
        var username = User.Identity.GetUserId();
        if (username == null)
        {
            JavaScript("alert('Please login first!');");
            return RedirectToAction("Login", "Account");
        }
        else
        {
            KNT_ShopDB db = new KNT_ShopDB();
            var checkAdmin = db.Users.FirstOrDefault(x => x.Id == username);
            if(checkAdmin.Rule == true)
            {
                var list = db.HoaDons.ToList();
                return View(list);
            }
            else
            {
                var list = db.HoaDons.Where(x => x.Id == username).ToList();
                return View(list);
            }
        }
    }
    
    public ActionResult Details(int id)
    {
        KNT_ShopDB db = new KNT_ShopDB();
        var hoaDon = db.HoaDons.FirstOrDefault(x => x.MaHoaDon == id);
        return View(hoaDon);
    }

    public ActionResult fillToInvoice(string id)
    {
        KNT_ShopDB db = new KNT_ShopDB();
        var username = User.Identity.GetUserId();
        var taikhoan = db.Users.FirstOrDefault(x => x.Id == id);
        if (username == null|| taikhoan == null)
        {
            JavaScript("alert('Please login first!');");
            return RedirectToAction("Login", "Account");
        }
        else
        {
            
            var checkAdmin = db.Users.FirstOrDefault(x => x.Id == username);
            if(checkAdmin.Rule == true) // lấy hoá đơn của admin
            {
                var list = db.HoaDons.Where(x=>x.Id == id).ToList();
                return View("Index",list);
            }
            else // lấy hóa đơn của tài khoản user
            {
                var list = db.HoaDons.Where(x => x.Id == username).ToList();
                return View("Index",list);
            }
        }
    }

    
    [HttpPost]
    public ActionResult UpdateState(int MaHoaDon, int TrangThaiGiaoHang)
    {
        KNT_ShopDB db = new KNT_ShopDB();
        var hoaDon = db.HoaDons.FirstOrDefault(x => x.MaHoaDon == MaHoaDon);
        if(hoaDon != null)
        {
            var chiTietHoaDon = db.ChiTietHoaDons.Where(x => x.MaHoaDon == MaHoaDon).ToList();
            foreach (var item in chiTietHoaDon)
            {
                if (TrangThaiGiaoHang > item.TrangThaiGiaoHang)
                {
                    item.TrangThaiGiaoHang = TrangThaiGiaoHang;
                    db.SaveChanges();
                }
            }

            return Content("Update successfully!");
        }
        else
        {
            return Content("Update failed!");
        }
        // var url = "https://localhost:44376/Invoice/UpdateState?MaHoaDon=" + MaHoaDon + "&TrangThaiGiaoHang=" + TrangThaiGiaoHang;
    }
    
    public ActionResult FetchAndSaveData()
    {
        // Create an HttpClient to make the API request
        using (var client = new HttpClient())
        {
            // Set the API endpoint URL
            string apiUrl = "https://example.com/api/data";

            // Make the API request and get the response
            var response = client.GetAsync(apiUrl).Result;

            // Check if the API request was successful
            if (response.IsSuccessStatusCode)
            {
                // Get the JSON data from the response
                var jsonData = response.Content.ReadAsStringAsync().Result;

                // Deserialize the JSON data into a list of objects
                var data = JsonConvert.DeserializeObject<List<ResponeJson>>(jsonData);

                // Save the data to the database
                using (var db = new KNT_ShopDB())
                {
                    foreach (var item in data)
                    {
                        var hoaDon = db.HoaDons.FirstOrDefault(x => x.MaHoaDon == item.MaHoaDon);
                        if (hoaDon != null)
                        {
                            var chiTietHoaDon = db.ChiTietHoaDons.Where(x => x.MaHoaDon == item.MaHoaDon).ToList();
                            
                            foreach (var temp in chiTietHoaDon)
                            {
                                if (item.TrangThai > temp.TrangThaiGiaoHang)
                                {
                                    temp.TrangThaiGiaoHang = item.TrangThai;
                                }
                            }
                            db.SaveChanges();
                        }
                    }
                }

                // Return a success message
                return Content("Data fetched and saved successfully!");
            }
            else
            {
                // Return an error message
                return Content("Error fetching data from API: " + response.ReasonPhrase);
            }
        }
    }
}