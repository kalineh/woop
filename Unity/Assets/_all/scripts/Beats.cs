using UnityEngine;

public class Beats
	: MonoBehaviour
{
    SfxrSynth synth_bar = new SfxrSynth();
    SfxrSynth synth_beat = new SfxrSynth();

    int previous_bar = -1;
    int previous_beat = -1;

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

        var t1 = chrono.CurrentTime;
        var t4 = chrono.CurrentTime * 4.0f;

        var bar = (int)(t1);
        var beat = (int)(t4);

        if (bar != previous_bar)
            synth_bar.Play();

        if (beat != previous_beat)
            synth_beat.Play();

        previous_bar = bar;
        previous_beat = beat;
    }
}
