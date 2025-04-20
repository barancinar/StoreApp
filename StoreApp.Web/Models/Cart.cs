using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml.Serialization;
using StoreApp.Data.Concrete;

namespace StoreApp.Web.Models;

public class Cart
{
    public List<CartItem> Items { get; set; } = new List<CartItem>();
    public virtual void AddItem(Product product, int quantity)
    {
        var items = Items.Where(p => p.Product.Id == product.Id).FirstOrDefault();
        if (items == null)
        {
            Items.Add(new CartItem { Product = product, Quantity = quantity });
        }
        else
        {
            items.Quantity += quantity;
        }
    }
    public virtual void RemoveItem(Product product)
    {
        Items.RemoveAll(p => p.Product.Id == product.Id);

    }
    public double CalculateTotal()
    {
        return Items.Sum(p => p.Product.Price * p.Quantity);
    }
    public virtual void Clear()
    {
        Items.Clear();
    }
}

public class CartItem
{
    public int CartItemId { get; set; }
    public Product Product { get; set; } = new();
    public int Quantity { get; set; }

}
