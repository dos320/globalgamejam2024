using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;

public struct Order
{
    public Order(GameObject parent, string[] orders)
    {
        Parent = parent;
        Orders = orders;
    }
    public GameObject Parent { get; set; }
    public string[] Orders {  get; set; }
}

public class GlobalVariables : MonoBehaviour
{
    public List<string> possibleStrings;

    public List<Order> orders = new List<Order>(); // each order is a list
    public List<GameObject> customerGameObjects = new List<GameObject>();
    private float nextActionTime = 0.0f;
    public float period = 10f;
    public int angerLevel = 0;

    // references to existing customers
    private GameObject customer_1;
    private GameObject customer_2;
    private GameObject customer_3;
    private TextMeshPro text_1;
    private TextMeshPro text_2;
    private TextMeshPro text_3;
    private GameObject manager;
    public GameObject[] customers;
    public TextMeshPro[] texts;

    private void Start()
    {
        possibleStrings = new List<string> { "bun", "patty", "lettuce", "onion", "cheese"};
        customer_1 = GameObject.Find("Customer_1");
        customer_2 = GameObject.Find("Customer_2");
        customer_3 = GameObject.Find("Customer_3");
        text_1 = GameObject.Find("Customer_1 order_text").GetComponent<TextMeshPro>();
        text_2 = GameObject.Find("Customer_2 order_text").GetComponent<TextMeshPro>();
        text_3 = GameObject.Find("Customer_3 order_text").GetComponent<TextMeshPro>();

        manager = GameObject.Find("Manager");
        customers = new GameObject[]{ customer_1, customer_2, customer_3};
        texts = new TextMeshPro[] { text_1, text_2, text_3 };
        string[] generatedOrder = GenerateOrder();
        GameObject newCustomer = Instantiate(customer_1);
        Debug.Log("---------->new customer generated in start");
        Order newOrder = new Order(newCustomer, new string[]{ "bun" });

        newCustomer.GetComponentInChildren<TextMeshPro>().text = string.Join(", ", new string[] { "bun" });
        customerGameObjects.Add(newCustomer);
        orders.Add(newOrder);
    }

    // generate new customers every once in a while
    private void Update()
    {
        System.Random rnd = new System.Random();
        //Debug.Log(Time.time);
        //if(Time.time > nextActionTime)
        if(customerGameObjects.Count == 0)
        {
            nextActionTime += period;
            string[] generatedOrder = GenerateOrder();
            //GameObject newCustomer = new GameObject("customer", typeof(TextMeshPro));
            int randomCustomer = rnd.Next(0, 3);
            GameObject newCustomer = Instantiate(customers[randomCustomer]); // choose random customer to copy/instantiate
            //TextMeshPro newText = Instantiate(texts[randomCustomer], newCustomer.transform); // customer is the parent of the text
            Order newOrder = new Order(newCustomer, generatedOrder);

            Debug.Log("NEWCUST OMER HERE: ->>>>>>>>>>>>>>>>>>>>" + newCustomer);
            newCustomer.GetComponentInChildren<TextMeshPro>().text = string.Join("", generatedOrder);

            customerGameObjects.Add(newCustomer);
            orders.Add(newOrder);
        }
    }

    private string[] GenerateOrder()
    {
        System.Random rnd = new System.Random();
        int totalItems = rnd.Next(1, 7);
        string[] completedOrder = new string[totalItems];

        for (int i = 0; i<totalItems; i++)
        {
            int randIndex = rnd.Next(0, 5);
            
            //if (!completedOrder.Contains(possibleStrings[randIndex]))
            //{
                completedOrder[i] = possibleStrings[randIndex];
            //}

        }

        return completedOrder;
    }

}

