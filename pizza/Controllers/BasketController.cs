using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using pizza.Custom;
using pizza.Service;

namespace pizza.Controllers
{
    [ApiController]
    [ExceptionFilter]
    [Route("[controller]")]
    public class BasketController : ControllerBase
    {

        private static List<BasketDto> _baskets;
        private readonly PizzaService _pizzaService = new PizzaService();
        
        [HttpGet]
        public ActionResult<BasketDto> Get()
        {
            if (_baskets != null)
            {
                return Ok(_baskets);
            }

            return NotFound();
        }
        
        [HttpDelete]
        public ActionResult<BasketDto> Clear()
        {
            _baskets.Clear();
            return Ok();
        }
        
        [HttpPost]
        public ActionResult<BasketDto> Post(string name)
        {
            PizzaDto pizza = _pizzaService.GetPizzas(name);
            
            if (!pizza.Equals(null))
            {
                if (_baskets is null)
                {
                    _baskets = new List<BasketDto>();
                }
                
                _baskets.Add(new BasketDto(1, pizza.Name, pizza.Price));
                return Ok(_baskets);
            }
            
            return NotFound();
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult> List(string name)
        {
            List<string> fruits = new List<string>();  
            fruits.Add("Apple");  
            fruits.Add("Banana");  
            fruits.Add("Bilberry");  
            fruits.Add("Blackberry");  
            fruits.Add("Blackcurrant");  
            fruits.Add("Blueberry");  
            fruits.Add("Cherry");  
            fruits.Add("Coconut");  
            fruits.Add("Cranberry");  
            fruits.Add("Date");  
            fruits.Add("Fig");  
            fruits.Add("Grape");  
            fruits.Add("Guava");  
            fruits.Add("Jack-fruit");  
            fruits.Add("Kiwi fruit");  
            fruits.Add("Lemon");  
            fruits.Add("Lime");  
            fruits.Add("Lychee");  
            fruits.Add("Mango");  
            fruits.Add("Melon");  
            fruits.Add("Olive");  
            fruits.Add("Orange");  
            fruits.Add("Papaya");  
            fruits.Add("Plum");  
            fruits.Add("Pineapple");  
            fruits.Add("Pomegranate");  

            TimeSpan ts = new TimeSpan();
            if (name == "parallel")
            {
                Parallel.ForEach(fruits, fruit =>
                {
                    Console.WriteLine(fruit);
                    Task.Delay(500);
                });
            }
            else
            {
                foreach (string fruit in fruits)
                {
                    Console.WriteLine(fruit);
                    await Task.Delay(500);
                }
            }
            
            var duration = ts.TotalSeconds;
            Console.WriteLine("Total: " + duration);
            
            
            return Ok("Total: " + duration);
        }
    }
}