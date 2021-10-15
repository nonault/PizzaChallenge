namespace pizza.Service
{
    public class BasketDto
    {
        public string PizzaName { get; set; }
        public string Price { get; set; }

        public int Quantite { get; set; }


        public BasketDto(int id, string pizzaName, string price, int quantite)
        {
            PizzaName = pizzaName;
            Price = price;
            Quantite = quantite;
        }
    }
}