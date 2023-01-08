using System;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using TP_Blazor.Models;

namespace TP_Blazor.Pages;

public partial class List
{
    public List()
    {
    }

    private List<Item> items;
    private int totalItem;

    [Inject]
    public HttpClient Http { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    private async Task OnReadData(DataGridReadDataEventArgs<Item> e)
    {
        if (e.CancellationToken.IsCancellationRequested)
        {
            return;
        }
        var response = (await Http.GetFromJsonAsync<Item[]>($"{NavigationManager.BaseUri}fake-data.json")).Skip((e.Page - 1) * e.PageSize).Take(e.PageSize).ToList();

        if (!e.CancellationToken.IsCancellationRequested)
        {
            totalItem = (await Http.GetFromJsonAsync<List<Item>>($"{NavigationManager.BaseUri}fake-data.json")).Count;
            items = new List<Item>(response); // an actual data for the current page
        }
    }

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);
    }


}

