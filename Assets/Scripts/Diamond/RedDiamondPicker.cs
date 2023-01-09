using UnityEngine;

public class RedDiamondPicker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<RedDiamond>(out RedDiamond redDiamond))
        {
            Score.CountOfDiamonds++;
            Destroy(redDiamond.gameObject);
        }
    }
}