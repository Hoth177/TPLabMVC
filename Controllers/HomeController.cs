using HomeAppStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HomeAppStore.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		ProductContext db;

		public HomeController(ILogger<HomeController> logger, ProductContext context)
		{
			_logger = logger;
			db = context;
		}

		public IActionResult Index()
		{
			return View(db.Products.ToList());
		}

        public IActionResult Orders()
        {
            return View(db.Orders.ToList());
        }

        public IActionResult Clients()
        {
            return View(db.Clients.ToList());
        }

        public IActionResult Privacy()
		{
			return View();
		}

        [HttpGet]
        public IActionResult Buy(int? id, int? cost)
        {
            if (id == null) return RedirectToAction("Index");
            ViewBag.ProductId = id;
            ViewBag.Cost = cost;
            return View();
        }
        [HttpPost]
        public string Buy(Order order)
        {
            // ищем клиента в таблице клиентов по имени
            Client? clientToUpdate = db.Clients.FirstOrDefault(p => p.Name == order.User);
            // если клент не найден, то создаем строку в таблице
            if (clientToUpdate==null)
            {
                // создаем информацию о новом клиенте и добавляем в бд
                clientToUpdate = new Client
                {
                    Name = order.User,
                    Product_cost1 = order.Cost,
                    Discount = (int)Math.Round((float)order.Cost * 0,001)
                };
                db.Clients.Add(clientToUpdate);
            } else
            {
                int order_cost = order.Cost;
                if (clientToUpdate.Product_cost2 == 0) {clientToUpdate.Product_cost2 = order_cost; order_cost = 0;}
                if (clientToUpdate.Product_cost3 == 0) { clientToUpdate.Product_cost3 = order_cost; order_cost = 0; }
                if (clientToUpdate.Product_cost4 == 0) { clientToUpdate.Product_cost4 = order_cost; order_cost = 0; }
                if (clientToUpdate.Product_cost5 == 0) { clientToUpdate.Product_cost5 = order_cost; order_cost = 0; }
                if (clientToUpdate.Product_cost6 == 0) { clientToUpdate.Product_cost6 = order_cost; }
                else
                {
                    clientToUpdate.Product_cost1 = clientToUpdate.Product_cost2;
                    clientToUpdate.Product_cost2 = clientToUpdate.Product_cost3;
                    clientToUpdate.Product_cost3 = clientToUpdate.Product_cost4;
                    clientToUpdate.Product_cost4 = clientToUpdate.Product_cost5;
                    clientToUpdate.Product_cost5 = clientToUpdate.Product_cost6;
                    clientToUpdate.Product_cost6 = order.Cost;
                }
                int sum = clientToUpdate.Product_cost1 + clientToUpdate.Product_cost2 + clientToUpdate.Product_cost3
                    + clientToUpdate.Product_cost4 + clientToUpdate.Product_cost5 + clientToUpdate.Product_cost6;
                clientToUpdate.Discount = (int)Math.Round((float)sum / 10000);
                db.Clients.Update(clientToUpdate);
            }
            db.Orders.Add(order);
            db.SaveChanges();
            return "Спасибо, " + order.User + ", за покупку!";
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
