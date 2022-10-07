using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuScript : MonoBehaviour
{
    public float mouseSens, sfxv, musicv;
    public Slider mss, svs, mvs;
    public AudioSource gameSong, button;
    public Light luz;
    public int state; //0 is neutral + 3 states for each side(going out, staying, coming back in) so 13 total
    public float totalAnimTime = 1, currAnimTime = 0;
    public GameObject stuff1, stuff2;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        mouseSens = PlayerPrefs.GetFloat("mouseSens", 100);
        sfxv = PlayerPrefs.GetFloat("sfxv", 50);
        musicv = PlayerPrefs.GetFloat("musicv", 50);
        mss.value = mouseSens;
        svs.value = sfxv;
        mvs.value = musicv;
    }

    // Update is called once per frame
    void Update()
    {
        stuff2.transform.parent.gameObject.GetComponent<RectTransform>().localScale = new Vector3(stuff2.transform.parent.parent.gameObject.GetComponent<RectTransform>().sizeDelta.y / 522.5f, stuff2.transform.parent.parent.gameObject.GetComponent<RectTransform>().sizeDelta.y / 522.5f, stuff2.transform.parent.parent.gameObject.GetComponent<RectTransform>().sizeDelta.y / 522.5f);
        mouseSens = mss.value;
        sfxv = svs.value;
        musicv = mvs.value;
        PlayerPrefs.SetFloat("mouseSens", mouseSens);
        PlayerPrefs.SetFloat("sfxv", sfxv);
        PlayerPrefs.SetFloat("musicv", musicv);
        gameSong.volume = musicv / 100f;
        button.volume = sfxv / 100f;
        if(state==4)
        {
            stuff1.transform.position = new Vector3(currAnimTime / totalAnimTime * -15, 0, 0);
            stuff2.GetComponent<RectTransform>().anchoredPosition = new Vector3(currAnimTime / totalAnimTime * -4000, 0, 0);
            currAnimTime += Time.smoothDeltaTime;
            if (currAnimTime>totalAnimTime)
            {
                state += 2;
                stuff1.transform.position = new Vector3(-15, 0, 0);
                stuff2.GetComponent<RectTransform>().anchoredPosition = new Vector3(-4000, 0, 0);
                currAnimTime = 0;
            }
        }
        if (state == 1)
        {
            stuff1.transform.position = new Vector3(currAnimTime / totalAnimTime * 15, 0, 0);
            stuff2.GetComponent<RectTransform>().anchoredPosition = new Vector3(currAnimTime / totalAnimTime * 4000, 0, 0);
            currAnimTime += Time.smoothDeltaTime;
            if (currAnimTime > totalAnimTime)
            {
                state += 2;
                stuff1.transform.position = new Vector3(15, 0, 0);
                stuff2.GetComponent<RectTransform>().anchoredPosition = new Vector3(4000, 0, 0);
                currAnimTime = 0;
            }
        }
        if (state == 5)
        {
            stuff1.transform.position = new Vector3(-15+currAnimTime / totalAnimTime * 15, 0, 0);
            stuff2.GetComponent<RectTransform>().anchoredPosition = new Vector3(-4000+currAnimTime / totalAnimTime * 4000, 0, 0);
            currAnimTime += Time.smoothDeltaTime;
            if (currAnimTime > totalAnimTime)
            {
                state = 0;
                stuff1.transform.position = new Vector3(0, 0, 0);
                stuff2.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
                currAnimTime = 0;
            }
        }
        if (state == 2)
        {
            stuff1.transform.position = new Vector3(15 - currAnimTime / totalAnimTime * 15, 0, 0);
            stuff2.GetComponent<RectTransform>().anchoredPosition = new Vector3(4000 - currAnimTime / totalAnimTime * 4000, 0, 0);
            currAnimTime += Time.smoothDeltaTime;
            if (currAnimTime > totalAnimTime)
            {
                state = 0;
                stuff1.transform.position = new Vector3(0, 0, 0);
                stuff2.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
                currAnimTime = 0;
            }
        }
        if (state == 7)
        {
            stuff1.transform.position = new Vector3(0, currAnimTime / totalAnimTime * 15, 0);
            stuff2.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,currAnimTime / totalAnimTime * 4000,0);
            currAnimTime += Time.smoothDeltaTime;
            if (currAnimTime > totalAnimTime)
            {
                state += 2;
                stuff1.transform.position = new Vector3(0, 15,  0);
                stuff2.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,4000,0);
                currAnimTime = 0;
            }
        }
        if (state == -1)
        {
            stuff1.transform.position = new Vector3(0, -5+currAnimTime / totalAnimTime * 5, 0);
            stuff2.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -500+currAnimTime / totalAnimTime * 500, 0);
            currAnimTime += Time.deltaTime;
            if (currAnimTime > totalAnimTime)
            {
                state = 0;
                stuff1.transform.position = new Vector3(0, 0, 0);
                stuff2.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
                currAnimTime = 0;
            }
        }
        if (state == 10)
        {
            stuff1.transform.position = new Vector3(0, currAnimTime / totalAnimTime * 15, 0);
            stuff2.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, currAnimTime / totalAnimTime * 4000, 0);
            currAnimTime += Time.smoothDeltaTime;
            if (currAnimTime > totalAnimTime)
            {
                state += 2;
                stuff1.transform.position = new Vector3(0, 15, 0);
                stuff2.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 4000, 0);
                currAnimTime = 0;
            }
        }
        if (state == 8)
        {
            stuff1.transform.position = new Vector3(0, 15 - currAnimTime / totalAnimTime * 15,  0);
            stuff2.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 4000 - currAnimTime / totalAnimTime * 4000,  0);
            currAnimTime += Time.smoothDeltaTime;
            if (currAnimTime > totalAnimTime)
            {
                state = 0;
                stuff1.transform.position = new Vector3(0, 0, 0);
                stuff2.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
                currAnimTime = 0;
            }
        }
        if (state == 100)
        {
            stuff1.transform.position = new Vector3(0, 15 + currAnimTime / totalAnimTime * 15, 0);
            stuff2.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 4000 + currAnimTime / totalAnimTime * 4000, 0);
            luz.color = new Color(Mathf.Lerp(1, 164f / 256, currAnimTime / totalAnimTime),Mathf.Lerp(1, 166f / 256, currAnimTime / totalAnimTime),Mathf.Lerp(1, 188f / 256, currAnimTime / totalAnimTime));
            currAnimTime += Time.smoothDeltaTime;
            if (currAnimTime > totalAnimTime)
            {
                SceneManager.LoadScene(3);
            }
        }
        if (state == 9) PlayerPrefs.SetInt("gameMode", 1);
        if (state == 12) PlayerPrefs.SetInt("gameMode", 2);
    }
    public void changeState(int newState)
    {
        state = newState;
        button.Play();
    }
    public void startGame(int difficulty)
    {
        PlayerPrefs.SetInt("difficulty", difficulty);
        state = 100;
    }
}
