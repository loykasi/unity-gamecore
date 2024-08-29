using UnityEngine;

namespace GameCore.Helpers
{
    public static class GizmosHelpers
    {
        public static void DrawWireCircle2D(Vector2 center, float radius, int segment = 20)
        {
            DrawWireArc(center, radius, 360, Quaternion.Euler(90f, 0f, 0f), segment);
        }

        public static void DrawWireCircle(Vector3 center, float radius, Quaternion rotation, int segment = 20)
        {
            DrawWireArc(center, radius, 360, rotation, segment);
        }

        public static void DrawWireSquare2D(Vector2 center, float size, float angle)
        {
            DrawWireRectangle(center, Vector2.one * size, Quaternion.Euler(0f, 0f, angle) * Quaternion.Euler(90f, 0f, angle));
        }

        public static void DrawWireSquare(Vector3 center, float size, Quaternion rotation)
        {
            DrawWireRectangle(center, Vector2.one * size, rotation);
        }

        public static void DrawWireRectangle2D(Vector3 center, Vector2 size, float angle)
        {
            DrawWireRectangle(center, Vector2.one * size, Quaternion.Euler(0f, 0f, angle) * Quaternion.Euler(90f, 0f, 0f));
        }

        public static void DrawWireRectangle(Vector3 center, Vector2 size, Quaternion rotation)
        {
            Matrix4x4 origin = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(center, rotation, Vector3.one);

            float halfX = size.x / 2f;
            float halfZ = size.y / 2f;
            Gizmos.DrawLine(new Vector3(- halfX, 0f, halfZ), new Vector3(halfX, 0f, halfZ));
            Gizmos.DrawLine(new Vector3(- halfX, 0f, - halfZ), new Vector3(halfX, 0f, - halfZ));
            Gizmos.DrawLine(new Vector3(halfX, 0f, halfZ), new Vector3(halfX, 0f, - halfZ));
            Gizmos.DrawLine(new Vector3(- halfX, 0f, halfZ), new Vector3(- halfX, 0f, - halfZ));

            Gizmos.matrix = origin;
        }

        public static void DrawWireCapsule2D(Vector2 center, float height, float radius, float angle, int segment = 20)
        {
            float half = height / 2f - radius;
            DrawWireRectangle2D(center, new Vector2(radius * 2, 2 * half), angle);
            DrawWireArc2D(center, Vector2.up * half, radius, 180, angle, 90f);
            DrawWireArc2D(center, Vector2.down * half, radius, 180, angle, - 90f);
        }

        public static void DrawWireCapsule(Vector3 center, float height, float radius, Quaternion rotation, int segment = 20)
        {
            Matrix4x4 origin = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(center, rotation, Vector3.one);

            float half = height / 2f - radius;

            DrawWireCylinder(center, 2 * half, radius, rotation, segment);

            DrawWireArc(center, Vector3.up * half, radius, 180, rotation, Quaternion.AngleAxis(90, Vector3.forward));
            DrawWireArc(center, Vector3.up * half, radius, 180, rotation, Quaternion.AngleAxis(90, Vector3.up) * Quaternion.AngleAxis(90, Vector3.forward));

            DrawWireArc(center, Vector3.down * half, radius, 180, rotation, Quaternion.AngleAxis(- 90, Vector3.forward));
            DrawWireArc(center, Vector3.down * half, radius, 180, rotation, Quaternion.AngleAxis(90, Vector3.up) * Quaternion.AngleAxis(- 90, Vector3.forward));

            Gizmos.matrix = origin;
        }

        public static void DrawWireCylinder(Vector3 center, float height, float radius, Quaternion rotation, int segment = 20)
        {
            Matrix4x4 origin = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(center, rotation, Vector3.one);

            float half = height / 2f;

            Gizmos.DrawLine(Vector3.forward * radius + Vector3.up * half, Vector3.forward * radius + Vector3.down * half);
            Gizmos.DrawLine(Vector3.right * radius + Vector3.up * half, Vector3.right * radius + Vector3.down * half);
            Gizmos.DrawLine(Vector3.left * radius + Vector3.up * half, Vector3.left * radius + Vector3.down * half);
            Gizmos.DrawLine(Vector3.back * radius + Vector3.up * half, Vector3.back * radius + Vector3.down * half);

            DrawWireArc(center, Vector3.up * half, radius, 360, rotation, segment);
            DrawWireArc(center, Vector3.down * half, radius, 360, rotation, segment);

            Gizmos.matrix = origin;
        }

