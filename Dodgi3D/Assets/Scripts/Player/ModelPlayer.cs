using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelPlayer : MonoBehaviour {

    public GameObject model;
    private Animator anim;

    private void Start()
    {
        anim = model.GetComponent<Animator>();
    }

    public void StartMoving()
    {
        anim.SetBool("IsMoving", true);
    }

    public void StopMoving()
    {
        anim.SetBool("IsMoving", false);
    }

    public void Death()
    {
        anim.SetTrigger("Death");
    }
}
