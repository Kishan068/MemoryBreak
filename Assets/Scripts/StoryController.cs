using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json.Linq;

public class StoryController : MonoBehaviour
{
    public Animator OverallAnimation;
    
    public string AnimationName;
    public Animator MainSoldier;
    public Animator MainSoldier2;
    public Animator MainSoldier3;
    public Animator MainSoldier4;
    public Animator Soldier1;
    public Animator Soldier2;
    public Animator Soldier3;
    public Animator Soldier4;
    public Animator Soldier5;

    public List<GameObject> AdvancedMech;

    public Animator BipedRobot;

    public Animator MainVillain;
    public GameObject MainVillainParent;
    public GameObject MainCameraObject;

    public GameObject BipedRobotGameObject;

    public bool IsTriggered;
    public string TriggerName;
    
    
    public GameObject laserShotPrefab;
    public GameObject laserShotPrefab2;

    public List<GameObject> Wave1Enemies;

    public List<GameObject> Wave2Enemies;

    public List<GameObject> Wave3Enemies;

    public List<GameObject> Wave4Enemies;

    public List<GameObject> Wave5Enemies;

    public List<GameObject> Wave6Enemies;

    public List<GameObject> Wave7Enemies;

    public List<GameObject> Wave8Enemies;

    public List<GameObject> Wave9Enemies;

    public List<GameObject> Wave10Enemies;

    public GameObject AdvancedEnemies;

    public GameObject QuadEnemy;

    public Animator QuadAnimationController;    

    public Animator GirlAnim;

     int CurrentEnemyCount = 0;
     int CurrentWave2EnemyCount =0;
     int CurrentWave3EnemyCount = 0;
     int CurrentWave4EnemyCount = 0;
     int CurrentWave5EnemyCount = 0;
     int CurrentWave6EnemyCount = 0;
     int CurrentWave7EnemyCount = 0;
    int CurrentWave8EnemyCount = 0;
    int CurrentWave9EnemyCount = 0;
    int CurrentWave10EnemyCount = 0;


    public List<int> participatingLanes = new List<int>();

    public GameObject QuadExplosion;

    //singleton
    public static StoryController Instance;

    public float wave1Time = 0;
    public float wave2Time = 0;
    public float wave3Time = 0;
    public float wave4Time = 0;
    public float wave5Time = 0;
    public float wave6Time = 0;
    public float wave7Time = 0;
    public float wave8Time = 0;
    public float wave9Time = 0;
    public float wave10Time = 0;

    public bool isFirstSpawn = true;

    //float commonWaveTime = 0;
   // bool isTimerStarted = false;
   // float TimeOut = 15f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }



    public void SetIdleTriggerForMainSoldier()
    {
        MainSoldier.SetTrigger("Idle");
    }

    public void SetShootTriggerForMainSoldier()
    {
        MainSoldier.SetTrigger("Shoot");
    }
    public void SetTalkTriggerForMainSoldier()
    {
        MainSoldier.SetTrigger("Talk");
    }
    public void SetTurnTriggerForMainSoldier()
    {
        MainSoldier.SetTrigger("Turn180");
    }

    public void SetAimTriggerForSoldier1()
    {
        Soldier1.SetTrigger("Aim");
    }
    public void SetDeathTriggerForSoldier1()
    {
        Soldier1.SetTrigger("Death");
    }

    public void SetAimTriggerForSoldier2()
    {
        Soldier2.SetTrigger("Aim");
    }
    public void SetDeathTriggerForSoldier2()
    {
        Soldier2.SetTrigger("Death");
    }

    public void SetAimTriggerForSoldier3()
    {
        Soldier3.SetTrigger("Crouch");
    }

    public void SetDeathTriggerForSoldier3()
    {
        Soldier3.SetTrigger("Death");
    }

    public void SetAimTriggerForSoldier4()
    {
        Soldier4.SetTrigger("Crouch");
    }

    public void SetDeathTriggerForSoldier4()
    {
        Soldier4.SetTrigger("Death");
    }

    public void SetAimTriggerForSoldier5()
    {
        Soldier5.SetTrigger("Aim");
    }

    public void SetDeathTriggerForSoldier5()
    {
        Soldier5.SetTrigger("Death");
    }


    public void SetIdleTriggerForBipedRobot()
    {
        BipedRobot.SetTrigger("Idle");
    }

    public void SetWalkTriggerForBipedRobot()
    {
        BipedRobot.SetTrigger("Walk");
    }

    public void SetDeathTriggerForBipedRobot()
    {
        BipedRobot.SetTrigger("Death");
    }

    public void SetLookTriggerForBipedRobot()
    {
        BipedRobot.SetTrigger("Look");
    }

    public void SetJumpTriggerForBipedRobot()
    {
        BipedRobot.SetTrigger("Jump");
    }
    public void setCurrentEnemyCount(int count)
    {
        //commonWaveTime = 0;
        //isTimerStarted = true;
        CurrentEnemyCount = count;
    }
    public void setAdvancedEnemiesActive()
    {
        //AdvancedEnemies.SetActive(true);
        //commonWaveTime = 0;
        //isTimerStarted = true;
        CurrentWave2EnemyCount = 1;
    }

    public void DisableAdvancedEnemies()
    {
        //AdvancedEnemies.SetActive(false);
        CurrentWave2EnemyCount = 0;
    }

    public void ActivateWave3()
    {
        //commonWaveTime = 0;
        //isTimerStarted = true;
        CurrentWave3EnemyCount = 1;
    }

