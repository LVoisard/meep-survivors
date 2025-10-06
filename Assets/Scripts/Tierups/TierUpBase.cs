using UnityEngine;
using UnityEngine.UI;

public class TierUpBase : MonoBehaviour
{

    [SerializeField] protected Button btn1;
    [SerializeField] protected Button btn2;
    [SerializeField] protected Button btn3;

    protected BaseMeep currentMeep;

    public void Ready(BaseMeep meep)
    {
        currentMeep = meep;
        gameObject.SetActive(true);
        Time.timeScale = 0.0f;
    }

    private void OnEnable()
    {
        btn1?.onClick.AddListener(Complete);
        btn2?.onClick.AddListener(Complete);
        btn3?.onClick.AddListener(Complete);
    }

    private void OnDisable()
    {
        btn1?.onClick.RemoveListener(Complete);
        btn2?.onClick.RemoveListener(Complete);
        btn3?.onClick.RemoveListener(Complete);
    }


    private void Complete()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }
}

