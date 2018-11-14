using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject upgradePrefab;
    public Gun gun;
    public float upgradeMaxTimeSpawn = 7.5f; //is the maximum time that will pass before the upgrade spawns.
    public GameObject player; //marine, helped to determine location
    public GameObject[] spawnPoints;
    public GameObject alien; //GameManager will create an instance of the alien prefab
    public int maxAliensOnScreen; //10
    public int totalAliens; //10
    public float minSpawnTime; //0
    public float maxSpawnTime; //3
    public int aliensPerSpawn; //Will determine how many aliens appear during a spawning event. (1)
    private int aliensOnScreen = 0;
    private float generatedSpawnTime = 0; //time between spawns
    private float currentSpawnTime = 0;
    private bool spawnedUpgrade = false; //has the upgrade spawn yet, it can only spawn once
    private float actualUpgradeTime = 0;
    private float currentUpgradeTime = 0; //keep track of time until upgrade
    // Use this for initialization
    void Start()
    {
        actualUpgradeTime = Random.Range(upgradeMaxTimeSpawn - 3.0f, upgradeMaxTimeSpawn);
        actualUpgradeTime = Mathf.Abs(actualUpgradeTime);
    }

    // Update is called once per frame
    void Update()
    {
        currentUpgradeTime += Time.deltaTime; //Keeps track of time passed between each frame
        currentSpawnTime += Time.deltaTime;
        //1 - check if upgrade has spawned
        if (currentUpgradeTime > actualUpgradeTime)
        {
            if (!spawnedUpgrade)
            {
                //2 - upgrade will spawn in alien spawnPoint
                int randomNumber = Random.Range(0, spawnPoints.Length - 1);
                GameObject spawnLocation = spawnPoints[randomNumber];
                //3 - create an upgrade, grab the attached script, grab the gun instance then associate with our current gun, then postiion the upgrade
                GameObject upgrade = Instantiate(upgradePrefab) as GameObject;
                Upgrade upgradeScript = upgrade.GetComponent<Upgrade>();
                upgradeScript.gun = gun;
                upgrade.transform.position = spawnLocation.transform.position;
                //4 - upgrade has ben spawned
                spawnedUpgrade = true;

                //play sound
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.powerUpAppear);
            }
        }



        if (currentSpawnTime > generatedSpawnTime) //when the time between spawns has been a while step in to the if which will handle spawntimes
        {

            currentSpawnTime = 0; //reset the timer
            generatedSpawnTime = Random.Range(minSpawnTime, maxSpawnTime); // genrate a random spawn time number between the min/max

            if (aliensPerSpawn > 0 && aliensOnScreen < totalAliens) //check whether to spawn or not based on the amount aliens on screen
            {
                List<int> previousSpawnLocations = new List<int>(); //where each alien has spawn and what location
                if (aliensPerSpawn > spawnPoints.Length)
                {
                    Debug.Log("Spawn Length: " + spawnPoints.Length);
                    aliensPerSpawn = spawnPoints.Length - 1; //limits the number of aliens based on the number of spawn points if it gets to high
                }
                aliensPerSpawn = (aliensPerSpawn > totalAliens) ? aliensPerSpawn - totalAliens : aliensPerSpawn; //this stops more aliens being created than the maxium allowed
               
                //spawning, iterates per once spawned alien
                for (int i = 0; i < aliensPerSpawn; i++)
                {
                    if (aliensOnScreen < maxAliensOnScreen)
                    {
                        aliensOnScreen += 1; //aliens less than on screen, add one
                        int spawnPoint = -1; //remember spawnPoint is an index -1 means a spawn point hasn't been selected
                        while (spawnPoint == -1) // loops and finds a spawn that is not in use or is no longer -1
                        {
                            int randomNumber = Random.Range(0, spawnPoints.Length - 1); //find random number between the range of the spawn points
                            if (!previousSpawnLocations.Contains(randomNumber)) //check if it was previous spawn point selected, if its not then we are good to spawn
                            {
                                previousSpawnLocations.Add(randomNumber);
                                spawnPoint = randomNumber;
                                //The number is added to the array and the spawnPoint is set, breaking the loop.
                                //If it finds a match, the loop iterates again with a new random number bc it will still be -1
                            }
                        }
                        GameObject spawnLocation = spawnPoints[spawnPoint]; //obtain spawnPoint location from array using newly created spawnPoint number (above)
                        GameObject newAlien = Instantiate(alien) as GameObject; //create an alien instance from a prefab, must be cast!
                        newAlien.transform.position = spawnLocation.transform.position; //This positions the alien at the spawn point

                        //setting the alien a target
                        Alien alienScript = newAlien.GetComponent<Alien>();
                        alienScript.target = player.transform; //sets alien to target player
                        //alien rotates towards player on y-axis (so that it doesn’t look upwards and stare straight ahead.)
                        Vector3 targetRotation = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
                        newAlien.transform.LookAt(targetRotation);

                    }
                }
            }
        }
    }
}
