using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCat : MonoBehaviour {
    GameObject target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       Cinemachine.CinemachineVirtualCamera a= this.gameObject.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        a.Follow = target.transform;
        a.LookAt = target.transform;
	}

    public void Selcat(GameObject A)
    {
        target = A;
    }
}
