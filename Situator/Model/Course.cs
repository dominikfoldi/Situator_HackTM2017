using System.Collections.Generic;

namespace Situator.Model
{
    public class Course
    {
        public int Id { get; set; }

        //public virtual Node RootNode { get; set;}

        public string Title { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Node> Nodes { get; set; }
    }
}