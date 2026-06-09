using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class testmovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private RaycastHit hitInfo = new RaycastHit();

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            var ray = Camera.main.ScreenPointToRay(mousePos);
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
                agent.destination = hitInfo.point;
            Debug.Log(agent.destination);
        }
    }
}
