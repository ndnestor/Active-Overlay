using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveOverlayForm {
    public class Component {
        //The type of component that this is
        public readonly string type;
        //The entity that this component is attached to
        public Entity parentEntity;

        //Constructor
        public Component(string type) {
            this.type = type;
        }

        //ToString override
        public override string ToString() {
            return type;
        }

        //Sets parentEntity
        public void SetParentEntity(Entity parent) {
            parentEntity = parent;
        }
    }
}
