using Unity.Burst.CompilerServices;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    public Camera mainCamera;

    public GameObject smokeEffect;

    public Animator Enemy_With_HalfHitAnimation;

    public string MyTargetId;

    void Start()
    {
        mainCamera = Camera.main;
    }
    void Update()
    {
        //// Check if the left mouse button is clicked
        //if (Input.GetMouseButtonUp(0))
        //{
        //    // Cast a ray from the mouse cursor position into the scene

        //    //make sure the raycast start point is the mouse position with respect to the current camera position
        //    Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        //    RaycastHit hit;

        //    // Check if the ray hits any collider
        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        Debug.Log((hit.collider.gameObject  + " Clicked on " + gameObject.name));
        //        // Check if the collider belongs to the object you want to click
        //        if (hit.collider.gameObject == gameObject)
        //        {
        //            Debug.Log("Clicked on " + gameObject.name);
        //            gameObject.GetComponent<HitPoint>().isHit = true;
        //            gameObject.SetActive(false);
        //            //call the hit method of the GroundEnemy script in the parent object
        //            transform.parent.parent.GetComponent<GroundEnemy>().Hit();

        //        }
        //    }
        //}
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 origin = (Input.mousePosition); // Example, adjust based on your needs
        Vector3 direction = Vector3.forward; // Example, adjust based on your needs
        if (Input.GetMouseButtonUp(0))
        { 
            RaycastHit hit;

        bool isHit = Physics.Raycast(ray.origin, ray.direction, out hit);

        if (isHit)
        {
            // Handle the hit! You'll have information about the collision point in 'hit'
            Debug.Log("Hit something at: " + hit.point + " hit.collider.gameObject" + hit.collider.gameObject);
                if (hit.collider.gameObject == gameObject)
                {
                    Debug.Log("Clicked on " + gameObject.name);
                    gameObject.GetComponent<HitPoint>().isHit = true;
                    gameObject.SetActive(false);
                    //call the hit method of the GroundEnemy script in the parent object
                    transform.parent.parent.GetComponent<GroundEnemy>().Hit();
                    //do smoke effect
                    if(smokeEffect != null)
                    { 
                        Instantiate(smokeEffect, hit.point, Quaternion.identity); 

                    }
                    if (Enemy_With_HalfHitAnimation != null)
                    {
                        Enemy_With_HalfHitAnimation.SetTrigger("Hit");
                    }
                    
                }   
            }
            else
        { 
            // Handle no hit scenario
        }
        }
    }

    private void OnDisable()
    {
       gameObject.SetActive(true);
    }

    void OnDrawGizmos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Draw the ray as a red line in the scene view
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        Gizmos.color = Color.red;
        Vector3 origin = Input.mousePosition; // Example, adjust based on your needs
        Vector3 direction = ray.direction; // Example, adjust based on your needs
        Gizmos.DrawRay(ray.origin, direction * 1000f); // Adjust the length of the ray as needed
    }

    public void Hit()
    {
        Debug.Log("Clicked on " + gameObject.name);
        gameObject.GetComponent<HitPoint>().isHit = true;
        gameObject.SetActive(false);
        //call the hit method of the GroundEnemy script in the parent object
        transform.parent.parent.GetComponent<GroundEnemy>().Hit();
        //do smoke effect
        if (smokeEffect != null)
        {
            Instantiate(smokeEffect, transform.position, Quaternion.identity);

        }
        if (Enemy_With_HalfHitAnimation != null)
        {
            Enemy_With_HalfHitAnimation.SetTrigger("Hit");
        }

    }

}
