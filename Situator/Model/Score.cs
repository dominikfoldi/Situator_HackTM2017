namespace Situator.Model
{
    public class Score
    {
        public int Id { get; set; }

        public int Point { get; set; }

        public int SkillId { get; set; }
        public virtual Skill Skill { get; set; }

        public int NodeId { get; set; }
        public virtual Node Node { get; set; }
    }
}