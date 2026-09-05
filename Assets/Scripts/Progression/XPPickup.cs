using UnityEngine;

public class XPPickup : MonoBehaviour
{
    private float xpAmount;
    private XPPickupPool pool;

    public void SetPool(XPPickupPool xpPool)
    {
        pool = xpPool;
    }

    public void Initialize(float amount)
    {
        xpAmount = amount;
    }

    private void OnTriggerEnter(Collider other)
    {
        XPSystem xpSystem = other.GetComponentInParent<XPSystem>();

        if (xpSystem == null)
            return;

        xpSystem.AddXP(xpAmount);

        if (pool != null)
        {
            pool.Release(this);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}