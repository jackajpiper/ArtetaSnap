using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetController : MonoBehaviour
{
    public GameManager gameManager;
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

    void OnMouseDown() {
        gameManager.GetComponent<GameManager>().BeginRestart();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, -0.5f * speed * Time.deltaTime, 0);
        if (isRunning) {
            transform.Translate(0, -0.5f * speed * Time.deltaTime, 0, Space.World);
            if (transform.position.y < -5) {
                gameObject.SetActive(false);
                transform.position = new Vector3(
                    transform.position.x,
                    0,
                    transform.position.z
                );
                isRunning = false;
            }
        }
    }
}
