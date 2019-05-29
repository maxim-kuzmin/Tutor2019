//Author Maxim Kuzmin//makc//

using System.Linq;

namespace Tutor2019.Apps.DockerWebMvc.Root
{
    public class RootDummyRepository : IRootRepository
    {
        private static RootProduct[] DummyData = new RootProduct[] {
            new RootProduct { Name = "Prod1",  Category = "Cat1", Price = 100 },
            new RootProduct { Name = "Prod2",  Category = "Cat1", Price = 100 },
            new RootProduct { Name = "Prod3",  Category = "Cat2", Price = 100 },
            new RootProduct { Name = "Prod4",  Category = "Cat2", Price = 100 },
        };

        public IQueryable<RootProduct> Products => DummyData.AsQueryable();
    }
}
