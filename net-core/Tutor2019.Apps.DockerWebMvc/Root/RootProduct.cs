//Author Maxim Kuzmin//makc//

namespace Tutor2019.Apps.DockerWebMvc.Root
{
    public class RootProduct
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }

        public RootProduct()
        {
        }

        public RootProduct(
            string name = null,
            string category = null,
            decimal price = 0
            )
        {
            Name = name;
            Category = category;
            Price = price;
        }
    }
}
