﻿using ggj_engine.Source.Entities;
using ggj_engine.Source.Utility;
using ggj_engine.Source.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Screens
{
    public class Screen
    {
        public Camera Camera;

        protected static Game1 game;
        public static Game1 Game { set { game = value; } }

        protected List<Entity> entities = new List<Entity>();
        protected List<Entity> createdEntities = new List<Entity>();
        protected List<Entity> deletedEntities = new List<Entity>();

        public void AddEntity(Entity entity)
        {
            createdEntities.Add(entity);
        }

        public void DeleteEntity(Entity entity)
        {
            deletedEntities.Add(entity);
        }

        public void CleanupEntities()
        {
            foreach(Entity e in createdEntities)
            {
                e.Active = true;
                entities.Add(e);
            }
            createdEntities.Clear();


            foreach (Entity e in deletedEntities)
            {
                entities.Remove(e);
                e.Destroy();
            }
            deletedEntities.Clear();
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach(Entity e in entities)
            {
                e.Update(gameTime);
            }

            HandleCollisions();

            CleanupEntities();
        }
        public void SpriteBatchCameraBegin(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp,
                    DepthStencilState.Default, RasterizerState.CullCounterClockwise, null, Camera.GetViewMatrix());
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //Draw all entities
            SpriteBatchCameraBegin(spriteBatch);
            foreach (Entity e in entities)
            {
                e.Draw(spriteBatch);
            }
            spriteBatch.End();

            if (Globals.DebugEntities)
            {
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp,
                    DepthStencilState.Default, RasterizerState.CullCounterClockwise, null, Camera.GetViewMatrix());
                foreach (Entity e in entities)
                {
                    e.DrawDebug(spriteBatch);
                }
                spriteBatch.End();
            }
        }

        private void HandleCollisions ()
        {
            foreach(Entity entityA in entities)
            {
                foreach(Entity entityB in entities)
                {
                    if(entityA != entityB)
                    {
                        if(entityA.Colliding(entityB))
                        {
                            entityA.OnCollision(entityB);
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Used to clean up a scene when it is closed
        /// Might want to call destroy on all entities
        /// </summary>
        public virtual void Close()
        {
            //
        }
    }
}
