using GameEngine;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Reflection.Metadata.Ecma335;
namespace MyGame
{
    class Meteor : GameObject
    {
        private const float Speed = 0.1f;
        private readonly Sprite _sprite = new Sprite();
        private const float _gravity = 0.2f;
        float gravity = _gravity;
        public Meteor(Vector2f pos)
        {
            _sprite.Texture = Game.GetTexture("Resources/meteor.png");
            _sprite.Position = pos;
            AssignTag("meteor");
            SetCollisionCheckEnabled(true);
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_sprite);
        }
        public override void Update(Time elapsed)
        {
            int msElapsed = elapsed.AsMilliseconds();
            Vector2f pos = _sprite.Position;
            float x = pos.X;
            float y = pos.Y;
            y += gravity;
            gravity += 0.1f;
            _sprite.Position = new Vector2f(x - Speed * msElapsed, y);
            if (y < -100)
            {
                gravity *= -1.0f;
            }
            if (y > 600)
            {
                gravity *= -1.0f;
            }
        }
        public override FloatRect GetCollisionRect()
        {
            return _sprite.GetGlobalBounds();
        }
        public override void HandleCollision(GameObject otherGameObject)
        {
            Vector2f pos = _sprite.Position;
            if (otherGameObject.HasTag("laser"))
            {
                otherGameObject.MakeDead();
                GameScene scene = (GameScene)Game.CurrentScene;
                scene.IncreaseScore();
                pos.X = pos.X + (float)_sprite.GetGlobalBounds().Width / 2.0f;
                pos.Y = pos.Y + (float)_sprite.GetGlobalBounds().Height / 2.0f;
                Explosion explosion = new Explosion(pos);
                Game.CurrentScene.AddGameObject(explosion);
                MakeDead();
            }
            if (otherGameObject.HasTag("ship"))
            {
                GameScene scene = (GameScene)Game.CurrentScene;
                scene.IncreaseScore();
                pos.X = pos.X + (float)_sprite.GetGlobalBounds().Width / 2.0f;
                pos.Y = pos.Y + (float)_sprite.GetGlobalBounds().Height / 2.0f;
                Explosion explosion = new Explosion(pos);
                Game.CurrentScene.AddGameObject(explosion);
                MakeDead();

            }
            if(pos.X == 0.0f)
            {
                MakeDead();
            }
        }
    }
}