using ActiveOverlayForm;
using ActiveOverlayForm.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TechDemo {
    class DesktopOverlay {
        private static List<Entity> snakeSegments;
        private static Entity snakeHead;
        private static Transform snakeHeadTransform;
        private static Sprite snakeHeadSprite;
        private static Entity reward;
        private static Transform rewardTransform;
        private static Sprite rewardSprite;

        private static Bitmap snakeBitmap;
        private static Bitmap rewardBitmap;

        private static bool leftKeyDown;
        private static bool rightKeyDown;
        private static bool downKeyDown;
        private static bool upKeyDown;

        private static int segmentSize;
        private static Random random;

        public static void Start() {
            snakeSegments = new List<Entity>();

            snakeHead = new Entity("Snake Head");
            snakeHeadTransform = new Transform(random.Next(segmentSize / 2, 1920 - segmentSize / 2), random.Next(segmentSize / 2, 1080 - segmentSize / 2));
            snakeHeadSprite = new Sprite(snakeBitmap);

            snakeHead.AddComponent(ref snakeHeadTransform);
            snakeHead.AddComponent(ref snakeHeadSprite);

            reward = new Entity("Reward");
            rewardTransform = new Transform(random.Next(segmentSize / 2, 1920 - segmentSize / 2), random.Next(segmentSize / 2, 1080 - segmentSize / 2));
            rewardSprite = new Sprite(rewardBitmap);

            reward.AddComponent(ref rewardTransform);
            reward.AddComponent(ref rewardSprite);
        }

        public static void FixedUpdate() {
            if(Keyboard.IsKeyDown(Key.W) || Keyboard.IsKeyDown(Key.Up)) {
                upKeyDown = true;
                leftKeyDown = false;
                rightKeyDown = false;
                downKeyDown = false;
            } else if(Keyboard.IsKeyDown(Key.A) || Keyboard.IsKeyDown(Key.Left)) {
                upKeyDown = false;
                leftKeyDown = true;
                rightKeyDown = false;
                downKeyDown = false;
            } else if(Keyboard.IsKeyDown(Key.S) || Keyboard.IsKeyDown(Key.Down)) {
                upKeyDown = false;
                leftKeyDown = false;
                rightKeyDown = false;
                downKeyDown = true;
            } else if(Keyboard.IsKeyDown(Key.D) || Keyboard.IsKeyDown(Key.Right)) {
                upKeyDown = false;
                leftKeyDown = false;
                rightKeyDown = true;
                downKeyDown = false;
            }

            if(upKeyDown) {
                Move(0, segmentSize);
            } else if(downKeyDown) {
                Move(0, -segmentSize);
            } else if(rightKeyDown) {
                Move(segmentSize, 0);
            } else if(leftKeyDown) {
                Move(-segmentSize, 0);
            }

            CheckForCollision();
        }

        private static void Move(int x, int y) {
            snakeHead.GetComponent<Transform>().Move(x, y, Transform.MoveMode.Translate);
            snakeHead.GetComponent<Transform>().Move(x, y, Transform.MoveMode.Translate);

            for(int i = snakeSegments.Count() - 1; i > 0; i--) {
                snakeSegments[i].GetComponent<Transform>().Move(snakeSegments[i - 1].GetComponent<Transform>().Position, Transform.MoveMode.Teleport);
            }
        }

        private static void CheckForCollision() {

        }

        public static void Restart() {

        }

        public static void Stop() {

        }
    }
}
