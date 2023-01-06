using UnityEngine;

public class Customer : MonoBehaviour
{
    public Transform exitPoint;
    public WayPoint[] wayPoints;

    public float navigationUpdate;
    private Animator anim;
    private Rigidbody2D rb;
    private int _target = 0;

    public Vector3 Movement { get => _movement; set => _movement = value; }
    public int Target { get => _target; set => _target = value; }

    private float _navigationTime = 0;
    private float _timeElapsed = 0;
    private Vector3 _movement;
  

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _movement = (wayPoints[_target].transform.position - transform.position).normalized;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _timeElapsed += Time.fixedDeltaTime;
        if (_target > 0 && _target < wayPoints.Length && _timeElapsed < wayPoints[_target-1].wayTime)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        if (wayPoints != null)
        {
            _navigationTime += Time.fixedDeltaTime;
            if (_navigationTime > navigationUpdate)
            {
                if (_target < wayPoints.Length)
                {
                    rb.velocity = _movement * _navigationTime;
                }

                _navigationTime = 0;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "WayPoint") {
            _target += 1;
            _timeElapsed = 0;
            if (_target < wayPoints.Length)
                _movement = (wayPoints[_target].transform.position - wayPoints[_target-1].transform.position).normalized;
        }
    }
}
