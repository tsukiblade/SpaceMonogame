using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders.Entity
{
    public abstract class Entity
    {
        protected Texture2D _image;
        protected EntityState _state;

        public Entity()
        {
            TransitionToState(new DefaultEntityState());
        }

        public Color Color { get; set; } = Color.White;

        public Texture2D Image
        {
            get => _image;
            protected set => _image = value;
        }

        public Vector2 Position { get; set; }

        public Vector2 Velocity { get; set; }

        public Vector2 Size => _image == null ? Vector2.Zero : new Vector2(_image.Width, _image.Height);

        public float Orientation { get; set; }

        public float Radius { get; protected set; }

        public bool IsExpired { get; set; } //true if entity was destroyed and should be deleted

        public void TransitionToState(EntityState state)
        {
            _state = state;
            Console.WriteLine($"Context: Transition to {state.GetType().Name}.");
            _state.SetEntityContext(this);
        }

        public void HandleCollision(Entity other)
        {
            _state.HandleCollision(other);
        }

        public abstract void Update();

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            _state.Draw(spriteBatch);
        }

        public virtual void OnCollision(Entity other)
        {
        }
    }
}