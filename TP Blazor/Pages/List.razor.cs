using System;
using Microsoft.AspNetCore.Components;
using TP_Blazor.Models;

namespace TP_Blazor.Pages;

public partial class List
{
    public List()
    {
    }

    private Item[] items;

    [Inject]
    public HttpClient Http { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    protected override async Task OnInitializedAsync()
    {
        items = await Http.GetFromJsonAsync<Item[]>($"{NavigationManager.BaseUri}fake-data.json");
    }
}

