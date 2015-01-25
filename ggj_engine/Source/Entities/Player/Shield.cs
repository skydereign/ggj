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
        public const float STRAND_SPACING = 1;
        public const float SHIELD_RADIUS = 16f;

        public float Health;
        public Vector2 Position;
        private List<ShieldNode> shieldNodes;
        private int flashDamage;

        public Shield()
        {
            Health = 100;
            shieldNodes = new List<ShieldNode>();
            float angleSeg;
            for (int i = 0; i < SEGMENTS; i++)
            {
                angleSeg = (float)(2 * Math.PI / ((float)SEGMENTS)) * (float)i;
                ShieldNode sn = new ShieldNode();
                sn.NeutralPosition = new Vector2((float)Math.Cos(angleSeg), (float)Math.Sin(angleSeg)) * SHIELD_RADIUS;
                sn.RelPosition = sn.NeutralPosition;
                shieldNodes.Add(sn);
            }
        }

        public void Damage(float amount)
        {
            Health -= amount;
            flashDamage = 3;
        }

        public void Update()
        {
            if (InputControl.GetKeyboardKeyPressed(Microsoft.Xna.Framework.Input.Keys.L))
            {
                shieldNodes[(int)RandomUtil.Next(SEGMENTS)].RelPosition = new Vector2(15, -15);
            }
            if (InputControl.GetKeyboardKeyPressed(Microsoft.Xna.Framework.Input.Keys.K))
            {
                Damage(5);
            }

            //Copy shieldnodes first to keep last frames state
            List<ShieldNode> newShieldList = new List<ShieldNode>();
            for (int i = 0; i < SEGMENTS;i++ )
            {
                ShieldNode s = new ShieldNode();
                s.NeutralPosition = shieldNodes[i].NeutralPosition;
                newShieldList.Add(s);
            }
            
            //Propagate changes across the shield
            /*for (int i = 0; i < shieldNodes.Count; i++)
            {
                float angleSeg;
                angleSeg = (float)(2 * Math.PI / ((float)SEGMENTS)) * (float)i;
                float arcLength = Vector2.Distance(new Vector2(SHIELD_RADIUS, 0), new Vector2((float)Math.Cos(angleSeg), (float)Math.Sin(angleSeg)) * SHIELD_RADIUS);

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

                forceNext *= 2f;
                forcePrev *= 2f;
                Vector2 forceAnchor = shieldNodes[i].RelPosition - shieldNodes[i].NeutralPosition;
                Vector2 totalForce = forcePrev / forcePrev.Length() * (forcePrev.Length() - arcLength) + 
                    forceNext / forceNext.Length() * (forceNext.Length() - arcLength) +
                    (forceAnchor / (forceAnchor.Length() + 0.00001f));

                shieldNodes[i].Velocity = shieldNodes[i].Velocity * 0.98f + totalForce * 0.01f;
                newShieldList[i].Velocity = shieldNodes[i].Velocity;
                newShieldList[i].RelPosition = shieldNodes[i].RelPosition + shieldNodes[i].Velocity;
            }
            shieldNodes = newShieldList;*/
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
                        Debug.DrawLine(spriteBatch, shieldNodes[i].RelPosition + Position, shieldNodes[i + 1].RelPosition + Position, shieldColor, shieldThick);
                    }
                    else
                    {
                        Debug.DrawLine(spriteBatch, shieldNodes[i].RelPosition + Position, shieldNodes[0].RelPosition + Position, shieldColor, shieldThick);
                    }
                }
            }
        }
        class ShieldNode
        {
            public Vector2 RelPosition;
            public Vector2 NeutralPosition;
            public Vector2 Velocity;
        }
    }



}
