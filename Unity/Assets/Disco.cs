using UnityEngine;

public class Disco
	: MonoBehaviour
{
    public float Step = 1.0f;
    public float Height = 8.0f;
    public float GridSize = 1.0f;
    public float HalfGridSize = 0.5f;
    public Vector3 Up = Vector3.up;

    private int PlaneDefaultSize = 5;

    public int GetGridCountX()
    {
        var size = Vector3.Scale(V3._111() * (float)PlaneDefaultSize, transform.localScale);
        var steps = size / GridSize;

        return (int)steps.x;
    }

    public int GetGridCountY()
    {
        var size = Vector3.Scale(V3._111() * (float)PlaneDefaultSize, transform.localScale);
        var steps = size / GridSize;

        return (int)steps.z;
    }
}
