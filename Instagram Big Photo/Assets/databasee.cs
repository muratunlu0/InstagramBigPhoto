using UnityEngine.UI;
using System.Collections;
using UnityEngine;

public class databasee : MonoBehaviour
{    
    [Header("ANASAYFA İCİN DEGİSKENLER")]
    public GameObject insta_image;
    public InputField instagram_nickname;
    public GameObject yukleniyor_paneli;
    public GameObject bildirim_obje;
    public Text bildirim_yazi;
    void Start()
    {
        Screen.fullScreen = false;
    }
    public void instagram_input_edit()
    {
        instagram_nickname.text =
            instagram_nickname.text.Replace("@", string.Empty);
        instagram_nickname.text =
           instagram_nickname.text.Replace("\n", string.Empty);
        instagram_nickname.text =
           instagram_nickname.text.Replace(" ", string.Empty);
        instagram_nickname.text =
           instagram_nickname.text.ToLower();
    }
    public void instagram()
    {
        Application.OpenURL("https://www.instagram.com/muratunlu0/");
    }
    public void kitsortapp()
    {
        Application.OpenURL("https://www.instagram.com/kitsortapp/");
    }
    public void googleplay()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.muratunlu0.bigprofilepicture");
    }
    string gecici = "";
    public void profile_photo_with_nickname_instagram()
    {
        if (PlayerPrefs.GetInt("toplam_kredi") > 0)
        {
            if (instagram_nickname.text != "" && gecici != instagram_nickname.text)
            {
                gecici = instagram_nickname.text;
                PlayerPrefs.SetInt("toplam_kredi", PlayerPrefs.GetInt("toplam_kredi") - 1);
                GameObject.Find("scripts").GetComponent<Purchaser>().kredi_miktar.text = PlayerPrefs.GetInt("toplam_kredi").ToString();
                instagram_input_edit();
                string url = "https://www.instagram.com/" + instagram_nickname.text + "/";
                yukleniyor_paneli.SetActive(true);
                StartCoroutine(AccessURL(url));
            }
        }
        else
        {
            bildirim_obje.SetActive(true);
            bildirim_yazi.text = "Daha fazla bakabilmen için krediye ihtiyacın var.";
            Invoke("bildirim_kapa", 2);
        }
    }
    IEnumerator AccessURL(string url)
    {
        WWW www = new WWW(url);
        yield return www;
        if (string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.text);           
            filtrele_contents(www.text);
        }
        else
        {
            yukleniyor_paneli.SetActive(false);
            bildirim_obje.SetActive(true);
            bildirim_yazi.text = "KULLANICI ADI MEVCUT DEĞİL";
            Invoke("bildirim_kapa", 2);
        }
    }   
    void filtrele_contents(string html_codee)
    {       
        string search = "\"profile_pic_url_hd\":\"";
        int p = html_codee.IndexOf(search);
        if (p >= 0)
        {
            int start = p + search.Length;
            int end = html_codee.IndexOf("\"", start);
            if (end >= 0)
            {
                string image_url = html_codee.Substring(start, end - start);
                image_url = image_url.Replace("\\u0026", "&");
                Debug.Log(image_url);
                
                StartCoroutine(profil_resmi_cek(image_url));
            }            
        }       
    }
    IEnumerator profil_resmi_cek(string url)
    {
        WWW www = new WWW(url);
        yield return www;
        if (string.IsNullOrEmpty(www.error))
        {
            Texture2D image = new Texture2D(1, 1, TextureFormat.RGB24, false);
            www.LoadImageIntoTexture(image);
            insta_image.GetComponentInChildren<RawImage>().texture = image;
            insta_image.GetComponent<RectTransform>().localScale = new Vector3(1f, www.texture.height / (float)www.texture.width, 1f);            
            yukleniyor_paneli.SetActive(false);
        }
        else
        {
            yukleniyor_paneli.SetActive(false);
            bildirim_obje.SetActive(true);
            bildirim_yazi.text = "İŞLEM BAŞARISIZ :(";
            Invoke("bildirim_kapa", 2);
            
        }
    }    
    void bildirim_kapa()
    {
        bildirim_obje.SetActive(false);
    }
}

