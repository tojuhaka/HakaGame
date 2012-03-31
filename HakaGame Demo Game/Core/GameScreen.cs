using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using HakaGame;

namespace HakaGame
{
    // GameScreen handles the components on the screen
    public abstract class GameScreen
    {
        // GameScreen is in charge of managing components, so 
        // we need a set of components
        protected List<Component> components = new List<Component>();

        // GameScreen is in charge of managing controls also, so 
        // we need a set of components
        protected List<Control> controls = new List<Control>();

        protected ControlManager Controls = new ControlManager();

        // Set to internal so Engine can access it without allowing
        // other classes to set engine. Engine must be set through
        // the Engine's PushGameScreen() method so that the stack
        // can be maintained
        internal Engine engine = null;

        public Engine Engine
        {
            get { return engine; }
        }

        // Load content only once
        bool loaded = false;
        public bool Loaded { get { return loaded; } }


        public GameScreen()
        {
        }

        public void AddControl(Control control)
        {
            Controls.Add(control);
        }

        public void RemoveControl(string name)
        {
            Control c = GetControl(name);
            if (c != null)
            {
                controls.Remove(c);
            }
        }

        public void RemoveControl(Control control)
        {
                controls.Remove(control);
        }

        public Control GetControl(string name)
        {
            foreach (Control c in controls)
            {
                if (c.Name.Equals(name))
                {
                    return c;
                }
            }
            return null;
        }
        public void AddComponent(Component component)
        {
            if (!components.Contains(component))
            {
                components.Add(component);
                component.Parent = this;
                component.LoadComponent();
                PutComponentInOrder(component);
            }
        }

        // The components are stored in their draw order, so it is easy to loop
        // through them and draw them in the correct order without having to sort
        // them every time they are drawn
        public void PutComponentInOrder(Component component)
        {
            if (components.Contains(component))
            {
                components.Remove(component);

                int i = 0;
               
                // Iterate through the components in order until we find one with
                // a higher or equal draw order, and insert the component at that
                // position.
                for (i = 0; i < components.Count; i++)
                    //TODO Check this
                    if (component is DrawableComponent)
                    {
                        if (((DrawableComponent)components[i]).DrawOrder >= ((DrawableComponent)component).DrawOrder)
                            break;
                    }

                components.Insert(i, component);
            }
        }

        // Allow for components to be retrieved with the [] index, example:
        // Component c = gameScreenInstance["Component1"];
        public Component this[string compName]
        {
            get
            {
                foreach (Component component in components)
                {
                    if (component.Name == compName)
                        return component;
                }

                return null;
            }
        }



        public void RemoveComponent(string name)
        {
            Component c = this[name];
            RemoveComponent(c);
        }

        public void RemoveComponent(Component component)
        {
            if (component != null && components.Contains(component))
            {
                components.Remove(component);
                component.Parent = null;
            }
        }

        // Updates all the components on the screen
        public virtual void Update()
        {
            // Copy the list of components so the game won't crash if the original
            // is modified while updating
            List<Component> updating = new List<Component>();

            foreach (Component c in components)
                updating.Add(c);

            foreach (Component c in updating)
                if (Engine != null) // Somehow the engine stays null and we are still updating
                {
                    if (c.Enabled)
                    {
                        c.Update();
                        c.Update(this.Engine.GameTime);
                    }
                }
            if (Engine != null)
                Controls.Update(Engine.GameTime);
        }

        // Update the drawing of all the components
        public virtual void Draw()
        {
            Engine.SpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Deferred, SaveStateMode.None);

            foreach (Component c in components)
            {
                if (c is DrawableComponent && ((DrawableComponent)c).Visible)
                {
                    ((DrawableComponent)c).Draw();
                }
            }
            if (Engine != null)
                  Controls.Draw(Engine.SpriteBatch);
            Engine.SpriteBatch.End();
        }

        
        public void LoadGameScreen()
        {
            if (loaded)
                return;

            loaded = true;

            Load();
        }

        // Load content
        protected virtual void Load()
        {
           
        }


    }
}
