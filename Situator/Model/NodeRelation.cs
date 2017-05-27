namespace Situator.Model
{
    public class NodeRelation
    {
        public int ParentId { get; set; }
        public int ChildId { get; set; }
        public virtual Node Parent { get; set; }
        public virtual Node Child { get; set; }
    }
}