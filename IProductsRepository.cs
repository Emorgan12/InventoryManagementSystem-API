using InventoryManagementSystem_API;

public interface IProductsRepository
{
    IEnumerable<Product> GetAllProducts(string name);
    Product GetProductById(int id);
    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(int id);
}