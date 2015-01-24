using ggj_engine.Source.AI.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.AI.DecisionTree
{
    public interface IBinaryNode
    {
        IAction MakeDecision();
    }
}
