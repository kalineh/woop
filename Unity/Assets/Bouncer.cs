using UnityEngine;

public class Bouncer
	: MonoBehaviour
{
    // when placing, snap silhouette to grid
    // when placed, spawn a bouncer

    int height = 0;
    int grid_x = 0;
    int grid_y = 0;

    void Update()
    {
        var chrono = FindObjectOfType<Chrono>();
        var disco = FindObjectOfType<Disco>();
        var y = Mathf.Sin(chrono.CurrentTime);


        transform.position = new Vector3(
            grid_x * disco.GridSize,
            y * disco.Height,
            grid_y * disco.GridSize
        );
    }
}
