using Newtonsoft.Json;
using PricatMVC.Models;
using RestSharp;

namespace PricatMVC.Services;


public class ProductService
{
    private readonly IConfiguration _configuration;
    private readonly string baseUrl;
    private readonly RestClient _restClient;
    public ProductService(RestClient restClient, IConfiguration configuration)
    {
        _configuration = configuration;
        baseUrl = configuration.GetSection("BaseUrl").Value;
        _restClient = restClient;
    }

    public async Task<List<Product>> GetAll()
    {
        var productList = await GetAllProducts();
        return productList;
    }

    private async Task<List<Product>> GetAllProducts()
    {
        var request = new RestRequest($"{baseUrl}/products", Method.Get);
        var response = await _restClient.GetAsync(request);

        List<Product>? data = JsonConvert.DeserializeObject<List<Product>>(response.Content!);

        return data!;
    }

    public async Task<Product> GetById(int id)
    {
        var productList = await GetProductById(id);
        return productList;
    }

    private async Task<Product> GetProductById(int id)
    {
        var request = new RestRequest($"{baseUrl}/products/{id}", Method.Get);

        var response = await _restClient.GetAsync(request);

        Product? data = JsonConvert.DeserializeObject<Product>(response.Content!);

        return data!;
    }

    public async Task<Product> CreateProduct(Product product)
    {
        var productResult = await PostProduct(product);
        return productResult;
    }

    private async Task<Product> PostProduct(Product product)
    {
        var request = new RestRequest($"{baseUrl}/products", Method.Post);
        request.AddJsonBody(product);

        var response = await _restClient.PostAsync(request);

        Product? data = JsonConvert.DeserializeObject<Product>(response.Content!);

        return data!;
    }

    public async Task<Product> EditProduct(int id, Product product)
    {
        var productResult = await PutProduct(product);
        return productResult;
    }

    private async Task<Product> PutProduct(Product product)
    {
        var request = new RestRequest($"{baseUrl}/products/{product.Id}", Method.Put);
        request.AddJsonBody(product);

        var response = await _restClient.PutAsync(request);

        Product? data = JsonConvert.DeserializeObject<Product>(response.Content!);

        return data!;
    }

    public async Task DeleteProduct(int id)
    {
        await RemoveProduct(id);
    }

    private async Task<bool> RemoveProduct(int id)
    {
        var request = new RestRequest($"{baseUrl}/products/{id}", Method.Delete);

        var response = await _restClient.DeleteAsync(request);

        var data = response.Content!;

        return response.IsSuccessStatusCode;
    }


}