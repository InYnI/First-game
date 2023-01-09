using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text _diamondsText;
    [SerializeField] private TMP_Text _redDiamondsText;

    public static int CountOfDiamonds = 0;
    public static int CountOfRedDiamonds = 0;

    private void Update()
    {
        _diamondsText.text = CountOfDiamonds.ToString();
        _redDiamondsText.text = CountOfRedDiamonds.ToString();
    }
}
