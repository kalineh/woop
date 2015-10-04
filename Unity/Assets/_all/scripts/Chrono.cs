using UnityEngine;
using System.Collections;

public class Chrono
	: MonoBehaviour
{
    public float CurrentTime = 0.0f;
    public float Speed = 1.0f;

    float previous_time = 0.0f;

    public void Update()
    {
        if (!ApplicationFocusState.Focused)
            return;

        previous_time = CurrentTime;
        CurrentTime += Time.deltaTime * Speed;

        var bt = GetBeatTime(1);

        transform.localScale = V3._111() * bt;
    }

    public bool IsBeat(int multiple)
    {
        var prev_time = previous_time * multiple;
        var prev_beat = (int)prev_time;

        var curr_time = CurrentTime * multiple;
        var curr_beat = (int)curr_time;

        return prev_beat != curr_beat;
    }

    public int GetBeatCount(int multiple)
    {
        var time = CurrentTime * multiple;
        var beat = (int)time;

        return beat;
    }

    public float GetBeatTime(int multiple)
    {
        var time = CurrentTime * multiple;
        var beat = (int)time;
        var fraction = time - (float)beat;

        return Mathf.Clamp01(fraction);
    }
}
