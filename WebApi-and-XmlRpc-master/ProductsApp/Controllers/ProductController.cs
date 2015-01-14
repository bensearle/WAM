using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProductsApp.Models;

namespace ProductsApp.Controllers
{
    public class ProductController : ApiController
    {
        // initialise some products
        Product[] products = new Product[]
        {
            new Product{ Id = 1, Category = "Groceries", Name = "Tomato Soup", Price = 1},
            new Product{ Id = 2, Category = "Tech", Name = "Computer", Price = 350.69M},
            new Product{ Id = 3, Category = "Hardware", Name = "Hammer", Price = 6M}
        };

        /// <summary>
        /// returns all products
        /// </summary>
        /// <returns>Products, in IEnumerable format</returns>
        [HttpGet]
        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }

        /// <summary>
        /// Returns a specific product
        /// </summary>
        /// <param name="id"> the id of the product</param>
        /// <returns>The Product</returns>
        [HttpGet]
        public IHttpActionResult GetProduct(int id)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public IHttpActionResult PostProduct(Product p)
        {
            if (p != null)
            {
                p.Name = "nice one";
            }
            else
            {
                p = new Product();
                p.Name = "sort it out";
                p.Id = 0;
                p.Price = 1;
                p.Category = "loser";
            }
            return Ok(p);
        }
    }
}
