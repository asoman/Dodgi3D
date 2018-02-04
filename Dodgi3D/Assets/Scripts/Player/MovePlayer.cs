using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {

    public float speed = 10;

    private ModelPlayer model;
    private Vector2 curentDestination;
    private CharacterController controller;

    private void OnEnable()
    {
        InputHandler.OnFingerSwipe += Move;
        curentDestination = new Vector3();
        model = GetComponent<ModelPlayer>();
        controller = GetComponent<CharacterController>();
    }

    private void OnDisable()
    {
        InputHandler.OnFingerSwipe -= Move;
    }

    public void Move(Vector2 bar)
    {
        StopAllCoroutines();
        curentDestination += bar;
        StartCoroutine(MovingCoroutine(curentDestination));
        Debug.Log(curentDestination);
    }

    IEnumerator MovingCoroutine(Vector2 destination)
    {
        Vector3 destination3D = new Vector3(destination.x, 0, destination.y);
        model.StartMoving();
        model.model.transform.LookAt(destination3D);
        Vector3 move = (destination3D - transform.position).normalized;
        move = move * speed * Time.deltaTime;
        //move = transform.TransformDirection(move);
        controller.Move(move);

        yield return new WaitForEndOfFrame();

        if ((transform.position - destination3D).magnitude < 0.1)
        {
            transform.position = destination3D;
            model.StopMoving();
        }
        else
            StartCoroutine(MovingCoroutine(destination));
    }
}
