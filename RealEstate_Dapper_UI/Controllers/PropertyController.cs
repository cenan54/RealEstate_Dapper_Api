﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.Dtos.ProductDetailDtos;
using RealEstate_Dapper_UI.Dtos.ProductDtos;

namespace RealEstate_Dapper_UI.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PropertyController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7062/api/Products/ProductListWithCategory");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> PropertySingle(int id)
        {
            id = 1;
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7062/api/Products/GetProductByProductId?id="+id);

            var responseMessage2 = await client.GetAsync("https://localhost:7062/api/ProductDetails/GetProductDetailByProductId?id=" + id);


            if (responseMessage.IsSuccessStatusCode&&responseMessage2.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ResultProductDto>(jsonData);

                 var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
                var values2 = JsonConvert.DeserializeObject<GetProductDetailByIdDto>(jsonData2);

                ViewBag.title1 = values.title;
                ViewBag.price = values.price;
                ViewBag.city = values.city;
                ViewBag.district = values.district;
                ViewBag.address = values.address;
                ViewBag.type = values.type;
                //ViewBag.datediff = values.adv;


                ViewBag.bathCount = values2.BathCount;


                return View();
            }
            return View();
        }
    }
}
