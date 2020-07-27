using UnityEngine;
using System.Collections;

public class PlayerToFloorPosition : MonoBehaviour
{
    public GameObject player;
    public Transform floor;
    public GameObject floorPrefab;
    public GameObject obstaclePrefab;
    
    private GameObject floorObj;
    private Vector3 initPosition;
    private GameObject newFloor;
    private GameObject[] oldObstacles;

    private float zVelocity = 0f;
    private float yVelocity = 0f;
    private float changeY = 0f;
    private float changeZ = 0f;

    private bool hitZero = true;
    private bool playerOnFloor = false;

    private float finalY = 0f;
    private float finalZ = 0f;
    private float finalX = 0f;

    private float timeChange = 0f;
    private float timeDelta = 0f;

    private int oldFloorTagNumber;

    void OnCollisionEnter(Collision collisionInfo) {
        Debug.Log(collisionInfo.collider);
        if(collisionInfo.collider.tag == "Player")
        {
            Debug.Log("here");
            playerOnFloor = true;
            //playerMovement.enabled = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
      player = GameObject.FindGameObjectWithTag("Player");
      floorObj = GameObject.FindGameObjectsWithTag("Floor")[GameObject.FindGameObjectsWithTag("Floor").Length - 1];
      floor = GameObject.FindGameObjectsWithTag("Floor")[GameObject.FindGameObjectsWithTag("Floor").Length - 1].transform;
    }

    // Update is called once per frame
    void Update()
    {
        float endDistance = player.transform.position.z - floor.position.z;
        float floorLength = floor.localScale.z;
        if((endDistance > (floorLength/2*0.95)) && (endDistance < floorLength/2*0.98))
        {
            playerOnFloor = false;
            timeDelta = Time.time;
            initPosition = player.transform.position;
        }
        if((endDistance >= (floorLength/2*.98)) && (hitZero == true))
        {
            hitZero = false;
            float nowTime = Time.time;
            float timeFall = Random.Range(2.5f,5.0f);
            Vector3 nowPlayerPos = player.transform.position;
            // Debug.Log("init");
            // Debug.Log(nowTime);
            // Debug.Log(timeDelta);
            // Debug.Log(nowPlayerPos);
            // Debug.Log(initPosition);
            // Debug.Log("vel");
            zVelocity = ((nowPlayerPos.z - initPosition.z)) / (nowTime - timeDelta);
            yVelocity = ((nowPlayerPos.y - initPosition.y)) / (nowTime - timeDelta);
            // Debug.Log(zVelocity);
            // Debug.Log(yVelocity);
            // Debug.Log("TF");
            // Debug.Log(timeFall);
            changeY = (yVelocity * timeFall) + ((0.5f * -Physics.gravity.magnitude) * Mathf.Pow(timeFall,2));
            changeZ = zVelocity * timeFall;
            // Debug.Log("Change");
            // Debug.Log(changeY);
            // Debug.Log(changeZ);
            float newLength = Mathf.Round(Random.Range(350,600));
            // Debug.Log("Newlength");
            // Debug.Log(newLength);
            finalY = floor.position.y + changeY - (Mathf.Tan((20 * Mathf.PI / 180)) * newLength/2);
            finalZ = floor.position.z + changeZ + newLength/4 - 100;
            // Debug.Log(finalZ - floor.position.z);
            if((newLength/(finalZ - floor.position.z)) > 0.2){
                finalZ += newLength / 4; 
            }
            finalX = floor.transform.position.x;
            // Debug.Log("Final");
            Debug.Log(finalZ);
            Debug.Log(finalY);
            Debug.Log(finalX);
            newFloor = Instantiate(floorPrefab, new Vector3(finalX, finalY, finalZ), floor.rotation);
            float newWidth = getRandomThreeMult();
            Vector3 newScale = new Vector3(newWidth, 1, newLength);
            newFloor.transform.localScale = newScale;
            createObstacles(newScale);
            // Debug.Log("=====");
        }

        if(GameObject.FindGameObjectsWithTag("Floor").Length == 3) {
            Destroy(GameObject.FindGameObjectsWithTag("Floor")[0]);
            oldObstacles = GameObject.FindGameObjectsWithTag("Obstacle" + oldFloorTagNumber.ToString());
            for(var i = 0 ; i < oldObstacles.Length ; i ++){
                Destroy(oldObstacles[i]);
            }
        }

        if(((Mathf.Abs(player.transform.position.x) > (floor.localScale.x/2 + 0.5))) && (playerOnFloor == true)) {
            FindObjectOfType<Score>().allowScore(false);
            FindObjectOfType<GameManager>().EndGame();
        }

    }

    void createObstacles(Vector3 scale){
        float beginningZ = newFloor.transform.position.z - (scale.z/2);
        float squaresDistance = 30f;
        float sizeOfObs = 3f;
        float numberRows = Mathf.Floor(scale.z / squaresDistance);
        float numberColumns = scale.x / sizeOfObs;
        string newTag;

        Debug.Log(numberRows);
        if(numberColumns == 2.0){
            numberRows *= 2f;
            squaresDistance /= 2f;
        }
        Debug.Log(numberRows);

        if(GameObject.FindGameObjectsWithTag("Obstacle1").Length == 0) {
            newTag = "Obstacle1";
            oldFloorTagNumber = 2;
        } else if(GameObject.FindGameObjectsWithTag("Obstacle2").Length == 0) {
            newTag = "Obstacle2";
            oldFloorTagNumber = 3;
        } else {
            newTag = "Obstacle3";
            oldFloorTagNumber = 1;
        }

        for (int i = 0; i < numberRows; i++)
        {
            float noObs = Mathf.Round(Random.Range(0,numberColumns - 1));
            float yPosition = newFloor.transform.position.y - (Mathf.Tan((20 * Mathf.PI / 180)) * ((scale.z/2) - ((i+1)*squaresDistance))) + 1;
            float zPosition = newFloor.transform.position.z + (scale.z/2 - ((i+1)*squaresDistance));
            
            for (int j = 0; j < numberColumns; j++ ) 
            {
                if(j != noObs) 
                {
                    float xPosition;

                    if(numberColumns % 2 == 0)  {
                        if(j < (numberColumns/2)) {
                            xPosition = 0f - (sizeOfObs/2) - ((numberColumns/2 - (j+1)) * sizeOfObs);
                        } else {
                            xPosition = 0f + (sizeOfObs/2) + ((j-(numberColumns/2)) * sizeOfObs);
                        }
                    } else {
                        if(j <= Mathf.Floor(numberColumns/2)) {
                            xPosition = 0f - ((Mathf.Floor(numberColumns/2) - j) * sizeOfObs);
                        } else {
                            xPosition = ((j-Mathf.Floor(numberColumns/2)) * sizeOfObs);
                        }
                    }
    
                    Vector3 obsPosition = new Vector3(xPosition, yPosition, zPosition);
                    GameObject newObs = Instantiate(obstaclePrefab, obsPosition, newFloor.transform.rotation);
                    newObs.gameObject.tag = newTag;
                }
            }
        }
    }

    float getRandomThreeMult() {
        float newNum = Mathf.Round(Random.Range(6,18));
        while (newNum % 3 != 0)
        {
            newNum = Mathf.Round(Random.Range(6,18));
        }
        return newNum;
    }
}
