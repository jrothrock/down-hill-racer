using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerScript playerMovement;
    void OnCollisionEnter(Collision collisionInfo) {
        if(collisionInfo.collider.tag == "Obstacle1" || collisionInfo.collider.tag == "Obstacle2" || collisionInfo.collider.tag == "Obstacle3")
        {
            Debug.Log("hit");
            //playerMovement.enabled = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
