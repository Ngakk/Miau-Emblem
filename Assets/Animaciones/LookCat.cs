using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCat : MonoBehaviour {
    GameObject target;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Cinemachine.CinemachineStateDrivenCamera a = this.gameObject.GetComponent<Cinemachine.CinemachineStateDrivenCamera>();
        a.m_AnimatedTarget = target.GetComponent<Animator>();
    }

    public void LoCat(GameObject A)
    {
        target = A;
    }
}
