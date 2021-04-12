using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Camera cam;
    public delegate void hitRaycast(RaycastHit hit);
    public event hitRaycast OnHitRaycast;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
		{
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 900))
			{
				OnHitRaycast?.Invoke(hit);
			}

        }
    }
}
