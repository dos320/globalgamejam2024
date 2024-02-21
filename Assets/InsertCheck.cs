using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;



public class InsertCheck : MonoBehaviour
{

    public float forceToBreak = 2000;
    public float torqueToBreak = 2000;
    public LayerMask m_LayerMask;
    public List<Collider> colliders = new List<Collider>();

    //FixedJoint joint;
    // Start is called before the first frame update
    void Start()
    {
        //joint = gameObject.AddComponent<FixedJoint>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // edit existing 'notCooked' tag on each ingredient's customTag to different values 
        // depending on how long it stays in collision with the microwave this script is attached to
        
        if (gameObject.tag == "container")
        {
            
            if (gameObject.GetComponent<MicrowaveTimer>().bCutoffTriggered)
            {
                {
                    int i = 0;
                    while (i < colliders.Count)
                    {
                        int iOvercookedAmount = gameObject.GetComponent<MicrowaveTimer>().iOvercookedAmount;
                        if (iOvercookedAmount < 1)
                            colliders[i].gameObject.GetComponent<CustomTag>().rename(1, "cooked");
                        else if(iOvercookedAmount == 1)
                            colliders[i].gameObject.GetComponent<CustomTag>().rename(1, "overcooked");
                        else if(iOvercookedAmount == 2)
                            colliders[i].gameObject.GetComponent<CustomTag>().rename(1, "burnt");
                    }
                }
            }
        }
    }

 

    /*
    private void OnTriggerEnter(Collider other)
    {
        // other.transform.parent.gameObject.GetComponent<CustomTag>().hasTag("canBeInserted")
        if (other.transform.gameObject.GetComponent<CustomTag>().hasTag("canBeInserted"))
        {
            // connect the two objects
            Debug.Log("attach");
        }
    }
    */

    private void OnCollisionEnter(Collision collision)
    {
        try
        {   
            // make ingredients stick together
            if (collision.gameObject.GetComponent<CustomTag>().hasTag("canBeInserted") && gameObject.tag != "container")
            {
                Debug.Log("collide");
                FixedJoint joint = gameObject.AddComponent<FixedJoint>();

                ContactPoint contact = collision.contacts[0];
                //joint.anchor = transform.InverseTransformPoint(contact.point);
                joint.connectedBody = collision.contacts[0].otherCollider.transform.GetComponent<Rigidbody>();

                //set the forces which will break the joint
                joint.breakForce = forceToBreak;
                joint.breakTorque = torqueToBreak;

                // stops objects from continuing to collide and creating more joints
                joint.enableCollision = false;
                Debug.Log(joint);
            }else if(gameObject.tag == "container" && collision.gameObject.GetComponent<CustomTag>().hasTag("canBeInserted"))
            {
                
            }
        }
        catch(NullReferenceException e)
        {
            
        }

    }

    // written from perspective of ingredient
    // microwave and fryer are triggers
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("other.gameobject:" + other.gameObject);
        try
        {
            if (gameObject.GetComponent<CustomTag>().hasTag("canBeMicrowaved") && other.gameObject.tag == "microwave")
            {
                // used for microwave
                Debug.Log("ingredient inserted into microwave");
                //gameObject.GetComponent<MicrowaveTimer>().bStartTimer = true;
                // increase time cooked on
                gameObject.GetComponent<IngredientProperties>().bStartTimer = true;
                colliders.Add(other); // enables update of tags on the ingredients
            }
        }catch(NullReferenceException e) { }
        
    }
    // same as above
    private void OnTriggerExit(Collider other)
    {
        try
        {
            if (gameObject.GetComponent<CustomTag>().hasTag("canBeMicrowaved") && other.gameObject.tag == "microwave")
            {
                Debug.Log("container exited collision");
                gameObject.GetComponent<IngredientProperties>().bStartTimer = false;
                colliders.Remove(other);
            }
        }
        catch (NullReferenceException e) { }   
        
    }
}