    public void DeactivateWave3()
    {
        CurrentWave3EnemyCount = 0;
    }

    public void ActivateWave4()
    {
        //commonWaveTime = 0;
        //isTimerStarted = true;
        CurrentWave4EnemyCount = 1;
    }

    public void DeactivateWave4()
    {
        CurrentWave4EnemyCount = 0;
    }

    public void ActivateWave5()
    {   
        //commonWaveTime = 0;
        //isTimerStarted = true;
        CurrentWave5EnemyCount = 1;
    }

    public void DeactivateWave5()
    {
        CurrentWave5EnemyCount = 0;
    }

    public void ActivateWave6()
    {
        //commonWaveTime = 0;
        //isTimerStarted = true;
        CurrentWave6EnemyCount = 1;
    }

    public void DeactivateWave6()
    {
        CurrentWave6EnemyCount = 0;
    }

    public void ActivateWave7()
    {
        //commonWaveTime = 0;
        //isTimerStarted = true;
        CurrentWave7EnemyCount = 1;
    }

    public void DeactivateWave7()
    {
        CurrentWave7EnemyCount = 0;
    }

    public void ActivateWave8()
    {
        //commonWaveTime = 0;
        //isTimerStarted = true;
        CurrentWave8EnemyCount = 1;
    }

    public void DeactivateWave8()
    {
        CurrentWave8EnemyCount = 0;
    }

    public void ActivateWave9()
    {
        //commonWaveTime = 0;
        //isTimerStarted = true;
        CurrentWave9EnemyCount = 1;
    }

    public void DeactivateWave9()
    {
        CurrentWave9EnemyCount = 0;
    }

    public void ActivateWave10()
    {
        //commonWaveTime = 0;
        //isTimerStarted = true;
        CurrentWave10EnemyCount = 1;
    }

    public void DeactivateWave10()
    {
        CurrentWave10EnemyCount = 0;
    }


    public void setGirlToCrouch()
    {
        GirlAnim.SetTrigger("Crouch");
    }

    public void setGirlToIdle()
    {
        GirlAnim.SetTrigger("Idle");

    }

    public void setGirlToFloat()
    {
        GirlAnim.SetTrigger("Fly");
    }

    public void setBipedLayerMaskToDefault()
    {
        //for each child in the BipedRobotGameObject
        BipedRobotGameObject.layer = LayerMask.NameToLayer("Default");
        foreach (Transform child in BipedRobotGameObject.transform)
        {
            //set the layer to default
            child.gameObject.layer = LayerMask.NameToLayer("Default");
        }
       
    }

    public void setAdvancedMechMaskToDefault()
    {
        foreach (GameObject advancedMech in AdvancedMech)
        {
            advancedMech.layer = LayerMask.NameToLayer("Default");
            foreach (Transform child in advancedMech.transform)
            {
                //set the layer to default
                foreach (Transform child2 in child)
                {
                    child2.gameObject.layer = LayerMask.NameToLayer("Default");
                }
                child.gameObject.layer = LayerMask.NameToLayer("Default");
            }
        }
    }

    public void SetIntroTriggerForVillain()
    {
        //reset the main villain position to (0,0,0) locally
        MainVillainParent.transform.localPosition = new Vector3(0, 0, 0);
        MainVillainParent.transform.parent.localPosition = new Vector3(0, 0, 0);
        MainCameraObject.transform.localPosition = new Vector3(0, 0, 0);
        MainVillain.SetTrigger("Intro");
    }

    public void SetIdleTriggerForVillain()
    {
        MainVillain.SetTrigger("Idle");
    }

    public void SetShootTriggerForVillain()
    {
        MainVillain.SetTrigger("Shoot");
    }

    public void SetDeathTriggerForVillain()
    {
        MainVillain.SetTrigger("Death");
    }

    public void SetWalkTriggerForVillain()
    {
        MainVillain.SetTrigger("Walk");
    }
    public void ResetVillainPosition()
    {
        MainVillain.SetTrigger("Entry");
    }

    public void ActivateQuadEnemy()
    {
        QuadEnemy.SetActive(true);
    }

    public void DeactivateQuadEnemy()
    {
        QuadEnemy.SetActive(false);
    }

    public void SetQuadEnemyIdle()
    {
        QuadAnimationController.SetTrigger("Idle");
    }

    public void SetQuadEnemyLeftFront()
    {
        QuadAnimationController.SetTrigger("LeftFront");
    }

    public void SetQuadEnemyRightFront()
    {
        QuadAnimationController.SetTrigger("RightFront");
    }

    public void SetQuadEnemyLeftBack()
    {
        QuadAnimationController.SetTrigger("LeftBack");
    }

    public void SetQuadEnemyRightBack()
    {
        QuadAnimationController.SetTrigger("RightBack");
    }

    public void SetQuadEnemyShoot()
    {
        QuadAnimationController.SetTrigger("Shoot");

    }

    public void SetQuadEnemyJumpShoot()
    {
        QuadAnimationController.SetTrigger("JumpShoot");

    }
    public void SetQuadEnemyDamage()
    {
        QuadAnimationController.SetTrigger("Damage");
    }

    public void SetQuadEnemyDeath()
    {
        QuadAnimationController.SetTrigger("Death");
    }



