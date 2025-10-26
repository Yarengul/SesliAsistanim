# SesliAsistanim


Bu proje, C# programlama dili ve Windows'un yerel `System.Speech` kütüphanesini kullanarak mikrofon aracılığıyla alınan sesli komutları metne çevirir ve tanımlanan komutlara göre aksiyon alır (Örn: "merhaba asistan").

## Sistem Gereksinimleri

Bu uygulama, Microsoft Windows işletim sistemine özgü Konuşma Tanıma motorlarını kullanmaktadır.

### 1. Zorunlu Kurulum: Konuşma Tanıma Paketi

Projenin `tr-TR` (Türkçe) dilini kullanarak çalışabilmesi için, Windows sisteminizde Türkçe Konuşma Tanıma (Speech Recognition) paketinin kurulu olması **zorunludur**.

Eğer uygulama "Yüklü tanıyıcı yok" hatası verirse, lütfen aşağıdaki adımları kontrol edin:

1.  **Windows Ayarları** > **Saat ve Dil** > **Dil ve Bölge** bölümüne gidin.
2.  **Türkçe** dilinin yanındaki **Dil Seçenekleri**'ne tıklayın.
3.  **İsteğe Bağlı Dil Özellikleri** veya **Konuşma** başlığı altında, **"Konuşma Tanıma" (Speech Recognition)** özelliğinin indirilmiş ve kurulmuş olduğundan emin olun.

### 2. Yönetici İzni Gereksinimi

Uygulamanın Windows'un ses donanımına ve yerel motorlarına erişim sağlayabilmesi için yönetici yetkisi gerekebilir.

* Projenin derlenmiş sürümünü (genellikle `bin\Debug` klasöründeki `.exe` dosyası) **sağ tıklayarak** ve **"Yönetici olarak çalıştır" (Run as administrator)** seçeneğiyle başlatın.

---

## Uygulamanın Çalıştırılması

1.  Visual Studio'yu **Yönetici olarak** açın.
2.  Projeyi derleyin ve başlatın (F5).
3.  Konsol ekranında "Sistem başlatıldı. Dinliyorum." mesajını gördüğünüzde konuşmaya başlayabilirsiniz.

### Tanınan Komutlar

Uygulama, temel olarak iki tür gramer kullanır:

1.  **Komut Grameri (Yüksek Doğruluk):**
    * `merhaba asistan`
    * `ne yapıyorsun`
    * `kapat`
2.  **Dikte Grameri (Serbest Konuşma):**
    * Yukarıdaki komutlar dışında söylenen diğer tüm sözcükleri metne çevirmeye çalışır.
