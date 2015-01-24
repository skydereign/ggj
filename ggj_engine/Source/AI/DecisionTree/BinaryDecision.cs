using ggj_engine.Source.AI.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.AI.DecisionTree
{
    class BinaryDecision : IBinaryDecision, IBinaryNode
    {
        ICondition condition;
        IBinaryNode trueBranch;
        IBinaryNode falseBranch;

        public Actions.IAction MakeDecision()
        {
            if(condition.Test())
            {
                return trueBranch.MakeDecision();
            }
            else
            {
                return falseBranch.MakeDecision();
            }
        }

        public void SetTrueBranch(IBinaryNode node)
        {
            trueBranch = node;
        }

        public void SetFalseBranch(IBinaryNode node)
        {
            falseBranch = node;
        }

        public IBinaryNode GetTrueBranch()
        {
            return trueBranch;
        }

        public IBinaryNode GetFalseBranch()
        {
            return falseBranch;
        }

        public void SetCondition(Conditions.ICondition condition)
        {
            this.condition = condition;
        }
    }
}