    public void BipedShootAtSoldiers()
    {
        //Fire a laser shot from the BipedRobot to the soldiers
        GameObject laserShot = Instantiate(laserShotPrefab, BipedRobotGameObject.transform.position, BipedRobotGameObject.transform.rotation);

        StartCoroutine(MoveLaserShot(laserShot, Soldier1.transform.position,0f,1f));

        GameObject laserShot2 = Instantiate(laserShotPrefab, BipedRobotGameObject.transform.position, BipedRobotGameObject.transform.rotation);
        
        StartCoroutine(MoveLaserShot(laserShot2, Soldier2.transform.position, 0.1f,1f));

        GameObject laserShot3 = Instantiate(laserShotPrefab, BipedRobotGameObject.transform.position, BipedRobotGameObject.transform.rotation);
      
        StartCoroutine(MoveLaserShot(laserShot3, Soldier3.transform.position, 0.2f, 1f));

        GameObject laserShot4 = Instantiate(laserShotPrefab, BipedRobotGameObject.transform.position, BipedRobotGameObject.transform.rotation);
        
        StartCoroutine(MoveLaserShot(laserShot4, Soldier4.transform.position, 0.3f, 1f));

        GameObject laserShot5 = Instantiate(laserShotPrefab, BipedRobotGameObject.transform.position, BipedRobotGameObject.transform.rotation);
        
        StartCoroutine(MoveLaserShot(laserShot5, Soldier5.transform.position, 0.4f, 1f));
    }

    //coroutine to make the laser shot move and hit the soldiers
    public IEnumerator MoveLaserShot(GameObject laserShot, Vector3 targetPosition, float waitTime, float heightOffset)
    {
        float time = 0;
        float duration = 1.0f;
        Vector3 startPosition = laserShot.transform.position;
        //offset the y of target position so that the laser shot hits the soldiers at the chest level
        targetPosition.y += heightOffset;
        yield return new WaitForSeconds(waitTime);
        while (time < duration)
        {
            time += Time.deltaTime;
            laserShot.transform.LookAt(targetPosition);
            laserShot.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            yield return null;
        }
        Destroy(laserShot);
    }

    public void MainSoldierShootsBiped()
    {
        //offset the y of mainsoldier so the laser is instantiated at the chest level
        Vector3 startPosition = new Vector3(MainSoldier.transform.position.x, MainSoldier.transform.position.y + 1f, MainSoldier.transform.position.z);
        GameObject laserShot = Instantiate(laserShotPrefab2, startPosition, MainSoldier.transform.rotation);
        StartCoroutine(MoveLaserShot(laserShot, BipedRobotGameObject.transform.position, 0f, 0.5f));
    }

