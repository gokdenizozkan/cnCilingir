using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class oyuncuEtkileşim : MonoBehaviour
{
    [Header("Zorunlular")]
    public OyunKontrol OyunKontrolYazışık;

    [Header("Etkileşim Kontrol Grubu")]
    public GameObject etkiAlanObje = null; //etkileşim alanındaki obje
    public objeEtkileşim objeEtkileşimYazışık = null; //etkileşim alanındaki obje [yazışığı (=script)] buradaki amaç, kod içerisinde diğer yazışıklara erişim sağlamak, NULL çünkü etki alanındaki objenin yazışığını buraya tanımlayacağız.
    public arayüzEtkileşim arayüzEtkileşimYazışık = null; //aynı şekilde etkileşim alanındaki obje şayet bir ek arayüze bağlıysa, bir üstteki ile aynı amaçla kullanılır.

    [Header("Diğer")]
    public bool tornavidaAlındı;
    public oyuncuEnvanter oyuncuEnvanterYazışık;



    void Update()
    {
        //OYNCU KONTROL (FARE ETKİLEŞİM)
        RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100f);

        //EYLEMLER
        if(hitInfo.collider != null)
        {
            UnityEngine.Debug.DrawLine(transform.position, hitInfo.point, Color.red);

            if(hitInfo.collider.CompareTag("etkiArayüz"))
            {
                UnityEngine.Debug.Log(hitInfo.collider.name);
                etkiAlanObje = hitInfo.collider.gameObject;
                arayüzEtkileşimYazışık = etkiAlanObje.GetComponent<arayüzEtkileşim>();                
            }

            if(hitInfo.collider.CompareTag("etkiObje"))  //eğer etki alanındaki obje "etkileşilebilen bir obje" ise
            {
                UnityEngine.Debug.Log(hitInfo.collider.name);
                etkiAlanObje = hitInfo.collider.gameObject; //etki alanındaki objeyi "etkileşilen obje" olarak tanımla
                objeEtkileşimYazışık = etkiAlanObje.GetComponent<objeEtkileşim>(); //etki alanındaki objenin "obje etkileşim" yazışığını, objeEtkileşimYazışık değişkenine tanımla (ki böylece üzerinde oynama yapabilelim.)
            }    
        } else {
            if (arayüzEtkileşimYazışık.arayüzAçık == false)
            {
                etkiAlanObje = null;
                arayüzEtkileşimYazışık = null;
            }
        }

        //DİĞER

        if (Input.GetMouseButtonDown(0) && etkiAlanObje) //etkileşim butonuna (e) basılmış ve etki alanında bir obje varsa
        {
            //OBJE ETKİLEŞİM YAZIŞIĞI VARSA
            if (objeEtkileşimYazışık != null) //şayet etkileşilen obje bir "obje etkileşim yazışığına" sahipse
            {
                if (objeEtkileşimYazışık.merdiven_altKattaki)
                {
                    OyunKontrolYazışık.SahneDeğiştir("ÜstKat");
                }
                if (objeEtkileşimYazışık.merdiven_üstKattaki)
                {
                    OyunKontrolYazışık.SahneDeğiştir("hall");
                }
                if (objeEtkileşimYazışık.envantereAlınabilir) //ve eğer etkileşilen obje "envantere alınabilir" ise
                {
                    if (objeEtkileşimYazışık.elFeneri) //el feneri ise
                    {
                        objeEtkileşimYazışık.elFeneriYazışıkObjesi.gameObject.SetActive(true); //el feneri yazışığını aktive et
                    }
                    oyuncuEnvanterYazışık.EşyaEkle(etkiAlanObje); //etki alanındaki objeyi "envantere ekle"
                }
            }
            //ARAYÜZ ETKİLEŞİM İSE
            if (arayüzEtkileşimYazışık != null) //şayet etkileşilen obje bir "arayüz etkileşim yazışığına" sahipse
            {
                UnityEngine.Debug.Log(arayüzEtkileşimYazışık.name);
                arayüzEtkileşimYazışık.ArayüzAç(arayüzEtkileşimYazışık.buBir); //arayüzEtkileşim yazışığında bulunan "ArayüzAç" fonksiyonunu çalıştır. (bu fonksiyon içerisinde tüm arayüz tiplerini barındırdığından ayrıyeten bir arayüz belirleme işlemi yapmamıza gerek yok.)
            }
        }

        //ARAYÜZ KAPAT
        if (Input.GetButtonDown("ESC")) //eğer ESC butonuna (escape) basılmışsa
        {
            arayüzEtkileşimYazışık.ArayüzKapa(); //arayüzEtkileşim yazışığındaki "ArayüzKapa" fonksiyonunu çalıştır.
        }
        //DÜNYA ÜZERİNDE KİLİTLİ OBJE AÇMA
        //açılabilir bir obje mi kontrol et
        if (Input.GetMouseButtonDown(0) && objeEtkileşimYazışık.açılabilir)
        {
            //kilitli mi kontrol et
            if (objeEtkileşimYazışık.kilitli)
            {
                //açmak için gerekli objeye oyuncu sahip mi, kontrol et
                //aranan obje için envanteri ara, bulunursa kilidi aç
                if (oyuncuEnvanterYazışık.EşyaBul(objeEtkileşimYazışık.gerekliObje))
                {
                    //eşya bulundu, aç
                    oyuncuEnvanterYazışık.EşyaSil();
                    objeEtkileşimYazışık.kilitli = false;
                    UnityEngine.Debug.Log(objeEtkileşimYazışık.name + " kilidi açıldı.");
                    if (objeEtkileşimYazışık.aletÇantası) //kilidi açılan obje bir alet çantası idi ise içerisindeki "tornavidayı" görünür kıl
                    {
                        objeEtkileşimYazışık.TornavidayıGörünürKıl(); //alet çantası içindeki tornavidayı görünür kıl
                        objeEtkileşimYazışık.GörünmezKıl(); //kilidi açılan obje görünmez kıl (animasyon hazırlanana kadar bu şekilde kalabilir)
                    }
                }
                else
                {
                    UnityEngine.Debug.Log(objeEtkileşimYazışık.name + " hala kilitli.");
                }
            }
            else
            {
                //obje kilitli değil, açmaya geçebiliriz
                UnityEngine.Debug.Log(objeEtkileşimYazışık.name + " açıldı.");
                objeEtkileşimYazışık.Aç();
            }
        }
    }

}
