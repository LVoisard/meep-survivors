using System.Collections.Generic;
using System.Linq;
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

    [SerializeField] AudioSource LootBoxSound;

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
            Time.timeScale = 0f;
            LootBoxSound.Play();
        }
    }

    private void Update()
    {
        if (!animating) return;

        transform.Rotate(0, 0, Time.unscaledDeltaTime * rotateSpeed);
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * maxScale, Time.unscaledDeltaTime * zoomSpeed);
        if (transform.localScale.x >= maxScale * 0.95f)
        {
            animating = false;
            canvas.enabled = true;

            Button[] buttons = canvas.GetComponentsInChildren<Button>();
            TMPro.TMP_Text[] texts = canvas.GetComponentsInChildren<TMPro.TMP_Text>();

            List<int> indices = Enumerable.Range(0, DataManager.Instance.LootboxDropUI.Length).ToList();
            var randomIndices = indices.OrderBy(i => Random.value).Take(3).ToList();

            for (int i = 0; i < 3; i++)
            {
                int ind = i;
                buttons[ind].onClick.AddListener(() => { ButtonClicked(randomIndices[ind]); });
                texts[i+1].text = DataManager.Instance.LootboxDropTitles[randomIndices[i]]; //i+1, skip title
                buttons[i].GetComponentsInChildren<Image>()[1].sprite = DataManager.Instance.LootboxDropUI[randomIndices[i]];
            }
        }
    }

    void ButtonClicked(int index)
    {
        canvas.enabled = false;

        PlayerManager player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        Time.timeScale = 1f;
        switch (index)
        {
            case 0:
                player.AreaOfEffectEffector += 100;
                break;
            case 1:
                player.CooldownEffector += 100;
                break;
            case 2:
                player.DamageEffector += 100;
                break;
            case 3:
                player.DurationEffector += 100;
                break;
            case 4:
                player.SpeedEffector += 100;
                break;
            case 5:
                player.TargetCountEffector += 1;
                break;
        }

        var attks = FindObjectsByType<MeepAttack>(FindObjectsSortMode.None);
        foreach (var att in attks)
        {
            if (att.tag != "Enemy")
                att.UpdateSkillEffectors();
        }

        Button[] buttons = canvas.GetComponentsInChildren<Button>();
        foreach (var button in buttons)
        {
            button.onClick.RemoveAllListeners();
        }

        Destroy(transform.gameObject);
    }
}
