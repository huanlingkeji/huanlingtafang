using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sMove : MonoBehaviour
{
	private static int MAX_Z = 235;
	private static int MIN_Z = 0;

	private float speed;
	private float initspeed;
	private List<float> multiple = new List<float>();
	private List<float> plus = new List<float>();

	private int direction=1;

	void Start()
	{
		initspeed = 10f;
		speed = initspeed;
	}

	// Update is called once per frame
	void Update()
	{
        if (Time.timeScale == 0)
            return;
        transform.Translate(0, 0, Time.deltaTime*speed);
		if (transform.position.z > MAX_Z) {
			direction = -1;
			setSpeed ();
		}
		if (transform.position.z < MIN_Z) {
			direction = 1;
			setSpeed ();
		}
	}

	private void setSpeed()
	{
		speed = initspeed;
		for (int i = 0; i < plus.Count; i++)
			speed += plus[i];
		for (int i = 0; i < multiple.Count; i++)
			speed *= multiple[i];
		speed *= direction;
	}
	public void changeSpeed(int type, float value, float keep_time)
	{
		if (type == 0)
			multiple.Add(value);
		else
			plus.Add(value);
		setSpeed();
		StartCoroutine(subSpeed(type, value, keep_time));
	}
	IEnumerator subSpeed(int type, float value, float keep_time)
	{
		yield return new WaitForSeconds(keep_time);
		if (type == 0)
			multiple.Remove(value);
		else
			plus.Remove(value);
		setSpeed();
	}
}
