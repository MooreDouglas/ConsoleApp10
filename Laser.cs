using GameEngine;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace MyGame
{
    class Laser : GameObject
    {
        private const float Speed = 0.001f;
        private const float _gravity = 0.2f;
        float gravity = _gravity;
        private readonly Sprite _sprite = new Sprite();
        public Laser(Vector2f pos)
        {
            _sprite.Texture = Game.GetTexture("Resources/laser.png");
            _sprite.Position = pos;
            AssignTag("laser");
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_sprite);
        }
        public override void Update(Time elapsed)
        {
            Vector2f pos = _sprite.Position;
            float x = pos.X;
            float y = pos.Y;
            FloatRect bounds = _sprite.GetGlobalBounds();
            float laserX = x + bounds.Width;
            float laserY = y + bounds.Height / 2.0f;
            int msElapsed = elapsed.AsMilliseconds();
            if (Keyboard.IsKeyPressed(Keyboard.Key.P))
            {
                MakeDead();
            }
        }
        public override FloatRect GetCollisionRect()
        {
            return _sprite.GetGlobalBounds();
        }
    }
}