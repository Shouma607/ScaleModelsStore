using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScaleModelsStore.Models
{
    public partial class ShoppingCart
    {
        ScaleModelsStoreEntities storeDb = new ScaleModelsStoreEntities();

        string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";

        public static ShoppingCart GetCart(HttpContextBase ctx)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(ctx);           
            return cart;
        }

        public bool AddToCart(Product product)
        {
            var item = storeDb.Carts.SingleOrDefault(c => c.CartId == ShoppingCartId
                                                      && c.ProductId == product.ProductId);
            var productDb = storeDb.Products.Single(p => p.ProductId == product.ProductId);

            bool isAdded = false;

            if(item==null)
            {
                item = new Cart
                {
                    ProductId = product.ProductId,
                    CartId = ShoppingCartId,
                    Quantity = 1
                };                
                storeDb.Carts.Add(item);
                productDb.QuantityInStock--; 
                isAdded = true;
            }
            else if(item.Quantity<3)
            {
                item.Quantity++;
                productDb.QuantityInStock--; //not correct
                isAdded = true;

            }
            else
            {                
                isAdded = false;
            }

            storeDb.SaveChanges();
            return isAdded;
        }

        public void RemoveRecord(int id)
        {
            var item = storeDb.Carts.Single(c => c.CartId == ShoppingCartId
                                              && c.RecordId == id);
            var product = storeDb.Products.Single(p => p.ProductId == item.ProductId);
            if (item != null)
            {
                storeDb.Carts.Remove(item);
                product.QuantityInStock = product.QuantityInStock + item.Quantity;
                storeDb.SaveChanges();
            }                                   
        }

        private void RemoveCart()
        {
            var items = storeDb.Carts.Where(c => c.CartId == ShoppingCartId);

            foreach(var item in items)
            {
                storeDb.Carts.Remove(item);
            }

            storeDb.SaveChanges();
        }

        public void RemoveByProductId(int id)
        {
            var item = storeDb.Carts.Single(c => c.ProductId == id);
            var product = storeDb.Products.Single(p => p.ProductId == id);
            if(item!=null)
            {
                product.QuantityInStock = product.QuantityInStock + item.Quantity;
                storeDb.Carts.Remove(item);
                storeDb.SaveChanges();
            }
        }

        public int RemoveUnit(int id)
        {
            var item = storeDb.Carts.Single(c => c.CartId == ShoppingCartId && c.RecordId == id);
            var product = storeDb.Products.Single(p => p.ProductId == item.ProductId);
            int changeQuantity=1;

            if(item!=null)
            {
                item.Quantity--;
                product.QuantityInStock++;
                changeQuantity = item.Quantity;
                storeDb.SaveChanges();                           
            }

            return changeQuantity;
        }

        public int AddUnit(int id)
        {
            var item = storeDb.Carts.Single(c => c.CartId == ShoppingCartId && c.RecordId == id);
            var product = storeDb.Products.Single(p => p.ProductId == item.ProductId);
            int changeQuantity = 3; //TODO: Add to Product Entity MaxQuantity property!

            if(item!=null&&product.QuantityInStock>0)
            {
                item.Quantity++;                
                product.QuantityInStock--; //not correct
                changeQuantity = item.Quantity;
                storeDb.SaveChanges();
            }
            else
            {
                changeQuantity = item.Quantity;
            }

            return changeQuantity;
        }

        public void CreateOrder(Order order)
        {
            var items = GetCartItems();

            foreach(var item in items)
            {
                var orderToProduct = new OrderToProduct
                {
                    OrderId = order.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };
                storeDb.OrderToProducts.Add(orderToProduct);
            }
            storeDb.SaveChanges();
            RemoveCart();
        }

        public int GetItemQuantity(int id)
        {
            var item = storeDb.Carts.Single(c => c.CartId == ShoppingCartId && c.RecordId == id);

            var itemQuantity = item.Quantity;

            return itemQuantity;
        }

        public List<Cart> GetCartItems()
        {
            return storeDb.Carts.Where(c => c.CartId == ShoppingCartId).ToList();
        }

        public int GetQuantity()
        {
            int? quantity = storeDb.Carts
                                .Where(c => c.CartId == ShoppingCartId)
                                .Select(c => (int?)c.Quantity).Sum();
            return quantity ?? 0;
        }

        public decimal GetTotal()
        {
            decimal? total = storeDb.Carts
                                    .Where(c => c.CartId == ShoppingCartId)
                                    .Select(c => (int?)c.Quantity * c.Product.Price).Sum();

            return total ?? decimal.Zero;
        }

        public string GetCartId(HttpContextBase ctx)
        {
            if(ctx.Session[CartSessionKey]==null)
            {
                if(!string.IsNullOrWhiteSpace(ctx.User.Identity.Name))
                {
                    ctx.Session[CartSessionKey] = ctx.User.Identity.Name;
                }
                else
                {
                    Guid tmpCartId = new Guid();   //For debug mode
                    //Guid tmpCartId = Guid.NewGuid(); //Run without debugging
                    ctx.Session[CartSessionKey] = tmpCartId.ToString();                    
                }
            }

            return ctx.Session[CartSessionKey].ToString();
        }
    }
}