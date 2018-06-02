using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OCore.DisplayManagement.Layout;

namespace OCore.DisplayManagement.Implementation
{
    public interface IShapeFactoryEvents
    {
        void Creating(ShapeCreatingContext context);
        void Created(ShapeCreatedContext context);
    }

    public class ShapeCreatingContext
    {
        public IShapeFactory ShapeFactory { get; set; }
        public dynamic New { get; set; }
        public string ShapeType { get; set; }
        public Func<Task<IShape>> CreateAsync { get; set; }
        public IList<Func<ShapeCreatedContext, Task>> OnCreated { get; set; }
        public Func<IShape> Create
        {
            set => CreateAsync = () => Task.FromResult(value());
        }
    }

    public class ShapeCreatedContext
    {
        public IShapeFactory ShapeFactory { get; set; }
        public dynamic New { get; set; }
        public string ShapeType { get; set; }
        public IShape Shape { get; set; }
    }

    public abstract class ShapeFactoryEvents : IShapeFactoryEvents
    {
        public virtual void Creating(ShapeCreatingContext context) { }
        public virtual void Created(ShapeCreatedContext context) { }
    }
}
