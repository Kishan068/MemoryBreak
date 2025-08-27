using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtPlayer : MonoBehaviour
{
    public GameObject laserShotPrefab;
    public float timeBetweenShots = 3.0f;
    private float timeSinceLastShot = 0.0f;
 

    public GameObject player;

    private List<GameObject> laserShotObjects = new List<GameObject>();
    public Vector3 ShooterOffset = new Vector3(0, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnDisable()
    {
        foreach (GameObject laserShot in laserShotObjects)
        {
            Destroy(laserShot);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot >= timeBetweenShots)
        {
            timeSinceLastShot = 0.0f;
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject laserShot = Instantiate(laserShotPrefab, transform.position, transform.rotation);
        //parent laser shot to this object so that it moves with the enemy
        laserShot.transform.parent = transform;
        laserShotObjects.Add(laserShot);
        //add shooter offset to the laser shot position
        laserShot.transform.position += ShooterOffset;
        Vector3 targetPosition = new Vector3(0, 0, 0);
        if (player == null)
        {
            targetPosition = Camera.main.transform.position;
            //offset the y of target position so that the laser shot hits the player at the chest level
            targetPosition.y -= 0.5f;

        }
        else
        {
            targetPosition = player.transform.position;
        }
        //Vector3 direction = player.transform.position - laserShot.transform.position;

        StartCoroutine(MoveLaserShot(laserShot, targetPosition, 0.0f));
        
    }

    //coroutine to make the laser shot move and hit the player
    public IEnumerator MoveLaserShot(GameObject laserShot, Vector3 targetPosition, float heightOffset)
    {
        float time = 0;
        float duration = 1.0f;
        Vector3 startPosition = laserShot.transform.position;
        //offset start position so that the laser shot starts from the chest level of the enemy
        startPosition.y += 0.5f;
        //offset the y of target position so that the laser shot hits the player at the chest level
        targetPosition.y += heightOffset;
        while (time < duration)
        {
            time += Time.deltaTime;
            laserShot.transform.LookAt(targetPosition);
            laserShot.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            yield return null;
        }
        Destroy(laserShot);
    }
}
