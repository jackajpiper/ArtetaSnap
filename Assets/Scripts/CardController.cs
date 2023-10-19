using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public DeckController deckController;
    private bool atRest = true;
    private bool movingUp = false;
    private bool rotating = false;
    private float startingYRotation;
    private bool movingDown = false;
    bool faceUp = false;
    bool isLocked = false;
    int artetaIndex;
    // Start is called before the first frame update
    void Start()
    {
        deckController = GameObject.Find("DeckController").GetComponent<DeckController>();
    }

    public void SetArteta(int index, Texture arteta) {
        artetaIndex = index;
        var front = transform.GetChild(1);
        var material = front.GetComponent<MeshRenderer>().material;
        material.SetTexture("_MainTex", arteta);
    }

    public void LockCard() {
        isLocked = true;
    }

    void OnMouseDown() {
        if (atRest && !isLocked && !faceUp && deckController.CanFlip()) {
            deckController.StartFlip();
            TriggerFlip();
        }
    }

    public void TriggerFlip() {
        StartCoroutine(FlipCard());
    }

    IEnumerator FlipCard() {
        atRest = false;
        faceUp = !faceUp;
        movingUp = true;

        while (!atRest) {
            CardMovementCoroutine();
            yield return null;
        }

        if (faceUp) {
            deckController.OnCardFlip(gameObject, artetaIndex, faceUp);
        }
    }

    void CardMovementCoroutine() {
        var direction = faceUp ? 1 : -1;
        if (movingUp) {
            transform.Translate(0, 0, 3 * Time.deltaTime * direction);
            if (transform.position.z <= -1) {
                movingUp = false;
                rotating = true;
                startingYRotation = transform.eulerAngles.y;
            }
        } else if (rotating) {
            transform.Rotate(0, 360 * Time.deltaTime * direction, 0);

            if (!faceUp) {
                if (transform.eulerAngles.y <= 180 && transform.eulerAngles.y > 1) {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    rotating = false;
                    movingDown = true;
                }
            } else {
                if (transform.eulerAngles.y > 358 || transform.eulerAngles.y < 2) {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    rotating = false;
                    movingDown = true;
                }
            }

            if (transform.eulerAngles.y > (startingYRotation-0.5) && transform.eulerAngles.y < startingYRotation) {
                transform.rotation = Quaternion.Euler(0, startingYRotation, 0);
                rotating = false;
                movingDown = true;
            }
        } else if (movingDown) {
            transform.Translate(0, 0, 3 * Time.deltaTime * direction);
            if (transform.position.z >= 0) {
                transform.position = new Vector3(
                    transform.position.x,
                    transform.position.y,
                    0
                );
                movingDown = false;
                atRest = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
