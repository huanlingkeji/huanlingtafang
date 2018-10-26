using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class base_jineng : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void Create()
    {

    }
    public virtual void Prepare()
    {

    }
    public virtual bool IsReady()
    {
        return false;
    }
    public virtual void Cancle()
    {

    }
}
