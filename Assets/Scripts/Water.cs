using UnityEngine;

public class Water : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out CharacterController player))
        {
            player.GetComponent<Animator>().speed = 0.5f;
            player.GetComponent<CharacterController>().moveSpeed /= 2;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out CharacterController player))
        {
            player.GetComponent<Animator>().speed = 1f;
            player.GetComponent<CharacterController>().moveSpeed *= 2;
        }
    }
}
