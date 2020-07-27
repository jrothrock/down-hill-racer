using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerScript playerMovement;
    void OnCollisionEnter(Collision collisionInfo) {
        if(collisionInfo.collider.tag == "Obstacle1" || collisionInfo.collider.tag == "Obstacle2" || collisionInfo.collider.tag == "Obstacle3")
        {
            if(collisionInfo.impulse.z > 1.0){
                Debug.Log(collisionInfo.impulse);
                Time.timeScale = 0.6f;
                playerMovement.enabled = false;
                FindObjectOfType<Score>().allowScore(false);
                FindObjectOfType<GameManager>().EndGame();
            }
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
