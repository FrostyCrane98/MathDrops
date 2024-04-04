using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WaterDropController : MonoBehaviour
{
    public GameObject WaterDropPrefab;
    public float MediumThreshold, HardThreshold, VeryHardThreshold,ImpossibleThreshold, InitialCooldown;

    private List<Waterdrop> waterdrops = new List<Waterdrop>();
    private float elapsedTime;
    private float cooldown;
    private enum difficulties {easy, medium, hard, veryHard, impossible};
    private int difficulty = 0;


    private void OnEnable()
    {
        EventManager.Instance.OnDropDespawn += DropDespawned;
        EventManager.Instance.OnPlayerInput += Check;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnDropDespawn -= DropDespawned;
        EventManager.Instance.OnPlayerInput -= Check;
    }

    // Start is called before the first frame update
    void Start()
    {
        cooldown = InitialCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        cooldown -= Time.deltaTime;

        if (difficulty != (int)difficulties.impossible)
        {
            if (elapsedTime > ImpossibleThreshold)
            {
                difficulty = (int)difficulties.impossible;
                InitialCooldown = 0.75f;
            }
            else if (elapsedTime > VeryHardThreshold)
            {
                difficulty = (int)difficulties.veryHard;
                InitialCooldown = 1f;
            }
            else if (elapsedTime > HardThreshold)
            {
                difficulty = (int)difficulties.hard;
                InitialCooldown = 1.5f;
            }
            else if (elapsedTime > MediumThreshold)
            {
                difficulty = (int)difficulties.medium;
                InitialCooldown = 2f;
            }
        }
        
        if (cooldown <= 0f)
        {
            SpawnWaterdrop();
        }
    }

    private void SpawnWaterdrop()
    {      
        GameObject newWaterdrop = Instantiate(WaterDropPrefab, new Vector3(Random.Range(-Camera.main.aspect * Camera.main.orthographicSize + 1f, (Camera.main.aspect * Camera.main.orthographicSize - 1f)),transform.position.y,transform.position.z), Quaternion.identity);
        waterdrops.Add(newWaterdrop.GetComponent<Waterdrop>());
        switch (difficulty)
        {
            case 0:
                newWaterdrop.GetComponent<Waterdrop>().DefineOperation(0, 10, false);
                break;
            case 1:
                newWaterdrop.GetComponent<Waterdrop>().DefineOperation(0, 10, true);
                break;
            case 2:
            case 3:
            case 4:
                newWaterdrop.GetComponent<Waterdrop>().DefineOperation(0, 15, true);
                break;

        }
        cooldown = InitialCooldown;
    }

    private void DropDespawned(Waterdrop waterdrop)
    {
        waterdrops.Remove(waterdrop);
    }

    private void Check(int playerInput)
    {
        Debug.Log(playerInput);
        for(int i = 0; i< waterdrops.Count; i++)
        {
            if (waterdrops[i].Result == playerInput)
            {
                waterdrops[i].PopWaterdrop();
                waterdrops.Remove(waterdrops[i]);
            }
        }
    }

}
