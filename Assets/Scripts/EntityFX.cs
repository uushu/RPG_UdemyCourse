using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    [Header("Flash FX")] 
    [SerializeField] private Material hitMat;

    private SpriteRenderer sr;
    private Material originalMat;

    private void Start()
    {
        sr=GetComponentInChildren<SpriteRenderer>();
        originalMat = sr.material;
        
    }

    private IEnumerator FlashFX()
    {
        sr.material=hitMat;
        
        yield return new WaitForSeconds(0.2f);
        
        sr.material = originalMat;
    }

    private void RedColorBlink()
    {
        if (sr.color != Color.white)
        {
            sr.color = Color.white;
        }
        else
        {
            sr.color = Color.red;
        }
        
    }

    private void CancelRedBlink()
    {
        CancelInvoke("RedColorBlink");
        sr.color=Color.white;
    }

}
