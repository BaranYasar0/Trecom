using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Trecom.Api.Services.Catalog.Models.Entities;

namespace Trecom.Api.Services.Catalog.Persistance.DataSeeding;

public static class SeedCatalogItems
{

    public static void SeedItemsToDb(ModelBuilder builder)
    {
        var tuple = ReadCatalogItems.ReadSeedFile("productdatas.txt");
        List<BaseCategory> baseCategories = GetBaseCategories(tuple);
        List<TypeCategory> typeCategories = GetTypeCategories(tuple,baseCategories);
        List<SpecificCategory> specificCategories = GetSpecificCategories(tuple,typeCategories);
        List<Product> products = GetProducts(tuple,specificCategories);
            
        builder.Entity<BaseCategory>().HasData(baseCategories);
        builder.Entity<TypeCategory>().HasData(typeCategories);
        builder.Entity<SpecificCategory>().HasData(specificCategories);
        builder.Entity<Product>().HasData(products);

    }

    public static List<BaseCategory> GetBaseCategories(List<(string, string, string, string)> tuple)
    {
        var categories = tuple.Select(x => x.Item1).Distinct().ToList();
        return categories.Select(x => new BaseCategory(x)).ToList();
    }

    public static List<TypeCategory> GetTypeCategories(List<(string, string, string, string)> tuple,List<BaseCategory> categories)
    {
        var distinctCategories = tuple.Select(x => x.Item2).Distinct().ToList();
            
        return distinctCategories.Select(x => new TypeCategory(x,
            categories.FirstOrDefault(y => y.Name ==
                                           tuple.FirstOrDefault(f => f.Item2 == x).Item1)
                .Id)).ToList();
    }

    public static List<SpecificCategory> GetSpecificCategories(List<(string, string, string, string)> tuple,List<TypeCategory> categories)
    {
        var distinctCategories=tuple.Select(x=>x.Item3).Distinct().ToList();
            
        return distinctCategories.Select(x => new SpecificCategory(x,
            categories.FirstOrDefault(y => y.Name ==
                                           tuple.FirstOrDefault(f => f.Item3 == x).Item2)
                .Id)).ToList();
    }

    public static List<Product> GetProducts(List<(string, string, string, string)> tuple,List<SpecificCategory> categories)
    {
        return tuple.Select(x => new Product(x.Item4, categories.FirstOrDefault(y => y.Name == x.Item3).Id,
            Guid.Parse("75274c0b-ea5c-444b-9a33-c5a88f4bbf24"), Guid.Parse("8990201c-d00b-4a10-8034-57f17dc21e98"))).ToList();
    }

}