using UnityEngine;

public class Chrono
	: MonoBehaviour
{
    public float CurrentTime = 0.0f;
    public float Speed = 1.0f;

    public void Update()
    {
        CurrentTime += Time.deltaTime * Speed;

        transform.localScale = V3._111() * (0.5f + 0.5f * Mathf.Abs(Mathf.Sin(CurrentTime)));
    }
}
