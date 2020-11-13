﻿using Terraria;

namespace API.Terraria.EntityComponents
{
    public abstract class EntityComponent
    {
        protected Entity Entity { get; private set; }

        public void Activate(Entity entity)
        {
            Entity = entity;
            OnActivate();
        }


        protected virtual void OnActivate()
        {
        }

        public virtual void OnDeactivate()
        {
        }

        public virtual void UpdateComponent()
        {
        }
    }
}