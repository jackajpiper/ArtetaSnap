using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextController : MonoBehaviour
{
    public float speed = 15f;
    bool isRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Appear() {
        gameObject.SetActive(true);
    }

    public void RunAway() {
        isRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning) {
            transform.Translate(0, 1f * speed * Time.deltaTime, 0, Space.World);
            if (transform.position.y > 18) {
                gameObject.SetActive(false);
                transform.position = new Vector3(
                    transform.position.x,
                    11f,
                    transform.position.z
                );
                isRunning = false;
            }
        }
    }
}
