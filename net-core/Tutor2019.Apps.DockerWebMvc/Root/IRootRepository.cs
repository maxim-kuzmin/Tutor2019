//Author Maxim Kuzmin//makc//

using System.Linq;

namespace Tutor2019.Apps.DockerWebMvc.Root
{
    public interface IRootRepository {

        IQueryable<RootProduct> Products { get; }
    }
}
