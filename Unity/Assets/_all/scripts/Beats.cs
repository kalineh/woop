using UnityEngine;

public class Beats
	: MonoBehaviour
{
    SfxrSynth synth_bar = new SfxrSynth();
    SfxrSynth synth_beat = new SfxrSynth();

    private string SynthBarParamsString;
    private string SynthBeatParamsString;

    void OnDisable()
    {
        SynthBarParamsString = synth_bar.parameters.GetSettingsString();
        SynthBeatParamsString = synth_beat.parameters.GetSettingsString();
    }

    void OnEnable()
    {
        if (SynthBarParamsString != null) { synth_bar.parameters.SetSettingsString(SynthBarParamsString); }
        if (SynthBeatParamsString != null) { synth_beat.parameters.SetSettingsString(SynthBeatParamsString); }
    }

    void Start()
    {
        synth_bar.parameters.GenerateExplosion();
        synth_bar.parameters.masterVolume = 0.05f;

        synth_beat.parameters.GenerateBlipSelect();
        synth_beat.parameters.masterVolume = 0.05f;

        SynthBarParamsString = synth_bar.parameters.GetSettingsString();
        SynthBeatParamsString = synth_beat.parameters.GetSettingsString();
    }

    void Update()
    {
        if (!ApplicationFocusState.Focused)
            return;

        var chrono = FindObjectOfType<Chrono>();
        var beat = chrono.IsBeat(1);
        var bar = chrono.IsBeat(4);

        if (beat)
            synth_beat.Play();
        if (bar)
            synth_bar.Play();
    }
}
