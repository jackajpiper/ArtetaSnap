using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waggle : MonoBehaviour
{
    public float speed = 60f;
    private int direction = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(transform.eulerAngles.y);
        transform.Rotate(0, speed * Time.deltaTime * direction, 0);
        transform.Translate(0, 0, 2 * Time.deltaTime * direction);
        if ((transform.eulerAngles.y <= 350 && transform.eulerAngles.y > 325) || (transform.eulerAngles.y >= 10 && transform.eulerAngles.y < 25)) {
            direction *= -1;
        }
    }
}
