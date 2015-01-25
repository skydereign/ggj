using ggj_engine.Source.Screens;
using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework;
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
       public enum KillGoals { EnemyFollower, EnemyYourMom, Player, Self };
       public enum TouchGoals { EnemyFollower, EnemyYourMom, Player };

       public GameGoals GameGoal;
       public KillGoals KillGoal;
       public TouchGoals TouchGoal;

       private int scoreKillEnemyFollower;
       private int scoreKillEnemyYourMom;
       private int scoreKillPlayer;
       private int scoreKillSelf;
       private int scoreDiebyEnemyFollower;
       private int scoreDiebyEnemyYourMom;
       private int scoreDiebyPlayer;
       private int scoreTouchEnemyFollower;
       private int scoreTouchEnemyYourMom;
       private int scoreTouchPlayer;

       public ScoreManager()
       {
           ChangeGameGoals();
       }

       public void ChangeGameGoals()
       {
           //Pick a new goal
           GameGoal = (GameGoals)(RandomUtil.Next(Enum.GetNames(typeof(GameGoals)).Length));
           switch(GameGoal)
           {
               case GameGoals.Kill:
                   KillGoal = (KillGoals)(RandomUtil.Next(Enum.GetNames(typeof(KillGoals)).Length));
                   break;
               case GameGoals.Touch:
                   TouchGoal = (TouchGoals)(RandomUtil.Next(Enum.GetNames(typeof(TouchGoals)).Length));
                   break;
               default:
                   throw new IndexOutOfRangeException("GameGoals invalid");
                   break;
           }

           //Set score values for each goal state
           scoreKillEnemyFollower = 0;
           scoreKillEnemyYourMom = 0;
           scoreKillPlayer = 0;
           scoreKillSelf = 0;
           scoreDiebyEnemyFollower = 0;
           scoreDiebyEnemyYourMom = 0;
           scoreDiebyPlayer = 0;
           scoreTouchEnemyFollower = 0;
           scoreTouchEnemyYourMom = 0;
           scoreTouchPlayer = 0;
           switch (GameGoal)
           {
               case GameGoals.Kill:
                   switch (KillGoal)
                   {
                       case KillGoals.EnemyFollower:
                           scoreKillEnemyFollower = 50;
                           scoreKillEnemyYourMom = -75;
                           scoreKillPlayer = -200;
                           scoreDiebyPlayer = -100;
                           break;
                       case KillGoals.EnemyYourMom:
                           scoreKillEnemyFollower = -50;
                           scoreKillEnemyYourMom = 75;
                           scoreKillPlayer = -200;
                           scoreDiebyPlayer = -100;
                           break;
                       case KillGoals.Player:
                           scoreKillPlayer = 300;
                           scoreDiebyPlayer = -100;
                           break;
                       case KillGoals.Self:
                           scoreKillSelf = 600;
                           scoreDiebyEnemyFollower = 100;
                           scoreDiebyEnemyYourMom = 100;
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
                           scoreTouchEnemyFollower = 100;
                           scoreTouchEnemyYourMom = -25;
                           break;
                       case TouchGoals.EnemyYourMom:
                           scoreTouchEnemyFollower = -25;
                           scoreTouchEnemyYourMom = 200;
                           break;
                       case TouchGoals.Player:
                           scoreTouchEnemyFollower = -25;
                           scoreTouchEnemyYourMom = -25;
                           scoreTouchPlayer = 1000;
                           break;
                       default:
                           throw new IndexOutOfRangeException("TouchGoals invalid");
                           break;
                   }
                   break;
           }
           Console.WriteLine("Main: " + GameGoal.ToString() + " | Kill: " + KillGoal.ToString() + " | Touch: " + TouchGoal.ToString());
       }

       public void GrantEnemyFollowerKill(Vector2 sourcePos)
       {
           MyScreen.GameManager.AddToScore(sourcePos, scoreKillEnemyFollower);
       }
       public void GrantEnemyYourMomrKill(Vector2 sourcePos)
       {
           MyScreen.GameManager.AddToScore(sourcePos, scoreKillEnemyYourMom);
       }
       public void GrantPlayerKill(Vector2 sourcePos)
       {
           MyScreen.GameManager.AddToScore(sourcePos, scoreKillPlayer);
       }
       public void GrantSelfKill(Vector2 sourcePos)
       {
           MyScreen.GameManager.AddToScore(sourcePos, scoreKillSelf);
       }
       public void GrantPlayerTouch(Vector2 sourcePos)
       {
           MyScreen.GameManager.AddToScore(sourcePos, scoreTouchPlayer);
       }
       public void GrantEnemyFollowerTouch(Vector2 sourcePos)
       {
           MyScreen.GameManager.AddToScore(sourcePos, scoreTouchEnemyFollower);
       }
       public void GrantPlayerDieby(Vector2 sourcePos)
       {
           MyScreen.GameManager.AddToScore(sourcePos, scoreDiebyPlayer);
       }
       public void GrantEnemyFollowerDieby(Vector2 sourcePos)
       {
           MyScreen.GameManager.AddToScore(sourcePos, scoreDiebyEnemyFollower);
       }
    }
}
