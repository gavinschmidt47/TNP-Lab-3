using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject player;
    private Vector3 directionToPlayer;
    private float distanceToPlayer;
    private float dot;
    private float startingDistanceToPlayer;

    [SerializeField]
    private float avoidanceSpeed = 12f;
    [SerializeField]
    private float distanceFromPlayerLimitMultiplier = 1.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        startingDistanceToPlayer = (player.transform.position - transform.position).magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        //Get direction and distance to player
        directionToPlayer = (player.transform.position - transform.position).normalized;
        distanceToPlayer = Mathf.Abs((player.transform.position - transform.position).magnitude);

        //Rotate towards player
        dot = Vector3.Dot(transform.TransformDirection(Vector3.left), directionToPlayer);
        //if dot is negative, rotate left, if positive rotate right
        if (dot < 0)
        {
            transform.Rotate(0, -1, 0);
        }
        else if (dot > 0)
        {
            transform.Rotate(0, 1, 0);
        }

        //Centripetal movement around player
        transform.position += transform.TransformDirection(Vector3.left) * distanceToPlayer * Time.deltaTime;

        //Move towards or away from player to maintain starting distance
        if (distanceToPlayer < startingDistanceToPlayer)
        {
            transform.position -= new Vector3(directionToPlayer.x, 0, directionToPlayer.z) * Time.deltaTime * (startingDistanceToPlayer - distanceToPlayer) * avoidanceSpeed;
        }
        else if (distanceToPlayer > startingDistanceToPlayer * distanceFromPlayerLimitMultiplier)
        {
            transform.position += new Vector3(directionToPlayer.x, 0, directionToPlayer.z) * Time.deltaTime * (distanceToPlayer - startingDistanceToPlayer * distanceFromPlayerLimitMultiplier);
        }
    }
}
