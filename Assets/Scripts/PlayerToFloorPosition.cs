using UnityEngine;
using System.Collections;

public class PlayerToFloorPosition : MonoBehaviour
{
    public GameObject Player;
    public Transform Floor;
    public GameObject FloorPrefab;
    public GameObject ObstaclePrefab;
    
    private GameObject _floorObj;
    private Vector3 _initPosition;
    private GameObject _newFloor;
    private GameObject[] _oldObstacles;

    private float _zVelocity = 0f;
    private float _yVelocity = 0f;
    private float _changeY = 0f;
    private float _changeZ = 0f;

    private bool _hitZero = true;
    private bool _playerOnFloor = false;

    private float _finalY = 0f;
    private float _finalZ = 0f;
    private float _finalX = 0f;

    private float _timeDelta = 0f;

    private int _oldFloorTagNumber;

    void OnCollisionEnter(Collision collisionInfo) {
        Debug.Log(collisionInfo.collider);
        if (collisionInfo.collider.tag == "Player")
        {
            Debug.Log("here");
            _playerOnFloor = true;
            //playerMovement.enabled = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
      Player = GameObject.FindGameObjectWithTag("Player");
      _floorObj = GameObject.FindGameObjectsWithTag("Floor")[GameObject.FindGameObjectsWithTag("Floor").Length - 1];
      Floor = GameObject.FindGameObjectsWithTag("Floor")[GameObject.FindGameObjectsWithTag("Floor").Length - 1].transform;
    }

    // Update is called once per frame
    void Update()
    {
        float endDistance = Player.transform.position.z - Floor.position.z;
        float floorLength = Floor.localScale.z;
        if ((endDistance > (floorLength/2*0.95)) && (endDistance < floorLength/2*0.98))
        {
            _playerOnFloor = false;
            _timeDelta = Time.time;
            _initPosition = Player.transform.position;
        }
        if ((endDistance >= (floorLength/2*.98)) && (_hitZero == true))
        {
            _hitZero = false;
            float nowTime = Time.time;
            float timeFall = Random.Range(2.5f,5.0f);
            Vector3 nowPlayerPos = Player.transform.position;
            // Debug.Log("init");
            // Debug.Log(nowTime);
            // Debug.Log(_timeDelta);
            // Debug.Log(nowPlayerPos);
            // Debug.Log(_initPosition);
            // Debug.Log("vel");
            _zVelocity = ((nowPlayerPos.z - _initPosition.z)) / (nowTime - _timeDelta);
            _yVelocity = ((nowPlayerPos.y - _initPosition.y)) / (nowTime - _timeDelta);
            // Debug.Log(_zVelocity);
            // Debug.Log(_yVelocity);
            // Debug.Log("TF");
            // Debug.Log(timeFall);
            _changeY = (_yVelocity * timeFall) + ((0.5f * -Physics.gravity.magnitude) * Mathf.Pow(timeFall,2));
            _changeZ = _zVelocity * timeFall;
            // Debug.Log("Change");
            // Debug.Log(_changeY);
            // Debug.Log(_changeZ);
            float newLength = Mathf.Round(Random.Range(350,600));
            // Debug.Log("Newlength");
            // Debug.Log(newLength);
            _finalY = Floor.position.y + _changeY - (Mathf.Tan((20 * Mathf.PI / 180)) * newLength/2);
            _finalZ = Floor.position.z + _changeZ + newLength/4 - 100;
            // Debug.Log(_finalZ - floor.position.z);
            if ((newLength/(_finalZ - Floor.position.z)) > 0.2){
                _finalZ += newLength / 4; 
            }
            _finalX = Floor.transform.position.x;
            // Debug.Log("Final");
            Debug.Log(_finalZ);
            Debug.Log(_finalY);
            Debug.Log(_finalX);
            _newFloor = Instantiate(FloorPrefab, new Vector3(_finalX, _finalY, _finalZ), Floor.rotation);
            float newWidth = getRandomThreeMult();
            Vector3 newScale = new Vector3(newWidth, 1, newLength);
            _newFloor.transform.localScale = newScale;
            createObstacles(newScale);
            // Debug.Log("=====");
        }

        if (GameObject.FindGameObjectsWithTag("Floor").Length == 3) {
            Destroy(GameObject.FindGameObjectsWithTag("Floor")[0]);
            _oldObstacles = GameObject.FindGameObjectsWithTag("Obstacle" + _oldFloorTagNumber.ToString());
            for (var i = 0 ; i < _oldObstacles.Length ; i ++){
                Destroy(_oldObstacles[i]);
            }
        }

        if (((Mathf.Abs(Player.transform.position.x) > (Floor.localScale.x/2 + 0.5))) && (_playerOnFloor == true)) {
            FindObjectOfType<Score>().AllowScore = false;
            FindObjectOfType<GameManager>().EndGame();
        }

    }

    void createObstacles(Vector3 scale){
        float beginningZ = _newFloor.transform.position.z - (scale.z/2);
        float squaresDistance = 30f;
        float sizeOfObs = 3f;
        float numberRows = Mathf.Floor(scale.z / squaresDistance);
        float numberColumns = scale.x / sizeOfObs;
        string newTag;

        Debug.Log(numberRows);
        if (numberColumns == 2.0){
            numberRows *= 2f;
            squaresDistance /= 2f;
        }
        Debug.Log(numberRows);

        if (GameObject.FindGameObjectsWithTag("Obstacle1").Length == 0) {
            newTag = "Obstacle1";
            _oldFloorTagNumber = 2;
        } else if (GameObject.FindGameObjectsWithTag("Obstacle2").Length == 0) {
            newTag = "Obstacle2";
            _oldFloorTagNumber = 3;
        } else {
            newTag = "Obstacle3";
            _oldFloorTagNumber = 1;
        }

        for (int i = 0; i < numberRows; i++)
        {
            float noObs = Mathf.Round(Random.Range(0,numberColumns - 1));
            float yPosition = _newFloor.transform.position.y - (Mathf.Tan((20 * Mathf.PI / 180)) * ((scale.z/2) - ((i+1)*squaresDistance))) + 1;
            float zPosition = _newFloor.transform.position.z + (scale.z/2 - ((i+1)*squaresDistance));
            
            for (int j = 0; j < numberColumns; j++ ) 
            {
                if (j != noObs) 
                {
                    float xPosition;

                    if (numberColumns % 2 == 0)  {
                        if (j < (numberColumns/2)) {
                            xPosition = 0f - (sizeOfObs/2) - ((numberColumns/2 - (j+1)) * sizeOfObs);
                        } else {
                            xPosition = 0f + (sizeOfObs/2) + ((j-(numberColumns/2)) * sizeOfObs);
                        }
                    } else {
                        if (j <= Mathf.Floor(numberColumns/2)) {
                            xPosition = 0f - ((Mathf.Floor(numberColumns/2) - j) * sizeOfObs);
                        } else {
                            xPosition = ((j-Mathf.Floor(numberColumns/2)) * sizeOfObs);
                        }
                    }
    
                    Vector3 obsPosition = new Vector3(xPosition, yPosition, zPosition);
                    GameObject newObs = Instantiate(ObstaclePrefab, obsPosition, _newFloor.transform.rotation);
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
