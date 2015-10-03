using UnityEngine;
using System.Collections.Generic;

public class TraceLog
	: MonoBehaviour
{
    public bool Tracing;
    public List<string> Entries;
    private int tracePrintedIndex;

    void Start()
    {
        Entries = new List<string>();
    }

    void Update()
    {
        while (Entries.Count > tracePrintedIndex)
        {
            Debug.Log(Entries[tracePrintedIndex], null);
            tracePrintedIndex++;
        }
    }

    public void Trace(string status)
    {
        Entries.Add(string.Format("{0}: {1}", Time.frameCount.ToString(), status));
    }
}
