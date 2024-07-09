using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagementSystem_API;
using Microsoft.AspNetCore.Mvc;
public interface ProductsRepository
{
    Task<IEnumerable<Product>> GetAllProducts();
    Task<Product> GetProduct(Guid id);
    Task CreateProduct(Product product);
    Task UpdateProduct(Product product);
    Task DeleteProduct(Guid id);
}