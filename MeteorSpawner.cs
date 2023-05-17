using GameEngine;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Drawing;
using System.Threading;

namespace MyGame
{
    class MeteorSpawner : GameObject
    {
        private bool spawning = false;
        private const int SpawnDelay = 100;
        private int _timer = 1;
        private readonly Sprite _sprite = new Sprite();
        public override void Update(Time elapsed)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Q))
            {
                if (!spawning)
                {
                    spawning = true;
                }
                else
                {
                    spawning = false;
                }
                Thread.Sleep(10);
            }
            int msElapsed = elapsed.AsMilliseconds();
            if (spawning)
            {
               _timer -= msElapsed;
            }
            if (_timer <= SpawnDelay)
            {
                Vector2u size = Game.RenderWindow.Size;
                float meteorX = size.X;
                int v = Game.Random.Next();
                int c = Game.Random.Next();
                float meteorY = v % size.Y;
                _timer = SpawnDelay;
                Meteor meteor = new Meteor(new Vector2f(meteorX, meteorY));
                Game.CurrentScene.AddGameObject(meteor);
            }
        }
    }
}