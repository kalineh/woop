﻿using UnityEngine;
using UnityEditor;

public class Bouncer
	: MonoBehaviour
{
    public SfxrSynth Synth = new SfxrSynth();
    public string SynthParamsString = "";

    public int Height = 0;
    public int GridX = 0;
    public int GridY = 0;

    public bool Controlled = false;

    void OnEnable()
    {
        Synth.parameters.GeneratePickupCoin();
        Synth.parameters.SetSettingsString(SynthParamsString);
    }

    void Start()
    {
        var disco = FindObjectOfType<Disco>();

        GridX = (int)Random.Range(0.0f, disco.GridCountX);
        GridY = (int)Random.Range(0.0f, disco.GridCountY);
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
        var inverse_height = disco.GridCountHigh - Height;
        var t = chrono.GetBeatTime(inverse_height);
        var y = Mathf.Abs(Mathf.Sin(t * Mathf.PI * 2.0f * 0.5f));
        var move_height = Height * disco.GridSize.y;
        var offset = transform.localScale * collider.radius;

        transform.position = new Vector3(
            GridX * disco.GridSize.x + disco.HalfGridSize.x,
            y * move_height + offset.y,
            GridY * disco.GridSize.z + disco.HalfGridSize.z
        );

        if (Height <= 0)
            return;

        var beat = chrono.IsBeat(inverse_height);
        if (beat)
        {
            SyncParametersFromPosition();

            Synth.parameters.masterVolume = 0.5f;
            Synth.SetParentTransform(transform);
            Synth.Play();
        }
    }

    void SyncParametersFromPosition()
    {
        var disco = FindObjectOfType<Disco>();

        var x = GridX / disco.GridCountX;
        var y = GridY / disco.GridCountY;

        Synth.parameters.lpFilterCutoff = Mathf.Lerp(0.2f, 0.9f, x);
        Synth.parameters.hpFilterCutoff = Mathf.Lerp(0.1f, 0.9f, y);
        Synth.parameters.masterVolume = 0.15f;
    }

    public void RecalculateGrid()
    {
        var disco = FindObjectOfType<Disco>();
        var pos = transform.position;

        GridX = (int)Mathf.Clamp(pos.x / disco.GridSize.x, 0.0f, disco.GridCountX);
        GridY = (int)Mathf.Clamp(pos.z / disco.GridSize.z, 0.0f, disco.GridCountY);

        // clamp for beat counts
        Height = (int)Mathf.Clamp(pos.y / disco.GridSize.y, 2.0f, disco.GridCountHigh - 1);
    }
}
