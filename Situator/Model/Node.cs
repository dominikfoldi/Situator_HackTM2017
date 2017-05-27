using System.Collections.Generic;

namespace Situator.Model
{
    public class Node
    {
        public int Id { get; set; }

        public bool IsRoot { get; set; }

        public bool IsLeaf { get; set; }

        public string Text { get; set; }

        public string VideoUrl { get; set; }

        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        public virtual ICollection<Score> Scores { get; set; }
        public virtual ICollection<NodeRelation> Parents { get; set; }
        public virtual ICollection<NodeRelation> Children { get; set; }

        //TODO: Canvas coorindates X,Y
    }
}