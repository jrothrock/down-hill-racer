using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float forwardForce = 800f;
    public float sidewaysForce = 1600f;
    public float sidewaysSpeed = 15f;

    private Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>(); 
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(0,-40,forwardForce * Time.deltaTime);

        // float x = Input.GetAxis("Horizontal") * Time.fixedDeltaTime * sidewaysSpeed;
        // rb.MovePosition(rb.position + Vector3.right * x);
        // if(Input.GetAxis("Horizontal") > 0)
        // {
        //     rb.AddForce(sidewaysForce * Time.deltaTime,0,0);
        // } 
        
        // if(Input.GetAxis("Horizontal") < 0)
        // {
        //     rb.AddForce(-sidewaysForce * Time.deltaTime,0,0);
        // } 

        if(Input.GetKey("right"))
        {
            rb.AddForce(sidewaysForce * Time.deltaTime,0,0);
        } 
        
        if(Input.GetKey("left"))
        {
            rb.AddForce(-sidewaysForce * Time.deltaTime,0,0);
        } 

        // Debug.Log("vel");
        // Debug.Log(rb.velocity.sqrMagnitude);
        if(rb.velocity.sqrMagnitude > 1225) {
            rb.velocity = rb.velocity.normalized * 35;
        }
    }
}
