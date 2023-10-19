using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DeckController : MonoBehaviour
{
    public GameManager GameManager;
    public GameObject card;
    public List<GameObject> cards;
    int rowNum = 3;
    int columnNum = 6;
    float xIncrement = 2.7f;
    float yIncrement = -3.5f;
    Vector3 startPos = new Vector3(-6.8f, 6.8f, 0);

    int artetaCount = 99;
    public Texture[] artetas;
    public Texture[] selectedArtetas;

    // stuff for handling card flips
    GameObject bankedArteta;
    bool bankedFlipping1 = false; // true if a card is already flipping
    bool bankedFlipping2 = false; // true if a card is already flipping

    // stuff for handling game state
    bool hasBegun = false;
    int score = 0;
    bool finished = false;
    public GameObject UIObj;
    private UIController UI;


    // put here because I don't have a more general script
    void Awake () {
        #if UNITY_EDITOR
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 60;
        #endif
    }

    // Start is called before the first frame update
    void Start()
    {
        UI = UIObj.GetComponent<UIController>();
        SetupDeck();
    }

    public void ResetBanked() {
        bankedArteta = null;
        bankedFlipping1 = false;
        bankedFlipping2 = false;
    }

    public void StartFlip() {
        if (!hasBegun) {
            hasBegun = true;
            UI.BeginTimer();
        }

        if (!bankedFlipping1) {
            bankedFlipping1 = true;
        } else {
            bankedFlipping2 = true;
        }
    }

    public bool CanFlip() {
        return !bankedFlipping1 || !bankedFlipping2;
    }

    public void OnCardFlip(GameObject card, int artetaIndex, bool faceUp) {

        var bankedTexture = bankedArteta?.transform.GetChild(1).GetComponent<MeshRenderer>().material.GetTexture("_MainTex");
        var cardTexture = card.transform.GetChild(1).GetComponent<MeshRenderer>().material.GetTexture("_MainTex");

        if (bankedTexture == null) {
            bankedArteta = card;
        } else if (bankedTexture == cardTexture) {
            // MATCH FOUND!
            card.GetComponent<CardController>().LockCard();
            bankedArteta.GetComponent<CardController>().LockCard();
            IncrementScore(true);
        } else {
            bankedArteta.GetComponent<CardController>().TriggerFlip();
            card.GetComponent<CardController>().TriggerFlip();
            IncrementScore(false);
        }
    }

    void IncrementScore(bool matchFound) {
        if (matchFound) {
            score++;
        }
        UI.IncrementFlips();
        ResetBanked();
    }

    void Shuffle(Texture[] arr)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < arr.Length; t++ )
        {
            Texture tmp = arr[t];
            int r = Random.Range(t, arr.Length);
            arr[t] = arr[r];
            arr[r] = tmp;
        }
    }

    void SetupDeck() {
        hasBegun = false;
        ResetBanked();
        Shuffle(artetas);
        artetaCount = (rowNum * columnNum) / 2;
        selectedArtetas = artetas.Take(artetaCount).ToArray();
        selectedArtetas = selectedArtetas.Concat(selectedArtetas).ToArray();
        Shuffle(selectedArtetas);

        // populates down-facing cards to begin the game
        var artetaIndex = 0;
        for (int i = 0; i < rowNum; i++) {
            for (int j = 0; j < columnNum; j++)
            {
                var position = new Vector3(
                    startPos.x + (xIncrement * j),
                    startPos.y + (yIncrement * i),
                    startPos.z
                );
                GameObject newCard = Instantiate(card, position, card.transform.rotation);
                newCard.GetComponent<CardController>().SetArteta(artetaIndex, selectedArtetas[artetaIndex]);
                cards.Add(newCard);
                artetaIndex++;
            }
        }
        finished = false;
        score = 0;
    }

    public void LockDeck() {
        foreach(var c in cards) {
            c.GetComponent<CardController>().LockCard();
        }
    }

    public void ResetDeck() {
        Debug.Log("resetting deck");
        foreach(var c in cards) {
            Destroy(c);
        }
        cards = new List<GameObject>();
        SetupDeck();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (score >= artetaCount && !finished) { // GAME HAS BEEN WON!!!
            finished = true;
            GameManager.OnFinish();
        }

        if (Input.GetKeyDown("f"))
        {
            Debug.Log("skipped to finish");
            score = 9;
        }
    }
}
