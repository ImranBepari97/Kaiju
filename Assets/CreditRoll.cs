using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditRoll : MonoBehaviour {

    private const float START_Y = -3481.0f, END_Y = 2354f, DURATION = 30.0f;

    private float timeElapsed;

    void Awake()
    {
        timeElapsed = 0;
    }

    // Update is called once per frame
    void Update () {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > DURATION)
        {
            timeElapsed = 0;
            return;
        } else
        {
            Vector3 v = this.transform.localPosition;
            Vector3 s = new Vector3(v.x, START_Y, v.z);
            Vector3 e = new Vector3(v.x, END_Y, v.z);

            this.transform.localPosition = Vector3.Lerp(s, e, timeElapsed / DURATION);
            return;
        }
	}
}
