// Licensed under the LGPL 3.0
// See the LICENSE file in the project root for more information.
// Author: alexandre.via@i2cat.net

using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameObject paddle;
    public Rigidbody rb;
    public bool serve = false;
    public float magunsConstant = 5f;

    private bool activateBulletTimeBall = false;
    private float bulletTimeMod = 0.0f;


    private Vector3 oldPos;
    public Vector3 velocity;
    public Vector3 myAngularVelocity;
    public float magnitude;
    public ServerManager serverManager;

    private void Start()
    {
        //Physics.gravity = new Vector3(0, -9.8f, 0);
        Physics.gravity = new Vector3(0, -5, 0);
        //Debug.Log("Gravity: " + Physics.gravity);
        //Physics.gravity = new Vector3(0, -4f, 0);
        rb = GetComponent<Rigidbody>();
        if(BulletTime.Instance != null) BulletTime.Instance.OnBulletTimeStarted += TriggerBulletTime;
    }

    public float forceMagnitude = 100; //800;
    private void Update()
    {
        if (paddle != null && serve)
        {
            rb.isKinematic = false;
            if (BulletTime.Instance != null) BulletTime.Instance.SwitchBulletTime = false; //BulletTime is ready to be activated.
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.rotation = Quaternion.identity;
            Vector3 paddlePos = paddle.transform.position;
            rb.isKinematic = false;
            //Debug.DrawLine(paddlePos, paddlePos + paddle.transform.forward * 0.2f, Color.red, 10);
            //Debug.DrawLine(paddlePos + paddle.transform.forward * 0.2f,
            //    paddlePos + paddle.transform.forward * 0.2f + paddle.transform.up * 0.5f,
            //    Color.blue, 10);

            transform.position = paddlePos + paddle.transform.forward * 0.2f + paddle.transform.up * 0.3f;
            Debug.DrawLine(paddlePos + paddle.transform.forward * 0.2f, transform.position, Color.green, 10);


            rb.AddForce((paddlePos + paddle.transform.forward * 0.2f - transform.position).normalized * forceMagnitude);

            serve = false;
        }



        if (activateBulletTimeBall) {
            ActivateBulletTime();
            activateBulletTimeBall = false;
        }

        if(paddle == null)
        {
            paddle = GameObject.Find(Constants.RightHand);
            //paddle = GameObject.Find("Oponent");
        }


    }

    private void FixedUpdate()
    {
        if (paddle != null)
        {
            velocity = (paddle.transform.position - oldPos) / Time.fixedDeltaTime;
            magnitude = velocity.magnitude;
            oldPos = paddle.transform.position;

        }

        Vector3 magnusForce = magunsConstant * this.GetComponent<Rigidbody>().mass * Vector3.Cross(velocity, new Vector3(magnitude, magnitude, magnitude));
        rb.AddRelativeForce(magnusForce);

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    //Uncoment upper comments and add this code
        //    myAngularVelocity = new Vector3(10.5f, 10.5f, 0);
        //    velocity = new Vector3(10.5f, 10.5f, 10.5f);

        //    Vector3 magnusForce = /*magunsConstant*/ this.GetComponent<Rigidbody>().mass *  Vector3.Cross(velocity, myAngularVelocity);
        //    rb.AddRelativeForce(magnusForce);
        //}

        if (BulletTime.Instance != null && BulletTime.Instance.SwitchBulletTime) {
            rb.AddForce((-1 * Physics.gravity) * (1 - bulletTimeMod), ForceMode.Acceleration);
        }

    }

    public float speedMultiplier = 5;
    public float collisionForce = 5;
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision with " + collision.gameObject.name);
        if(collision.gameObject.name == Constants.RightHand)
        {
            //Debug.Log("Impulse " + collision.impulse);
            //deactivate bullet time if activated
            if (BulletTime.Instance != null) {
                if (BulletTime.Instance.SwitchBulletTime) {
                    serverManager.SendBulletStop();
                }

                BulletTime.Instance.SwitchBulletTime = false; //BulletTime is ready to be activated.
            }
            Vector3 dir = collision.contacts[0].point - transform.position;
            dir = -dir.normalized;

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(dir * collisionForce);
            //Debug.Log(dir * collisionForce);
            Debug.DrawLine(transform.position, collision.contacts[0].point * 100, Color.black, 10);
        }

        //if(collision.gameObject.name == Constants.RightHand && magnitude > 0.3f)
        //{
        //    Debug.Log("Collision with paddle");
        //    rb.velocity = velocity * speedMultiplier;
        //}

        //if (collision.gameObject.name == "paddle")
        //{
        //    Vector3 dir = collision.relativeVelocity.normalized;

        //    if(paddleSpeed.velocity.magnitude > 0.2f)
        //    {
        //        rb.velocity = Vector3.zero;
        //        rb.angularVelocity = Vector3.zero;
        //        rb.AddForce(dir * 2);
        //    }
        //    else
        //        rb.AddForce(dir);

        //    Debug.Log(paddleSpeed.velocity.magnitude);
        //}
    }

    private void TriggerBulletTime(float p_mod) {
        activateBulletTimeBall = true;
        bulletTimeMod = p_mod;
    }

    private void ActivateBulletTime() {
        rb.velocity *= bulletTimeMod;
        if (BulletTime.Instance != null) BulletTime.Instance.SwitchBulletTime = true;
    }

    private void OnDestroy() {
        if (BulletTime.Instance != null) BulletTime.Instance.OnBulletTimeStarted -= TriggerBulletTime;
    }
}
