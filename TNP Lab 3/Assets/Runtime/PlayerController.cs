using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float XZmoveSpeed = 5f;

    private Rigidbody rb;
    private void Awake()
    {
        rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }
    public void OnMove(InputValue input)
    {
        Vector3 move = new Vector3(input.Get<Vector2>().x, 0, input.Get<Vector2>().y).normalized;
        if (move != Vector3.zero)
        {
            rb.linearVelocity = new Vector3(move.x * XZmoveSpeed * Time.deltaTime, rb.linearVelocity.y, move.z * XZmoveSpeed * Time.deltaTime);
        }
        else
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
    }
}
