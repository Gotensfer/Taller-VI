using UnityEngine;

[System.Serializable]
public class LaunchModule : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    private Vector3 dir;
    [HideInInspector] public float angle = 0f;
    [HideInInspector] public float force = 1f;

    //VARIABLES UTILIDAD
    private float angleRad;

    void Start()
    {
        //deberia de solicitar a un script con la info del player su componente RigidBody
        //_rb = PlayerInfo.instance.rb;
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

        dir.x = Mathf.Cos(angleRad);
        dir.y = Mathf.Sin(angleRad);
    }

    void Stop()
    {
        _rb.velocity = Vector2.zero;
        _rb.angularVelocity = 0;
    }
}
