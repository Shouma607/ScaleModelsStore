using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScaleModelsStore.Models
{
    public class ProductMaxQuantityCheck
    {       

        public static List<Product> CheckMaxQuantity(ShoppingCart cart, Order order)
        {
            ScaleModelsStoreEntities storeDb = new ScaleModelsStoreEntities();

            List<Product> RestrictedProducts=new List<Product>();            
            var carts = cart.GetCartItems();
            if(carts.Count!=0)
            {
                var orders = storeDb.Orders.Where(o => o.PostalCode.Trim() == order.PostalCode.Trim()
                                                && o.Country.Trim() == order.Country.Trim()
                                                && String.Compare(o.Address.Trim(), order.Address.Trim(), true) == 0
                                                && String.Compare(o.City.Trim(), order.City.Trim(), true) == 0
                                                || o.Email.ToLower() == order.Email.ToLower()).ToList();
                var products = storeDb.Products.ToList();
                foreach (var cartItem in carts)
                {
                    var product = products.Single(p => p.ProductId == cartItem.ProductId);
                    int sum = cartItem.Quantity;
                    foreach (var orderItem in orders)
                    {
                        var productInOrder = orderItem.OrderToProducts.SingleOrDefault(orp => orp.ProductId == cartItem.ProductId);
                        if (productInOrder != null)
                            sum = sum + productInOrder.Quantity;
                    }
                    if (sum > 3)
                    {
                        RestrictedProducts.Add(product);
                    }
                }
            }
            
            return RestrictedProducts;
        }

    }
}