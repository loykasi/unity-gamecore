using UnityEngine;

namespace GameCore.Helpers
{
    public static class CameraHelpers
    {
        public static bool IsPointInView(Vector3 point, Camera camera, LayerMask layerMask)
        {
            Vector3 viewportPoint = camera.WorldToViewportPoint(point);

            if (viewportPoint.z < 0)
            {
                return false;
            }

            if (viewportPoint.x < 0 || viewportPoint.x > 1 || viewportPoint.y < 0 || viewportPoint.y > 1)
            {
                return false;
            }

            bool isHit = Physics.Linecast(point, camera.transform.position, layerMask);
            if (isHit)
            {
                return false;
            }
            return true;
        }
    }
}