using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class managerScript : MonoBehaviour
{
    public Transform collectables;
    public int scoreNeeded;
    public float winTime = -1;
    public GameObject[] inBin;
    public clock timer;
    public int score = 0;
    public data data;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI itemText;
    public AudioSource song;
    public AudioSource[] sfxSources;
    // Start is called before the first frame update
    void Start()
    {
        inBin = new GameObject[5];
        for (int i = 0; i < inBin.Length; i++) inBin[i] = null;
        itemText.text = collectables.GetChild((int)Random.Range(0, collectables.childCount)).gameObject.name;

        float sfxv = PlayerPrefs.GetFloat("sfxv", 50);
        
        song.volume = PlayerPrefs.GetFloat("musicv", 50) / 100f;
        for(int i = 0; i<sfxSources.Length; i++)sfxSources[i].volume = sfxv / 100f;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer.timer<0 && winTime == -1)
        {
            data.screenShot = ScreenCapture.CaptureScreenshotAsTexture();
            SceneManager.LoadScene(1);
        }
        if (score >= scoreNeeded && winTime == -1 && PlayerPrefs.GetInt("gameMode", 1) == 2)
        {
            if (PlayerPrefs.GetInt("difficulty", 0) != 0) winTime = timer.timer;
            else
            {
                data.screenShot = ScreenCapture.CaptureScreenshotAsTexture();
                SceneManager.LoadScene(2);
            }
        }
        if (timer.timer < winTime - 1)
        {
            data.screenShot = ScreenCapture.CaptureScreenshotAsTexture();
            SceneManager.LoadScene(2);
        }
        scoreText.text = "Score: " + score;
        data.score = score;
    }
    public AudioSource scoreSound;
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 10 && other.gameObject.GetComponent<collectable>() != null)
        {
            itemText.text = "Error";
            for (int i = 0; i < 100; i++)
            {
                int r = (int)Random.Range(0, collectables.childCount);
                if(!collectables.GetChild(r).GetComponent<collectable>().scored)
                {
                    itemText.text = collectables.GetChild(r).gameObject.name;
                    break;
                }
            }
            other.gameObject.GetComponent<collectable>().scored = true;
            score++;
            timer.timer += timer.boost;
            scoreSound.Play();
            int nullI = -1;
            float lowestY = 100f;
            int lowestYI = -1;
            for (int i = 0; i < inBin.Length; i++)
            {
                if(inBin[i] == null)
                {
                    nullI = i;
                    break;
                } else if(inBin[i].transform.position.y < lowestY)
                {
                    lowestY = inBin[i].transform.position.y;
                    lowestYI = i;
                }
            }
            if(nullI == -1)
            {
                if(PlayerPrefs.GetInt("gameMode",1) == 2)Destroy(inBin[lowestYI]);
                if (PlayerPrefs.GetInt("gameMode", 1) == 1)
                {
                    score++;
                    timer.boost *= 0.8f;
                    timer.timer += timer.boost;
                    inBin[lowestYI].GetComponent<collectable>().ResetPos();
                    inBin[lowestYI].GetComponent<collectable>().scored = false;
                }
            } else
            {
                inBin[nullI] = other.gameObject;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 10 && other.gameObject.GetComponent<collectable>() != null)
        {
            score--;
            timer.timer -= timer.boost;
            for (int i = 0; i < inBin.Length; i++)
            {
                if (inBin[i] == other.gameObject)
                {
                    inBin[i] = null;
                    return;
                }
            }
            Debug.Log("what");
        }
    }
}
