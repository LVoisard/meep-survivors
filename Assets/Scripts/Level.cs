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
    [SerializeField] private AudioSource LevelTheme;

    private void Start()
    {
        foreach (AudioSource leveltheme in LevelTheme.transform.parent.GetComponentsInChildren<AudioSource>())
        {
            leveltheme.volume = 0f;
        }
        LevelTheme.volume = 0.2f;
    }

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
