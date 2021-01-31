using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objeEtkileşim : MonoBehaviour
{
    [Header("Navigation")]
    public bool merdiven_üstKattaki;
    public bool merdiven_altKattaki;
    public bool hall_solButon;
    public bool hall2_sağButon;
    
    [Header("Diğerleri")]
    public bool sandık;                //eğer "doğru" ise obje, alet çantasıdır.
    public GameObject gerekliObje;          //etkileşim için gerekli eşya
    public GameObject oldKeyObje;

    public Animator anim;  
        
    public void GörünmezKıl()
    {
        gameObject.SetActive(false); //eşyayı görünmez kıl
    }

    public void Aç()
    {
        anim.SetBool("acik", true); //açılma animasyonu (şu an yok) 
    }
}
