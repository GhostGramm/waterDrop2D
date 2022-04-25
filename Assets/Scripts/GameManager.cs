using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [Header("Prefabs")]
    public GameObject Player;
    public GameObject testObject;
    public GameObject GoodPlatform;
    public GameObject BadPlatform;

    [Header("References")]
    public Transform PlatformParent;

    //-----------Object Pool----------------//
    private List<GameObject> pooledPlatforms = new List<GameObject>();
    private int AmountToPool = 6;
    //-------------End---------------//

    private List<GameObject> activePlatforms = new List<GameObject>();
    private bool canSpawnPowerUp = false;


    // Start is called before the first frame update
    void Start()
    {
        CreateObjectPool();
        SpawnStartingPlatforms();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnStartingPlatforms()
    {
        float yPosition = 2;
        Vector2 playerSpawnPosition = new Vector2();
        for (int i = 0; i < 4; i++)
        {
            var location = new Vector2(Random.Range(-3f, 3f), yPosition);
            GameObject platform = Instantiate(GoodPlatform, location, transform.rotation);
            yPosition -= 2;
            if (i == 1)
                playerSpawnPosition = platform.transform.position + Vector3.up;
        }
        //---------------------------SPAWN PLAYER ON THE SECOND PLATFORM LOCATION -------------------//
        SpawnPlayer(playerSpawnPosition);
        InvokeRepeating("SubsequentPlatform", 2f, 2f);
        StartCoroutine(SpawnBadPlatform());
        StartCoroutine(ActivatePowerUpSpawning());
    }

    public void SubsequentPlatform()
    {
        Vector2 spawnPosition = new Vector2(Random.Range(-3f, 3f), -6);
        GameObject platform = GetPooledObject();
        platform.transform.position = spawnPosition;
        platform.SetActive(true);
        if (canSpawnPowerUp)
        {
            SpawnPowerUp(spawnPosition);
            canSpawnPowerUp = false;
        }
    }

    public IEnumerator SpawnBadPlatform()
    {
        float counter = Random.Range(2, 15);
        yield return new WaitForSeconds(counter);
        Vector2 spawnPosition = new Vector2(Random.Range(-3f, 3f), -6);
        Instantiate(BadPlatform, spawnPosition, transform.rotation);
        StartCoroutine(SpawnBadPlatform());
    }

    public IEnumerator ActivatePowerUpSpawning()
    {
        float counter = Random.Range(8, 15);
        yield return new WaitForSeconds(counter);
        canSpawnPowerUp = true;
        StartCoroutine(ActivatePowerUpSpawning());
    }

    public void SpawnPlayer(Vector2 spawnPosition)
    {
        Instantiate(Player, spawnPosition, transform.rotation);
    }

    //-----------------------Obhect Pooling--------------------//
    public void CreateObjectPool()
    {
        for (int i = 0; i < AmountToPool; i++)
        {
            GameObject platform = Instantiate(GoodPlatform);
            platform.SetActive(false);
            platform.transform.parent = PlatformParent;
            pooledPlatforms.Add(platform);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledPlatforms.Count; i++)
        {
            if (!pooledPlatforms[i].activeInHierarchy)
            {
                return pooledPlatforms[i];
            }
        }
        return null;
    }

    //----------------------End-----------------------------//

    public Vector2 GetLocationforPowerUpSpawn()
    {
        //if (PlatformParent.childCount != 0) return;
        Vector2 spawnPosition = new Vector2();
        activePlatforms.Clear();
        for (int i = 0; i < PlatformParent.childCount; i++)
        {
            GameObject platform = PlatformParent.GetChild(i).gameObject;
            if (platform.activeSelf)
            {
                if (!activePlatforms.Contains(platform))
                {
                    activePlatforms.Add(platform);
                    Debug.Log(activePlatforms.Count);
                }
            }
        }
        if (activePlatforms.Count != 0)
        {
            GameObject recentPlatform = activePlatforms[activePlatforms.Count - 1];
            spawnPosition = new Vector2(recentPlatform.transform.position.x, recentPlatform.transform.position.y + 1);
        }
        return spawnPosition;
    }

    public void SpawnPowerUp(Vector2 spawnPosition)
    {
        Instantiate(testObject, new Vector2(spawnPosition.x, spawnPosition.y + 0.5f), transform.rotation);
    }
}