        public static void DrawWireArc2D(Vector2 center, float radius, float angle, float rotation, int segment = 20)
        {
            DrawWireArc(center, radius, angle, Quaternion.Euler(0f, 0f, rotation) * Quaternion.Euler(- 90f, 0f, 0f), segment);
        }

        public static void DrawWireArc2D(Vector2 center, Vector2 offset, float radius, float angle, float rotation, int segment = 20)
        {
            DrawWireArc(center, offset, radius, angle, Quaternion.Euler(0f, 0f, rotation) * Quaternion.Euler(- 90f, 0f, 0f), segment);
        }

        public static void DrawWireArc(Vector3 center, float radius, float angle, Quaternion rotation, int segment = 20)
        {
            if (angle <= 0)
            {
                return;
            }
            Matrix4x4 origin = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(center, rotation, Vector3.one);
            int step = (int) angle / segment;
            if (step <= 0)
            {
                return;
            }
            Vector3 from = Vector3.forward * radius;
            for (int i = 0; i <= angle; i += step)
            {
                Vector3 to = new Vector3(Mathf.Sin(i * Mathf.Deg2Rad) * radius, 0f, Mathf.Cos(i * Mathf.Deg2Rad) * radius);
                Gizmos.DrawLine(from, to);
                from = to;
            }
            Gizmos.matrix = origin;
        }

        public static void DrawWireArc(Vector3 center, Vector3 offset, float radius, float angle, Quaternion rotation, int segment = 20)
        {
            if (angle <= 0)
            {
                return;
            }
            Matrix4x4 origin = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(center, rotation, Vector3.one);
            int step = (int) angle / segment;
            if (step <= 0)
            {
                return;
            }
            Vector3 from = offset + Vector3.forward * radius;
            for (int i = 0; i <= angle; i += step)
            {
                Vector3 to = offset + new Vector3(Mathf.Sin(i * Mathf.Deg2Rad) * radius, 0f, Mathf.Cos(i * Mathf.Deg2Rad) * radius);
                Gizmos.DrawLine(from, to);
                from = to;
            }
            Gizmos.matrix = origin;
        }

        public static void DrawWireArc2D(Vector2 center, Vector2 offset, float radius, float angle, float rotation, float localRotaion, int segment = 20)
        {
            DrawWireArc(center, offset, radius, angle, Quaternion.Euler(0f, 0f, rotation), Quaternion.AngleAxis(localRotaion, Vector3.forward) * Quaternion.AngleAxis(- 90f, Vector3.right), segment);
        }

        public static void DrawWireArc(Vector3 center, Vector3 offset, float radius, float angle, Quaternion rotation, Quaternion localRotation, int segment = 20)
        {
            if (angle <= 0)
            {
                return;
            }
            Matrix4x4 origin = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(center, rotation, Vector3.one);
            int step = (int) angle / segment;
            if (step <= 0)
            {
                return;
            }
            Vector3 from = Vector3.forward * radius;
            from = localRotation * from + offset;
            for (int i = 0; i <= angle; i += step)
            {
                Vector3 to = localRotation * new Vector3(Mathf.Sin(i * Mathf.Deg2Rad) * radius, 0f, Mathf.Cos(i * Mathf.Deg2Rad) * radius) + offset;
                Gizmos.DrawLine(from, to);
                from = to;
            }
            Gizmos.matrix = origin;
        }

        public static void DrawArrow(Vector3 position, Vector3 direction)
        {
            if (direction == Vector3.zero)
            {
                return;
            }
            Gizmos.DrawRay(position, direction);
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0f, 180f + 45f, 0f) * Vector3.forward;
            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0f, 180f - 45f, 0f) * Vector3.forward;
            Gizmos.DrawRay(position + direction, left * 0.1f);
            Gizmos.DrawRay(position + direction, right * 0.1f);
        }
    }
}