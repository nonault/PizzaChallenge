using System;
using System.Collections.Generic;
using System.Linq;
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

            return Ok("Le panier est vide");
        }
        
        [HttpDelete]
        public ActionResult<BasketDto> Clear()
        {
            _baskets = null;
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

                IEnumerable<BasketDto> query = _baskets.Where(n => n.PizzaName == pizza.Name);

                if (query.Count() != 0)
                {
                    int index = _baskets.FindIndex(item => pizza.Name == item.PizzaName);
                    double prixTotal = 0;
                    try
                    {
                        prixTotal = Double.Parse(_baskets[index].Price.Replace("€","")) + Double.Parse(pizza.Price.Replace("€", ""));
                    }
                    catch(Exception e)
                    {
                        throw e;
                    }
                    _baskets[index].Price = prixTotal.ToString()+"€";
                    _baskets[index].Quantite = _baskets[index].Quantite + 1;
                }
                else
                {
                    _baskets.Add(new BasketDto(1,pizza.Name, pizza.Price,1));
                }

                return Ok(_baskets);
            }
            
            return NotFound();
        }

        [HttpGet]
        [Route("list/{name}")]
        public async Task<ActionResult> List(string name = null)
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
                    Task.Delay(50);
                });
            }
            else
            {
                foreach (string fruit in fruits)
                {
                    Console.WriteLine(fruit);
                    await Task.Delay(50);
                }
            }
            
            var duration = ts.TotalSeconds;
            Console.WriteLine("Total: " + duration);
            
            
            return Ok("Total: " + duration);
        }
    }
}