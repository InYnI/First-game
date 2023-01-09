using UnityEngine;

public class DiamondPicker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Diamond>(out Diamond diamond))
        {
            Score.CountOfDiamonds++;
            Destroy(diamond.gameObject);
        }

        else if (other.TryGetComponent<RedDiamond>(out RedDiamond redDiamond))
        {
            Score.CountOfRedDiamonds++;
            Destroy(redDiamond.gameObject);
        }
    }
}
