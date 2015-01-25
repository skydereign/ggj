using ggj_engine.Source.Screens;
using ggj_engine.Source.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.GameManagement
{
   public class ScoreManager
    {
       public Screen MyScreen;

       public enum GameGoals { Kill, Touch };
       public enum KillGoals { EnemyFollower, Player, Self };
       public enum TouchGoals { EnemyFollower, Player };

       public GameGoals GameGoal;
       public KillGoals KillGoal;
       public TouchGoals TouchGoal;

       private int scoreKillEnemyFollower;
       private int scoreKillPlayer;
       private int scoreKillSelf;
       private int scoreDiebyEnemyFollower;
       private int scoreDiebyPlayer;
       private int scoreTouchEnemyFollower;
       private int scoreTouchPlayer;

       public ScoreManager()
       {
           ChangeGameGoals();
       }

       public void ChangeGameGoals()
       {
           //Pick a new goal
           GameGoal = (GameGoals)(RandomUtil.Next(Enum.GetNames(typeof(GameGoals)).Length + 1));
           switch(GameGoal)
           {
               case GameGoals.Kill:
                   KillGoal = (KillGoals)(RandomUtil.Next(Enum.GetNames(typeof(KillGoals)).Length + 1));
                   break;
               case GameGoals.Touch:
                   TouchGoal = (TouchGoals)(RandomUtil.Next(Enum.GetNames(typeof(TouchGoals)).Length + 1));
                   break;
               default:
                   throw new IndexOutOfRangeException("GameGoals invalid");
                   break;
           }

           //Set score values for each goal state
           scoreKillEnemyFollower = 0;
           scoreKillPlayer = 0;
           scoreKillSelf = 0;
           scoreDiebyEnemyFollower = 0;
           scoreDiebyPlayer = 0;
           scoreTouchEnemyFollower = 0;
           scoreTouchPlayer = 0;
           switch (GameGoal)
           {
               case GameGoals.Kill:
                   switch (KillGoal)
                   {
                       case KillGoals.EnemyFollower:
                           scoreKillEnemyFollower = 50;
                           scoreKillPlayer = -200;
                           scoreKillSelf = 0;
                           scoreDiebyEnemyFollower = 0;
                           scoreDiebyPlayer = -100;
                           break;
                       case KillGoals.Player:
                           scoreKillEnemyFollower = 0;
                           scoreKillPlayer = 300;
                           scoreKillSelf = 0;
                           scoreDiebyEnemyFollower = 0;
                           scoreDiebyPlayer = -100;
                           break;
                       case KillGoals.Self:
                           scoreKillEnemyFollower = 0;
                           scoreKillPlayer = 0;
                           scoreKillSelf = 600;
                           scoreDiebyEnemyFollower = 100;
                           scoreDiebyPlayer = 200;
                           break;
                       default:
                           throw new IndexOutOfRangeException("Killgoals invalid");
                           break;
                   }
                   break;
               case GameGoals.Touch:
                   switch (TouchGoal)
                   {
                       case TouchGoals.EnemyFollower:
                           scoreKillEnemyFollower = 0;
                           scoreKillPlayer = 0;
                           scoreKillSelf = 0;
                           scoreDiebyEnemyFollower = 0;
                           scoreDiebyPlayer = 0;
                           scoreTouchEnemyFollower = 0;
                           scoreTouchPlayer = 1000;
                           break;
                       case TouchGoals.Player:
                           scoreKillEnemyFollower = 0;
                           scoreKillPlayer = 0;
                           scoreKillSelf = 0;
                           scoreDiebyEnemyFollower = 0;
                           scoreDiebyPlayer = 0;
                           scoreTouchEnemyFollower = 100;
                           scoreTouchPlayer = 0;
                           break;
                       default:
                           throw new IndexOutOfRangeException("TouchGoals invalid");
                           break;
                   }
                   break;
           }
           
       }

       public void GrantEnemyFollowerKill()
       {
           MyScreen.GameManager.AddToScore(scoreKillEnemyFollower);
       }
       public void GrantPlayerKill()
       {
           MyScreen.GameManager.AddToScore(scoreKillPlayer);
       }
       public void GrantSelfKill()
       {
           MyScreen.GameManager.AddToScore(scoreKillSelf);
       }
       public void GrantPlayerTouch()
       {
           MyScreen.GameManager.AddToScore(scoreTouchPlayer);
       }
       public void GrantEnemyFollowerTouch()
       {
           MyScreen.GameManager.AddToScore(scoreTouchEnemyFollower);
       }
       public void GrantPlayerDieby()
       {
           MyScreen.GameManager.AddToScore(scoreDiebyPlayer);
       }
       public void GrantEnemyFollowerDieby()
       {
           MyScreen.GameManager.AddToScore(scoreDiebyEnemyFollower);
       }
    }
}
