using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    public float rayDistance = 10f;
    public LayerMask layerMask;
    private Vector2 moveDirection;

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        if(moveX != 0 || moveY != 0)
        {
            moveDirection = new Vector2(moveX,moveY).normalized;            }

        if(Input.GetKeyDown(KeyCode.X))
        {
            Debug.DrawRay(transform.position,moveDirection * rayDistance,Color.red,0.1f);

            RaycastHit2D hit = Physics2D.Raycast(transform.position,moveDirection,rayDistance,layerMask);

            if(hit.collider != null)
            {
                Debug.Log("Hit object:" + hit.collider.name);
            }
            else
            {
            Debug.Log("No object hit.");
            }
        }
    }
}

