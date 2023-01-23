using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using TP_Blazor.Factories;
using TP_Blazor.Models;

namespace TP_Blazor.Services;

public class DataLocalService:IDataService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorageService;
    private readonly NavigationManager _navigationManager;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public DataLocalService(HttpClient httpClient, ILocalStorageService localStorageService, NavigationManager navigationManager, IWebHostEnvironment webHostEnvironment)
    {
        _httpClient = httpClient;
        _localStorageService = localStorageService;
        _navigationManager = navigationManager;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task Add(ItemModel item)
    {
        var currentItems = await _localStorageService.GetItemAsync<List<Item>>("data");
        item.Id = currentItems.Max(s=>s.Id)+1;
        // currentItems.Add(new Item
        // {
        //     Id = item.Id,
        //     Name = item.Name,
        //     DisplayName = item.DisplayName,
        //     RepairWith = item.RepairWith,
        //     EnchantCategories = item.EnchantCategories,
        //     MaxDurability = item.MaxDurability,
        //     StackSize = item.StackSize,
        //     CreatedDate = DateTime.Now
        // });
        currentItems.Add(ItemFactory.Create(item));
        
        var imagePathsInfo = new DirectoryInfo(Path.Combine($"{_webHostEnvironment.ContentRootPath}/images"));
        if (!imagePathsInfo.Exists)
        {
            imagePathsInfo.Create();
        }
        
        var fileName = new FileInfo($"{imagePathsInfo}/{item.Name}.png");
        await File.WriteAllBytesAsync(fileName.FullName, item.ImageContent);
        await _localStorageService.SetItemAsync("data", currentItems);
    }

    public async Task<int> Count()
    {
        var currentItems = _localStorageService.GetItemAsync<Item[]>("data");
        if (currentItems== null)
        {
            var originalItems = _httpClient.GetFromJsonAsync<Item[]>($"f{_navigationManager.BaseUri}ake-data.json");
            await _localStorageService.SetItemAsync("data", originalItems);
        }
        return (await _localStorageService.GetItemAsync<Item[]>("data")).Length;
    }

    public async Task<List<Item>> List(int page, int pageSize)
    {
        var currentItems = _localStorageService.GetItemAsync<Item[]>("data");
        if (currentItems == null)
        {
            var originalItems = _httpClient.GetFromJsonAsync<Item[]>($"f{_navigationManager.BaseUri}ake-data.json");
            _localStorageService.SetItemAsync("data", originalItems);
        }
        return (await _localStorageService.GetItemAsync<Item[]>("data")).Skip((page-1)*pageSize).Take(pageSize).ToList();
    }

    public async Task<Item> GetById(int id)
    {
        var currentItems =await _localStorageService.GetItemAsync<List<Item>>("data");
        var item = currentItems.FirstOrDefault(s => s.Id == id);
        if (item == null)
        {
            throw new Exception($"Item with id {id} not found");
        }
        return item;
    }

    public async Task Update(int id, ItemModel item)
    {
        var currentItems = await _localStorageService.GetItemAsync<List<Item>>("data");
        var itemToUpdate = currentItems.FirstOrDefault(s => s.Id == id);

        if (itemToUpdate == null)
        {
            throw new Exception($"Item with id {id} not found");
        }
        var imagePathsInfo = new DirectoryInfo($"{_webHostEnvironment.ContentRootPath}/images");
        if (!imagePathsInfo.Exists)
        {
            imagePathsInfo.Create();
        }

        if (itemToUpdate.Name != item.Name)
        {
            var oldFileName = new FileInfo($"{imagePathsInfo}/{itemToUpdate.Name}.png");
            if (oldFileName.Exists)
            {
                oldFileName.Delete();
            }
        }
        
        var fileName = new FileInfo($"{imagePathsInfo}/{item.Name}.png");
        await File.WriteAllBytesAsync(fileName.FullName, item.ImageContent);
        // itemToUpdate.Name = item.Name;
        // itemToUpdate.DisplayName = item.DisplayName;
        // itemToUpdate.RepairWith = item.RepairWith;
        // itemToUpdate.EnchantCategories = item.EnchantCategories;
        // itemToUpdate.MaxDurability = item.MaxDurability;
        // itemToUpdate.StackSize = item.StackSize;
        // itemToUpdate.UpdatedDate = DateTime.Now;
        ItemFactory.Update(itemToUpdate, item);
        await _localStorageService.SetItemAsync("data", currentItems);
    }

    public async Task Delete(int id)
    {
        var currentItems =await _localStorageService.GetItemAsync<List<Item>>("data");
        var itemToDelete = currentItems.FirstOrDefault(s => s.Id == id);
        currentItems.Remove(itemToDelete);
        var imagePathsInfo = new DirectoryInfo($"{_webHostEnvironment.ContentRootPath}/images");
        var fileName = new FileInfo($"{imagePathsInfo}/{itemToDelete.Name}.png");
        if (fileName.Exists)
        {
            File.Delete(fileName.FullName);
        }
        await _localStorageService.SetItemAsync("data", currentItems);
    }
}