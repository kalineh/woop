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
        var y = Mathf.SmoothStep(0.0f, 1.0f, Mathf.Abs(Mathf.Sin(chrono.CurrentTime)));
        var offset = transform.localScale * collider.radius;

        transform.position = new Vector3(
            grid_x * disco.GridSize + disco.HalfGridSize,
            y * disco.Height + offset.y,
            grid_y * disco.GridSize + disco.HalfGridSize
        );
    }
}
