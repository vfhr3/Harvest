using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public static class TargetSelector
    {
        private static Collider2D[] _results = new Collider2D[50];

        public static Transform FindNearestEnemy(Vector3 position, float maxDistance = 20f)
        {
            int count = Physics2D.OverlapCircleNonAlloc(position, maxDistance, _results, LayerMask.GetMask("Enemy"));
            
            Transform nearest = null;
            float nearestDistance = float.MaxValue;

            for (int i = 0; i < count; i++)
            {
                float distance = Vector3.Distance(position, _results[i].transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearest = _results[i].transform;
                }
            }

            return nearest;
        }

        public static List<Transform> FindNearestEnemies(Vector3 position, int count, float maxDistance = 20f)
        {
            int resultCount = Physics2D.OverlapCircleNonAlloc(position, maxDistance, _results, LayerMask.GetMask("Enemy"));
            
            var enemies = new List<(Transform transform, float distance)>();
            for (int i = 0; i < resultCount; i++)
            {
                float distance = Vector3.Distance(position, _results[i].transform.position);
                enemies.Add((_results[i].transform, distance));
            }

            enemies.Sort((a, b) => a.distance.CompareTo(b.distance));

            var result = new List<Transform>();
            for (int i = 0; i < Mathf.Min(count, enemies.Count); i++)
            {
                result.Add(enemies[i].transform);
            }

            return result;
        }
    }
}