using System.Reflection;

namespace Trecom.Api.Services.Catalog.Persistance.DataSeeding;

public static class ReadCatalogItems
{
    public static List<(string, string, string, string)> ReadSeedFile(string fileName)
    {
        string filePath = Path.Combine(Assembly.GetExecutingAssembly().ToString(), "Trecom.Shared", fileName);
        string temp = "D:\\Projects\\NewProjects\\Trecom\\src\\Shared\\Trecom.Shared\\productdatas.txt";
        string line;
        string product = "";
        List<(string, string, string, string)> productList = new();

        StreamReader reader = new StreamReader(temp);

        while ((line = reader.ReadLine()) != null)
        {
            if (line.Count(x => x == '>') > 2)
            {
                var categories = line.Split(" > ", StringSplitOptions.RemoveEmptyEntries);
                string category1 = categories[0].Trim();
                string category2 = categories[1].Trim();
                string category3 = categories[2].Trim();

                if (line.Count(y => y == '>') > 3)
                    product = categories[4].Trim();
                product = categories[3].Trim();
                productList.Add((category1, category2, category3, product));

            }
        }

        return productList;
    }

}