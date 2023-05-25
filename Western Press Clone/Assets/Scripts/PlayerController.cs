using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerIndex;
    public Animator anim;

    private int keyIndex = 8;
    private bool canPlay = false;


    private float timer;
    private float nextKeyTime;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (LevelController.instance.canPlay == false)
        {
            yield return null;
        }

        canPlay = true;
    }

    // Update is called once per frame
    void Update()
    {

        if(canPlay == false)
        {
            return;
        }
         
        timer += Time.deltaTime;

        if(playerIndex == 1)
        {
            if(Time.time > nextKeyTime)
            {
                nextKeyTime = Time.time + Random.Range(0.25f, 0.5f);
                KeyPress();
            }
            return; 
        }

        if(Input.GetKeyDown(LevelController.instance.gameKeys[keyIndex].Key))
        {
            KeyPress();
        }
    }

    void KeyPress()
    {
        LevelController.instance.NextKey(playerIndex, keyIndex);
        keyIndex--;

        if(keyIndex < 0 )
        {
            canPlay = false;
            LevelController.instance.UpdatePlayerTime(timer, playerIndex);
        }
    }

    public void SetAnimation(string triggerAnimation)
    {
        anim.SetTrigger(triggerAnimation); 
    }
}
