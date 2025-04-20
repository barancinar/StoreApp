using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StoreApp.Data.Abstract;
using StoreApp.Data.Concrete;
using StoreApp.Web.Models;

namespace StoreApp.Web.Controllers;

public class OrderController : Controller
{
    private Cart cart;
    private readonly IOrderRepository _orderRepository;
    public OrderController(Cart cartService, IOrderRepository orderRepository)
    {
        cart = cartService;
        _orderRepository = orderRepository;
    }
    public IActionResult Checkout()
    {
        return View(new OrderModel { Cart = cart });
    }
    [HttpPost]
    public IActionResult Checkout(OrderModel model)
    {
        if (cart.Items.Count == 0)
        {
            ModelState.AddModelError("", "Sepetinizde ürün yok!");
        }
        if (ModelState.IsValid)
        {
            // Order işlemleri burada yapılacak
            // Örnek olarak, veritabanına kaydetme işlemi yapılabilir
            // orderRepository.SaveOrder(model);
            var order = new Order
            {
                CustomerName = model.CustomerName,
                City = model.City,
                Phone = model.Phone,
                Email = model.Email,
                AddressLine = model.AddressLine,
                OrderDate = DateTime.Now,
                OrderItems = cart.Items.Select(c => new StoreApp.Data.Concrete.OrderItem
                {
                    ProductId = c.Product.Id,
                    Quantity = c.Quantity,
                    Price = (double)c.Product.Price
                }).ToList()
            };
            model.Cart = cart;
            var payment = ProcessPayment(model); // Ödeme işlemi yapılır
            if (payment.Status == TaskStatus.RanToCompletion)
            {
                _orderRepository.SaveOrder(order);
                cart.Clear(); // Sepeti temizle
                return RedirectToPage("/Completed", new { OrderId = order.Id });

            }
            model.Cart = cart;
            return View(model);
        }
        else
        {
            model.Cart = cart;
            return View(model);
        }

    }

    private async Task<Payment> ProcessPayment(OrderModel model)
    {
        Options options = new Options();
        options.ApiKey = "sandbox-QCUVAMM1oA026XPgpxX3hFF30aBWrh9r";
        options.SecretKey = "sandbox-87ydHdNdZrg9ny3gIbgYU1fVwzKvf95I";
        options.BaseUrl = "https://sandbox-api.iyzipay.com";

        CreatePaymentRequest request = new CreatePaymentRequest();
        request.Locale = Locale.TR.ToString();
        request.ConversationId = new Random().Next(111111111, 999999999).ToString();
        request.Price = model.Cart.CalculateTotal().ToString();
        request.PaidPrice = model.Cart.CalculateTotal().ToString();
        request.Currency = Currency.TRY.ToString();
        request.Installment = 1;
        request.BasketId = "B67832";
        request.PaymentChannel = PaymentChannel.WEB.ToString();
        request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

        PaymentCard paymentCard = new PaymentCard();
        paymentCard.CardHolderName = model.CartName;
        paymentCard.CardNumber = model.CartNumber;
        paymentCard.ExpireMonth = model.ExpirationMonth;
        paymentCard.ExpireYear = model.ExpirationYear;
        paymentCard.Cvc = model.Cvc;
        paymentCard.RegisterCard = 0;
        request.PaymentCard = paymentCard;

        Buyer buyer = new Buyer();
        buyer.Id = "BY789";
        buyer.Name = "John";
        buyer.Surname = "Doe";
        buyer.GsmNumber = "+905350000000";
        buyer.Email = "email@email.com";
        buyer.IdentityNumber = "74300864791";
        buyer.LastLoginDate = "2015-10-05 12:43:35";
        buyer.RegistrationDate = "2013-04-21 15:12:09";
        buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
        buyer.Ip = "85.34.78.112";
        buyer.City = "Istanbul";
        buyer.Country = "Turkey";
        buyer.ZipCode = "34732";
        request.Buyer = buyer;

        Address shippingAddress = new Address();
        shippingAddress.ContactName = "Jane Doe";
        shippingAddress.City = "Istanbul";
        shippingAddress.Country = "Turkey";
        shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
        shippingAddress.ZipCode = "34742";
        request.ShippingAddress = shippingAddress;

        Address billingAddress = new Address();
        billingAddress.ContactName = "Jane Doe";
        billingAddress.City = "Istanbul";
        billingAddress.Country = "Turkey";
        billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
        billingAddress.ZipCode = "34742";
        request.BillingAddress = billingAddress;

        List<BasketItem> basketItems = new List<BasketItem>();

        foreach (var item in model.Cart.Items ?? Enumerable.Empty<CartItem>())
        {
            BasketItem firstBasketItem = new BasketItem();
            firstBasketItem.Id = item.Product.Id.ToString();
            firstBasketItem.Name = item.Product.Name;
            firstBasketItem.Category1 = "Telefon";
            firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            firstBasketItem.Price = item.Product.Price.ToString();
            basketItems.Add(firstBasketItem);
        }

        request.BasketItems = basketItems;

        Payment payment = await Payment.Create(request, options);
        return payment;
    }
}
