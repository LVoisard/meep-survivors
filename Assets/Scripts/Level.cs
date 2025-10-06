using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private GameObject blocker;
    [SerializeField] private GameObject portal;
    [SerializeField] private GameObject nextLevel;
    public void Complete()
    {
        blocker.SetActive(false);
        portal.SetActive(true);
        nextLevel.SetActive(true);
    }
}
