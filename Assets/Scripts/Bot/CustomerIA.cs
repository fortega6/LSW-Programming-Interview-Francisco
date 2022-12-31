using UnityEngine;

public class CustomerIA : MonoBehaviour
{
    public Transform exitPoint;
    public Transform [] wayPoints;

    private float navigationUpdate;
    private Animator anim;
    private Rigidbody2D rb;
    private int target = 0;
    private Transform enemy;
    private Collider2D enemyCollider;
    private float navigationTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (wayPoints != null)
        {
            navigationTime += Time.deltaTime;
            if (navigationTime > navigationUpdate)
            {
                if (target < wayPoints.Length)
                {
                    //rb.MovePosition(transform.position + wayPoints[target].position * Time.fixedDeltaTime * navigationTime);
                    enemy.position = Vector2.MoveTowards(enemy.position, wayPoints[target].position, 0.8f * navigationTime);
                }
                else
                {
                    enemy.position = Vector2.MoveTowards(enemy.position, exitPoint.position, 0.8f * navigationTime);
                    //rb.MovePosition(transform.position + wayPoints[target].position * Time.fixedDeltaTime * navigationTime);
                }
                navigationTime = 0;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D");
        if (other.tag == "WayPoint")
            target += 1;
    }
}