    private void Update()
    {
        //Run the common wave timer. 
        //if (isTimerStarted && commonWaveTime < TimeOut)
        //{
        //    commonWaveTime += Time.deltaTime;
        //}
        
    
        if(Input.GetKeyDown(KeyCode.Q))
        {
            BipedShootAtSoldiers();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            CurrentEnemyCount = 1;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            CurrentWave5EnemyCount = 1;
        }

        //Check if the number of enemies for a player is less than 1, then spawn an enemy for that player
        if (Wave1Enemies.Count(enemy => enemy.activeSelf == true) < CurrentEnemyCount*4)
        {
            //Debug.Log(CurrentEnemyCount + " and first participating lane is " + participatingLanes[0]);

            //get the player numnber of all the active enemies. If a number from 1 to 4 is missing, spawn an enemy for that player
            //set timeout as  TimeOut = wave1Time - commonWaveTime. But if the wave1Time is 0 or if wave1Time - commonWaveTime is -ve, then set the timeout to 15 seconds
            //if (wave1Time - commonWaveTime > 0 )
            //{
            //    TimeOut = wave1Time - commonWaveTime;
            //}
            //else
            //{
            //    TimeOut = 15f;
            //}

            if (Wave1Enemies.Count(enemy => enemy.activeSelf == true && enemy.GetComponent<GroundEnemy>().PlayerNumber == 1) < CurrentEnemyCount && participatingLanes.Contains(1))
            {
                SpawnCommonEnemyForPlayer(1,1);
            }
            if (Wave1Enemies.Count(enemy => enemy.activeSelf == true && enemy.GetComponent<GroundEnemy>().PlayerNumber == 2) < CurrentEnemyCount && participatingLanes.Contains(2))
            {
                SpawnCommonEnemyForPlayer(2, 1);
            }
            if (Wave1Enemies.Count(enemy => enemy.activeSelf == true && enemy.GetComponent<GroundEnemy>().PlayerNumber == 3) < CurrentEnemyCount && participatingLanes.Contains(3))
            {
                SpawnCommonEnemyForPlayer(3, 1);
            }
            if (Wave1Enemies.Count(enemy => enemy.activeSelf == true && enemy.GetComponent<GroundEnemy>().PlayerNumber == 4) < CurrentEnemyCount && participatingLanes.Contains(4))
            {
                SpawnCommonEnemyForPlayer(4, 1);
            }

            if(Wave1Enemies.Count(enemy => enemy.activeSelf == true) == CurrentEnemyCount * 4 && isFirstSpawn)
            {
                //Debug.Log("First Spawn");
                isFirstSpawn = false;
            }
            isFirstSpawn = false;
        }

        if (Wave2Enemies.Count(enemy => enemy.activeSelf == true) < CurrentWave2EnemyCount * 4)
        {
            //Debug.Log(CurrentWave2EnemyCount);
            //get the player numnber of all the active enemies. If a number from 1 to 4 is missing, spawn an enemy for that player
            //if (wave2Time - commonWaveTime > 0)
            //{
            //    TimeOut = wave2Time - commonWaveTime;
            //}
            //else
            //{
            //    TimeOut = 15f;
            //}
            if (Wave2Enemies.Count(enemy => enemy.activeSelf == true && enemy.GetComponent<GroundEnemy>().PlayerNumber == 1) < CurrentWave2EnemyCount && participatingLanes.Contains(1))
            {
                SpawnCommonEnemyForPlayer(1, 2);
            }
            if (Wave2Enemies.Count(enemy => enemy.activeSelf == true && enemy.GetComponent<GroundEnemy>().PlayerNumber == 2) < CurrentWave2EnemyCount && participatingLanes.Contains(2))
            {
                SpawnCommonEnemyForPlayer(2, 2);
            }
            if (Wave2Enemies.Count(enemy => enemy.activeSelf == true && enemy.GetComponent<GroundEnemy>().PlayerNumber == 3) < CurrentWave2EnemyCount && participatingLanes.Contains(3))
            {
                SpawnCommonEnemyForPlayer(3, 2);
            }
            if (Wave2Enemies.Count(enemy => enemy.activeSelf == true && enemy.GetComponent<GroundEnemy>().PlayerNumber == 4) < CurrentWave2EnemyCount && participatingLanes.Contains(4))
            {
                SpawnCommonEnemyForPlayer(4, 2);
            }
            isFirstSpawn = false;
        }

        if (Wave3Enemies.Count(enemy => enemy.activeSelf == true) < CurrentWave3EnemyCount * 4)
        {
            ////Debug.Log(CurrentWave3EnemyCount);
            ////get the player numnber of all the active enemies. If a number from 1 to 4 is missing, spawn an enemy for that player
            //if (wave3Time - commonWaveTime > 0)
            //{
            //    TimeOut = wave3Time - commonWaveTime;
            //}
            //else
            //{
            //    TimeOut = 15f;
            //}
            if (Wave3Enemies.Count(enemy => enemy.activeSelf == true && enemy.GetComponent<GroundEnemy>().PlayerNumber == 1) < CurrentWave3EnemyCount && participatingLanes.Contains(1))
            {
                SpawnCommonEnemyForPlayer(1, 3);
            }
            if (Wave3Enemies.Count(enemy => enemy.activeSelf == true && enemy.GetComponent<GroundEnemy>().PlayerNumber == 2) < CurrentWave3EnemyCount && participatingLanes.Contains(2))
            {
                SpawnCommonEnemyForPlayer(2, 3);
            }
            if (Wave3Enemies.Count(enemy => enemy.activeSelf == true && enemy.GetComponent<GroundEnemy>().PlayerNumber == 3) < CurrentWave3EnemyCount && participatingLanes.Contains(3))
            {
                SpawnCommonEnemyForPlayer(3, 3);
            }
            if (Wave3Enemies.Count(enemy => enemy.activeSelf == true && enemy.GetComponent<GroundEnemy>().PlayerNumber == 4) < CurrentWave3EnemyCount && participatingLanes.Contains(4))
            {
                SpawnCommonEnemyForPlayer(4, 3);
            }
            isFirstSpawn = false;
        }

        if (Wave4Enemies.Count(enemy => enemy.activeSelf == true) < CurrentWave4EnemyCount * 4)
        {
            //Debug.Log(CurrentWave4EnemyCount);
            //get the player numnber of all the active enemies. If a number from 1 to 4 is missing, spawn an enemy for that player
            //if (wave4Time - commonWaveTime > 0)
            //{
            //    TimeOut = wave4Time - commonWaveTime;
            //}
            //else
            //{
            //    TimeOut = 15f;
            //}
            if (Wave4Enemies.Count(enemy => enemy.activeSelf == true && enemy.GetComponent<GroundEnemy>().PlayerNumber == 1) < CurrentWave4EnemyCount && participatingLanes.Contains(1))
            {
                SpawnCommonEnemyForPlayer(1, 4);
            }
            if (Wave4Enemies.Count(enemy => enemy.activeSelf == true && enemy.GetComponent<GroundEnemy>().PlayerNumber == 2) < CurrentWave4EnemyCount && participatingLanes.Contains(2))
            {
                SpawnCommonEnemyForPlayer(2, 4);
            }
            if (Wave4Enemies.Count(enemy => enemy.activeSelf == true && enemy.GetComponent<GroundEnemy>().PlayerNumber == 3) < CurrentWave4EnemyCount && participatingLanes.Contains(3))
            {
                SpawnCommonEnemyForPlayer(3, 4);
            }
            if (Wave4Enemies.Count(enemy => enemy.activeSelf == true && enemy.GetComponent<GroundEnemy>().PlayerNumber == 4) < CurrentWave4EnemyCount && participatingLanes.Contains(4))
            {
                SpawnCommonEnemyForPlayer(4, 4);
            }

            isFirstSpawn = false;
        }

        if(Wave5Enemies.Count(enemy => enemy.activeSelf == true) < CurrentWave5EnemyCount * 4)
        {
            //Debug.Log(CurrentWave5EnemyCount + " is current wave 5 enemyCount");
            ////get the player numnber of all the active enemies. If a number from 1 to 4 is missing, spawn an enemy for that player
            //if (wave5Time - commonWaveTime > 0)
            //{
            //    TimeOut = wave5Time - commonWaveTime;
            //}
            //else
            //{
            //    TimeOut = 15f;
            //}
            if (Wave5Enemies.Count(enemy => enemy.activeSelf == true && enemy.GetComponent<GroundEnemy>().PlayerNumber == 1) < CurrentWave5EnemyCount && participatingLanes.Contains(1))
            {
                SpawnCommonEnemyForPlayer(1, 5);
            }
            if (Wave5Enemies.Count(enemy => enemy.activeSelf == true && enemy.GetComponent<GroundEnemy>().PlayerNumber == 2) < CurrentWave5EnemyCount && participatingLanes.Contains(2))
            {
                SpawnCommonEnemyForPlayer(2, 5);
            }
            if (Wave5Enemies.Count(enemy => enemy.activeSelf == true && enemy.GetComponent<GroundEnemy>().PlayerNumber == 3) < CurrentWave5EnemyCount && participatingLanes.Contains(3))
            {
                SpawnCommonEnemyForPlayer(3, 5);
            }
            if (Wave5Enemies.Count(enemy => enemy.activeSelf == true && enemy.GetComponent<GroundEnemy>().PlayerNumber == 4) < CurrentWave5EnemyCount && participatingLanes.Contains(4))
            {
                SpawnCommonEnemyForPlayer(4, 5);
            }
            isFirstSpawn = false;
        }

        if (Wave6Enemies.Count(enemy => enemy.activeSelf == true) < CurrentWave6EnemyCount * 4)
        {
            //Debug.Log(CurrentWave4EnemyCount);
            //get the player numnber of all the active enemies. If a number from 1 to 4 is missing, spawn an enemy for that player
            //if (wave6Time - commonWaveTime > 0)
            //{
            //    TimeOut = wave6Time - commonWaveTime;
            //}
            //else
            //{
            //    TimeOut = 15f;
            //}
            if (Wave6Enemies.Count(enemy => enemy.activeSelf == true && enemy.GetComponent<GroundEnemy>().PlayerNumber == 1) < CurrentWave6EnemyCount && participatingLanes.Contains(1))
            {
                SpawnCommonEnemyForPlayer(1, 6);
            }
            if (Wave6Enemies.Count(enemy => enemy.activeSelf == true && enemy.GetComponent<GroundEnemy>().PlayerNumber == 2) < CurrentWave6EnemyCount && participatingLanes.Contains(2))
            {
                SpawnCommonEnemyForPlayer(2, 6);
            }
            if (Wave6Enemies.Count(enemy => enemy.activeSelf == true && enemy.GetComponent<GroundEnemy>().PlayerNumber == 3) < CurrentWave6EnemyCount && participatingLanes.Contains(3))
            {
                SpawnCommonEnemyForPlayer(3, 6);
            }
            if (Wave6Enemies.Count(enemy => enemy.activeSelf == true && enemy.GetComponent<GroundEnemy>().PlayerNumber == 4) < CurrentWave6EnemyCount && participatingLanes.Contains(4))
            {
                SpawnCommonEnemyForPlayer(4, 6);
            }
            isFirstSpawn = false;
        }

        if (Wave7Enemies.Count(enemy => enemy.activeSelf == true) < CurrentWave7EnemyCount * 4)
        {
            //Debug.Log(CurrentWave4EnemyCount);
            //get the player numnber of all the active enemies. If a number from 1 to 4 is missing, spawn an enemy for that player
            //if (wave7Time - commonWaveTime > 0)
            //{
            //    TimeOut = wave7Time - commonWaveTime;
            //}
            //else
            //{
            //    TimeOut = 15f;
            //}
            if (Wave7Enemies.Count(enemy => enemy.activeSelf == true && enemy.GetComponent<GroundEnemy>().PlayerNumber == 1) < CurrentWave7EnemyCount && participatingLanes.Contains(1))
            {
                SpawnCommonEnemyForPlayer(1, 7);
            }
            if (Wave7Enemies.Count(enemy => enemy.activeSelf == true && enemy.GetComponent<GroundEnemy>().PlayerNumber == 2) < CurrentWave7EnemyCount && participatingLanes.Contains(2))
            {
                SpawnCommonEnemyForPlayer(2, 7);
            }
            if (Wave7Enemies.Count(enemy => enemy.activeSelf == true && enemy.GetComponent<GroundEnemy>().PlayerNumber == 3) < CurrentWave7EnemyCount && participatingLanes.Contains(3))
            {
                SpawnCommonEnemyForPlayer(3, 7);
            }
            if (Wave7Enemies.Count(enemy => enemy.activeSelf == true && enemy.GetComponent<GroundEnemy>().PlayerNumber == 4) < CurrentWave7EnemyCount && participatingLanes.Contains(4))
            {
                SpawnCommonEnemyForPlayer(4, 7);
            }
            isFirstSpawn = false;
        }

        if (Wave8Enemies.Count(enemy => enemy.activeSelf == true) < CurrentWave8EnemyCount * 4)
        {
            //Debug.Log(CurrentWave4EnemyCount);
            //get the player numnber of all the active enemies. If a number from 1 to 4 is missing, spawn an enemy for that player
            //if (wave8Time - commonWaveTime > 0)
            //{
            //    TimeOut = wave8Time - commonWaveTime;
            //}
            //else
            //{
            //    TimeOut = 15f;
            //}
            if (Wave8Enemies.Count(enemy => enemy.activeSelf == true && enemy.GetComponent<GroundEnemy>().PlayerNumber == 1) < CurrentWave8EnemyCount && participatingLanes.Contains(1))
            {
                SpawnCommonEnemyForPlayer(1, 8);
            }
            if (Wave8Enemies.Count(enemy => enemy.activeSelf == true && enemy.GetComponent<GroundEnemy>().PlayerNumber == 2) < CurrentWave8EnemyCount && participatingLanes.Contains(2))
            {
                SpawnCommonEnemyForPlayer(2, 8);
            }
            if (Wave8Enemies.Count(enemy => enemy.activeSelf == true && enemy.GetComponent<GroundEnemy>().PlayerNumber == 3) < CurrentWave8EnemyCount && participatingLanes.Contains(3))
            {
                SpawnCommonEnemyForPlayer(3, 8);
            }
            if (Wave8Enemies.Count(enemy => enemy.activeSelf == true && enemy.GetComponent<GroundEnemy>().PlayerNumber == 4) < CurrentWave8EnemyCount && participatingLanes.Contains(4))
            {
                SpawnCommonEnemyForPlayer(4, 8);
            }
            isFirstSpawn = false;
        }

        if (Wave9Enemies.Count(enemy => enemy.activeSelf == true) < CurrentWave9EnemyCount * 4)
        {
            //Debug.Log(CurrentWave4EnemyCount);
            //get the player numnber of all the active enemies. If a number from 1 to 4 is missing, spawn an enemy for that player
            //if (wave9Time - commonWaveTime > 0)
            //{
            //    TimeOut = wave9Time - commonWaveTime;
            //}
            //else
            //{
            //    TimeOut = 15f;
            //}
            if (Wave9Enemies.Count(enemy => enemy.activeSelf == true && enemy.GetComponent<GroundEnemy>().PlayerNumber == 1) < CurrentWave9EnemyCount && participatingLanes.Contains(1))
            {
                SpawnCommonEnemyForPlayer(1, 9);
            }
            if (Wave9Enemies.Count(enemy => enemy.activeSelf == true && enemy.GetComponent<GroundEnemy>().PlayerNumber == 2) < CurrentWave9EnemyCount && participatingLanes.Contains(2))
            {
                SpawnCommonEnemyForPlayer(2, 9);
            }
            if (Wave9Enemies.Count(enemy => enemy.activeSelf == true && enemy.GetComponent<GroundEnemy>().PlayerNumber == 3) < CurrentWave9EnemyCount && participatingLanes.Contains(3))
            {
                SpawnCommonEnemyForPlayer(3, 9);
            }
            if (Wave9Enemies.Count(enemy => enemy.activeSelf == true && enemy.GetComponent<GroundEnemy>().PlayerNumber == 4) < CurrentWave9EnemyCount && participatingLanes.Contains(4))
            {
                SpawnCommonEnemyForPlayer(4, 9);
            }
            isFirstSpawn = false;
        }

        if (Wave10Enemies.Count(enemy => enemy.activeSelf == true) < CurrentWave10EnemyCount * 4)
        {
            //Debug.Log(CurrentWave4EnemyCount);
            //get the player numnber of all the active enemies. If a number from 1 to 4 is missing, spawn an enemy for that player
            //if (wave10Time - commonWaveTime > 0)
            //{
            //    TimeOut = wave10Time - commonWaveTime;
            //}
            //else
            //{
            //    TimeOut = 15f;
            //}
            if (Wave10Enemies.Count(enemy => enemy.activeSelf == true && enemy.GetComponent<GroundEnemy>().PlayerNumber == 1) < CurrentWave10EnemyCount && participatingLanes.Contains(1))
            {
                SpawnCommonEnemyForPlayer(1, 10);
            }
            if (Wave10Enemies.Count(enemy => enemy.activeSelf == true && enemy.GetComponent<GroundEnemy>().PlayerNumber == 2) < CurrentWave10EnemyCount && participatingLanes.Contains(2))
            {
                SpawnCommonEnemyForPlayer(2, 10);
            }
            if (Wave10Enemies.Count(enemy => enemy.activeSelf == true && enemy.GetComponent<GroundEnemy>().PlayerNumber == 3) < CurrentWave10EnemyCount && participatingLanes.Contains(3))
            {
                SpawnCommonEnemyForPlayer(3, 10);
            }
            if (Wave10Enemies.Count(enemy => enemy.activeSelf == true && enemy.GetComponent<GroundEnemy>().PlayerNumber == 4) < CurrentWave10EnemyCount && participatingLanes.Contains(4))
            {
                SpawnCommonEnemyForPlayer(4, 10);
            }
            isFirstSpawn = false;
        }

       




        if(Input.GetKeyDown(KeyCode.Keypad0))
        {
            OverallAnimation.SetTrigger("Start");
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            OverallAnimation.speed = 6;


            //skip to frame 150 of the animation
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            OverallAnimation.speed = 1;
        }

    }

