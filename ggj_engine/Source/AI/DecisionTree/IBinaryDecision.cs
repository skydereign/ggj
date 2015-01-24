using ggj_engine.Source.AI.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.AI.DecisionTree
{
    public interface IBinaryDecision
    {
        void SetTrueBranch(IBinaryNode node);
        void SetFalseBranch(IBinaryNode node);
        IBinaryNode GetTrueBranch();
        IBinaryNode GetFalseBranch();
        void SetCondition(ICondition condition);
    }
}
