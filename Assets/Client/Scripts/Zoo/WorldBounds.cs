using UnityEngine;

namespace Kadoy.ZooWorld
{
    public readonly struct WorldBounds
    {
        public Vector2 Minimum { get; }
        public Vector2 Maximum { get; }

        public Vector2 Center => (Minimum + Maximum) * 0.5f;

        public WorldBounds(Vector2 minimum, Vector2 maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        public static WorldBounds FromCamera(Camera camera)
        {
            var ground = new Plane(Vector3.up, Vector3.zero);
            var bottomLeft = camera.ViewportPointToRay(Vector3.zero);
            var topRight = camera.ViewportPointToRay(Vector3.one);
            if (false == ground.Raycast(bottomLeft, out var nearDistance) ||
                false == ground.Raycast(topRight, out var farDistance))
            {
                throw new System.InvalidOperationException("The camera must face the ground.");
            }

            var minimum = bottomLeft.GetPoint(nearDistance);
            var maximum = topRight.GetPoint(farDistance);
            return new WorldBounds(new Vector2(minimum.x, minimum.z), new Vector2(maximum.x, maximum.z));
        }

        public bool Contains(Vector2 position)
        {
            return position.x >= Minimum.x && position.x <= Maximum.x &&
                position.y >= Minimum.y && position.y <= Maximum.y;
        }

        public Vector2 GetPoint(float horizontal, float vertical, float margin)
        {
            var inset = Vector2.Min(Vector2.one * margin, (Maximum - Minimum) * 0.5f);
            return new Vector2(
                Mathf.Lerp(Minimum.x + inset.x, Maximum.x - inset.x, horizontal),
                Mathf.Lerp(Minimum.y + inset.y, Maximum.y - inset.y, vertical));
        }

        public Vector2 DirectionToCenter(Vector2 position)
        {
            return (Center - position).normalized;
        }
    }
}
