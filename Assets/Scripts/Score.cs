﻿using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Transform player;
    public Text scoreText;

    // Update is called once per frame
    void Update()
    {
       scoreText.text = (Mathf.Floor(player.position.z + 52)).ToString();
    }
}