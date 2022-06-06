using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using System;
using UnityEngine.UI;
public class Purchaser : MonoBehaviour,IStoreListener {

    IStoreController controller;
    string[] Products = new string[] {"remove_ads","urun1", "urun2", "urun3", "urun4", "urun5" };
    public Text kredi_miktar;

    public Text urun1_price;
    public Text urun2_price;
    public Text urun3_price;
    public Text urun4_price;
    public Text urun5_price;

    public void wacth2pic()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            bildirim_create("Bağlantı Hatası", 2);
        }
        else
        {
            PlayerPrefs.SetInt("toplam_kredi", PlayerPrefs.GetInt("toplam_kredi") + 1);
            kredi_miktar.text = PlayerPrefs.GetInt("toplam_kredi").ToString();
            bildirim_create(1 + " Kredi hesabına eklendi.", 2);
        }
    }

    void Start()
    {
        var module = StandardPurchasingModule.Instance();
        ConfigurationBuilder builder = ConfigurationBuilder.Instance(module);
        foreach(string s in Products)
        {
            if (s.Contains("ads"))
            {
                builder.AddProduct(s, ProductType.NonConsumable);
            }
            else
            {
                builder.AddProduct(s, ProductType.Consumable);
            }
            builder.AddProduct(s,ProductType.Consumable);
        }
        UnityPurchasing.Initialize(this,builder);

        if(PlayerPrefs.GetInt("reklam_varmı")==1)
        {
           // reklamları_kaldır_butonu.interactable = false;
            
        }
        if (PlayerPrefs.GetInt("Kitsort273cuz62_Birdefa") == 0)
        {
            PlayerPrefs.SetInt("toplam_kredi", 30);
            PlayerPrefs.SetInt("Kitsort273cuz62_Birdefa", 1);
            kredi_miktar.text = PlayerPrefs.GetInt("toplam_kredi").ToString();
        }
        else
        {
            kredi_miktar.text = PlayerPrefs.GetInt("toplam_kredi").ToString();
        }
    }
    public void BuyProduct(string ProductId)
    {
        if (ProductId.Contains("ads"))
        {
            Product product0 = controller.products.WithID(ProductId);
            if(product0.hasReceipt)
            {
                PlayerPrefs.SetInt("remove_ads", 1);                
                //reklamları_kaldır_butonu.interactable = false;
                PlayerPrefs.SetInt("reklam_varmı",1);
            }
            else
            {
                buyProduct(ProductId);
            }
        }
        else
        {
            buyProduct(ProductId);
        }
        
    }
    void buyProduct(string ProductId)
    {
        Product product = controller.products.WithID(ProductId);

        if(product!= null && product.availableToPurchase)
        {
            Debug.Log("Ürün satın alınıyor...");
            controller.InitiatePurchase(product);
        }
        else
        {
            Debug.Log("Ürün bulunamadı ya da satın alınabilir değil");
        }
    }
    public void OnInitialized(IStoreController controller,IExtensionProvider provider)
    {
        this.controller = controller;
        Product product0 = controller.products.WithID("remove_ads");

        urun1_price.text = controller.products.WithID("urun1").metadata.localizedPriceString;
        urun2_price.text = controller.products.WithID("urun2").metadata.localizedPriceString;
        urun3_price.text = controller.products.WithID("urun3").metadata.localizedPriceString;
        urun4_price.text = controller.products.WithID("urun4").metadata.localizedPriceString;
        urun5_price.text = controller.products.WithID("urun5").metadata.localizedPriceString;

        if (product0.hasReceipt)
        {
            PlayerPrefs.SetInt("remove_ads", 1);
        }
        else
        {
            PlayerPrefs.SetInt("remove_ads", 0);
        }
        Debug.Log("Sistem Hazır.");
    }
    public void OnInitializeFailed(InitializationFailureReason reason)
    {
        Debug.Log("Yükleme Hatası: "+reason.ToString());
    }
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
              
        //if (string.Equals(args.purchasedProduct.definition.id, Products[0], StringComparison.Ordinal))
        //{
        //    PlayerPrefs.SetInt("remove_ads", 1);

        //  //  reklamlar_kaldırıldı.SetActive(true);
        //}
       
        if (string.Equals(args.purchasedProduct.definition.id, Products[1], StringComparison.Ordinal))
        {
            PlayerPrefs.SetInt("toplam_kredi", PlayerPrefs.GetInt("toplam_kredi") + 10);
            kredi_miktar.text = PlayerPrefs.GetInt("toplam_kredi").ToString();
            bildirim_create(10 + " Kredi hesabına eklendi.", 2);
        }
        if (string.Equals(args.purchasedProduct.definition.id, Products[2], StringComparison.Ordinal))
        {
            PlayerPrefs.SetInt("toplam_kredi", PlayerPrefs.GetInt("toplam_kredi") + 25);
            kredi_miktar.text = PlayerPrefs.GetInt("toplam_kredi").ToString();
            bildirim_create(25 + " Kredi hesabına eklendi.", 2);
        }
        if (string.Equals(args.purchasedProduct.definition.id, Products[3], StringComparison.Ordinal))
        {
            PlayerPrefs.SetInt("toplam_kredi", PlayerPrefs.GetInt("toplam_kredi") + 40);
            kredi_miktar.text = PlayerPrefs.GetInt("toplam_kredi").ToString();
            bildirim_create(40 + " Kredi hesabına eklendi.", 2);
        }
        if (string.Equals(args.purchasedProduct.definition.id, Products[4], StringComparison.Ordinal))
        {
            PlayerPrefs.SetInt("toplam_kredi", PlayerPrefs.GetInt("toplam_kredi") + 65);
            kredi_miktar.text = PlayerPrefs.GetInt("toplam_kredi").ToString();
            bildirim_create(65 + " Kredi hesabına eklendi.", 2);
        }
        if (string.Equals(args.purchasedProduct.definition.id, Products[5], StringComparison.Ordinal))
        {
            PlayerPrefs.SetInt("toplam_kredi", PlayerPrefs.GetInt("toplam_kredi") + 80);
            kredi_miktar.text = PlayerPrefs.GetInt("toplam_kredi").ToString();
            bildirim_create(110 + " Kredi hesabına eklendi.", 2);
        }
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product,PurchaseFailureReason reason)
    {
        Debug.Log("Bu ürün satın alınamadı: "+product.ToString());
    }

    [Header("BİLDİRİM ATMA İLE İLGİLİ DEGİSKENLER")]
    public Text bildirim_yazisi;
    public GameObject toast_mesaj_paneli;

    void debug_kapat()
    {
        toast_mesaj_paneli.SetActive(false);
    }

    public void bildirim_create(string mesaj, int bildirim_suresi)
    {
        if (bildirim_suresi != -1)
        {
            toast_mesaj_paneli.SetActive(true);
            bildirim_yazisi.text = mesaj;
            Invoke("debug_kapat", bildirim_suresi);
        }
        else if (bildirim_suresi == -1) // kapat
        {
            Invoke("debug_kapat", 0);
        }
        else if (bildirim_suresi == -2) // zamansız bildirim ac
        {
            toast_mesaj_paneli.SetActive(true);
            bildirim_yazisi.text = mesaj;
        }
    }
}
