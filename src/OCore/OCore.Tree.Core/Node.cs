using System;

namespace OCore.Tree
{
    public class Node
    {
        public Node(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
        }

        public Node(string name, string description) : this(name)
        {
            Description = description;
        }

        public Node(string name, string description, Node parent) : this(name, description)
        {
            Parent = parent;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public Node Parent { get; set; }

        public string Path
        {
            get
            {
                if (Parent == null)
                {
                    return string.Empty;
                }
                return Parent.FullName;
            }
        }

        public string FullName
        {
            get
            {
                if (Parent == null)
                {
                    return Name;
                }
                return $"{Parent.FullName}/{Name}";
            }

        }
        public int Depth
        {
            get
            {
                if (Parent == null)
                {
                    return 1;
                }
                return Parent.Depth + 1;
            }
        }

    }
}