    public void storyIdle()
    {
        explodeGroundEnemy();
        setGirlToIdle();
        SetIdleTriggerForMainSoldier();
        Soldier1.SetTrigger("Aim");
        Soldier2.SetTrigger("Aim");
        Soldier3.SetTrigger("Aim");
        Soldier4.SetTrigger("Aim");
        Soldier5.SetTrigger("Aim");
       

        CurrentEnemyCount = 0;
        DisableAdvancedEnemies();
        
        DeactivateWave3();
        DeactivateWave4();
        
        DeactivateWave5();
        DeactivateWave6();
        DeactivateWave7();
        DeactivateWave8();
        DeactivateWave9();
        DeactivateWave10();
       
        OverallAnimation.SetTrigger("Idle");
        //isTimerStarted = false;
    }

    public void NonStoryIdle()
    {
        explodeGroundEnemy();
        setGirlToIdle();
        SetIdleTriggerForMainSoldier();
        Soldier1.SetTrigger("Aim");
        Soldier2.SetTrigger("Aim");
        Soldier3.SetTrigger("Aim");
        Soldier4.SetTrigger("Aim");
        Soldier5.SetTrigger("Aim");


        CurrentEnemyCount = 0;
        DisableAdvancedEnemies();

        DeactivateWave3();
        DeactivateWave4();

        DeactivateWave5();
        DeactivateWave6();
        DeactivateWave7();
        DeactivateWave8();
        DeactivateWave9();
        DeactivateWave10();

        //check if the current trigger is OtherGames, then set the trigger to Idle
        if (OverallAnimation.GetCurrentAnimatorStateInfo(0).IsName("OtherGames"))
        {
           
        }
        else
        {
            OverallAnimation.SetTrigger("OtherGames");
        }
        
    }

