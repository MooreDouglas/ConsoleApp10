using System;
using System.Collections.Generic;
using GameEngine;
using SFML.Graphics;
using SFML.System;

namespace MyGame
{
    class Scene
    {
        private readonly List<GameObject> _gameObjects = new List<GameObject>();

        public void AddGameObject(GameObject gameObject)
        {
            _gameObjects.Add(gameObject);
        }

        public void Update(Time time)
        {
            Game.RenderWindow.Clear();


            Game.RenderWindow.DispatchEvents();

            HandleCollisions();
            UpdateGameObjects(time);
            RemoveDeadGameObjects();
            DrawGameObjects();

            Game.RenderWindow.Display();
        }

        private void HandleCollisions()
        {
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                var gameObject = _gameObjects[i];

                if (!gameObject.IsCollisionCheckEnabled()) continue;

                FloatRect collisionRect = gameObject.GetCollisionRect();

                if (collisionRect.Height == 0 || collisionRect.Width == 0) continue;

                for (int j = 0; j < _gameObjects.Count; j++)
                {
                    var otherGameObject = _gameObjects[j];

                    if (gameObject == otherGameObject) continue;

                    if (gameObject.IsDead()) return;

                    if (collisionRect.Intersects(otherGameObject.GetCollisionRect()))
                    {
                        gameObject.HandleCollision(otherGameObject);
                        otherGameObject.HandleCollision(gameObject);
                    }
                }
            }
        }

        private void UpdateGameObjects(Time time)
        {
            for (int i = 0; i < _gameObjects.Count; i++) _gameObjects[i].Update(time);
        }

        private void DrawGameObjects()
        {
            foreach (var gameObject in _gameObjects) gameObject.Draw();
        }

        private void RemoveDeadGameObjects()
        {

            Predicate<GameObject> isDead = gameObject => gameObject.IsDead();


            _gameObjects.RemoveAll(isDead);
        }
    }
}