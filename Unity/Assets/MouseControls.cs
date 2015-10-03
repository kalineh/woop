using UnityEngine;

public class MouseControls
    : MonoBehaviour
{
    enum InputState
    {
        Empty,
        Hover,
        Place,
    }

    InputState state;

    GameObject hover_object = null;

    void Update()
    {
        switch (state)
        {
            case InputState.Empty:
                UpdateEmpty();
                break;
            case InputState.Hover:
                UpdateHover();
                break;
            case InputState.Place:
                UpdatePlace();
                break;
        }
    }

    void UpdateEmpty()
    {
        var chrono = FindObjectOfType<Chrono>();
        var disco = FindObjectOfType<Disco>();
        var collider = GetComponent<SphereCollider>();

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var info = new RaycastHit();
        var hit = Physics.Raycast(ray, out info);

        if (!hit)
            return;

        var obj = info.collider.gameObject;
        if (!obj)
            return;

        if (!obj.CompareTag("Bouncer"))
            return;

        hover_object = obj;
        hover_object.GetComponent<Renderer>().material.color = Color.green;

        state = InputState.Hover;
    }

    void UpdateHover()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var info = new RaycastHit();
        var hit = Physics.Raycast(ray, out info);

        if (!hit)
        {
            hover_object.GetComponent<Renderer>().material.color = Color.white;
            hover_object = null;

            state = InputState.Empty;
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            hover_object.GetComponent<Renderer>().material.color = Color.blue;
            hover_object.GetComponent<Bouncer>().Controlled = true;

            state = InputState.Place;
            return;
        }
    }

    void UpdatePlace()
    {
        if (!Input.GetMouseButton(0))
        {
            hover_object.GetComponent<Renderer>().material.color = Color.white;
            hover_object.GetComponent<Bouncer>().Controlled = false;

            state = InputState.Empty;

            UpdateEmpty();
            return;
        }

        var disco = FindObjectOfType<Disco>();
        var plane = new Plane(disco.Up, hover_object.transform.position.y);

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var enter = 0.0f;
        var hit = plane.Raycast(ray, out enter);

        if (!hit)
            return;

        if (hit)
        {
            // convert point
            Debug.DrawLine(V3._000(), plane.normal * enter);
        }
    }

}
