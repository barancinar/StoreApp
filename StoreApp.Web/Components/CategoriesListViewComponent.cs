using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Abstract;
using StoreApp.Web.Models;

namespace StoreApp.Web.Components;

public class CategoriesListViewComponent : ViewComponent
{
    private readonly IStoreRepository _storeRepository;
    public CategoriesListViewComponent(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        ViewBag.SelectedCategory = RouteData?.Values["category"];
        var categories = await _storeRepository
                                        .Categories
                                        .Select(c => new CategoryViewModel
                                        {
                                            Id = c.Id,
                                            Name = c.Name,
                                            Url = c.Url
                                        })
                                        .ToListAsync();
        return View(categories);
    }


}

