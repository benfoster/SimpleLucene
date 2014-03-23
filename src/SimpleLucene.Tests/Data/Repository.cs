using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleLucene.Tests.Entities;

namespace SimpleLucene.Tests.Data
{
    public class Repository
    {
        public IList<Product> Products {
            get {
                return new List<Product> {
                    new Product { Id = 1, Name = "Football" },
                    new Product { Id = 2, Name = "Coffee Cup"},
                    new Product { Id = 3, Name = "Nike Trainers"},
                    new Product { Id = 4, Name = "Apple iPod Nano"},
                    new Product { Id = 5, Name = "Asus eeePC"},
                    new Product { Id = 6, Name = "Pack of cards"},
                    new Product { Id = 7, Name = "Funky TShirt"},
                    new Product { Id = 8, Name = "Blackberry 9800"},
                    new Product { Id = 9, Name = "Guitar Hero"},
                    new Product { Id = 10, Name = "House Plant"},
                };
            }
        }
    }
}
