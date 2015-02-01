using ggj_engine.Source.Entities.Projectiles;
using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Entities.Player
{
    public class Shield
    {
        public const int SEGMENTS = 20;
        public const float STRAND_THICKNESS = 1;
        public const float SHIELD_RADIUS = 16f;


        public Player MyPlayer;
        public float Health;
        public Vector2 Position;
        private List<ShieldNode> shieldNodes;
        private int flashDamage;
        private float arcLength;

        public Shield(Player myPlayer)
        {
            arcLength = Vector2.Distance(new Vector2(SHIELD_RADIUS, 0), new Vector2((float)Math.Cos(2 * Math.PI / ((float)SEGMENTS)), (float)Math.Sin(2 * Math.PI / ((float)SEGMENTS))) * SHIELD_RADIUS);

            Health = 100;
            shieldNodes = new List<ShieldNode>();
            float angleSeg;
            for (int i = 0; i < SEGMENTS; i++)
            {
                angleSeg = (float)(2 * Math.PI / ((float)SEGMENTS)) * (float)i;
                ShieldNode sn = new ShieldNode();
                sn.NeutralPosition = new Vector2((float)Math.Cos(angleSeg), (float)Math.Sin(angleSeg)) * SHIELD_RADIUS;
                sn.Anchor1Position = new Vector2((float)Math.Cos(angleSeg), (float)Math.Sin(angleSeg)) * (SHIELD_RADIUS + arcLength);
                sn.Anchor2Position = new Vector2((float)Math.Cos(angleSeg), (float)Math.Sin(angleSeg)) * (SHIELD_RADIUS - arcLength);
                sn.RelPosition = sn.NeutralPosition;
                shieldNodes.Add(sn);
            }
            MyPlayer = myPlayer;

        }

        public void Damage(Entity source, float amount)
        {
            if (!MyPlayer.Dead)
            {
                Health -= amount;
                flashDamage = 3;
                //Loop through shield and find nodes to push
                float attackAngle = (float)Math.Atan2((source.Position - Position).Y, (source.Position - Position).X);
                foreach(ShieldNode sn in shieldNodes)
                {
                    if (Math.Abs(Math.Atan2(sn.RelPosition.Y,sn.RelPosition.X) - attackAngle) < Math.PI * 0.05f)
                    {
                        sn.RelPosition = sn.RelPosition * 0.25f;
                    }
                }

                if (Health <= 0)
                {
                    //Self kill
                    if (source == null)
                    {
                        MyPlayer.MyScreen.GameManager.ScoreManager.GrantSelfKill(Position);
                    }
                    if (source is Projectile)
                    {
                        if (((Projectile)source).Owner is Enemies.Follower)
                        {
                            MyPlayer.MyScreen.GameManager.ScoreManager.GrantEnemyFollowerKill(Position);
                        }
                        if (((Projectile)source).Owner is Enemies.YourMom)
                        {
                            MyPlayer.MyScreen.GameManager.ScoreManager.GrantEnemyYourMomKill(Position);
                        }
                    }
                }
            }
        }

        public void ResetShield()
        {
            Health = 100;
            shieldNodes.Clear();
            float angleSeg;
            for (int i = 0; i < SEGMENTS; i++)
            {
                angleSeg = (float)(2 * Math.PI / ((float)SEGMENTS)) * (float)i;
                ShieldNode sn = new ShieldNode();
                sn.NeutralPosition = new Vector2((float)Math.Cos(angleSeg), (float)Math.Sin(angleSeg)) * SHIELD_RADIUS;
                sn.Anchor1Position = new Vector2((float)Math.Cos(angleSeg), (float)Math.Sin(angleSeg)) * (SHIELD_RADIUS + arcLength);
                sn.Anchor2Position = new Vector2((float)Math.Cos(angleSeg), (float)Math.Sin(angleSeg)) * (SHIELD_RADIUS - arcLength);
                sn.RelPosition = sn.NeutralPosition;
                shieldNodes.Add(sn);
            }
        }

        public void Update()
        {
            if (InputControl.GetKeyboardKeyPressed(Microsoft.Xna.Framework.Input.Keys.K))
            {
                Damage(MyPlayer, 5);
                shieldNodes[4].RelPosition = new Vector2(0, 0);
            }

            //Copy shieldnodes first to keep last frames state
            List<ShieldNode> newShieldList = new List<ShieldNode>();
            for (int i = 0; i < SEGMENTS;i++ )
            {
                ShieldNode s = new ShieldNode();
                s.NeutralPosition = shieldNodes[i].NeutralPosition;
                s.Anchor1Position = shieldNodes[i].Anchor1Position;
                s.Anchor2Position = shieldNodes[i].Anchor2Position;
                newShieldList.Add(s);
            }
            
            //Propagate changes across the shield
            for (int i = 0; i < shieldNodes.Count; i++)
            {
                Vector2 forcePrev, forceNext;
                //Edge case first
                if (i == 0)
                {
                    forcePrev = shieldNodes[SEGMENTS - 1].RelPosition - shieldNodes[i].RelPosition;
                    forceNext = shieldNodes[i + 1].RelPosition - shieldNodes[i].RelPosition;
                }
                //Edge case last
                else if (i == shieldNodes.Count - 1)
                {
                    forcePrev = shieldNodes[i - 1].RelPosition - shieldNodes[i].RelPosition;
                    forceNext = shieldNodes[0].RelPosition - shieldNodes[i].RelPosition;
                }
                //General case
                else
                {
                    forcePrev = shieldNodes[i - 1].RelPosition - shieldNodes[i].RelPosition;
                    forceNext = shieldNodes[i + 1].RelPosition - shieldNodes[i].RelPosition;
                }

                Vector2 forceAnchor1 = shieldNodes[i].Anchor1Position - shieldNodes[i].RelPosition;
                Vector2 forceAnchor2 = shieldNodes[i].Anchor2Position - shieldNodes[i].RelPosition;
                forcePrev = forcePrev / forcePrev.Length() * (forcePrev.Length() - arcLength);
                forceNext = forceNext / forceNext.Length() * (forceNext.Length() - arcLength);
                forceAnchor1 = forceAnchor1 / forceAnchor1.Length() * (forceAnchor1.Length() - arcLength);
                forceAnchor2 = forceAnchor2 / forceAnchor2.Length() * (forceAnchor2.Length() - arcLength);

                Vector2 totalForce = forcePrev + forceNext + forceAnchor1 + forceAnchor2;


                shieldNodes[i].Velocity = shieldNodes[i].Velocity * 0.96f + totalForce * 0.08f;
                newShieldList[i].Velocity = shieldNodes[i].Velocity;
                newShieldList[i].RelPosition = shieldNodes[i].RelPosition + shieldNodes[i].Velocity;
                newShieldList[i].TotalForce = totalForce;
            }
            shieldNodes = newShieldList;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Color shieldMaskColor = new Color(255,255,255);
            Color shieldColor = new Color(120, 220, 220);

            //Damage flash
            if (flashDamage > 0)
            {
                shieldColor = Color.Red;
                flashDamage--;
            }

            float shieldThick = Math.Max(1f,Health / 20f);

            //Main shield
            if (Health > 0)
            {
                for (int i = 0; i < SEGMENTS; i++)
                {
                    if (i != SEGMENTS - 1)
                    {
                        Debug.DrawLine(spriteBatch, shieldNodes[i].RelPosition + Position, shieldNodes[i + 1].RelPosition + Position, shieldColor, shieldThick * STRAND_THICKNESS);
                    }
                    else
                    {
                        Debug.DrawLine(spriteBatch, shieldNodes[i].RelPosition + Position, shieldNodes[0].RelPosition + Position, shieldColor, shieldThick * STRAND_THICKNESS);
                    }
                    Debug.DrawCircle(spriteBatch, shieldNodes[i].RelPosition + Position, shieldColor, shieldThick * STRAND_THICKNESS * 0.5f);
                    //Forces on shield
                    //Debug.DrawLine(spriteBatch, shieldNodes[i].RelPosition + Position, shieldNodes[i].RelPosition + Position + shieldNodes[i].TotalForce, Color.Red, 1);
                }
            }
        }
        class ShieldNode
        {
            public Vector2 RelPosition;
            public Vector2 NeutralPosition;
            public Vector2 Anchor1Position;
            public Vector2 Anchor2Position;
            public Vector2 Velocity;
            public Vector2 TotalForce;
        }
    }



}
