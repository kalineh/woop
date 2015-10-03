using UnityEngine;

public class Beats
	: MonoBehaviour
{
    SfxrSynth synth;

    void Start()
    {
        synth = new SfxrSynth();
        synth.parameters.GenerateBlipSelect();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.R)) { synth.parameters.Randomize(); }
        //if (Input.GetKeyDown(KeyCode.T)) { synth.parameters.GenerateBlipSelect(); }
        //if (Input.GetKeyDown(KeyCode.Y)) { synth.Play(); }
    }
}
