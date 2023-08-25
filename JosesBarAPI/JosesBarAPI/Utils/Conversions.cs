using JosesBarAPI.Entities;
using System.Collections.Generic;

namespace JosesBarAPI.Utils
{
    public class Conversions
    {
        public static byte[] ListToFile(List<Product> products)
        {
            var path = Path.GetTempPath();
            var fileName = Path.ChangeExtension(Guid.NewGuid().ToString(),".csv");
            fileName = Path.Combine(path, fileName);

            using (TextWriter tw = new StreamWriter(fileName))
            {
                tw.WriteLine(Product.GetHeader());
                foreach (var item in products)
                {
                    tw.WriteLine(item.ToString());
                }
            }
            var returnBytes = File.ReadAllBytes(fileName);
            return returnBytes;
            
        }
    }
}
