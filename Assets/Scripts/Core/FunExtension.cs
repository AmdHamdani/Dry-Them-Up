using UnityEngine;

public static class FunExtension
{
    public static Vector3 ToWorldPosition(this Vector3 position)
    {
        return Camera.main.ScreenToWorldPoint(position);
    }
}