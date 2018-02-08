using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {

    public float speed = 10;

    private ModelPlayer model;
    private Vector3 curentDestination;
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

    public void Move(Vector2 way)
    {
        StopAllCoroutines();
        curentDestination += new Vector3(-way.y,0,way.x);
        model.model.transform.LookAt(curentDestination);

        curentDestination = CalcalateDistinationWithColliders(curentDestination);
        Debug.Log(curentDestination);
        StartCoroutine(MovingCoroutine(curentDestination));
    }

    IEnumerator MovingCoroutine(Vector3 destination)
    {     
        Vector3 move = (destination - transform.position).normalized;
        move = move * speed * Time.deltaTime;
        if(move.magnitude > 0)
        {
            model.StartMoving();
        }
        controller.Move(move);
        

        yield return new WaitForEndOfFrame();

        if ((transform.position - destination).magnitude < 0.1)
        {
            transform.position = destination;
            model.StopMoving();
        }
        else
            StartCoroutine(MovingCoroutine(destination));
    }

    private Vector3 CalcalateDistinationWithColliders(Vector3 destination)
    {
        Vector3 path = destination - transform.position;
        Ray ray = new Ray(transform.position, path.normalized);
        //Debug.Log(path);
        RaycastHit hit;
        if(Physics.SphereCast(ray, 0.3f, out hit, path.magnitude))
        {
            Debug.Log("HIT!" + hit.point);
            destination = ShortPath(destination, path);
        }

        return destination;
    }

    private Vector3 ShortPath(Vector3 destination, Vector3 path)
    {
        if(destination.x<transform.position.x)
            destination.x += path.x != 0 ? 1 : 0;
        else
            destination.x -= path.x != 0 ? 1 : 0;

        if (destination.z < transform.position.z)
            destination.z += path.z != 0 ? 1 : 0;
        else
            destination.z -= path.z != 0 ? 1 : 0;
        return destination;
    }
}


