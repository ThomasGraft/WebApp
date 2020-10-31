using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using WebApp.Models;
using Microsoft.AspNetCore.Builder;

namespace WebApp
{
    public static class WebServiceEndpoint
    {
        private static string BASEURL = "api/products";

        public static void MapWebService(this IEndpointRouteBuilder app)
        {
            // Endpoint for Get requests that includes ProductId to return matching Product
            app.MapGet($"{BASEURL}/{{id}}", async context =>
            {
                long key = long.Parse(context.Request.RouteValues["id"] as string);
                DataContext data = context.RequestServices.GetService<DataContext>();

                // Search database for Product by ProductId
                Product p = data.Products.Find(key);
                if (p == null)
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                }
                else
                {
                    context.Response.ContentType = "application/json";
                    // Serialize Product object to json string for response
                    await context.Response.WriteAsync(JsonSerializer.Serialize<Product>(p));
                }
            });

            // Endpoint for Get requests to return all Products
            app.MapGet(BASEURL, async context =>
            {
                DataContext data = context.RequestServices.GetService<DataContext>();
                context.Response.ContentType = "application/json";
                // Serialize collection of Products to json string for response
                await context.Response.WriteAsync(JsonSerializer.Serialize<IEnumerable<Product>>(data.Products));
            });

            // Endpoint for Post requests to add Product to database
            app.MapPost(BASEURL, async context =>
            {
                DataContext data = context.RequestServices.GetService<DataContext>();
                // Serialize request body to json string and then deserialize json to Product
                Product p = await JsonSerializer.DeserializeAsync<Product>(context.Request.Body);
                // Add product to database and save 
                await data.AddAsync(p);
                await data.SaveChangesAsync();
                context.Response.StatusCode = StatusCodes.Status200OK;
            });
        }
    }
}
