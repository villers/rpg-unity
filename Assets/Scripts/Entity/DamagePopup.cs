using System.Collections;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    public float duration = 1.0f;
    public float moveSpeed = 1.0f;
    public float fadeSpeed = 1.0f;

    private TextMeshPro textMeshPro;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshPro>();
    }

    public void Setup(int damage)
    {
        Debug.Log(damage);
        textMeshPro.text = damage.ToString();
        StartCoroutine(FadeAndDestroy());
    }

    public void Setup(string damage)
    {
        Debug.Log(damage);
        textMeshPro.text = damage;
        StartCoroutine(FadeAndDestroy());
    }

    private IEnumerator FadeAndDestroy()
    {
        float timer = 0;

        while (timer < duration)
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;

            Color textColor = textMeshPro.color;
            textColor.a -= fadeSpeed * Time.deltaTime;
            textMeshPro.color = textColor;

            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
