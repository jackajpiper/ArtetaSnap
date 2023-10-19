using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelebrationManager : MonoBehaviour
{
    public GameObject textObj;
    public GameObject resetObj;
    TextController textController;
    ResetController resetController;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        textController = textObj.GetComponent<TextController>();
        resetController = resetObj.GetComponent<ResetController>();
    }

    public void OnHide() {
        textController.RunAway();
        resetController.RunAway();
    }

    public void OnShow() {
        textController.Appear();
        resetController.Appear();
        gameObject.SetActive(true);
    }

    // public void SetActive(bool active) {
    //     gameObject.SetActive(active);
    // }

    // Update is called once per frame
    void Update()
    {
        
    }
}