    public void StartGame()
    {
        QuadExplosion.SetActive(false);
        Debug.Log("Game Started");
        OverallAnimation.SetTrigger("Start");
        //isTimerStarted = false;
    }

    public void endGame()
    {
        explodeGroundEnemy();
        QuadExplosion.SetActive(true);
        QuadEnemy.SetActive(false);

       // OnDemandTargetStarter.Instance.EndGame();
    }
    public void explodeGroundEnemy()
    {   
        //Call the explode function in the ground enemy script for all the enemies in the wave
        //if (Wave1Enemies.Count > 0)
        //{
        //    foreach (GameObject enemy in Wave1Enemies)
        //    {
        //        if (enemy.activeSelf == true)
        //        {
        //            enemy.GetComponent<GroundEnemy>().Explode();
        //        }
        //    }
        //}

        //if (Wave2Enemies.Count > 0)
        //{
        //    foreach (GameObject enemy in Wave2Enemies)
        //    {
        //        if (enemy.activeSelf == true)
        //        {
        //            enemy.GetComponent<GroundEnemy>().Explode();
        //        }
        //    }
        //}

        //if (Wave3Enemies.Count > 0)
        //{
        //    foreach (GameObject enemy in Wave3Enemies)
        //    {
        //        if (enemy.activeSelf == true)
        //        {
        //            enemy.GetComponent<GroundEnemy>().Explode();
        //        }
        //    }
        //}

        //if (Wave4Enemies.Count > 0)
        //{
        //    foreach (GameObject enemy in Wave4Enemies)
        //    {
        //        if (enemy.activeSelf == true)
        //        {
        //            enemy.GetComponent<GroundEnemy>().Explode();
        //        }
        //    }
        //}

        //if (Wave5Enemies.Count > 0)
        //{
        //    foreach (GameObject enemy in Wave5Enemies)
        //    {
        //        if (enemy.activeSelf == true)
        //        {
        //            enemy.GetComponent<GroundEnemy>().Explode();
        //        }
        //    }
        //}

        //if (Wave6Enemies.Count > 0)
        //{
        //    foreach (GameObject enemy in Wave6Enemies)
        //    {
        //        if (enemy.activeSelf == true)
        //        {
        //            enemy.GetComponent<GroundEnemy>().Explode();
        //        }
        //    }
        //}

        //if (Wave7Enemies.Count > 0)
        //{
        //    foreach (GameObject enemy in Wave7Enemies)
        //    {
        //        if (enemy.activeSelf == true)
        //        {
        //            enemy.GetComponent<GroundEnemy>().Explode();
        //        }
        //    }
        //}

        //if (Wave8Enemies.Count > 0)
        //{
        //    foreach (GameObject enemy in Wave8Enemies)
        //    {
        //        if (enemy.activeSelf == true)
        //        {
        //            enemy.GetComponent<GroundEnemy>().Explode();
        //        }
        //    }
        //}

        //if (Wave9Enemies.Count > 0)
        //{
        //    foreach (GameObject enemy in Wave9Enemies)
        //    {
        //        if (enemy.activeSelf == true)
        //        {
        //            enemy.GetComponent<GroundEnemy>().Explode();
        //        }
        //    }
        //}

        //if (Wave10Enemies.Count > 0)
        //{
        //    foreach (GameObject enemy in Wave10Enemies)
        //    {
        //        if (enemy.activeSelf == true)
        //        {
        //            enemy.GetComponent<GroundEnemy>().Explode();
        //        }
        //    }
        //}

        //perform the above operation for all the enemies in the wave
        for (int i = 1; i <= 10; i++)
        {
            List<GameObject> waveEnemies = new List<GameObject>();
            switch (i)
            {
                case 1:
                    waveEnemies = Wave1Enemies;
                    break;
                case 2:
                    waveEnemies = Wave2Enemies;
                    break;
                case 3:
                    waveEnemies = Wave3Enemies;
                    break;
                case 4:
                    waveEnemies = Wave4Enemies;
                    break;
                case 5:
                    waveEnemies = Wave5Enemies;
                    break;
                case 6:
                    waveEnemies = Wave6Enemies;
                    break;
                case 7:
                    waveEnemies = Wave7Enemies;
                    break;
                case 8:
                    waveEnemies = Wave8Enemies;
                    break;
                case 9:
                    waveEnemies = Wave9Enemies;
                    break;
                case 10:
                    waveEnemies = Wave10Enemies;
                    break;
            }

            if (waveEnemies.Count > 0)
            {
                foreach (GameObject enemy in waveEnemies)
                {
                    if (enemy.activeSelf == true)
                    {
                        enemy.GetComponent<GroundEnemy>().Explode();
                    }
                }
            }
        }
       

    }




