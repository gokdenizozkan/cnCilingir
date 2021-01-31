using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class arayüzEtkileşim : MonoBehaviour
{
    public bool[] buBir = new bool[4]; // 0 -> painting; 1 -> pendulumClock; 2 -> table, 3 -> firelit
    public bool arayüzAçık = false;

    public OyunKontrol OyunKontrolYazışık;
    public oyuncuEtkileşim oyuncuEtkileşimYazisik;
    public short aşama = 0;  //0 -> normal, 1 -> memoryWipe 1. aşama, 2 -> 2. aşama

    [Header("Hall")]
    //painting
    public GameObject paintingOverlayObject;

    //pendulumClock
    public GameObject pendulumClockOverlayObject;

    [Header("Hall2")]
    //table
    public GameObject tableOverlayObject;

    //firelit + şifre
    public GameObject firelitOverlayObject;

    public string şifre = "1327";
    public GameObject inputFieldObjesi;

    public void ŞifreKontrol()
    {
        Debug.Log("Şifre Kontrol fonksiyonuna yönlendirme yapıldı.");
        string girdi;
        girdi = inputFieldObjesi.GetComponent<InputField>().text;
        if (girdi == şifre) 
        {
            inputFieldObjesi.gameObject.SetActive(false);
            ArayüzKapa()
        }
    }
    public void ArayüzAç(bool[] kontrol) //etki alanındaki obje adı
    {
        arayüzAçık = true;
        if (SceneManager.GetActiveScene().name == "hall")
        {
            if (kontrol[0])
            {
                paintingOverlayObject.gameObject.SetActive(true); //arayüzü görünür kılar
            }
            if (kontrol[1])
            {
                pendulumClockOverlayObject.gameObject.SetActive(true); //arayüzü görünür kılar
            }
        }
        if (SceneManager.GetActiveScene().name == "hall2")
        {
            if (kontrol[2])
            {
                tableOverlayObject.gameObject.SetActive(true);
            }
            if (kontrol[3])
            {
                firelitOverlayObject.gameObject.SetActive(true);
            }
        }
    }

    public void ArayüzKapa()
    {
        arayüzAçık = false;
        if (SceneManager.GetActiveScene().name == "hall")
        {
            paintingOverlayObject.gameObject.SetActive(false); //arayüzü görünmez kılar
            pendulumClockOverlayObject.gameObject.SetActive(false); //arayüzü görünmez kılar
        }
        if (SceneManager.GetActiveScene().name == "hall2")
        {
            tableOverlayObject.gameObject.SetActive(false);
            firelitOverlayObject.gameObject.SetActive(false);            
        }

    }

}