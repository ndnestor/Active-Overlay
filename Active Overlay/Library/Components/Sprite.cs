using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ActiveOverlayForm.Components {
    public class Sprite : Component {
        //The name given to this sprite
        public string name;
        //The sprite (does not include transformations)
        private Bitmap sprite;
        //The sprite that is rendered (includes transformations)
        private Bitmap spriteToRender;
        //Dicates the way that the sprite is to be rendered
        public SpriteMode spriteMode = SpriteMode.KeepAspectRatio;
        //The angle at which the sprite is rotated
        private float rotationAngle;
        //The top down order that this sprite falls in (higher number if further up) TODO
        //private uint zOrder;

        public enum SpriteMode {
            KeepAspectRatio,
            Stretch
        }

        //Constructors
        public Sprite() : base("Sprite") {
            name = "Sprite";
            AddToSpriteList();
        }
        public Sprite(Bitmap sprite) : base("Sprite") {
            name = "Sprite";
            SetSprite(sprite);
            AddToSpriteList();

        }

        //Sets the sprite property
        public void SetSprite(Bitmap newSprite) {
            sprite = newSprite;
        }

        //Gets the sprite property
        public Bitmap GetSprite() {
            return sprite;
        }

        //Sets the spriteToRender property based on the sprite mode
        internal void SetSpriteToRender() {
            lock(sprite) {
                Transform transform = parentEntity.GetComponent<Transform>();

                if(spriteMode == SpriteMode.KeepAspectRatio) {
                    double spriteAspectRatio = (double)sprite.Width / sprite.Height;
                    if(transform.scale.Width / transform.scale.Height <= spriteAspectRatio) {
                        spriteToRender = new Bitmap(sprite, new Size((int)(sprite.Width * transform.scale.Width), (int)(sprite.Height * transform.scale.Width)));
                    } else {
                        spriteToRender = new Bitmap(sprite, new Size((int)(sprite.Width * transform.scale.Height), (int)(sprite.Height * transform.scale.Height)));
                    }
                } else if(spriteMode == SpriteMode.Stretch) {
                    spriteToRender = new Bitmap(sprite, new Size((int)(sprite.Width * transform.scale.Width), (int)(sprite.Height * transform.scale.Height)));
                }
            }
        }

        //Gets the spriteToRender property
        internal Bitmap GetSpriteToRender() {
            return spriteToRender;
        }
        
        //Adds this sprite to the rendering queue
        private void AddToSpriteList() {
            ImageRenderer.sprites.Add(this);
        }

        //Rotate the sprite
        //NOTE: I got this method from the internet
        //NOTE: This method can most likely be optimized and it leads to very bad artifacts
        public void Rotate(float angle) {
            lock (sprite) {
                rotationAngle = angle;

                //create a new empty bitmap to hold rotated image
                Bitmap returnBitmap = new Bitmap(sprite.Width, sprite.Height);
                //make a graphics object from the empty bitmap
                Graphics g = Graphics.FromImage(returnBitmap);
                //move rotation point to center of image
                g.TranslateTransform((float)sprite.Width / 2, (float)sprite.Height / 2);
                //rotate
                g.RotateTransform(angle + rotationAngle);
                //move image back
                g.TranslateTransform(-(float)sprite.Width / 2, -(float)sprite.Height / 2);
                //draw passed in image onto graphics object
                g.DrawImage(sprite, new Point(0, 0));
                g.Dispose();
                spriteToRender = returnBitmap;
            }
        }

        //Returns rotationAngle
        public float GetRotationAngle() {
            return rotationAngle;
        }
    }
}
