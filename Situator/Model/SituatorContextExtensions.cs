using Situator.Model;
using Situator.Models;
using System.Collections.Generic;

namespace Situator.Data
{
    public static class SituatorContextExtensions
    {
        public static void EnsureSeedData(this SituatorContext context)
        {
            var empathy = context.Skills.Add(new Skill { Name = "Empathy" }).Entity;
            var reactivity = context.Skills.Add(new Skill { Name = "Reactivity" }).Entity;
            var aggressivity = context.Skills.Add(new Skill { Name = "Aggressivity" }).Entity;
            var leadership = context.Skills.Add(new Skill { Name = "Leadership" }).Entity;
            var energetic = context.Skills.Add(new Skill { Name = "Energetic" }).Entity;

            var course = new Course { Category = "Education", Description = "...", Title = "This is a Course" };

            var rootNode = new Node
            {
                IsRoot = true,
                Text = "Team building course",
                VideoUrl = "/",
                Course = course,
                Scores = new List<Score> {
                    new Score { Skill = empathy, Point = 0 },
                    new Score { Skill = reactivity, Point = 0 },
                    new Score { Skill = aggressivity, Point = 0 },
                    new Score { Skill = leadership, Point = 0 },
                    new Score { Skill = energetic, Point = 0 }
                },
                PositionX = 350,
                PositionY = 50
            };

            var NodeA = context.Nodes.Add(new Node
            {
                IsRoot = false,
                IsLeaf = true,
                Text = "Choice: A",
                VideoUrl = "/",
                Course = course,
                Scores = new List<Score> {
                    new Score { Skill = empathy, Point = 3 },
                    new Score { Skill = reactivity, Point = 6 },
                    new Score { Skill = aggressivity, Point = 4 },
                    new Score { Skill = leadership, Point = 2 },
                    new Score { Skill = energetic, Point = 1 }
                },
                PositionX = 200,
                PositionY = 200

            }).Entity;

            var NodeB = context.Nodes.Add(new Node
            {
                IsRoot = false,
                IsLeaf = true,
                Text = "Choice: B",
                VideoUrl = "/",
                Course = course,
                Scores = new List<Score> {
                    new Score { Skill = empathy, Point = 4 },
                    new Score { Skill = reactivity, Point = 7 },
                    new Score { Skill = aggressivity, Point = 4 },
                    new Score { Skill = leadership, Point = 2 },
                    new Score { Skill = energetic, Point = 8 }
                },
                PositionX = 350,
                PositionY = 200
            }).Entity;

            var NodeC = context.Nodes.Add(new Node
            {
                IsRoot = false,
                IsLeaf = true,
                Text = "Choice C",
                VideoUrl = "/",
                Course = course,
                Scores = new List<Score> {
                    new Score { Skill = empathy, Point = 0 },
                    new Score { Skill = reactivity, Point = 5 },
                    new Score { Skill = aggressivity, Point = 5 },
                    new Score { Skill = leadership, Point = 4 },
                    new Score { Skill = energetic, Point = 1 }
                },
                   PositionX = 500,
                PositionY = 200
            }).Entity;

            var NodeAA = context.Nodes.Add(new Node
            {
                IsRoot = false,
                IsLeaf = true,
                Text = "Choice AA",
                VideoUrl = "/",
                Course = course,
                Scores = new List<Score> {
                    new Score { Skill = empathy, Point = 1 },
                    new Score { Skill = reactivity, Point = 4 },
                    new Score { Skill = aggressivity, Point = 2 },
                    new Score { Skill = leadership, Point = 2 },
                    new Score { Skill = energetic, Point = 1 }
                },
                PositionX = 200,
                PositionY = 400
            }).Entity;

            var NodeAB = context.Nodes.Add(new Node
            {
                IsRoot = false,
                IsLeaf = true,
                Text = "Choice AB",
                VideoUrl = "/",
                Course = course,
                Scores = new List<Score> {
                    new Score { Skill = empathy, Point = 8 },
                    new Score { Skill = reactivity, Point = 0 },
                    new Score { Skill = aggressivity, Point = 0 },
                    new Score { Skill = leadership, Point = 1 },
                    new Score { Skill = energetic, Point = 2 }
                },
                PositionX = 350,
                PositionY = 400
            }).Entity;

            var NodeAC = context.Nodes.Add(new Node
            {
                IsRoot = false,
                IsLeaf = true,
                Text = "Choice AC",
                VideoUrl = "/",
                Course = course,
                Scores = new List<Score> {
                    new Score { Skill = empathy, Point = 0 },
                    new Score { Skill = reactivity, Point = 1 },
                    new Score { Skill = aggressivity, Point = 0 },
                    new Score { Skill = leadership, Point = 2 },
                    new Score { Skill = energetic, Point = 8 }
                },
                PositionX = 500,
                PositionY = 400
            }).Entity;

            var relations = new NodeRelation[]
            {
                new NodeRelation { Parent = rootNode, Child = NodeA },
                new NodeRelation { Parent = rootNode, Child = NodeB },
                new NodeRelation { Parent = rootNode, Child = NodeC },
                new NodeRelation { Parent = NodeA, Child = NodeAA },
                new NodeRelation { Parent = NodeA, Child = NodeAB },
                new NodeRelation { Parent = NodeA, Child = NodeAC }
            };
            foreach (var rel in relations)
                context.NodeRelations.Add(rel);

            course.Nodes.Add(rootNode);
            course.Nodes.Add(NodeA);
            course.Nodes.Add(NodeB);
            course.Nodes.Add(NodeC);
            course.Nodes.Add(NodeAA);
            course.Nodes.Add(NodeAB);
            course.Nodes.Add(NodeAC);

            context.SaveChanges();
        }
    }
}