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

        public void AddToCart(Product product)
        {
            var item = storeDb.Carts.SingleOrDefault(c => c.CartId == ShoppingCartId
                                                      && c.ProductId == product.ProductId);

            if(item==null)
            {
                item = new Cart
                {
                    ProductId = product.ProductId,
                    CartId = ShoppingCartId,
                    Quantity = 1
                };

                storeDb.Carts.Add(item);
            }
            else
            {
                item.Quantity++;
            }

            storeDb.SaveChanges();
        }

        public int RemoveRecord(int id)
        {
            var item = storeDb.Carts.Single(c => c.CartId == ShoppingCartId
                                              && c.RecordId == id);
            int itemQuantity = 0;
            if(item!=null)
            {
                if(item.Quantity>1)
                {
                    item.Quantity--;
                    itemQuantity = item.Quantity;
                }
                else
                {
                    storeDb.Carts.Remove(item);
                }
                storeDb.SaveChanges();
            }

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
                    //Guid tmpCartId = new Guid();   //For debug mode
                    Guid tmpCartId = Guid.NewGuid(); //Run without debugging
                    ctx.Session[CartSessionKey] = tmpCartId.ToString();                    
                }
            }

            return ctx.Session[CartSessionKey].ToString();
        }
    }
}