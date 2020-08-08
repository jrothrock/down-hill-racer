using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Transform Player;
    public Text ScoreText;
    private bool _allowScore;

    public bool AllowScore { get {return _allowScore;} set {_allowScore = value;} }

    // Update is called once per frame
    void Update()
    {
        updateScore();
    }

    public void updateScore()
    {
        if (_allowScore) {
            ScoreText.text = (Mathf.Floor(Player.position.z + 52)).ToString();
        }
    }
}
