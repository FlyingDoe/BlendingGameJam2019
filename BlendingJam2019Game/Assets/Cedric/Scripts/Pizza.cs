using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour {

    public bool isTurning;
    public float turningSpeed = 5;
    public float stoppedSpeed = 0.25f;

	// Use this for initialization
	void Start () {
        isTurning = true;
	}
	public void StartTurning()
    {
        isTurning = true;
    }

    public void StopTurning()
    {
        StartCoroutine(DelayPizzaRestart(20));
    }

    IEnumerator DelayPizzaRestart(int time)
    {
        isTurning = false;
        yield return new WaitForSeconds(time);
        isTurning = true;
    }

    // Update is called once per frame
    void Update () {
        if(isTurning)transform.Rotate(Vector3.up * (turningSpeed * Time.deltaTime));
        else transform.Rotate(Vector3.up * (stoppedSpeed * Time.deltaTime));

        if (Input.GetKeyDown(KeyCode.B)) StartTurning();
        if (Input.GetKeyDown(KeyCode.N)) StopTurning();
    }
}
