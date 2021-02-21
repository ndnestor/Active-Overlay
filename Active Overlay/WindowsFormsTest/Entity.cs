using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ActiveOverlayForm.Components;

namespace ActiveOverlayForm {
    public class Entity {
        //An identifier for the entity. Does not have to be unique
        public string name;
        //A list of components that are attached to this entity
        internal readonly List<Component> components = new List<Component>();

        public Entity(string name) {
            this.name = name;
        }

        public T AddComponent<T>(ref T component) where T : Component {
            components.Add(component);
            component.SetParentEntity(this);
            return component;
        }

        public T GetComponent<T>() where T : Component {
            foreach(Component currComponent in components) {
                if(currComponent is T) {
                    return (T)currComponent;
                }
            }
            return null;
        }

        public List<Component> GetComponents() {
            return components;
        }

        //ToString override
        public override string ToString() {
            return name;
        }
    }
}