    public void SpawnCommonEnemyForPlayer(int playerNumber, int waveNumber)
    {
        //forevery enemy in Wave1Enemies, get the ground enemy script and activate the enemy whose playerNumber in the script is 2
        List<GameObject> playerEnemies = new List<GameObject>();
        List<GameObject> waveEnemies = new List<GameObject>();
        switch (waveNumber)
        {
            case 1:
                waveEnemies = Wave1Enemies;
                break;
            case 2:
                waveEnemies = Wave2Enemies;
                break;
            case 3:
                waveEnemies = Wave3Enemies;
                break;
            case 4:
                waveEnemies = Wave4Enemies;
                break;
            case 5:
                waveEnemies = Wave5Enemies;
                break;
            case 6:
                waveEnemies = Wave6Enemies;
                break;
            case 7:
                waveEnemies = Wave7Enemies;
                break;
            case 8:
                waveEnemies = Wave8Enemies;
                break;
            case 9:
                waveEnemies = Wave9Enemies;
                break;
            case 10:
                waveEnemies = Wave10Enemies;
                break;
        }

        foreach (GameObject enemy in waveEnemies)
        {
            GroundEnemy groundEnemy = enemy.GetComponent<GroundEnemy>();
            if (groundEnemy.PlayerNumber == playerNumber)
            {
                playerEnemies.Add(enemy);
            }
        }
        int randomIndex = Random.Range(0, playerEnemies.Count);
        if (playerEnemies[randomIndex].activeSelf == false)
        {
            playerEnemies[randomIndex].SetActive(true);
            Dictionary<int, List<string>> LaneTargets = new Dictionary<int, List<string>>();
            LaneTargets.Add(playerNumber, playerEnemies[randomIndex].GetComponent<GroundEnemy>().myTargetIds);
            //OnDemandTargetStarter.Instance.ClearAndAddInstructions(LaneTargets, TimeOut);

            //For each hitPoint in the playerEnemies[randomIndex].GetComponent<GroundEnemy>().hitPoints, update the LaneTargets dictionary and timeout
            foreach (HitPoint hitPoint in playerEnemies[randomIndex].GetComponent<GroundEnemy>().hitPoints)
            {
                hitPoint.LaneTargets = LaneTargets;
                //hitPoint.TimeOut = TimeOut;
            }
        }

    }

    

