using UnityEngine;

[System.Serializable]
public class LaunchModule : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector3 dir;
    [HideInInspector] public float angle { get; set; }
    [HideInInspector] public float force { get; set; }

    //Temporal
    Vector3[] points = new Vector3[2];

    //VARIABLES UTILIDAD
    private float angleRad;
    public float length = 2;

    void Start()
    {
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
        _rb.AddForce(dir * force, ForceMode2D.Impulse);
    }

    void CalculateDirection()
    {
        angleRad = Mathf.Deg2Rad * angle;

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
