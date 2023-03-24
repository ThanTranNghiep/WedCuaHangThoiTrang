﻿using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using KNT_SHOP.Models;
using KNT_SHOP.Models.ViewModel;
using Newtonsoft.Json;
using NonActionAttribute = System.Web.Http.NonActionAttribute;

namespace KNT_SHOP.Controllers;

public class InvoiceController : Controller
{
    // GET
    public ActionResult Index()
    {
        if (Session["username"] == null)
        {
            JavaScript("alert('Please login first!');");
            return RedirectToAction("Index", "Login");
        }
        else
        {
            KNT_ShopDB db = new KNT_ShopDB();
            var username = Session["username"].ToString();
            var checkAdmin = db.TaiKhoans.FirstOrDefault(x => x.TenTaiKhoan == username);
            if(checkAdmin.Rule == true)
            {
                var list = db.HoaDons.ToList();
                return View(list);
            }
            else
            {
                var list = db.HoaDons.Where(x => x.TenTaiKhoan == username).ToList();
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
        var username = Session["username"].ToString();
        var taikhoan = db.TaiKhoans.FirstOrDefault(x => x.TenTaiKhoan == id);
        if (Session["username"] == null || taikhoan == null)
        {
            JavaScript("alert('Please login first!');");
            return RedirectToAction("Index", "Login");
        }
        else
        {
            
            var checkAdmin = db.TaiKhoans.FirstOrDefault(x => x.TenTaiKhoan == username);
            if(checkAdmin.Rule == true) // lấy hoá đơn của admin
            {
                var list = db.HoaDons.Where(x=>x.TenTaiKhoan == id).ToList();
                return View("Index",list);
            }
            else // lấy hóa đơn của tài khoản user
            {
                var list = db.HoaDons.Where(x => x.TenTaiKhoan == username).ToList();
                return View("Index",list);
            }
        }
    }

    
    // [HttpPost]
    // public ActionResult UpdateState(int MaHoaDon, int TrangThaiGiaoHang)
    // {
    //     KNT_ShopDB db = new KNT_ShopDB();
    //     var chiTietHoaDon = db.HoaDons.FirstOrDefault(x => x.MaHoaDon == MaHoaDon);
    //     var url = "https://localhost:44376/Invoice/UpdateState?MaHoaDon=" + MaHoaDon + "&TrangThaiGiaoHang=" + TrangThaiGiaoHang;
    //     
    //     return ;
    // }
    
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
                
                
                //........................................................................................................
                
                
                // Save the data to the database
                

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