using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class DeliveryZone : MonoBehaviour
{
    private bool bCheckIngredientsCalled = false;
    List<Collider> colliders = new List<Collider>();
    private GameObject global;
    AudioSource audioData;
    public AudioClip correct;
    public AudioClip incorrectBuzzer;

    private void Start()
    {
        global = GameObject.Find("Global");
        audioData = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("delivery zone trigger entered");
        colliders.Add(other);
        if (!bCheckIngredientsCalled)
            StartCoroutine(checkIngredients());
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("delivery zone trigger exited");
        colliders.Remove(other);
    }

    IEnumerator checkIngredients()
    {
        bCheckIngredientsCalled = true;
        yield return new WaitForSeconds(4);
        string[] foundIngredients = new string[colliders.Count]; // check how many ingredients are in zone after inserting
        // check against all orders
        int counter = 0;
        foreach (var collider in colliders)
        {
            foreach (var tag in collider.gameObject.GetComponent<CustomTag>().getTags())
            {
                if (global.gameObject.GetComponent<GlobalVariables>().possibleStrings.Contains(tag))
                {
                    foundIngredients[counter++] = tag;
                    Debug.Log("found ingredient");
                }
            }
        }

        // go through all orders in GlobalVariables 
        // success: remove Order, make the order's parent go away
        // fail: increase anger level
        bool bMatchFound = false;
        foreach(Order order in global.GetComponent<GlobalVariables>().orders)
        {
            if (ArraysEquals(order.Orders, foundIngredients))
            {
                //remove order
                Debug.Log("correct ingredients detected in zone" + ArraysEquals(order.Orders, foundIngredients));
                Destroy(order.Parent); // destroy order parent
                global.GetComponent<GlobalVariables>().customerGameObjects.Clear();
                global.GetComponent<GlobalVariables>().orders.Remove(order);
                bMatchFound = true;
                break;
            }

        }
        // increase angerLevel if no match was found (ie. incorrect match)
        // also destroy items
        // allow check ingredients to be called again
        bCheckIngredientsCalled = false;
        if (!bMatchFound)
        {
            Debug.Log("wrong ingredients found, play buzzer");
            global.GetComponent<GlobalVariables>().angerLevel++;
            // play BAD SOUND EFFECT
            audioData.clip = incorrectBuzzer;
            audioData.Play(0);
        }
        else
        {
            RemoveColliders();
            global.GetComponent<GlobalVariables>().angerLevel--;
            audioData.clip = correct;
            audioData.Play(0);
        }

    }

    static bool ArraysEquals(string[] A, string[] B)
    {
        Debug.Log(String.Join("", A) + " " + String.Join("", B));
        bool result = (A.Length == B.Length);
        if (result)
        {
            foreach (string X in A)
            {
                if (!B.Contains(X))
                {
                    return false;
                }
            }
        }
        return result;
    }

    void RemoveColliders()
    {
        try
        {
            foreach (var collider in colliders)
            {
                Destroy(collider.gameObject);
            }
        }catch (Exception ex) { }
        
    }
}
