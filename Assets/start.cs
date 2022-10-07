using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class start : MonoBehaviour
{
    public float startAnimTimer = 0, totalStartAnimTime = 2;
    bool starting = true;
    public Transform cam;
    public clock timer;
    public playerMove pm;
    public playerLook pl;
    public hookShooting hs;
    public GameObject canv;

    // Update is called once per frame
    void Update()
    {
        if(starting)
        {
            timer.enabled = false;
            pm.enabled = false;
            pl.enabled = false;
            hs.enabled = false;
            canv.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, Mathf.Lerp(canv.GetComponent<RectTransform>().sizeDelta.y, 0, startAnimTimer / totalStartAnimTime));
            cam.localPosition = new Vector3(0, Mathf.Lerp(5f, 0.9f, startAnimTimer / totalStartAnimTime),Mathf.Lerp(19.13f, 0, startAnimTimer / totalStartAnimTime));
            cam.GetChild(0).localPosition = new Vector3(0, Mathf.Lerp(-4.1f, 0, startAnimTimer / totalStartAnimTime), Mathf.Lerp(-19.13f, 0, startAnimTimer / totalStartAnimTime));
            cam.localRotation = Quaternion.Euler(Mathf.Lerp(90, 0, startAnimTimer / totalStartAnimTime), 0, 0);
            cam.GetChild(0).localRotation = Quaternion.Euler(Mathf.Lerp(-90, 0, startAnimTimer / totalStartAnimTime), 0, 0);
            if (startAnimTimer > totalStartAnimTime)
            {
                cam.localPosition = new Vector3(0, 0.9f, 0);
                cam.localRotation = Quaternion.Euler(0, 0, 0);
                cam.GetChild(0).localPosition = new Vector3(0, 0, 0);
                cam.GetChild(0).localRotation = Quaternion.Euler(0, 0, 0);
                starting = false;
                timer.enabled = true;
                pm.enabled = true;
                pl.enabled = true;
                hs.enabled = true;
            }
            startAnimTimer += Time.deltaTime;
        }
    }
}