    public void ClearAllTargets()
    {
        List<int> lanes = new List<int> { 1, 2, 3, 4 };
        OnDemandTargetStarter.Instance.ClearAndDeactivateTargets(lanes);
    }

    public void StopGame()
    {
        //GameEventHandler.Instance.StopGame(0);
    }


    public void UpdateParticipants(JObject payload)
    {
        


        //clear the participating lanes
        participatingLanes.Clear();

        //Get the player payloads
        JArray playerPayloads = (JArray)payload["playerPayloads"];
        //for each player payload, get the lane payloads
        foreach (JObject playerPayload in playerPayloads)
        {
            JArray lanePayloads = (JArray)playerPayload["lanePayloads"];
            //for each lane payload, get the active station ids
            foreach (JObject lanePayload in lanePayloads)
            {
                JArray activeStationIds = (JArray)lanePayload["activeStationIds"];
                //if the active station ids is not empty, add the lane id to the participating lanes
                if (activeStationIds.Count > 0)
                {
                    //check if the lane id is not already in the participating lanes
                    if (!participatingLanes.Contains((int)lanePayload["laneId"]))
                    {
                        participatingLanes.Add((int)lanePayload["laneId"]);
                    }
                    
                }
            }
        }
    }


    public void HitEnemy(string DeviceId)
    {
        

        //perform the above operation for all the enemies in the wave
        for (int i = 1; i <= 10; i++)
        {
            List<GameObject> waveEnemies = new List<GameObject>();
            switch (i)
            {
                case 1:
                    waveEnemies = Wave1Enemies;
                    break;
                case 2:
                    waveEnemies = Wave2Enemies;
                    break;
                case 3:
                    waveEnemies = Wave3Enemies;
                    break;
                case 4:
                    waveEnemies = Wave4Enemies;
                    break;
                case 5:
                    waveEnemies = Wave5Enemies;
                    break;
                case 6:
                    waveEnemies = Wave6Enemies;
                    break;
                case 7:
                    waveEnemies = Wave7Enemies;
                    break;
                case 8:
                    waveEnemies = Wave8Enemies;
                    break;
                case 9:
                    waveEnemies = Wave9Enemies;
                    break;
                case 10:
                    waveEnemies = Wave10Enemies;
                    break;
            }

            if (waveEnemies.Count > 0)
            {
                foreach (GameObject enemy in waveEnemies)
                {
                    if (enemy.activeSelf && enemy.GetComponent<GroundEnemy>().myTargetIds.Contains(DeviceId))
                    {
                        if (enemy.activeSelf == true)
                            foreach (HitPoint hitHandler in enemy.GetComponent<GroundEnemy>().hitPoints)
                            {
                                if (hitHandler.gameObject.GetComponent<ClickHandler>().MyTargetId == DeviceId)
                                {
                                    hitHandler.gameObject.GetComponent<ClickHandler>().Hit();
                                    return;
                                }
                            }

                    }
                }
            }
        }


       

    }

    public void SwitchToOtherGames()
    {

    }

    public void SwitchToStoryGameIdle()
    {
        storyIdle();
    }


}
