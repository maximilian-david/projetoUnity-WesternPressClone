using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

//Essa class vai ser uma class de dado, que vai ser servi para gente mante qual tecla a gente deve pressionar e qual é a imagem corespondente naquela tecla


[Serializable]
public class Keys {
    public Sprite KeySprite; //uma imagem. a representação da tecla
    public KeyCode Key; // a tecla corespodente
}

public class LevelController : MonoBehaviour
{
    public static LevelController instance;

    public Keys[] keys;

    public List<Keys> gameKeys;

    public Image[] player1Images;
    public Image[] player2Images;

    public Text massagemtext;
    public Text[] playersTimeText;
    public float[] playersTime;

    public PlayerController[] players;

    public bool canPlay = false;

    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartingKeys());
    }

   

    IEnumerator StartingKeys()
    {
        for (int i = 0; i < player1Images.Length; i++)
        {
            gameKeys.Add(keys[UnityEngine.Random.Range(0, keys.Length)]);
            player1Images[i].sprite = gameKeys[i].KeySprite;
            player1Images[i].preserveAspect = true;
            player1Images[i].enabled = true;
            player2Images[i].sprite = gameKeys[i].KeySprite;
            player2Images[i].preserveAspect = true;
            player2Images[i].enabled = true;
            yield return new WaitForSeconds(0.25f);
        }

        canPlay = true;
        massagemtext.text = "Go";
        StartCoroutine(Fading(massagemtext));
      
    }

    IEnumerator Fading(Text text)
    {
        Color newColor = text.color;
        while (newColor.a > 0 )
        {
            newColor.a -= Time.deltaTime;
            text.color = newColor;
            yield return null;
        }
    }

    public void NextKey(int playerIndex, int keyIndex)
    {
        if(playerIndex == 0)
        {
            player1Images[keyIndex].enabled = false;
        }else
        {
            player2Images[keyIndex].enabled = false;
        }
    }

    public void UpdatePlayerTime(float time, int player)
    {
        playersTime[player] = time;

        if(playersTime[0] > 0 && playersTime[1] > 0)
        {
           
            string[] triggersAnims = new string[2];
            if(playersTime[0] > playersTime[1])
            {
                triggersAnims[0] = "Dead";
                triggersAnims[1] = "Shoot";
            }
            else
            {
                triggersAnims[1] = "Dead";
                triggersAnims[0] = "Shoot";
            }

            for (int i = 0; i < playersTime.Length; i++)
            {
                players[i].SetAnimation(triggersAnims[i]);
                playersTimeText[i].text = playersTime[i].ToString("0.00") + " s";
            }
        }


        //playersTimeText[player].text = playersTime[player].ToString("0.00") + " s";
    }
}
