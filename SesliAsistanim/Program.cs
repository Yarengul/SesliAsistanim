using System;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading;

namespace SesliAsistanim
{
    class Program
    {
       
        private static SpeechRecognitionEngine recognizer;
     
        private static SpeechSynthesizer synthesizer;

        
        private static ManualResetEvent completionEvent = new ManualResetEvent(false);

        static void Main(string[] args)
        {
            Console.WriteLine("--- C# SESLİ ASİSTAN BAŞLATILIYOR ---");

            try
            {
               
                recognizer = new SpeechRecognitionEngine();
                synthesizer = new SpeechSynthesizer();
                synthesizer.SetOutputToDefaultAudioDevice();

            
                recognizer.SetInputToDefaultAudioDevice();

             
                LoadGrammars();

                
                recognizer.SpeechRecognized += Recognizer_SpeechRecognized;
                recognizer.SpeechRecognitionRejected += Recognizer_SpeechRejected;

                
                recognizer.RecognizeAsync(RecognizeMode.Multiple);

                synthesizer.SpeakAsync("Sistem başlatıldı. Dinliyorum.");
                Console.WriteLine("Dinleme başladı. Kapatmak için ENTER tuşuna basınız.");

                
                completionEvent.WaitOne();

               
                recognizer.RecognizeAsyncStop();
                recognizer.Dispose();
                synthesizer.Dispose();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[KRİTİK HATA] Lütfen Windows Konuşma Tanıma ayarlarınızı kontrol edin.");
                Console.WriteLine("Detay: " + ex.Message);
                Console.ResetColor();
                Console.ReadLine();
            }
        }

        private static void LoadGrammars()
        {
         
            Choices choices = new Choices();
            choices.Add(new string[] { "merhaba asistan", "ne yapıyorsun", "kapat" });

            GrammarBuilder gb = new GrammarBuilder(choices);
            gb.Culture = new System.Globalization.CultureInfo("tr-TR"); 
            Grammar komutGrameri = new Grammar(gb);
            komutGrameri.Name = "KomutGrameri";
            recognizer.LoadGrammar(komutGrameri);

           
            DictationGrammar dictationGrammar = new DictationGrammar();
            dictationGrammar.Name = "DikteGrameri";

         
            recognizer.LoadGrammar(dictationGrammar);
        }

        private static void Recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string recognizedText = e.Result.Text;
            float confidence = e.Result.Confidence;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n[Algılandı] Metin: {recognizedText} | Güven: {confidence:P0}");
            Console.ResetColor();

         
            if (recognizedText.ToLower().Contains("merhaba asistan"))
            {
                synthesizer.SpeakAsync("Merhaba! Sana nasıl yardımcı olabilirim?");
            }
            else if (recognizedText.ToLower().Contains("kapat"))
            {
                synthesizer.SpeakAsync("Görüşmek üzere!");
                completionEvent.Set(); 
            }
            else if (recognizedText.ToLower().Contains("not al"))
            {
                synthesizer.SpeakAsync("Not almanız gereken nedir?");
             
            }
        }

        private static void Recognizer_SpeechRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            // Tanıma motoru bir ses duydu ama hiçbir gramer ile eşleştiremedi.
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n[Reddedildi] Güven: {e.Result.Confidence:P0} - Tanıyamadı.");
            Console.ResetColor();
        }
    }
}