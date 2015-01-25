using ggj_engine.Source.AI.DecisionTree;
using ggj_engine.Source.Level;
using ggj_engine.Source.Media;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Entities.Enemies
{
    class YourMom : Enemy
    {
        private BinaryDecision playerInSightRange;
        private BinaryDecision playerInCombatRange;
        private float sightRange, combatRange;

        public YourMom(Vector2 position)
        {
            Position = position;
        }

        public override void Init()
        {
            PerformingAction = false;
            Speed = 1.5f;
            sprite = ContentLibrary.Sprites["test_animation"];
            sprite.Tint = Color.SaddleBrown;
            sightRange = 12 * 16;
            combatRange = 6 * 16;
            playerInSightRange = new BinaryDecision();
            playerInCombatRange = new BinaryDecision();
            CurrentPath = new Stack<Tile>();
            wayPoints = new List<Vector2>();
            LoadWayPoints();
            SetDecisionTree();
            base.Init();
        }

        private void LoadWayPoints()
        {
            throw new NotImplementedException();
        }
    }
}
