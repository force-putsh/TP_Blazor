using System;
using System.Security.Cryptography;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TP_Blazor.Models;
using TP_Blazor.Services;

namespace TP_Blazor.Pages;

public partial class Add
{
    [Inject]
    ILocalStorageService LocalStorage { get; set; }

    [Inject]
    IWebHostEnvironment WebHostEnvironment { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    private List<string> enchantCategories = new List<string>() { "armor", "armor_head", "armor_chest", "weapon", "digger", "breakable", "vanishable" };
    private List<string> repairWith = new List<string>() { "oak_planks", "spruce_planks", "birch_planks", "jungle_planks", "acacia_planks", "dark_oak_planks", "crimson_planks", "warped_planks" };
    
    [Inject]
    private IDataService DataService{ get; set; }
    



    private ItemModel itemModel = new()
    {
        EnchantCategories = new List<string>(),
        RepairWith = new List<string>()
    };

    private async void OnHandleValidSubmit()
    {
        /*var currentData = await LocalStorage.GetItemAsync<List<Item>>("data");
        itemModel.Id = currentData.Max(s => s.Id) + 1;
        currentData.Add(new Item
        {
            Id = itemModel.Id,
            DisplayName = itemModel.DisplayName,
            Name = itemModel.Name,
            RepairWith = itemModel.RepairWith,
            EnchantCategories = itemModel.EnchantCategories,
            MaxDurability = itemModel.MaxDurability,
            StackSize = itemModel.StackSize,
            CreatedDate = DateTime.Now
        });

        var imagePathInfo = new DirectoryInfo($"{WebHostEnvironment.WebRootPath}/images");

        if (!imagePathInfo.Exists)
        {
            imagePathInfo.Create();
        }

        var fileName = new FileInfo($"{imagePathInfo}/{itemModel.Name}.png");
        await File.WriteAllBytesAsync(fileName.FullName, itemModel.ImageContent);

        await LocalStorage.SetItemAsync("data", currentData);
        NavigationManager.NavigateTo("list");*/
        await DataService.Add(itemModel);
        NavigationManager.NavigateTo("list");
    }

    private async Task LoadImage(InputFileChangeEventArgs e)
    {

        using (var memoryStream = new MemoryStream())
        {
            await e.File.OpenReadStream().CopyToAsync(memoryStream);
            itemModel.ImageContent = memoryStream.ToArray();
        }
    }

    private void OnEnchantCategoriesChange(string item, object checkedValue)
    {
        if ((bool)checkedValue)
        {
            if (!itemModel.EnchantCategories.Contains(item))
            {
                itemModel.EnchantCategories.Add(item);
            }

            return;
        }

        if (itemModel.EnchantCategories.Contains(item))
        {
            itemModel.EnchantCategories.Remove(item);
        }
    }

    private void OnRepairWithChange(string item, object checkedValue)
    {
        if ((bool)checkedValue)
        {
            if (!itemModel.RepairWith.Contains(item))
            {
                itemModel.RepairWith.Add(item);
            }

            return;
        }

        if (itemModel.RepairWith.Contains(item))
        {
            itemModel.RepairWith.Remove(item);
        }
    }

}

