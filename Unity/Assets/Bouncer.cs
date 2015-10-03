using UnityEngine;

public class Bouncer
	: MonoBehaviour
{
    int height = 0;
    int grid_x = 0;
    int grid_y = 0;

    public bool Controlled = false;

    void Start()
    {
        var disco = FindObjectOfType<Disco>();

        grid_x = (int)Random.Range(0.0f, disco.GridCountX);
        grid_y = (int)Random.Range(0.0f, disco.GridCountY);
    }

    void Update()
    {
        if (Controlled)
            return;

        var chrono = FindObjectOfType<Chrono>();
        var disco = FindObjectOfType<Disco>();
        var collider = GetComponent<SphereCollider>();
        var t = chrono.CurrentTime * (disco.Height - height);
        var y = Mathf.SmoothStep(0.0f, 1.0f, Mathf.Abs(Mathf.Sin(t)));
        var offset = transform.localScale * collider.radius;

        transform.position = new Vector3(
            grid_x * disco.GridSize.x + disco.HalfGridSize.x,
            y * height * disco.GridSize.y + offset.y,
            grid_y * disco.GridSize.z + disco.HalfGridSize.z
        );
    }

    public void RecalculateGrid()
    {
        var disco = FindObjectOfType<Disco>();
        var pos = transform.position;

        grid_x = (int)Mathf.Clamp(pos.x / disco.GridSize.x, 0.0f, disco.GridCountX);
        grid_y = (int)Mathf.Clamp(pos.z / disco.GridSize.z, 0.0f, disco.GridCountY);

        height = (int)Mathf.Clamp(pos.y / disco.GridSize.y, 0.0f, disco.GridCountHigh);
    }
}
