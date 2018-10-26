using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class texiao_jineng1_liuxing : MonoBehaviour {
    private ParticleSystem.ShapeModule shape;
    // Use this for initialization
    void Start()
    {
        shape = this.gameObject.GetComponent<ParticleSystem>().shape;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
            return;
        if (shape.radius < 10)
            shape.radius += 10 * Time.deltaTime;
    }
}
