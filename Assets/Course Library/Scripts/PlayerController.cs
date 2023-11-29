using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    //private float speed = 20;
    public float horsepower;
    private float turnSpeed = 45f;
    private float horizontalInput;
    private float forwardInput;
    private bool isVerticalMove;
    private string controller;
    private Rigidbody playerRb;
    //  [SerializeField] GameObject centerOfMass;
    [SerializeField] TextMeshProUGUI speedometerTextRight;
    [SerializeField] TextMeshProUGUI speedometerTextLeft;
    [SerializeField] TextMeshProUGUI rpmTextRight;
    [SerializeField] TextMeshProUGUI rpmTextLeft;
    [SerializeField] float rightCarSpeed;
    [SerializeField] float leftCarSpeed;
    [SerializeField] float rpmRight;
    [SerializeField] float rpmLeft;




    //switcing between cameras variables
    public GameObject Camera_1;
    public GameObject Camera_2;

    public bool isCamera1Active = true;


    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        // this is the tutorial advice for vehicle to not flip, but i find naother way in rigidbody component.
        //      playerRb.centerOfMass = centerOfMass.transform.position;

        if (player.name == "LeftVehicle")
        {
            controller = " WASD";
        }
        else
        {
            controller = " Arrow";
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal" + controller);
        forwardInput = Input.GetAxis("Vertical" + controller);
        isVerticalMove = Input.GetButton("Vertical" + controller);

        // transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        playerRb.AddRelativeForce(Vector3.forward * forwardInput * horsepower);

        if (isVerticalMove)
        {
            transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            isCamera1Active = !isCamera1Active;
            ChangeCamera();
        }
        //    rightCarSpeed = Mathf.Round(GameObject.FindWithTag("RightCar"));
        rightCarSpeed = Mathf.Round(GameObject.FindWithTag("RightCar").GetComponent<Rigidbody>().velocity.magnitude * 3.6f);
        //.velocity.magnitude * 3.6f); // 2.237 for mph
        leftCarSpeed = Mathf.Round(GameObject.FindWithTag("LeftCar").GetComponent<Rigidbody>().velocity.magnitude * 3.6f);

        speedometerTextRight.SetText("Speed: " + rightCarSpeed + " kmh");
        speedometerTextLeft.SetText("Speed: " + leftCarSpeed + " kmh");

        rpmRight = Mathf.Round((rightCarSpeed % 30) * 40);
        rpmTextRight.SetText("RPM: " + rpmRight);

        rpmLeft = Mathf.Round((leftCarSpeed % 30) * 40);
        rpmTextLeft.SetText("RPM: " + rpmLeft);


    }

    public void ChangeCamera()
    {
        if (isCamera1Active)
        {
            Cam_1();
        }
        else if (!isCamera1Active)
        {
            Cam_2();
        }
    }
    void Cam_1()
    {
        Camera_1.SetActive(true);
        Camera_2.SetActive(false);
    }
    void Cam_2()
    {
        Camera_1.SetActive(false);
        Camera_2.SetActive(true);
    }
}
