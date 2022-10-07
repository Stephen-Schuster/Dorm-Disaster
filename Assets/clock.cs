using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class clock : MonoBehaviour
{
    public ParticleSystem animeLines;
    public RawImage ri;
    public float startingTime = 60;
    public float timer = 60;
    public float boost;
    public TextMeshProUGUI text;
    // Update is called once per frame
    void Start()
    {
        int diff = PlayerPrefs.GetInt("difficulty", 1);
        if (diff == 0)
        {
            timer = 100000000;
            text.enabled = false;
        }
        else if (diff == 1)
        {
            timer = 600;
            boost = 45;
        }
        else if (diff == 2)
        {
            timer = 300;
            boost = 20;
        }
        else if (diff == 3)
        {
            timer = 175;
            boost = 5;
        }
        if(PlayerPrefs.GetInt("gameMode", 1) == 2)boost = 0;
    }
    void Update()
    {
        if (timer < 30)
        {
            if (!animeLines.gameObject.activeSelf) animeLines.gameObject.SetActive(true);
             var main = animeLines.main;
            main.startSpeed = 50 - timer;
            animeLines.gameObject.transform.localPosition = new Vector3(0, 0, -5 - timer * 8f / 30);
        }
        else animeLines.gameObject.SetActive(false);
        timer -= Time.smoothDeltaTime;
        ri.enabled = (timer % 2 < 1);
        text.text = (int)timer+"";
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Floor((timer % 8) / 2) * 90);
    }
}
