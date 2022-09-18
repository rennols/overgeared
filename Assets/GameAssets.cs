using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets m_pInstance;
    public static GameAssets I { 
        get 
        {
            if (m_pInstance == null) 
            {
                m_pInstance = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
            }
            return m_pInstance; 
        } 
    }


    public GameObject pfFloatingDamage;
}
