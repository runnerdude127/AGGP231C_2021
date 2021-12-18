using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<Fish> fishList;
    public GameObject pond;
    public GameObject rod;
    public GameObject winText;
    public GameObject loseText;

    int gameState;
    public GameObject idleUI;
    public GameObject fishingUI;
    public GameObject reelingUI;
    public GameObject catchUI;
    CatchData catchD;

    public Slider reelMeter;
    public TMP_Text topMessage;

    float powerMeter;

    bool pondState = false;
    const int IDLE = 0;
    const int FISHING = 1;
    const int REELING = 2;
    const int CATCH = 3;

    Scene scene;

    public Animator anim;

    private void Awake()
    {
        instance = this;
        scene = SceneManager.GetActiveScene();
        anim = rod.GetComponent<Animator>();
        catchD = catchUI.GetComponent<CatchData>();
        catchUI.SetActive(false);

        gameState = IDLE;
    }

    private void Update()
    {
        //pondState = true;
        if (pondState == true)
        {
            if (gameState == IDLE || gameState == FISHING)
            {
                if (Input.GetMouseButton(0))
                {
                    if (gameState != FISHING)
                    {
                        gameState = FISHING;
                        castLine();
                    }
                }
                else
                {
                    if (gameState != IDLE)
                    {
                        gameState = IDLE;
                        retract();
                    }
                }
            }
            else if (gameState == REELING)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    reel();
                }
            }
            
            if (gameState == IDLE)
            {
                idleUI.SetActive(true);
                fishingUI.SetActive(false);
                reelingUI.SetActive(false);
            }
            else if (gameState == FISHING)
            {
                idleUI.SetActive(false);
                fishingUI.SetActive(true);
                reelingUI.SetActive(false);
            }
            else if (gameState == REELING)
            {
                idleUI.SetActive(false);
                fishingUI.SetActive(false);
                reelingUI.SetActive(true);
            }
            else if (gameState == CATCH)
            {
                idleUI.SetActive(false);
                fishingUI.SetActive(false);
                reelingUI.SetActive(false);
            }
            else
            {
                idleUI.SetActive(false);
                fishingUI.SetActive(false);
                reelingUI.SetActive(false);
            }
        }
        else if (pondState == false)
        {
            idleUI.SetActive(false);
            fishingUI.SetActive(false);
            reelingUI.SetActive(false);
            gameState = IDLE;
        }
        

        reelMeter.value = powerMeter;
    }

    public void castLine()
    {
        anim.SetTrigger("StartFishing");
        StartCoroutine(waitForFish());
    }

    public void retract()
    {
        anim.SetTrigger("StopFishing");
    }

    IEnumerator waitForFish()
    {
        Debug.Log("waiting...");
        int getfish = 0;
        yield return new WaitForSeconds(Random.Range(3f, 5f));
        getfish = Random.Range(1, 3);
        if (pondState == true && gameState == FISHING)
        {
            if (getfish == 2)
            {
                Debug.Log("FISH TIME");
                anim.SetTrigger("FishBiting");
                beginReel(getFish());
            }
            else
            {
                Debug.Log("wait longer");
                StartCoroutine(waitForFish());
            }
        } 
    }

    void beginReel(Fish fishGetting)
    {
        gameState = REELING;
        powerMeter = 50;
        StartCoroutine(reelMinigame(fishGetting));
    }

    IEnumerator reelMinigame(Fish fishOpponent)
    {
        yield return new WaitForSeconds((10 - fishOpponent.strength) / 25);
        if (pondState == true)
        {
            powerMeter = powerMeter - 2;
            if (powerMeter == 0)
            {
                Debug.Log("FAILED: " + fishOpponent.name);
                gameState = IDLE;
            }
            else if (powerMeter >= 99)
            {
                anim.SetTrigger("CatchFish");
                Debug.Log("Success! Got: " + fishOpponent.name);
                catchD.setFishData(fishOpponent);
                gameState = CATCH;
                catchUI.SetActive(true);
            }
            else
            {
                if (powerMeter <= 50)
                {
                    anim.SetBool("Losing", true);
                }
                else
                {
                    anim.SetBool("Losing", false);
                }
                StartCoroutine(reelMinigame(fishOpponent));
            }
        }
    }

    public void reel()
    {
        powerMeter += 2;
    }

    public void pondFound()
    {
        topMessage.text = "";
        pondState = true;
    }

    public void pondLost()
    {
        if (gameState == IDLE)
        {
            topMessage.text = "Find a pond using an Image Target!";
        }
        else if (gameState == FISHING)
        {
            topMessage.text = "You pulled your line out of the pond! Find a pond again.";
        }
        else if (gameState == REELING)
        {
            topMessage.text = "Your line snapped. Find a pond again.";
        }
        else
        {
            topMessage.text = "Pond lost. Find a pond again.";
        }
        pondState = false;   
    }

    public void okayButton()
    {
        anim.SetTrigger("PutAwayFish");
        catchUI.SetActive(false);
        gameState = IDLE;
    }

    public void winCondition()
    {
        winText.SetActive(true);
    }

    public void loseCondition()
    {
        loseText.SetActive(true);
    }

    public void restartScene()
    {
        SceneManager.LoadScene(scene.name);
    }

    public Fish getFish()
    {
        List<Fish> commonFish = new List<Fish>();
        List<Fish> uncommonFish = new List<Fish>();
        List<Fish> rareFish = new List<Fish>();
        List<Fish> superFish = new List<Fish>();
        List<Fish> legendaryFish = new List<Fish>();

        foreach (Fish fishy in fishList)
        {
            if (fishy.rarity == 1)
            {
                commonFish.Add(fishy);
            }
            else if (fishy.rarity == 2)
            {
                uncommonFish.Add(fishy);
            }
            else if (fishy.rarity == 3)
            {
                rareFish.Add(fishy);
            }
            else if (fishy.rarity == 4)
            {
                superFish.Add(fishy);
            }
            else if (fishy.rarity == 5)
            {
                legendaryFish.Add(fishy);
            }
            else
            {
                Debug.LogError("Fish with weird rarity found: " + fishy.name);
            }
        }

        while (1 == 1)
        {
            float rngNum = Random.Range(1f, 100f);
            Debug.Log("Got " + rngNum);
            if (rngNum >= 95 && rngNum < 101)
            {
                if (legendaryFish.Count > 0)
                {
                    return legendaryFish[Random.Range(0, legendaryFish.Count)];
                }
            }
            else if (rngNum >= 85 && rngNum < 95)
            {
                if (superFish.Count > 0)
                {
                    return superFish[Random.Range(0, superFish.Count)];
                }  
            }
            else if (rngNum >= 70 && rngNum < 85)
            {
                if (rareFish.Count > 0)
                {
                    return rareFish[Random.Range(0, rareFish.Count)];
                }
            }
            else if (rngNum >= 50 && rngNum < 70)
            {
                if (uncommonFish.Count > 0)
                {
                    return uncommonFish[Random.Range(0, uncommonFish.Count)];
                }                   
            }
            else if (rngNum >= 1 && rngNum < 50)
            {
                if (commonFish.Count > 0)
                {
                    return commonFish[Random.Range(0, commonFish.Count)];
                }     
            }

            Debug.Log("Yielded no fish.");
        }
    }
}


