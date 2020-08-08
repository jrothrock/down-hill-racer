using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float ForwardForce = 800f;
    public float SidewaysForce = 1600f;
    public float SideWaysSpeed = 15f;

    private Rigidbody _rb;

    private void Start() {
        _rb = GetComponent<Rigidbody>(); 
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        _rb.AddForce(0,-40,ForwardForce * Time.deltaTime);

        float x = Input.GetAxis("Horizontal") * Time.fixedDeltaTime * SideWaysSpeed;
        _rb.MovePosition(_rb.position + Vector3.right * x);
        if (Input.GetAxis("Horizontal") > 0)
        {
            _rb.AddForce(SidewaysForce * Time.deltaTime,0,0);
        } 
        
        if (Input.GetAxis("Horizontal") < 0)
        {
            _rb.AddForce(-SidewaysForce * Time.deltaTime,0,0);
        } 

        if (_rb.velocity.sqrMagnitude > 1225) {
            _rb.velocity = _rb.velocity.normalized * 35;
        }
    }
}
