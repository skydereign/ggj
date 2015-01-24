using ggj_engine.Source.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Screens
{
    public abstract class Screen
    {
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

        public void HandleEntities()
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
            HandleEntities();
        }

        public abstract void Draw(SpriteBatch spriteBatch);


        /// <summary>
        /// Used to clean up a scene when it is closed
        /// Might want to call destroy on all entities
        /// </summary>
        public abstract void Close();
    }
}
