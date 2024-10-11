# ZST Extractor

ZST Extractor is a .NET Framework 4.7.2 console application that automates the process of downloading `.tar.zst` files, decompressing them, and extracting their contents. The project uses **Zstandard.Net** to handle `.zst` decompression and **SharpCompress** to extract `.tar` archives.

## Features

- Downloads `.tar.zst` files from provided URLs.
- Decompresses `.zst` files into `.tar` format using **Zstandard.Net**.
- Extracts `.tar` archives into a specified directory using **SharpCompress**.

## Dependencies

- [Zstandard.Net](https://www.nuget.org/packages/Zstandard.Net): For handling `.zst` decompression.
- [SharpCompress](https://www.nuget.org/packages/SharpCompress): For extracting `.tar` files.

## How to Use

1. Clone the repository.
2. Install the required NuGet packages:
   - `Zstandard.Net`
   - `SharpCompress`
3. Run the application to download, decompress, and extract `.tar.zst` files.
