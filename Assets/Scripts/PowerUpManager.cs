using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject ForceField;

    public static PowerUpManager instance = null;
    public bool isShieldActive = false;

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

    //-------------Multiplies the Score of the Player for 15 seconds---------------//
    public IEnumerator ScoreMultiplier(int Score)
    {
        int timer = 15;
        while (timer > 0)
        {
            Score += 200;
            timer--;
            Debug.Log(Score);
            yield return new WaitForSeconds(1f);
        }
    }

    //------------------Activates a forceField around the player for 5 seconds-----------------//
    public IEnumerator ActivateShield(Transform player)
    {
        GameObject forceField = Instantiate(ForceField, player.position, transform.rotation, player);
        isShieldActive = true;

        yield return new WaitForSeconds(5f);
        isShieldActive = false;
        Destroy(forceField);
    }
}
