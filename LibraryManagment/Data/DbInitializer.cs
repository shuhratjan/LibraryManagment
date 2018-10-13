using LibraryManagment.Data.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagment.Data
{
    public static class DbInitializer
    {
        public static void Seed(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                //Add Customers
                var shuhratjan = new Customer { Name = "Shuhratjan" };
                var moldir = new Customer { Name = "Moldir" };
                var nurtileu = new Customer { Name = "Nurtileu" };

                context.Customers.Add(shuhratjan);
                context.Customers.Add(moldir);
                context.Customers.Add(nurtileu);

                //Add Authors
                var authorElmurad = new Author
                {
                    Name = "Elmurad Karimberdiyev",
                    Books = new List<Book>()
                    {
                        new Book{Title="Key of Success"},
                        new Book{Title = "Thinking like smart"}

                    }
                 
                };

                var authorDilmurad = new Author
                {
                    Name = "Dilmurad Karimberdiyev",
                    Books = new List<Book>()
                    {
                        new Book{Title="Secrets of ASP.NET MVC"},
                        new Book{Title = "Purpose of Live"},
                        new Book{Title = "Quick way to develop Project"}
                    }
                };

                context.Authors.Add(authorElmurad);
                context.Authors.Add(authorDilmurad);
                context.SaveChanges();
            }
        }
    }
}
