using GameEngine;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Drawing;
using System.Numerics;
using System.Threading;

namespace MyGame
{
    class Ship : GameObject
    {
        private const float Speed = 0.3f;
        private const int FireDelay = 0;
        private int _fireTimer;
        private Vector2 _playerPosition;
        private Vector2 _playerVelocity;
        float playerSide;
        private const float _playerSpeed = 5f;
        private const float _gravity = 0.2f;
        float gravity = _gravity;
        private bool _isJumping = false;
        private bool control = true;
        private bool controlAir = true;
        private readonly Sprite _sprite = new Sprite();
        public Ship()
        {
            _sprite.Texture = Game.GetTexture("Resources/ship.png");
            _sprite.Position = new Vector2f(100, 100);
            AssignTag("ship");
            SetCollisionCheckEnabled(true);
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_sprite);
        }
        public override void Update(Time elapsed)
        {
            FloatRect bounds = _sprite.GetGlobalBounds();
            Vector2f pos = _sprite.Position;
            float x = pos.X;
            float y = pos.Y;
            int msElapsed = elapsed.AsMilliseconds();
            if (Keyboard.IsKeyPressed(Keyboard.Key.C))
            {
                if (!controlAir)
                {
                    controlAir = true;
                }
                else
                {
                    controlAir = false;
                }
                Thread.Sleep(10);
            }

            if(controlAir)
            {
                gravity *= 0.9f;
            }
            else
            {
                gravity += 0.1f;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Up) && !_isJumping)
            {
                gravity += -1.0f;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
            {
                gravity += 1.0f;
            }
            y += gravity;
            if (y < -100)
            {
                y += 700;
            }
            if (y > 600)
            {
                y -= 700;
            }
            if (x < -100)
            {
                x += 900;
            }
            if (x > 800)
            {
                x -= 900;
            }
            /*            if(gravity > 100.0f || gravity < -100.0f)
                        {
                            gravity = 0.0f;
                        }
            */
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left)) { playerSide -= 1.0f; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right)) { playerSide += 1.0f; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.B))
            {
                gravity = 0.0f;
                playerSide = 0.0f;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.V))
            {
                if (!control)
                {
                    control = true;
                }
                else
                {
                    control = false;
                }
                Thread.Sleep(10);
            }
            if (!control)
            {
                playerSide *= 1.0f;
            }
            else
            {
                playerSide *= 0.9f;
            }

            x += playerSide;
            GameScene scene = (GameScene)Game.CurrentScene;
            _sprite.Position = new Vector2f(x, y);
            if (_fireTimer > 0) { _fireTimer -= msElapsed; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space) && _fireTimer <= 0)
                {

                    _fireTimer = FireDelay;
                    float laserX = x + bounds.Width;
                    float laserY = y + bounds.Height / 2.0f;
                    Laser laser = new Laser(new Vector2f(laserX, laserY));
                    Game.CurrentScene.AddGameObject(laser);
                }

            Console.WriteLine(y);
        }
        public override FloatRect GetCollisionRect()
        {
            return _sprite.GetGlobalBounds();
        }
        public override void HandleCollision(GameObject otherGameObject)
        {

        }
    }
}