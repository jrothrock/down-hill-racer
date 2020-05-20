using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerScript playerMovement;
    void OnCollisionEnter(Collision collisionInfo) {
        if(collisionInfo.collider.tag == "Obstacle")
        {
            Debug.Log("hit");
            playerMovement.enabled = false;

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
