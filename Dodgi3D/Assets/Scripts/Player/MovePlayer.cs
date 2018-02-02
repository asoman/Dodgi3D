using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {


    private void OnEnable()
    {
        InputHandler.OnFingerSwipe += Move;
    }

    private void OnDisable()
    {
        InputHandler.OnFingerSwipe -= Move;
    }

    public void Move(Vector2 bar)
    {
        transform.Translate(new Vector3(bar.x,0,bar.y));
    }
}
