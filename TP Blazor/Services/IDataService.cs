using TP_Blazor.Models;

namespace TP_Blazor.Services;

public interface IDataService
{
    public Task Add(ItemModel item);
    Task<int> Count();
    Task<List<Item>> List(int page, int pageSize);
    Task<Item> GetById(int id);
    Task Update(int id,ItemModel item);
    Task Delete(int id);
}