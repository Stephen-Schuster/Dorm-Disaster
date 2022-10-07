using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class moveSphere : MonoBehaviour
{
    public float speed;
    public GameObject[] gos;
    public GameObject cube;
    public Material baseMat;
    public AudioSource end;
    public bool played = false;
    public float dist;
    public GameObject text;
    float moveTextDist = -5;
    float transitionDist = -8;
    float textSpeed = 5;
    public Transform scale;
    public RectTransform canv;
    public TextMeshProUGUI scoreText;
    void Start()
    {
        scale.localScale = new Vector3(canv.sizeDelta.x / canv.sizeDelta.y / (13f / 8), 1, 1);
        end.volume = PlayerPrefs.GetFloat("sfxv", 50)*0.3f;
        GameObject data = GameObject.Find("data");
        scoreText.text = "Score: "+data.GetComponent<data>().score;
        Texture2D texture = data.GetComponent<data>().screenShot;
        int texWidth = texture.width;
        int texHeight = texture.height;
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 13; x++)
            {
                Color[] pixels = texture.GetPixels(x * texWidth / 13, y * texHeight / 8, texWidth / 13, texHeight / 8);
                Texture2D newtex = new Texture2D(texWidth / 13, texHeight / 8);
                //newtex.filterMode = FilterMode.Point;

                newtex.SetPixels(pixels);
                newtex.Apply();
                Material newMat = new Material(baseMat);
                newMat.mainTexture = newtex;
                gos[y].transform.GetChild(x).gameObject.GetComponent<MeshRenderer>().material = newMat;
            }
        }
        Destroy(data);
    }
    // Update is called once per frame
    void Update()
    {
        transform.position -= speed * Time.deltaTime * new Vector3(0,0,1);
        if (!played && transform.position.z < dist)
        {
            end.Play();
            played = true;
            cube.SetActive(false);
        }
        if(transform.position.z < moveTextDist)
        {
            text.transform.position += new Vector3(0, textSpeed * Time.deltaTime, 0);
            scoreText.gameObject.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 100 * Time.deltaTime);
        }
        if (transform.position.z < transitionDist)
        {
            SceneManager.LoadScene(0);
        }
    }
}
