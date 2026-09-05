using UnityEngine;
using System.Collections.Generic;

public class XPPickupPool : MonoBehaviour
{
    [SerializeField] private XPPickup xpPickupPrefab;
    [SerializeField] private int initialSize = 20;

    private readonly Queue<XPPickup> pool = new Queue<XPPickup>();

    private void Awake()
    {
        for (int i = 0; i < initialSize; i++)
        {
            CreatePickup();
        }
    }

    private XPPickup CreatePickup()
    {
        XPPickup pickup = Instantiate(xpPickupPrefab, transform);
        pickup.SetPool(this);
        pickup.gameObject.SetActive(false);

        pool.Enqueue(pickup);

        return pickup;
    }

    public XPPickup Get(Vector3 position, float xpAmount)
    {
        if (pool.Count == 0)
        {
            CreatePickup();
        }

        XPPickup pickup = pool.Dequeue();

        pickup.transform.position = position;
        pickup.Initialize(xpAmount);
        pickup.gameObject.SetActive(true);

        return pickup;
    }

    public void Release(XPPickup pickup)
    {
        pickup.gameObject.SetActive(false);
        pool.Enqueue(pickup);
    }
}