using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {

    }

    //-------------Multiplies the Score of the Player---------------//
    public IEnumerator ScoreMultiplier(int Score)
    {
        int timer = 15;
        while (timer > 0)
        {
            Score += 100;
            timer--;
            Debug.Log(Score);
            yield return new WaitForSeconds(1f);
        }
    }

    public void Shield()
    {

    }
}
