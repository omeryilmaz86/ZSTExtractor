using System;
using System.IO;
using System.Net;
using ZstdSharp;
using SharpCompress.Archives;
using SharpCompress.Archives.Tar;
using SharpCompress.Common;

namespace ZSTExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            // İndirilecek dosyaların listesi
            string[] urls = {
                // linkleri buraya ekleyin
                "https://URL_1.tar.zst",
                "https://URL_2.tar.zst",
            };

            string downloadDir = "downloads";
            Directory.CreateDirectory(downloadDir);

            foreach (var url in urls)
            {
                string fileName = Path.GetFileName(url);
                string zstFilePath = Path.Combine(downloadDir, fileName);
                string tarFilePath = Path.Combine(downloadDir, Path.GetFileNameWithoutExtension(fileName) + ".tar");

                // Dosyayı indir
                DownloadFile(url, zstFilePath);

                // ZST dosyasını aç
                DecompressZst(zstFilePath, tarFilePath);

                // TAR dosyasını aç
                ExtractTar(tarFilePath, downloadDir);
            }


            Console.WriteLine($"TÜMÜ İNDİRİLDİ");
            Console.ReadLine();
        }

        // Dosya indirme metodu
        static void DownloadFile(string url, string filePath)
        {
            using (WebClient client = new WebClient())
            {
                Console.WriteLine($"İndiriliyor: {url}");
                client.DownloadFile(url, filePath);
                Console.WriteLine($"İndirildi: {filePath}");
                Console.WriteLine($"{Environment.NewLine}");
            }
        }

        // ZST dosyasını açma metodu
        static void DecompressZst(string zstFilePath, string tarFilePath)
        {
            Console.WriteLine($"ZST dosyası açılıyor: {zstFilePath}");

            // Dosyayı byte[] olarak oku
            byte[] compressedData = File.ReadAllBytes(zstFilePath);

            using (var decompressor = new ZstdSharp.Decompressor())
            {
                // Çıkartılmış byte dizisinin uzunluğunu tahmin etmek için bir buffer oluşturun
                var decompressedDataSpan = decompressor.Unwrap(compressedData);

                // Span<byte> dizisini byte[] dizisine dönüştür
                byte[] decompressedData = decompressedDataSpan.ToArray();

                // Çıkarılan veriyi tar dosyası olarak kaydet
                File.WriteAllBytes(tarFilePath, decompressedData);
            }

            Console.WriteLine($"Açıldı: {tarFilePath}");
        }

        // TAR dosyasını açma metodu
        static void ExtractTar(string tarFilePath, string destinationDirectory)
        {
            Console.WriteLine($"TAR dosyası açılıyor: {tarFilePath}");
            using (var archive = TarArchive.Open(tarFilePath))
            {
                foreach (var entry in archive.Entries)
                {
                    if (!entry.IsDirectory)
                    {
                        entry.WriteToDirectory(destinationDirectory, new ExtractionOptions
                        {
                            ExtractFullPath = true,
                            Overwrite = true
                        });
                        Console.WriteLine($"Çıkartıldı: {entry.Key}");
                    }
                }
            }
        }
    }
}
