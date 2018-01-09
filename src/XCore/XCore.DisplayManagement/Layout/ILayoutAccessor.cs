using System.Threading.Tasks;

namespace XCore.DisplayManagement.Layout
{
    public interface ILayoutAccessor
    {
        Task<IShape> GetLayoutAsync();
    }
}
