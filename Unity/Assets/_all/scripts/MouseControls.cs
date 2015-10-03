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

        if (obj.CompareTag("Bouncer"))
        {
            hover_object = obj;
            hover_object.GetComponent<Renderer>().material.color = Color.green;

            state = InputState.Hover;
            return;
        }

        if (obj.CompareTag("Palette"))
        {
            hover_object = obj;
            hover_object.GetComponent<Renderer>().material.color = Color.green;

            state = InputState.Hover;
            return;
        }
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
            if (hover_object.CompareTag("Bouncer"))
            {
                hover_object.GetComponent<Renderer>().material.color = Color.blue;
                hover_object.GetComponent<Bouncer>().Controlled = true;
            }

            if (hover_object.CompareTag("Palette"))
            {
                hover_object.GetComponent<Renderer>().material.color = Color.white;

                var child = hover_object.GetComponent<Palette>().Spawn();

                child.transform.position = hover_object.transform.position;

                hover_object = child;

                hover_object.GetComponent<Renderer>().material.color = Color.blue;
                hover_object.GetComponent<Bouncer>().Controlled = true;
            }

            Cursor.visible = false;

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

            hover_object.GetComponent<Bouncer>().RecalculateGrid();

            state = InputState.Empty;

            Cursor.visible = true;

            UpdateEmpty();
            return;
        }

        var disco = FindObjectOfType<Disco>();
        var plane = new Plane(disco.transform.up, hover_object.transform.position.y);

        float sensitivity = 0.5f;
        float deltaX = Input.GetAxis("Mouse X") * sensitivity;
        float deltaY = 0.0f;
        float deltaZ = Input.GetAxis("Mouse Y") * sensitivity;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            deltaX = 0.0f;
            deltaY = deltaZ;
            deltaZ = 0.0f;
        }

        hover_object.transform.position +=
            disco.transform.right * deltaX +
            disco.transform.up * deltaY +
            disco.transform.forward * deltaZ;
    }

}
