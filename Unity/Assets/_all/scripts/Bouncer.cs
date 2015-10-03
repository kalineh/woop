using UnityEngine;
using UnityEditor;

public class Bouncer
	: MonoBehaviour
{
    public SfxrSynth Synth;
    public SfxrParams Params;

    float y_prev = 0.0f;
    float y_prev_prev = 0.0f;

    int height = 0;
    int grid_x = 0;
    int grid_y = 0;

    public bool Controlled = false;

    void Start()
    {
        Params = new SfxrParams();
        Params.GeneratePickupCoin();

        Synth = new SfxrSynth();
        Synth.parameters = Params;

        var disco = FindObjectOfType<Disco>();

        grid_x = (int)Random.Range(0.0f, disco.GridCountX);
        grid_y = (int)Random.Range(0.0f, disco.GridCountY);
    }

    void Update()
    {
        // TODO: fix reload issues
        if (Synth == null)
            Synth = new SfxrSynth() { parameters = Params, };

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


        var prev_dir = Mathf.Sign(y_prev_prev - y_prev);
        var dir = Mathf.Sign(y_prev - y);

        if (prev_dir != dir && prev_dir > 0.0f && height > 0)
        {
            Synth.Play();
        }

        y_prev_prev = y_prev;
        y_prev = y;
    }

    void OnApplicationFocus(bool focus)
    {
        Synth.parameters.masterVolume = focus ? 1.0f : 0.0f;
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
