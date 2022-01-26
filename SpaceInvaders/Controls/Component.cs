using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Core;
using SpaceInvaders.States;

namespace SpaceInvaders.Controls
{
    public abstract class Component
    {
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);

        public virtual void UpdateText(string text)
        {

        }

        public virtual void Add(Component component)
        {
            throw new NotImplementedException();
        }

        public virtual void Remove(Component component)
        {
            throw new NotImplementedException();
        }
    }

    public class ComponentComposite : Component
    {
        public List<Component> _children = new List<Component>();

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var component in _children) component.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _children) component.Update(gameTime);
        }

        public override void Add(Component component)
        {
            _children.Add(component);
        }

        public override void Remove(Component component)
        {
            _children.Remove(component);
        }

        public void UpdateSafeAlignedStrings(string text)
        {
            _children.OfType<TitleSafeAlignedString>().ToList();
            foreach (var component in _children)
            {
                if (!(component is TitleSafeAlignedString))
                    continue;
                component.UpdateText(text);
            }
        }

        public void UpdateRightAlignedStrings(string text)
        {
            _children.OfType<TitleSafeRightAlignedString>().ToList();
            foreach (var component in _children)
            {
                if (!(component is TitleSafeRightAlignedString))
                    continue;
                component.UpdateText(text);
            }
        }
    }

    public class RightAlignedString : Component
    {
        private string _text;
        private float _y;
        public RightAlignedString(string text, float y)
        {
            _text = text;
            _y = y;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var textWidth = Art.Font.MeasureString(_text).X;
            spriteBatch.DrawString(Art.Font, _text, new Vector2(GameState.ScreenSize.X - textWidth - 5, _y), Color.White);
        }

        public override void Update(GameTime gameTime)
        {
        }
        public override void UpdateText(string text)
        {
            _text = text;
        }
    }

    public class TitleSafeAlignedString : Component
    {
        private string _text;
        private float _y;
        public TitleSafeAlignedString(string text, float y)
        {
            _text = text;
            _y = y;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Art.Font, _text, new Vector2(GameState.Viewport.TitleSafeArea.X + _y), Color.White);
        }

        public override void Update(GameTime gameTime)
        {
        }
        public override void UpdateText(string text)
        {
            _text = text;
        }
    }

    public class TitleSafeRightAlignedString : Component
    {
        private string _text;
        private float _y;
        public TitleSafeRightAlignedString(string text, float y)
        {
            _text = text;
            _y = y;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var textWidth = Art.Font.MeasureString(_text).X;
            spriteBatch.DrawString(Art.Font, _text, new Vector2(GameState.ScreenSize.X - textWidth - 5 - GameState.Viewport.TitleSafeArea.X, GameState.Viewport.TitleSafeArea.Y + _y), Color.White);
        }

        public override void UpdateText(string text)
        {
            _text = text;
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}