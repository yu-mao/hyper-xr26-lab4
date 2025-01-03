using UnityEngine;

public class KillTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject != null)
        {
            Destroy(other.gameObject);
        }
    }
}
