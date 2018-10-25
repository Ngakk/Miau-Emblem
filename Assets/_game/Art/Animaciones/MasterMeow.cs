using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterMeow : MonoBehaviour {
    public LookCat Looker;
    public SelectCat Selecter;
    public GameObject THECAT;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Selecter.Selcat(THECAT);
        Looker.LoCat(THECAT);
	}
}
