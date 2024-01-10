using HomeAppStore.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeAppStore
{
	public static class SampleData
	{
		public static void Initialize(IServiceProvider serviceProvider)
		{
			using (var context = new ProductContext(
				serviceProvider.GetRequiredService<
					DbContextOptions<ProductContext>>()))
			{
				if (context.Products.Any())
				{
					return;   // База данных была запущена
				}

				context.Products.AddRange(
					new Product
					{
						Name = "Газовая плита Bosch HGG120E21R",
						Price = 21999
					},
					new Product
					{
						Name = "Электрическая плита De Luxe 5004.22э",
						Price = 15799
					},
					new Product
					{
						Name = "Микроволновая печь Hyundai HYM-M2005",
						Price = 5299
					},
					new Product
					{
						Name = "Стиральная машина LG F2J6HG8W",
						Price = 49999
					},
					new Product
					{
						Name = "Утюг Philips Azur GC4902/20",
						Price = 13499
					}
				);
				context.SaveChanges();
			}
		}
	}
}
