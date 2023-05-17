using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace GameEngine
{
    abstract class GameObject
    {
        private bool _isCollisionCheckEnabled;

        private bool _isDead;

        private readonly HashSet<string> _tags = new HashSet<string>();

        public void AssignTag(string tag)
        {
            _tags.Add(tag);
        }

        public bool HasTag(string tag)
        {
            return _tags.Contains(tag);
        }

        public bool IsDead()
        {
            return _isDead;
        }

        public void MakeDead()
        {
            _isDead = true;
        }

        public abstract void Update(Time elapsed);


        public virtual void Draw()
        {
        }

        public bool IsCollisionCheckEnabled()
        {
            return _isCollisionCheckEnabled;
        }

        public void SetCollisionCheckEnabled(bool isCollisionCheckEnabled)
        {
            _isCollisionCheckEnabled = isCollisionCheckEnabled;
        }

        public virtual FloatRect GetCollisionRect()
        {
            return new FloatRect();
        }
        public virtual void HandleCollision(GameObject otherGameObject)
        {
        }
    }
}