﻿using UnityEngine;

public class Beats
	: MonoBehaviour
{
    SfxrSynth synth_bar = new SfxrSynth();
    SfxrSynth synth_note = new SfxrSynth();
    SfxrSynth synth_half = new SfxrSynth();

    public string SynthBarParamsString = "";
    public string SynthNoteParamsString = "";
    public string SynthHalfParamsString = "";

    void OnEnable()
    {
        synth_bar.parameters.GenerateExplosion();
        synth_note.parameters.GenerateBlipSelect();
        synth_half.parameters.GenerateHitHurt();

        synth_bar.parameters.SetSettingsString(SynthBarParamsString);
        synth_note.parameters.SetSettingsString(SynthNoteParamsString);
        synth_half.parameters.SetSettingsString(SynthHalfParamsString);
    }

    void Start()
    {
        SynthBarParamsString = synth_bar.parameters.GetSettingsString();
        SynthNoteParamsString = synth_note.parameters.GetSettingsString();
        SynthHalfParamsString = synth_half.parameters.GetSettingsString();
    }

    void Update()
    {
        if (!ApplicationFocusState.Focused)
            return;

        var chrono = FindObjectOfType<Chrono>();
        var bar = chrono.IsBeat(1);
        var note = chrono.IsBeat(4);
        var half = chrono.IsBeat(8);

        if (half)
            synth_half.Play();
        if (note)
            synth_note.Play();
        if (bar)
            synth_bar.Play();
    }
}
