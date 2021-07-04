using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSpawner : MonoBehaviour
{
    bool isHit
    {
        get
        {
            return hits.Length > 0;
        }
    }
    private Vector3 direction = -Vector3.up;
    public float maxDistance;
    public LayerMask layerMask;
    public Vector3 positionOffset = Vector3.zero;
    private NodeObject nodeObject;
    RaycastHit hit;
    public RaycastHit[] hits = new RaycastHit[0];

    public void Spawn(int x, int y, Vector3 worldPoint, NodeObject nodePrefab, GameObject GridNodes, NodeGrid NodeGrid)
	{

        this.nodeObject = Instantiate(nodePrefab, worldPoint, Quaternion.identity);
        hits = Physics.BoxCastAll(worldPoint, this.nodeObject.transform.lossyScale / 2, direction, this.nodeObject.transform.rotation, maxDistance, layerMask);
        bool walkable = false;
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


            Vector3 vector3 = hit.point + positionOffset + new Vector3(0, this.nodeObject.rnd.bounds.size.y / 2, 0);
            this.nodeObject.transform.position = new Vector3(this.nodeObject.transform.position.x, vector3.y, this.nodeObject.transform.position.z);
            walkable = !(hit.transform.gameObject.layer == LayerMask.NameToLayer("Unwalkable"));
        }

        worldPoint = this.nodeObject.transform.position;

        TIleMode tileMode;
        if(walkable)
		{
            tileMode = TIleMode.DEFAULT;
        }
        else
		{
            tileMode = TIleMode.UNREACHABLE;
		}
        this.nodeObject.transform.parent = GridNodes.transform;
        NodeGrid.NodeArray[x, y] = new Node(walkable, worldPoint, x, y, (int)tileMode,this.nodeObject);

        this.nodeObject = null;
    }

    void OnDrawGizmos()
    {

        if (nodeObject == null) return;


        if (isHit)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(nodeObject.transform.position, direction * hit.distance);
            Gizmos.DrawWireCube(nodeObject.transform.position + direction * hit.distance, nodeObject.transform.lossyScale);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(nodeObject.transform.position, direction * maxDistance);
        }
    }
}
