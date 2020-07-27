using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Transform player;
    public Text scoreText;
    public bool _canUpdate = true;

    // Update is called once per frame
    void Update()
    {
        updateScore();
    }

    public void allowScore(bool allowScore)
    {
        Debug.Log("called");
        Debug.Log(allowScore);
        _canUpdate = allowScore;
    }

    public void updateScore()
    {
        if(_canUpdate == true) {
            scoreText.text = (Mathf.Floor(player.position.z + 52)).ToString();
        }
    }
}
