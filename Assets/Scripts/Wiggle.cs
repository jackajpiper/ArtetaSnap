using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour
{
    public float speed = 40f;
    private int direction = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(transform.eulerAngles.y);
        transform.Rotate(0, 0, speed * Time.deltaTime * direction);
        transform.Translate(0, -2 * Time.deltaTime * direction, 0);
        if ((transform.eulerAngles.z <= 350 && transform.eulerAngles.z > 345) || (transform.eulerAngles.z >= 10 && transform.eulerAngles.z < 15)) {
            direction *= -1;
        }
    }
}
