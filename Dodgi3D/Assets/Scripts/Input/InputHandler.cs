using UnityEngine.Events;
using UnityEngine;
using Lean.Touch;

public class InputHandler : MonoBehaviour {

    public delegate void SwipeEvent(Vector2 direction);
    public static event SwipeEvent OnFingerSwipe;
   
    public bool IgnoreGuiFingers = true;    //Ignore fingers with StartedOverGui

    public float DeltaThreshold = 0.25f;   //Threshold for defining direction

    protected void OnEnable()
    {
        LeanTouch.OnFingerSwipe += FingerSwipe;
    }

    protected void OnDisable()
    {
        LeanTouch.OnFingerSwipe -= FingerSwipe;
    }

    private void FingerSwipe(LeanFinger finger)
    {
        // Ignore this finger?
        if (IgnoreGuiFingers == true && finger.StartedOverGui == true)
        {
            return;
        }

        AlignDirectionSwipe(finger, finger.SwipeScreenDelta);
    }

    protected void AlignDirectionSwipe(LeanFinger finger, Vector2 delta)
    {
        
        OnFingerSwipe(DirectionAlign(delta));
    }

    private Vector2 DirectionAlign(Vector2 direction)
    {
        Vector2 normDirection = direction.normalized;
        Debug.Log("Swipe Delta" + normDirection.ToString());
        return new Vector2(OneDirectionAlign(normDirection.x),
                            OneDirectionAlign(normDirection.y));
    }

    private int OneDirectionAlign(float x)
    {
        float temp = Mathf.Abs(x);

        if (temp < DeltaThreshold) return 0;

        return x < 0 ? -1 : 1;
    }


}
