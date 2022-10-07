using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class data : MonoBehaviour
{
    void Start() { DontDestroyOnLoad(gameObject); }
    public Texture2D screenShot;
    public int score;
}
