using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneGrid : MonoBehaviour
{
    public Vector3 direction = -Vector3.up;
    public RaycastHit hit;
    public float maxDistance;
    public LayerMask layerMask;
    public Vector3 positionOffset = Vector3.zero;
    public NodeObject nodeObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Vector3 InstantiateNodeObject(NodeObject nObj, Vector3 pos)
	{
        Debug.DrawRay(nObj.transform.position, direction * maxDistance, Color.green);
        if (Physics.Raycast(pos, direction, out hit, maxDistance, layerMask))
        {
            print(hit.point);
            Vector3 hitPos = hit.point + positionOffset + new Vector3(0, nObj.rnd.bounds.size.y / 2, 0);
            Vector3 moddedPos = nObj.transform.position;
            moddedPos.y = hitPos.y;
            nObj.transform.position = moddedPos;
        }

        return nObj.transform.position;
    }
}
