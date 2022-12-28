using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Serialization;
using LoginDetails.LoginDetailsModels;
using LoginDetails.OpenWeatherMapModels;
using LoginDetails.Repositories;

namespace LoginDetails.Controllers
{
    public class HomeController : Controller
    {
        private readonly IForecastRepository _forecastRepository;
        // Dependency Injection
        public HomeController(IForecastRepository forecastAppRepo)
        {
            _forecastRepository = forecastAppRepo;
        }
        public IActionResult Index()
        {
            LoginModel obj = new LoginModel();
            return View(obj);
        }

        // GET: ForecastApp/SearchCity
        [HttpGet]
        public IActionResult SearchCity()
        {
            var viewModel = new SearchCity();
            return View(viewModel);
        }

        public IActionResult City(string city)
        {
            // Consume the OpenWeatherAPI in order to bring Forecast data in our page.
            WeatherResponse weatherResponse = _forecastRepository.GetForecast(city);
            City viewModel = new City();

            if (weatherResponse != null)
            {
                viewModel.Name = weatherResponse.Name;
                viewModel.Humidity = weatherResponse.Main.Humidity;
                viewModel.Pressure = weatherResponse.Main.Pressure;
                viewModel.Temp = weatherResponse.Main.Temp;
                viewModel.Weather = weatherResponse.Weather[0].Main;
                viewModel.Wind = weatherResponse.Wind.Speed;
            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SearchCity(SearchCity model)
        {
            // If the model is valid, consume the Weather API to bring the data of the city
            if (ModelState.IsValid)
            {
                return RedirectToAction("City", new { city = model.CityName });
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(LoginModel objuserlogin)
            {
                var display = Userloginvalues().Where(m => m.UserID == objuserlogin.UserID && m.Password == objuserlogin.Password).FirstOrDefault();
                if (display != null)
                {
                
                return RedirectToAction("SearchCity");
                //ViewBag.Status = "User is a valid user";
            }
                else
                {
                    return RedirectToAction("City");
                    //   ViewBag.Status = "Invalid User";
                }
                return View(objuserlogin);
            }


        public List<LoginModel> Userloginvalues()
            {
                List<LoginModel> objModel = new List<LoginModel>();
                objModel.Add(new LoginModel { UserID = "Komali", Password = "1" });
                objModel.Add(new LoginModel { UserID = "Komali1", Password = "Komali987" });
                return objModel;
            }
        }
    }



