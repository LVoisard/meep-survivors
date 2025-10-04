using UnityEngine;
using UnityEngine.UI;

public class Lootbox : MonoBehaviour
{
    [Header("Animation Settings")]
    public float rotateSpeed = 90f;       // degrees per second
    public float zoomSpeed = 2f;          // scale speed
    public float maxScale = 10f;          // how large it gets before stopping

    private bool animating = false;
    private Vector3 initialScale;
    private Camera targetCamera;
    private Canvas canvas;

    private void Awake()
    {
        canvas = GameObject.FindWithTag("SelectionCanvas").GetComponent<Canvas>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            initialScale = transform.localScale;
            targetCamera = Camera.main;
            animating = true;

        }
    }

    private void Update()
    {
        if (!animating) return;

        transform.Rotate(0,0, Time.deltaTime * rotateSpeed);
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * maxScale, Time.deltaTime * zoomSpeed);
        if (transform.localScale.x >= maxScale * 0.95f)
        {
            animating = false;
            canvas.enabled = true;

                        Button[] buttons = canvas.GetComponentsInChildren<Button>();
            TMPro.TMP_Text[] texts = canvas.GetComponentsInChildren<TMPro.TMP_Text>();

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].onClick.AddListener(ButtonClicked);
                texts[i].text = "joe " + i;
            }  
        }
    }


    void ButtonClicked()
    {
        canvas.enabled = false;
        Destroy(gameObject);
    }
}
