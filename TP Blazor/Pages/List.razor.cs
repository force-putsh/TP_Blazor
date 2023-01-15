using System;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using TP_Blazor.Models;
using Blazored;
using Blazored.LocalStorage;
using Blazored.Modal;
using Blazored.Modal.Services;
using TP_Blazor.Modals;
using TP_Blazor.Services;
using Microsoft.Extensions.Localization;

namespace TP_Blazor.Pages;

public partial class List
{
    public List()
    {
    }

    private List<Item> items;
    private int totalItem;

    [Inject]
    public IStringLocalizer<List> Localizer { get; set; }
    [Inject]
    public HttpClient HttpClient { get; set; }
    
    [Inject]
    public IDataService DataService { get; set; }
    
    [CascadingParameter]
    public IModalService Modal { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    IWebHostEnvironment WebHostEnvironment { get; set; }

    [Inject]
    public ILocalStorageService LocalStorage { get; set; }

    

    private async Task OnReadData(DataGridReadDataEventArgs<Item> e)
    {
        if (e.CancellationToken.IsCancellationRequested)
        {
            return;
        }
        //var response = (await HttpClient.GetFromJsonAsync<Item[]>($"{NavigationManager.BaseUri}fake-data.json")).Skip((e.Page - 1) * e.PageSize).Take(e.PageSize).ToList();

        if (!e.CancellationToken.IsCancellationRequested)
        {
            //totalItem = (await HttpClient.GetFromJsonAsync<List<Item>>($"{NavigationManager.BaseUri}fake-data.json")).Count;
            //items = new List<Item>(response); // an actual data for the current page
            items = await DataService.List(e.Page, e.PageSize);
            totalItem = await DataService.Count();
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            return;
        }

        var currentData = await LocalStorage.GetItemAsync<Item[]>("data");

        if (currentData==null)
        {
            var originalData = HttpClient.GetFromJsonAsync<Item[]>($"{NavigationManager.BaseUri}fake-data.json").Result;
            await LocalStorage.SetItemAsync("data", originalData);
        }
    }
    
    private async void OnDelete(int id)
    {
        var parameters = new ModalParameters();
        parameters.Add(nameof(Item.Id), id);

        var modal = Modal.Show<DeleteConfirmation>("Delete Confirmation", parameters);
        var result = await modal.Result;

        if (result.Cancelled)
        {
            return;
        }

        await DataService.Delete(id);

        // Reload the page
        NavigationManager.NavigateTo("list", true);
    }
}

