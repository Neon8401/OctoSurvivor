using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using static Charadata;

public class DamegePointUI : MonoBehaviour
{

    private Text damageText;
    //　フェードアウトするスピード
    private float fadeOutSpeed = 1f;
    //　移動値
    [SerializeField]
    private float moveSpeed = 0.4f;
    [SerializeField] Charadata _charadata;

    void Start()
    {
        damageText = GetComponentInChildren<Text>();
    }

    void LateUpdate()
    {
        damageText.text = _charadata.ATK.ToString();
        transform.rotation = Camera.main.transform.rotation;
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        damageText.color = Color.Lerp(damageText.color, new Color(1f, 0f, 0f, 0f), fadeOutSpeed * Time.deltaTime);

        if (damageText.color.a <= 0.1f)
        {
            Destroy(gameObject);
        }
    }
}