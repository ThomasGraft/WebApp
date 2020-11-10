﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using Microsoft.Extensions.Logging;
using System.Runtime;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private DataContext context;

        public ProductsController(DataContext ctx)
        {
            context = ctx;
        }

        [HttpGet]
        public IAsyncEnumerable<Product> GetProducts()
        {
            return context.Products;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(long id)
        {
            Product p = await context.Products.FindAsync(id);
            if (p == null)
            {
                return NotFound();
            }
            return Ok(new
            {
                p.ProductId, p.Name, p.Price, p.CategoryId, p.SupplierId
            });
        }

        [HttpPost]
        public async Task<IActionResult> SaveProduct(ProductBindingTarget target)
        {
            Product p = target.ToProduct();
            await context.Products.AddAsync(p);
            await context.SaveChangesAsync();
            return Ok(p);
        }

        [HttpPut]
        public async Task UpdateProduct(Product product)
        {
            context.Update(product);
            await context.SaveChangesAsync();
        }

        [HttpDelete("{id}")]
        public async Task DeleteProduct(long id)
        {
            context.Products.Remove(new Product
            {
                ProductId = id
            });
            await context.SaveChangesAsync();
        }

        [HttpGet("redirect")]
        public IActionResult Redirect()
        {
            return RedirectToAction(nameof(GetProduct), new { Id = 1 });
        }
    }
}
