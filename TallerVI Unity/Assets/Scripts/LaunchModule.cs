using System.Linq.Expressions;
using UnityEngine;

[System.Serializable]
public class LaunchModule : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector3 dir;
    [HideInInspector] public float angle { get; set; }
    [HideInInspector] public float force { get; set; }

    [SerializeField] PlayerEvents_Interface playerEvents;
    [SerializeField] private int angleMovementVelocity;

    [SerializeField] GameObject launchZoneButton;

    //Temporal
    Vector3[] points = new Vector3[2];

    //VARIABLES UTILIDAD
    private float angleRad;
    public float length = 2;
    private bool flag = false; //Angle animator switch
    

    void Start()
    {
        angle = 0;
        _rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        points[0] = _rb.transform.position;
    }
    
    void Update()
    {
        CalculateDirection();
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Launch();
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            Stop();
        }
    }

    public void Launch()
    {
        force = Test_LaunchData.strenght;
        if (Test_LaunchData.strenght < 5) force = 30;
        Test_LaunchData.ResetLaunchData();
        _rb.AddForce(dir * force, ForceMode2D.Impulse);
        playerEvents.LaunchEvent.Invoke();
        launchZoneButton.SetActive(false);
    }

    void CalculateDirection()
    {
        //Angle Animator
        switch (flag)
        {
            case false:
                angle += angleMovementVelocity*Time.deltaTime;
                if (angle >= 90)
                {
                    flag = true;
                } 
                break;
            case true:
                angle -= angleMovementVelocity*Time.deltaTime;
                if (angle<=0)
                {
                    flag = false;
                }
                break;
        }
        
        //Transformation to Radians
        angleRad = Mathf.Deg2Rad * angle;

        //Calculate direction
        dir.x = length * Mathf.Cos(angleRad);
        dir.y = length * Mathf.Sin(angleRad);



        points[1] = new Vector3(dir.x + points[0].x, dir.y + points[0].y, dir.z);

        FindObjectOfType<LineController>().SetUp(points);
    }
    

    void Stop()
    {
        _rb.velocity = Vector2.zero;
        _rb.angularVelocity = 0;
    }
}
