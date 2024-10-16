using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagementSystem_API;
using InventoryManagementSystem_API.Data;
using Microsoft.AspNetCore.Mvc;
public class ProductsRepository : IProductsRepository
{
    private readonly DataContext _context;
    public ProductsRepository(DataContext context)
    {
        _context = context;
    }
    public IEnumerable<Product> GetAllProducts(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            return _context.Products.Where(p => p.name == name).ToList();
        }
        return _context.Products.ToList();
    }

    public Product GetProductById(int id)
    {
        return _context.Products.Find(id);
    }
    public void AddProduct(Product product)
    {
        _context.Add(product);
        _context.SaveChanges();
    }
    public void UpdateProduct(Product product)
    {
        _context.Update(product);
    }
    public void DeleteProduct(int id)
    {
        _context.Remove(_context.Products.Find(id));
    }
}