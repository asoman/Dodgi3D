using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {

    public float speed = 10;

    private ModelPlayer model;
    private Vector2 CurentDestination;
    private Coroutine moving;

    private void OnEnable()
    {
        InputHandler.OnFingerSwipe += Move;
        model = GetComponent<ModelPlayer>();
    }

    private void OnDisable()
    {
        InputHandler.OnFingerSwipe -= Move;
    }

    public void Move(Vector2 bar)
    {
        StopCoroutine(moving);
        CurentDestination += bar;
        moving = StartCoroutine(MovingCoroutine(CurentDestination));
        transform.Translate(new Vector3(bar.x,0,bar.y));
    }

    IEnumerator MovingCoroutine(Vector2 destination)
    {
        Vector3 destination3D = new Vector3(destination.x, 0, destination.y);
        float dist = Vector3.Distance(transform.position, destination3D);
        model.StartMoving();
        Vector3 moveVector = Vector3.ClampMagnitude(destination3D - transform.position,
            dist>speed ? speed: dist);

        transform.Translate(moveVector);
        yield return new WaitForEndOfFrame();
        if (Vector3.Distance(destination3D, transform.position) <= 0.01)
            model.StopMoving();
        else
            moving = StartCoroutine(MovingCoroutine(destination));
    }
}
