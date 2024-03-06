using Newtonsoft.Json;
using PricatMVC.Models;
using RestSharp;
using System.Net.Http;

namespace PricatMVC.Services;


public class CategoryService
{
    private readonly IConfiguration _configuration;
    private readonly string baseUrl;
    private readonly RestClient _restClient;
    public CategoryService(RestClient restClient, IConfiguration configuration)
    {
        _configuration = configuration;
        baseUrl = configuration.GetSection("BaseUrl").Value;
        _restClient = restClient;
    }

    public async Task<List<Category>> GetAll()
    {
        var categoryList = await GetAllCategories();
        return categoryList;
    }

    private async Task<List<Category>> GetAllCategories()
    {
        var request = new RestRequest($"{baseUrl}/categories", Method.Get);
        var response = await _restClient.GetAsync(request);

        List<Category>? data = JsonConvert.DeserializeObject<List<Category>>(response.Content!);

        return data!;
    }


    public async Task<Category> GetById(int id)
    {
        var categoryList = await GetCategoryById(id);
        return categoryList;
    }

    private async Task<Category> GetCategoryById(int id)
    {
        var request = new RestRequest($"{baseUrl}/categories/{id}", Method.Get);

        var response = await _restClient.GetAsync(request);

        Category? data = JsonConvert.DeserializeObject<Category>(response.Content!);

        return data!;
    }

    public async Task<Category> CreateCategory(Category category)
    {
        var categoryResult = await PostCategory(category);
        return categoryResult;
    }

    private async Task<Category> PostCategory(Category category)
    {
        var request = new RestRequest($"{baseUrl}/categories", Method.Post);
        request.AddJsonBody(category);

        var response = await _restClient.PostAsync(request);

        Category? data = JsonConvert.DeserializeObject<Category>(response.Content!);

        return data!;
    }

    public async Task<Category> EditCategory(int id, Category category)
    {
        var categoryResult = await PutCategory(category);
        return categoryResult;
    }

    private async Task<Category> PutCategory(Category category)
    {
        var request = new RestRequest($"{baseUrl}/categories/{category.Id}", Method.Put);
        request.AddJsonBody(category);

        var response = await _restClient.PutAsync(request);

        Category? data = JsonConvert.DeserializeObject<Category>(response.Content!);

        return data!;
    }

    public async Task DeleteCategory(int id)
    {
        await RemoveCategory(id);
    }

    private async Task<bool> RemoveCategory(int id)
    {
        var request = new RestRequest($"{baseUrl}/categories/{id}", Method.Delete);

        var response = await _restClient.DeleteAsync(request);

        var data = response.Content!;

        return response.IsSuccessStatusCode;
    }

    
}