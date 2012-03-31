using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;

namespace HakaGame
{
    // A base class for all the components used, every
    // Component inherits this class
    public class Component
    {

        // Keep track of the number of each type of component that have been
        // created, so we can generate a unique name for each component
        static Dictionary<Type, int> componentTypeCounts = new Dictionary<Type, int>();

        // GameScreen where the component has been added.
        // Make sure we wont cause infinte loop or anything
        // else screwing the parent or component
        GameScreen parent;

        // Load only once
        bool loaded = false;
        public bool Loaded { get { return loaded; } }

        public GameScreen Parent
        {
            get { return parent; }
            set
            {
                if (parent == value)
                    return;

                if (parent != null)
                    parent.RemoveComponent(this);

                parent = value;
                if (value != null)
                    parent.AddComponent(this);
            }
        }

        // Unique name for the component
        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception("Component must not be null " +
                        "and be greater than 0 characters");
                else
                    name = value;
            }
        }

        // Sets the order where we want to draw the object.
        // Logically higher numbers will be drawn later
        int drawOrder = 0;

        public int DrawOrder
        {
            get { return drawOrder; }
            set
            {
                this.drawOrder = value;

                if (Parent != null)
                    Parent.PutComponentInOrder(this);
            }
        }

        // Generate a unique name for the component simply using the type name
        // and the number of that type have been created
        private void generateUniqueName()
        {
            Type t = this.GetType();

            if (!componentTypeCounts.ContainsKey(t))
                componentTypeCounts.Add(t, 0);

            componentTypeCounts[t]++;
            this.name = t.Name + componentTypeCounts[t];
        }

        // Load the component only once
        public void LoadComponent()
        {
            if (!loaded)
                Load();

            loaded = true;
        }

        public Component()
        {
            generateUniqueName();
        }


        public virtual void Update()
        {
            // Used for overriding
        }

        protected virtual void Load()
        {
            //load assets like sounds, textures, fonts etc
        }

    }
}
