using Microsoft.Cci;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public const float MAX_AIRACCEL = 76.2f;//4000 HU/s^2 = 76.2 m/s^2
    public const float MAX_AIRSPEED = 0.5715f;//30 HU/s = 0.5715 m/s
    public const float MAX_GROUNDACCEL = 76.2f;//400 HU/s^2 = 7.62 m/s^2
    public const float MAX_GROUNDSPEED = 7.62f;//30 HU/s = 0.5715 m/s
    public const float FRICTION = 10f;
    public const float SPRINT_FACTOR = 2f;
    public PlayerHealth playerHealth;
    public int CollisionDamage = 10;
    private Camera mainCam;

    public LineRenderer lr;
    public Transform grappleOrigin;
    public GameObject grappleTarget = null;
    public int grappleRange = 200;
    public static float rotationSpeed = 180f;//might change this to central configuration file
    public float jumpTimeDelay = 0.1f;//change this to possibly make bhopping easier if buttonDown is used
    public float jumpTimer = 0f;
    public float jumpForce = 100f;
    public float groundedTimer = 0f;
    public float groundedTime = 0.1f;
    public float gravity = 9.8f;
    public float airDashForce = 10f;
    public float dashForce = 20f;
    public float airKnockBackForce = -5f;
    public float knockBackForce = -10;
    public float dashRefreshTime = 1f;
    public float dashTimer = 1f;
    public float grappleForce = 10f;
    public int dashCounter = 1;
    public int maxDashes = 1;
    public bool sprinting = false;
    public Vector3 vel;
    public PlayerAudio playerAudio;
    public float stepTimer = 2f;
    CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        vel = new Vector3(0,0,0);
        controller = GetComponent<CharacterController>();
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //rotation
        Vector3 rotation = new Vector3(0,Input.GetAxisRaw("Mouse X"),0);
        transform.Rotate(rotation * rotationSpeed * Time.deltaTime);
        print(rotation);

        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));
        input = transform.rotation * input;//make input relative to rotation of the character
        input.Normalize();

        applyGravity();

        checkGround();

        //jump needs to be after ground detection and horizontal move cancellation
        jump();
        //movement
        grapple();

        bool onground = groundedTimer > 0;

        if(!onground)
            airMove(input);
        else
            groundMove(input);
    	
        dash(input, !onground);

        controller.Move(vel * Time.deltaTime);
        //print(vel.magnitude);
    }

    public void addForce(Vector3 force)
    {
        vel += force;
    }

    private void checkGrappleTarget()
    {
        // cast ray and try to find a target to grapple towards
        // TODO: currently uses a raycast but might be better for gameplay if it uses something different
        // that way an obstruction of view wont disable grappling ability
        Vector3 mainDir = mainCam.transform.forward;
        Vector3 startPoint = mainCam.ViewportToWorldPoint (new Vector3(0.5f, 0.5f, mainCam.nearClipPlane));

        RaycastHit hit;

        if(grappleTarget != null)
        {
            grappleTarget.GetComponent<GrappleTargetScript>().deselectTarget();
        }
    	Debug.DrawRay(startPoint,mainDir * grappleRange,Color.green,0.5f);
        if(Physics.Raycast(startPoint,mainDir,out hit, grappleRange))
        {
            if(hit.collider.CompareTag("grappleTarget"))
            {
                GrappleTargetScript newT = hit.collider.gameObject.GetComponent<GrappleTargetScript>();
                newT.selectTarget();

                grappleTarget = hit.collider.gameObject;
            }
        }else //no target found
        {
            grappleTarget = null;
        }
    }

    private void grapple()
    {
        if (Input.GetKey(KeyCode.Q) && grappleTarget != null)
        {
            lr.enabled = true;
            lr.SetPositions(new Vector3[]{grappleOrigin.position,grappleTarget.transform.position});
            grappleAccelerate(grappleTarget.transform.position);
            //grappleAccelerate(new Vector3(0,10,0));
        }else
        {
            lr.enabled = false;
            checkGrappleTarget();
        }
    }

    private void grappleAccelerate(Vector3 target)
    {
        // Vector3 wishdir = target - transform.position;
        // float currentSpeed = Vector3.Dot(vel,wishdir);
        // float testFactor = 0.75f;
        // float addSpeed = Mathf.Clamp(MAX_AIRSPEED - currentSpeed,0,MAX_AIRACCEL * testFactor * Time.deltaTime);

        // vel += addSpeed * wishdir;
        
        //currently only uses gravity as constant force, dont think other forces should be accounted for
        Vector3 n = transform.position - target;//vector from target to player
        float dist = n.magnitude;//distance from target to player
        n.Normalize();
        float comp = Mathf.Max(0,Vector3.Dot(n,vel));//component of velocity away from target
        vel -= n * comp;//keep vel in line, should make it perpundicular to the n vector
        Vector3 Fg = new Vector3(0,-gravity,0);//Force of gravity, m = 1
        float Ft = Vector3.Dot(n,Fg);//ratio between n up direction and force of gravity

        Vector3 tensionF = -n * Ft;
        
        vel += tensionF * Time.deltaTime;
    }

    private void dash(Vector3 wishdir, bool inAir)
    {
        Vector3 input = wishdir;
        if(wishdir.magnitude == 0)
        {
            input = transform.rotation * Vector3.forward;
        }
        //refresh dash when on ground
        dashTimer -= (dashTimer < 0 || dashCounter == maxDashes) ? 0 : Time.deltaTime;
        if (dashTimer <= 0 && dashCounter < maxDashes)
        {
            dashCounter++;
            dashTimer = dashRefreshTime;
        }
        if(Input.GetButtonDown("Dash") && dashCounter > 0)
        {
            // print("Dash");
            float force = inAir ? airDashForce : dashForce;
            //Same as jump no time.deltaTime since this should be only applied
            // for one instance of time and should be equal for all framerates
            vel += input * force;
            dashCounter--;
        }
    }
    public void knockBack()
    {   
        print("Knocback");
        Vector3 mainDir = mainCam.transform.forward;
        float force = knockBackForce;
        vel = vel + mainDir * force;
    }


    private void jump()
    {
        jumpTimer -= jumpTimer < 0 ? 0 : Time.deltaTime;//only decrease jumptimer when neccecary
        //witch these two if expressions to enable/disable bhopping
        // if(Input.GetButtonDown("Jump"))
        if(Input.GetButton("Jump"))
        {
            jumpTimer = jumpTimeDelay;
        }

        if(jumpTimer >= 0 && groundedTimer > 0)
        {
            //DO NOT USE Time.deltaTime for jumping, its only applied in one frame
            // a different framerate will result in a different change in velocity.
            vel.y += jumpForce; //apply upwards force  
            groundedTimer = 0; 
            jumpTimer = -1;
            //play step sound
        }
    }

    private void applyFriction()
    {
        float speed = vel.magnitude;
        
        if(speed == 0) 
            return;
        
        float drop = speed * FRICTION * Time.deltaTime;
        float newspeed = speed - drop;
        
        if(newspeed < 0)
            newspeed = 0;

        newspeed /= speed;
        vel *= newspeed;
    }

    private void groundMove(Vector3 wishdir)
    {
        if(wishdir.magnitude > 0.1){stepTimer -= Time.deltaTime;}
        if(stepTimer < 0)
        {
            playerAudio.playStep();
            stepTimer = 0.5f;
        }


        sprinting = false;

        if(Input.GetKey(KeyCode.LeftShift))
        {
            sprinting = true;
        }

        groundAccelerate(wishdir);
    }

    private void groundAccelerate(Vector3 wishdir)
    {
        applyFriction();

        float currentSpeed = Vector3.Dot(vel,wishdir);
        float maxSpeed = MAX_GROUNDSPEED;
        float maxAcc = MAX_GROUNDACCEL;
        if(sprinting)
        {
            maxSpeed *= SPRINT_FACTOR;
            maxAcc *= SPRINT_FACTOR;
        }

        //print(maxSpeed);
        float addSpeed = Mathf.Clamp(maxSpeed - currentSpeed,0,maxAcc * Time.deltaTime);

        vel += addSpeed * wishdir;
    }

    private void airMove(Vector3 wishdir)
    {
        airAccelerate(wishdir);
    }

    private void airAccelerate(Vector3 wishdir)
    {
        float currentSpeed = Vector3.Dot(vel,wishdir);
        float addSpeed = Mathf.Clamp(MAX_AIRSPEED - currentSpeed,0,MAX_AIRACCEL * Time.deltaTime);

        vel += addSpeed * wishdir;
    }

    private void applyGravity()
    {
        vel.y -= gravity * Time.deltaTime;
    }

    private void checkGround()//update onground variable
    {
        groundedTimer -= groundedTimer < 0 ? 0 : Time.deltaTime;
        
        //if were moving upwards we can not be grounded right?
        //
        if(vel.y > 0) return;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        Vector3 rayPos = transform.position + new Vector3(0,-1,0);
        Vector3 down = Vector3.down;
        float dist = 0.2f;
        Debug.DrawRay(rayPos, Vector3.down * dist, Color.yellow);
        
        if (Physics.Raycast(rayPos,down,out hit,dist))
        {
            if(hit.collider.gameObject.CompareTag("Ground"))
            {
                groundedTimer = groundedTime;
                vel.y = 0;//were on the ground so no downwards movement is neccesary
                
                //snap player to ground
                Vector3 point = hit.point;
                Vector3 target = rayPos + down * dist;
                Vector3 diff = point - target;
                controller.Move(diff);
            }
        }
    }

    private void OnTriggerEnter(Collider other){
        if(other.tag =="Enemy" || other.tag == "Damage_Source"){
            playerHealth.takeDamage(CollisionDamage);
        }

    }

}
