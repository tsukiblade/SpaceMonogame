using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.UI
{
    public abstract class WindowComponent
    {   
        public WindowComponent()
        {
        }

        public abstract void Operation(); //some shared operation

        public abstract void Render();

        public abstract void Position(Vector2 vector);

        public virtual bool IsComposite()
        {
            return true;
        }

        public virtual void Add(WindowComponent component)
        {
            throw new NotImplementedException();
        }

        public virtual void Remove(WindowComponent component)
        {
            throw new NotImplementedException();
        }
    }

    public class WindowComposite : WindowComponent
    {
        protected List<WindowComponent> _children = new List<WindowComponent>();

        public override void Add(WindowComponent component)
        {
            _children.Add(component);
        }

        public override void Remove(WindowComponent component)
        {
            _children.Remove(component);
        }

        public override void Operation()
        {
            throw new NotImplementedException();
        }

        public override void Render()
        {
            throw new NotImplementedException();
        }

        public override void Position(Vector2 vector)
        {
            throw new NotImplementedException();
        }
    }
}
