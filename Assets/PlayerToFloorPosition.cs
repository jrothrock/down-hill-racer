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

    private float finalY = 0f;
    private float finalZ = 0f;
    private float finalX = 0f;

    private float timeChange = 0f;
    private float timeDelta = 0f;

    private int oldFloorTagNumber;

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
        // GameObject player = GameObject.FindGameObjectWithTag("Player");
        // GameObject floorObj = GameObject.FindGameObjectsWithTag("Floor")[GameObject.FindGameObjectsWithTag("Floor").Length - 1];
        // Transform floor = GameObject.FindGameObjectsWithTag("Floor")[GameObject.FindGameObjectsWithTag("Floor").Length - 1].transform;
        float absDistance = Vector3.Distance(player.transform.position, floor.position);
        float endDistance = player.transform.position.z - floor.position.z;
        //Debug.Log(timeFall);
        // Debug.Log(endDistance);
        if((endDistance > 65) && (endDistance < 60))
        {
            //Debug.Log("change");
            timeDelta = Time.time;
            initPosition = player.transform.position;
        }
        if((endDistance >= 65) && (hitZero == true))
        {
            hitZero = false;
            float timeFall = Random.Range(1f,3.0f);
            // Debug.Log("init");
            // Debug.Log(Time.time);
            // Debug.Log(player.transform.position);
            // Debug.Log(initPosition);
            // Debug.Log("vel");
            zVelocity = ((player.transform.position.z - initPosition.z)) / (Time.time - timeDelta);
            yVelocity = ((player.transform.position.y - initPosition.y)) / (Time.time - timeDelta);
            // Debug.Log(zVelocity);
            // Debug.Log(yVelocity);
            changeY = (yVelocity * timeFall) + ((0.5f * Physics.gravity.magnitude) * Mathf.Pow(timeFall,2));
            changeZ = zVelocity * timeFall;
            // Debug.Log("Change");
            // Debug.Log(changeY);
            // Debug.Log(changeZ);
            //finalY = (yVelocity * Mathf.Sin(20) * 5f) + (4.9f * Mathf.Pow(5,2));
            // velocity = ((player.transform.position - initPosition).magnitude) / Time.deltaTime;
            // float gravity = Physics.gravity.magnitude;
            // Debug.Log(Physics.gravity.magnitude);
            // Debug.Log(changeY);
            // Debug.Log(changeZ);
            float newLength = Mathf.Round(Random.Range(150,400));

            finalY = floor.position.y + changeY - (Mathf.Tan(20) * 70);
            finalZ = floor.position.z + changeZ + newLength/2;
            finalX = floor.transform.position.x;
            // finalY = -(Mathf.Pow(5,2) * gravity + Mathf.Sin(20) * Mathf.Sqrt(velocity));
            // finalZ = -finalY / Mathf.Tan(20) + 50;
            // Debug.Log(finalZ);
            // Debug.Log(finalY);
            // Debug.Log(finalX);
            newFloor = Instantiate(floorPrefab, new Vector3(finalX, finalY, finalZ), floor.rotation);
            float newWidth = getRandomThreeMult();
            Vector3 newScale = new Vector3(newWidth, 1, newLength);
            newFloor.transform.localScale = newScale;
            createObstacles(newScale);
            // Debug.Log("=====");
            timeChange = Time.time;
        }

        if(GameObject.FindGameObjectsWithTag("Floor").Length == 3) {
            Destroy(GameObject.FindGameObjectsWithTag("Floor")[0]);
            oldObstacles = GameObject.FindGameObjectsWithTag("Obstacle" + oldFloorTagNumber.ToString());
            for(var i = 0 ; i < oldObstacles.Length ; i ++){
                Destroy(oldObstacles[i]);
            }
        }

    }

    void createObstacles(Vector3 scale){
        float beginningZ = newFloor.transform.position.z - (scale.z/2);
        int squaresDistance = 30;
        int sizeOfObs = 3;
        float numberRows = Mathf.Floor(scale.z / squaresDistance);
        float numberColumns = scale.x / sizeOfObs;
        string newTag;

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
                    float xPosition = (j*sizeOfObs) - (numberColumns) + (sizeOfObs / 2 / sizeOfObs);
                    Vector3 obsPosition = new Vector3(xPosition, yPosition, zPosition);
                    GameObject newObs = Instantiate(obstaclePrefab, obsPosition, newFloor.transform.rotation);
                    newObs.gameObject.tag = newTag;
                }
            }
        }
    }

    float getRandomThreeMult() {
        float newNum = Mathf.Round(Random.Range(4f,16f));
        while (newNum % 3 != 0)
        {
            newNum = Random.Range(6,18);
        }
        return newNum;
    }
}
