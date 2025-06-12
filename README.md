# Tugas Besar CLO4 - Sistem Penjadwalan Tugas Karyawan

## 📌 Deskripsi Aplikasi
Aplikasi ini merupakan sistem penjadwalan tugas berbasis **console** (CLI) yang dikembangkan dengan bahasa pemrograman **C#**. Sistem memungkinkan **admin untuk mengatur tugas**, dan **karyawan untuk memilih, menyelesaikan, dan melihat laporan tugas** yang sudah dikerjakan.

Proyek ini merupakan pengembangan dari **Tugas Besar CLO2**, dengan penambahan **fitur modular, design pattern, pengujian unit, dan coverage**.

---

## 👨‍💼 Fitur Utama

### Untuk Admin
- Login sebagai admin menggunakan password (dengan masking `*`)
- Menambahkan tugas baru sesuai status karyawan (Junior, Middle, Senior)
- Mengatur **prioritas tugas** (Rendah, Sedang, Tinggi)
- Menghapus tugas
- Melihat semua daftar tugas dan statusnya

### Untuk Karyawan
- Login berdasarkan nama (multi-user)
- Melihat daftar tugas yang tersedia sesuai status
- Mendaftarkan diri untuk mengerjakan tugas
- Menyelesaikan tugas yang sedang dikerjakan
- Melihat statistik dan laporan kinerja

---

## 🧱 Teknik Konstruksi yang Digunakan

| Teknik Konstruksi          | Implementasi                                                            |
|---------------------------|-------------------------------------------------------------------------|
| **Automata**              | Digunakan untuk pengaturan status tugas (`AutomataStatus`, `AutomataPemesanan`) |
| **Table-Driven**          | Mengatur batas waktu kerja & penyimpanan tugas (`TableDrivenBatas`, `TableDrivenJadwal`) |
| **Parameterization**      | Menggunakan `Generics` pada database internal tugas                      |
| **Code Reuse**            | Modul seperti `FileHandler`, `UserSession`, dan `PrioritasTugas` digunakan ulang di banyak bagian |
| **Runtime Configuration** | Data tugas disimpan dan dimuat dari file JSON (`FileHandler.cs`)        |
| **Design Pattern**        | `Singleton Pattern` pada `FileHandler.cs` dan `Strategy Pattern` pada `LoginStrategies.cs` |

---

## 📦 Struktur Proyek
```bash
Tubes_Kelompok_BisaYukk_Final/
│
├── Modules/
│ ├── AutomataStatus.cs
│ ├── AutomataPemesanan.cs
│ ├── TableDrivenBatas.cs
│ ├── TableDrivenJadwal.cs
│ ├── PrioritasTugas.cs
│ ├── FileHandler.cs
│ ├── UserSession.cs
│ └── LoginStrategies.cs
│
├── Models/
│ └── StatusKaryawan.cs
│
├── Program.cs
├── data.json
```
---

## 🧪 Pengujian Unit & Coverage

- Framework: `xUnit`
- Lokasi: `Tubes_Kelompok_BisaYukk.Tests/`
- Tools: `coverlet.collector` untuk mengukur code coverage
- Menjalankan testing:

```bash
dotnet test Tubes_Kelompok_BisaYukk.Tests --collect:"XPlat Code Coverage"
```
- Hasil coverage disimpan dalam file TestResults/coverage.cobertura.xml


## 🛠️ Cara Menjalankan Aplikasi
- Buka folder proyek di Visual Studio 2022

- Set Tubes_Kelompok_BisaYukk_Final sebagai Startup Project

- Tekan Ctrl + F5 untuk menjalankan tanpa debugging

## 🎥 Video Presentasi
📺 YouTube Link (wajib unlisted atau public)

## 👥 Anggota Kelompok dan Tanggung Jawab

| Nama                   | Teknik Konstruksi                    | File Bertanggung Jawab                                   |
|------------------------|--------------------------------------|----------------------------------------------------------|
| Fauzan Wahyu Mubarak   | Automata, Singleton Pattern          | `AutomataPemesanan.cs`, `FileHandler.cs`                 |
| Rizky Hanifa Afania    | Automata, Generics                   | `AutomataStatus.cs`, `PrioritasTugas.cs`                 |
| Muhammad Idham Cholid  | Table-Driven                         | `TableDrivenJadwal.cs`                                   |
| Althafia Defiyandrea L. W. | Unit Testing & Coverage          | `TableDrivenBatas.cs`                                    |
| Andera Singgih Pratama | -        | -          |


## 🔐 Fitur Keamanan & Kebersihan Kode

✅ Validasi Input
Memastikan semua input pengguna seperti status, pilihan menu, dan angka telah divalidasi untuk mencegah kesalahan atau eksploitasi.

✅ Masking Input Password
Menyembunyikan input password saat login admin untuk menjaga kerahasiaan informasi.

✅ Modularisasi Kode
Setiap fitur dan fungsi dipisahkan ke dalam file dan modul yang berbeda agar mudah dikelola dan dikembangkan.

✅ Cegah Duplikasi & Input Tidak Valid
Sistem tidak mengizinkan input tugas yang duplikat serta menolak data yang tidak valid secara otomatis.

## 📂 Repository
🔗 kelompok-kpl/CLO4_TB_KPL

## 📃 Lisensi
Proyek ini dikembangkan khusus untuk kepentingan akademik dalam mata kuliah Konstruksi Perangkat Lunak - Semester 6.
Tidak digunakan untuk tujuan komersial.

---
