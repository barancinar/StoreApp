using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using StoreApp.Data.Abstract;
using StoreApp.Web.Helpers;
using StoreApp.Web.Models;

namespace StoreApp.Web.Pages
{
    public class CartModel : PageModel
    {
        private readonly IStoreRepository _storeRepository;
        public CartModel(IStoreRepository storeRepository, Cart cartService)
        {
            _storeRepository = storeRepository;
            Cart = cartService;
        }
        public Cart? Cart { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost(int Id)
        {
            var product = _storeRepository.Products.FirstOrDefault(p => p.Id == Id);
            if (product != null)
            {
                Cart?.AddItem(product, 1);
            }
            return RedirectToPage("/cart");
        }
        public IActionResult OnPostRemove(int Id)
        {

            Cart?.RemoveItem(Cart.Items.First(p => p.Product.Id == Id).Product);
            return RedirectToPage("/cart");
        }
    }
}
