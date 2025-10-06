using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour
{
    [SerializeField] private GameObject blocker;
    [SerializeField] private GameObject portal;
    [SerializeField] private GameObject nextLevel;

    [SerializeField] private bool final = false;
    public UnityEvent levelFinished = new();
    [SerializeField] private int level = 1;
    public void Complete()
    {
        if (!final)
        {
            blocker.SetActive(false);
            portal.SetActive(true);
            nextLevel.SetActive(true);
        }
        levelFinished?.Invoke();
    }


}
