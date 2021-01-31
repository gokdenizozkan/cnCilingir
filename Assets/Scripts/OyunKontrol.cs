using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OyunKontrol : MonoBehaviour
{
    public bool oyunBitti = false;

    public bool süreBitti = false;

    public void SahneDeğiştir(string sahne)
    {
        SceneManager.LoadScene(sahne);
    }

    public void OyunuBitir()
    {
        if (süreBitti)
        {
            SceneManager.LoadScene("oyunBitti_öldün");
        }
    }
}
