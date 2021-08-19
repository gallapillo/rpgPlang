using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManeger : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfFullHeart;
    public Sprite emptyHeart;
    public Sprite quaterHeart;
    public Sprite threeFourthHeart;
    public FloatValue heartContainers;
    public FloatValue playerCurrentHeath;

    // Start is called before the first frame update
    void Start()
    {
        InitHearts();
    }

    private void Update()
    {
        float tempHealth = playerCurrentHeath.RuntimeValue / 2;
        for (int i = 0; i < heartContainers.initialValue; i++)
        {
            if (i <= tempHealth-1)
            {
                hearts[i].sprite = fullHeart;
            }// Полное сердце
            else if (i > tempHealth)
            {
                hearts[i].sprite = emptyHeart;
            }
            //пустое сердце
            else
            {
                hearts[i].sprite = halfFullHeart;
            }
        }

    }

    public void InitHearts()
    {
        for(int i = 0; i < heartContainers.initialValue; i++)
        {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullHeart;
        }
    }

    public void UpdateHearts()
    {
        float tempHealth = playerCurrentHeath.initialValue / 2;
        for (int i = 0; i < heartContainers.initialValue; i++)
        {
            if(i <= tempHealth)
            {
                hearts[i].sprite = fullHeart;
            }// Полное сердце
            else if(i > tempHealth)
            {
                hearts[i].sprite = emptyHeart;
            }
            //пустое сердце
            else
            {
                hearts[i].sprite = halfFullHeart;
            }
        }
    }
}
