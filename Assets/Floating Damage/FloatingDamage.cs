using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingDamage : MonoBehaviour
{
    public static FloatingDamage Create(Vector3 vPosition, int iDamageAmount) 
    {
        GameObject pFloatingDamageObject = Instantiate(GameAssets.I.pfFloatingDamage, vPosition, Quaternion.identity);
        FloatingDamage pFloatingDamage = pFloatingDamageObject.GetComponent<FloatingDamage>();
        pFloatingDamage.Setup(iDamageAmount);

        return pFloatingDamage;
    }

    public float MoveYSpeed;
    public float DisappearSpeed;
    
    private TextMeshPro pTextMesh;
    private float m_fDisappearTimer;
    private Color m_pTextColor;

    private void Awake() 
    {
        pTextMesh = GetComponent<TextMeshPro>();
    }

    public void Setup(int iDamageAmount) 
    {
        pTextMesh.SetText(iDamageAmount.ToString());
        m_pTextColor = pTextMesh.color;
        m_fDisappearTimer = 1f;
    }

    private void Update()
    {
        transform.position += new Vector3(0, MoveYSpeed) * Time.deltaTime;

        m_fDisappearTimer -= Time.deltaTime;
        if (m_fDisappearTimer < 0)
        {
            m_pTextColor.a -= DisappearSpeed * Time.deltaTime;
            pTextMesh.color = m_pTextColor;

            if (m_pTextColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
