using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public float time = 0;
    public TMP_Text timeText;
    public int flips = 0;
    public TMP_Text flipText;
    bool isTiming = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void BeginTimer() {
        isTiming = true;
    }

    public void PauseTimer() {
        isTiming = false;
    }

    public void Reset() {
        isTiming = false;
        flips = 0;
        flipText.text = "Flips:";
        time = 0;
        timeText.text = "Time:";
    }

    public void IncrementFlips() {
        flips += 1;
        flipText.text = "Flips: " + flips;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTiming) {
            time += Time.deltaTime;
            timeText.text = "Time: " + time.ToString("F2");
        }
    }
}
