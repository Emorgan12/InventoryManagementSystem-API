using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.Runtime.Internal.Util;
using InventoryManagementSystem_API;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Http.Logging;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
public class MongoDbRepository : ProductsRepository{
    public MongoDbRepository(IMongoClient mongoClient)
    {
        IMongoDatabase database = mongoClient.GetDatabase(databaseName);
        productsCollection = database.GetCollection<Product>(collectionName);
    }

    private const string databaseName = "InventoryManagementSystem";
    private const string collectionName = "Products";
    private readonly IMongoCollection<Product> productsCollection;
    private readonly FilterDefinitionBuilder<Product> filterBuilder = Builders<Product>.Filter;
    private readonly ILogger<MongoDbRepository> logger;

    public async Task CreateProduct(Product product)
    {
        await productsCollection.InsertOneAsync(product);
    }

    public async Task DeleteProduct(Guid id)
    {
        var filter = filterBuilder.Eq(Product => Product.id, id);
        await productsCollection.DeleteOneAsync(filter);
    }

    public async Task<Product> GetProduct(Guid id)
    {
        var filter = filterBuilder.Eq(product => product.id, id);
        return await productsCollection.Find(filter).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        return await productsCollection.Find(new BsonDocument()).ToListAsync();
    }

    public Task UpdateProduct(Product product)
    {
        var filter = filterBuilder.Eq(existingProduct => existingProduct.id, product.id);
        return productsCollection.ReplaceOneAsync(filter, product);
    }
}