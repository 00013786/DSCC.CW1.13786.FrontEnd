using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DSCC.CW1._13786.FrontEnd.Models;

namespace DCSS.CW1.FrontEnd._13786.Controllers
{
    public class CarController : Controller
    {
        private readonly string baseUrl = "https://localhost:44303/api/Car";

        // GET: Car
        public async Task<ActionResult> Index()
        {
            List<Car> cars = new List<Car>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    cars = JsonConvert.DeserializeObject<List<Car>>(responseContent);
                }
                else
                {
                    ViewBag.ErrorMessage = "Engine failed to start";
                }
            }

            return View(cars);
        }

        // GET: Car/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Car car = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"{baseUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    car = JsonConvert.DeserializeObject<Car>(responseContent);
                }
                else
                {
                    return HttpNotFound("The part cannot be found");
                }
            }

            return View(car);
        }

        // GET: Car/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Car/Create
        [HttpPost]
        public async Task<ActionResult> Create(Car car)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(JsonConvert.SerializeObject(car), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to assemble the car";
                    return View(car);
                }
            }
        }

        // GET: Car/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Car car = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"{baseUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    car = JsonConvert.DeserializeObject<Car>(responseContent);
                }
                else
                {
                    return HttpNotFound("Car cannot be found");
                }
            }

            return View(car);
        }

        // POST: Car/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, Car car)
        {
            if (id != car.Id)
            {
                return new HttpStatusCodeResult(400, "ID mismatch between route and body.");
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(JsonConvert.SerializeObject(car), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync($"{baseUrl}/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to upgrade the car";
                    return View(car);
                }
            }
        }

        // GET: Car/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Car car = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"{baseUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    car = JsonConvert.DeserializeObject<Car>(responseContent);
                }
                else
                {
                    return HttpNotFound("Car cannot found.");
                }
            }

            return View(car);
        }

        // POST: Car/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.DeleteAsync($"{baseUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to delete the food item.";
                    return RedirectToAction("Delete", new { id = id });
                }
            }
        }
    }
}
