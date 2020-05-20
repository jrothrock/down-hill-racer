using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float forwardForce = 1000f;
    public float sidewaysForce = 600f;

    // Update is called once per frame
    void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>(); 
        rb.AddForce(0,-40,forwardForce * Time.deltaTime);
        
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
