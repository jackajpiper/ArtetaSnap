using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // private int highScore = -1;
    // private bool hasCompletedOnce = false;
    public DeckController DeckController;
    
    public GameObject celebrations;
    public GameObject UIObj;
    private UIController UI;
    private bool paused = false;

    private int direction = -1;
    private bool isRestarting = false;
    private bool isResetting = false;

    // Start is called before the first frame update
    void Start() {
        UI = UIObj.GetComponent<UIController>();
    }

    public void OnFinish() {
        paused = true;
        DeckController.LockDeck();
        UI.PauseTimer();
        celebrations.GetComponent<CelebrationManager>().OnShow();
    }

    public void BeginRestart() {
        isRestarting = true;
        UI.Reset();
        celebrations.GetComponent<CelebrationManager>().OnHide();
    }
    void EndRestart() {
        isRestarting = false;
        isResetting = false;
        direction *= -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (paused) { // game has finished, or the menu has been pulled up
            if (isRestarting) {
                transform.Translate(15 * Time.deltaTime * direction, 0, 0);
                var positionComparison = direction == -1
                    ? transform.position.x > -0.3
                    : transform.position.x < -0.3;
                if (positionComparison && !isResetting) {
                    DeckController.ResetDeck();
                    isResetting = true;
                }

                var endComparison = direction == -1
                    ? transform.position.x > 17
                    : transform.position.x < -17;
                if (endComparison) {
                    EndRestart();
                }
            } else if (Input.GetKeyDown("r")) { // 'restart' for now
                BeginRestart();
            }
        }
    }
}
