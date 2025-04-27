using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class DamagePopup : MonoBehaviour
{
    public static DamagePopup Create(Vector3 position, int damageAmount)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopup, position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount);

        return damagePopup;
    }
    

    private TextMesh textMesh;
    private float disappearTimer;
    private Color textColor;

    private void Awake()
    {
        textMesh = GetComponent<TextMesh>();
    }

    public void Setup(int damageAmount)
    {
        textMesh.text = damageAmount.ToString();
        textColor = textMesh.color;
        disappearTimer = 1f;
    }

    public void Update()
    {
        float moveYSpeed = 2f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;

            textMesh.color = textColor;
            if (textMesh.color.a < 0)
            {
                Destroy(gameObject);

            }
        
        }


    }
}
