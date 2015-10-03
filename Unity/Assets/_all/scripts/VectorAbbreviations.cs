using UnityEngine;

public class V3
{
    public static Vector3 _000() { return new Vector3(0.0f, 0.0f, 0.0f); }
    public static Vector3 _001() { return new Vector3(0.0f, 0.0f, 1.0f); }
    public static Vector3 _010() { return new Vector3(0.0f, 1.0f, 0.0f); }
    public static Vector3 _100() { return new Vector3(1.0f, 0.0f, 0.0f); }
    public static Vector3 _101() { return new Vector3(1.0f, 0.0f, 1.0f); }
    public static Vector3 _110() { return new Vector3(1.0f, 1.0f, 0.0f); }
    public static Vector3 _011() { return new Vector3(0.0f, 1.0f, 1.0f); }
    public static Vector3 _111() { return new Vector3(1.0f, 1.0f, 1.0f); }

    public static Vector3 fromV2(Vector2 v) { return new Vector3(v.x, v.y, 0.0f); }
    public static Vector3 fromV2(Vector2 v, float f) { return new Vector3(v.x, v.y, f); }

    public static Vector3 V(float x) { return new Vector3(x, 0.0f, 0.0f); }
    public static Vector3 V(float x, float y) { return new Vector3(x, y, 0.0f); }
    public static Vector3 V(float x, float y, float z) { return new Vector3(x, y, z); }
}

public class V2
{
    public static Vector2 _00() { return new Vector2(0.0f, 0.0f); }
    public static Vector2 _01() { return new Vector2(0.0f, 1.0f); }
    public static Vector2 _10() { return new Vector2(1.0f, 0.0f); }
    public static Vector2 _11() { return new Vector2(1.0f, 1.0f); }

    public static Vector2 fromV3(Vector3 v) { return new Vector2(v.x, v.y); }

    public static Vector2 V(float x) { return new Vector3(x, 0.0f); }
    public static Vector2 V(float x, float y) { return new Vector2(x, y); }
}
