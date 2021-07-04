using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneGrid : MonoBehaviour
{
    bool isHit
    {
        get
        {
            return hits.Length > 0;
        }
    }
    public Vector3 direction = -Vector3.up;
    public float maxDistance;
    public LayerMask layerMask;
    public Vector3 positionOffset = Vector3.zero;
    public NodeObject nodeObject;
    RaycastHit hit;
    public RaycastHit[] hits = new RaycastHit[0];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Vector3 InstantiateNodeObject2(NodeObject nObj, Vector3 pos)
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
    public Vector3 InstantiateNodeObject(NodeObject nObj, Vector3 pos)
    {
        nodeObject = nObj;
        hits = Physics.BoxCastAll(pos, nObj.transform.lossyScale / 2, direction, nObj.transform.rotation, maxDistance, layerMask);
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (i == 0)
                {
                    hit = hits[i];
                }
                else
                {
                    if (hit.point.y < hits[i].point.y)
                    {
                        hit = hits[i];
                    }
                }
            }

            Vector3 vector3 = hit.point + positionOffset + new Vector3(0, nObj.rnd.bounds.size.y / 2, 0);
            nObj.transform.position = new Vector3(nObj.transform.position.x, vector3.y, nObj.transform.position.z);

        }

        nodeObject = null;

        return nObj.transform.position;
    }
    void OnDrawGizmos()
    {

        if (nodeObject == null) return;


        if (isHit)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, direction * hit.distance);
            Gizmos.DrawWireCube(transform.position + direction * hit.distance, transform.lossyScale);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, direction * maxDistance);
        }
    }
}
