using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private Image[] lives;
    [SerializeField] private Sprite fullLive;
    [SerializeField] private int health;
    [SerializeField] private int numberOfLives;

    private void Update()
    {
        for (int i = 0; i < lives.Length; i++)
        {
            if (i < numberOfLives)
            {
                lives[i].enabled = true;
            }
            else 
            {
                lives[i].enabled = false;
            }
        }
    }
}
