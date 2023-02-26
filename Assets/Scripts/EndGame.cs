using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerActions>(out PlayerActions playerActions))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
