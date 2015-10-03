using UnityEngine;
using UnityEditor;

public class Bouncer
	: MonoBehaviour
{
    public SfxrSynth Synth = new SfxrSynth();
    private string SynthParamsString;

    float y_prev = 0.0f;
    float y_prev_prev = 0.0f;

    int height = 0;
    int grid_x = 0;
    int grid_y = 0;

    public bool Controlled = false;

    void OnDisable()
    {
        SynthParamsString = Synth.parameters.GetSettingsString();
    }

    void OnEnable()
    {
        if (SynthParamsString != null)
        {
            Synth.parameters.SetSettingsString(SynthParamsString);
        }
    }

    void Start()
    {
        SynthParamsString = Synth.parameters.GetSettingsString();

        var disco = FindObjectOfType<Disco>();

        grid_x = (int)Random.Range(0.0f, disco.GridCountX);
        grid_y = (int)Random.Range(0.0f, disco.GridCountY);
    }

    void Update()
    {
        if (Controlled)
            return;

        if (!ApplicationFocusState.Focused)
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
            SyncParametersFromPosition();

            Synth.parameters.masterVolume = 0.5f;
            Synth.SetParentTransform(transform);
            Synth.Play();
        }

        y_prev_prev = y_prev;
        y_prev = y;
    }

    void SyncParametersFromPosition()
    {
        var disco = FindObjectOfType<Disco>();

        var x = grid_x / disco.GridCountX;
        var y = grid_y / disco.GridCountY;

        Synth.parameters.lpFilterCutoff = Mathf.Lerp(0.2f, 0.9f, x);
        Synth.parameters.hpFilterCutoff = Mathf.Lerp(0.1f, 0.9f, y);
        Synth.parameters.masterVolume = 0.15f;
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
