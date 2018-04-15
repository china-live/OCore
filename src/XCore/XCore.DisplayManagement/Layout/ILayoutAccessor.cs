using System.Threading.Tasks;

namespace XCore.DisplayManagement.Layout
{
    public interface ILayoutAccessor
    {
        Task<IShape> GetLayoutAsync();
    }

    public class LayoutAccessor : ILayoutAccessor
    {
        private IShape _layout;
        private readonly IShapeFactory _shapeFactory;

        public LayoutAccessor(IShapeFactory shapeFactory)
        {
            _shapeFactory = shapeFactory;
        }

        public async Task<IShape> GetLayoutAsync()
        {
            if (_layout == null)
            {
                _layout = await _shapeFactory.CreateAsync("Layout", Arguments.Empty);
            }

            return _layout;
        }
    }
}
