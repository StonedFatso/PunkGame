using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineController : MonoBehaviour
{
    LayerMask mask;
    LayerMask humanMask;
    private Health enemy;
    // Start is called before the first frame update
    void Start()
    {
        mask = LayerMask.GetMask("MovableObjects");
        humanMask = LayerMask.GetMask("People");
    }

    private void OnTriggerEnter(Collider other)
    {
        Blow(other);
    }

    void Blow(Collider foot)
    {
        Vector3 p1 = transform.position;
        GameObject footObj = foot.gameObject;
        footObj.GetComponent<Rigidbody>().AddForce((Vector3.up) * 10f, ForceMode.Impulse);
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 20, new Vector3(0, 1, 0), 20, mask, QueryTriggerInteraction.Ignore);
        RaycastHit[] peopleHits = Physics.SphereCastAll(transform.position, 20, new Vector3(0, 1, 0), 20, humanMask, QueryTriggerInteraction.Ignore);
        for (int i = 0; i < hits.Length; i++)
        {
            GameObject obj = hits[i].collider.gameObject;
            obj.GetComponent<Rigidbody>().AddForce((obj.transform.position - p1) * 1.5f, ForceMode.Impulse);
        }
        for (int i = 0; i < peopleHits.Length; i++)
        {
            GameObject obj = peopleHits[i].collider.gameObject;
            obj.GetComponent<Rigidbody>().AddForce((obj.transform.position - p1) * 1.5f, ForceMode.Impulse);
            enemy = foot.gameObject.GetComponent<Health>();
            enemy.Injury(50);
        }
        Destroy(gameObject);
    }
}
