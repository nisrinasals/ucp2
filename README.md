# 🏅 Sistem Manajemen Atlet Fakultas Teknik

Aplikasi desktop berbasis **C# (.NET Framework 4.8)** untuk pengelolaan data atlet, prestasi, keuangan, dan event di lingkungan Fakultas Teknik. Dibangun menggunakan Windows Forms dengan koneksi ke **SQL Server** (SSMS).

---

## 📸 Screenshots

| Form Menu Utama |
|---|
| <img width="1920" height="1080" alt="Screenshot 2026-04-16 092657" src="https://github.com/user-attachments/assets/52206f94-81af-4b29-8869-2f4ee574bf93" /> |

| Form Atlet |
|---|
| <img width="1920" height="1080" alt="Screenshot 2026-04-16 092445" src="https://github.com/user-attachments/assets/eeb03e83-d550-45fc-a67b-09fb4c526be1" /> |

| Form Prestasi |
|---|
| <img width="1920" height="1080" alt="Screenshot 2026-04-16 092358" src="https://github.com/user-attachments/assets/219e7493-b31d-4d95-8789-0d4872ddb484" /> |

| Form Keuangan |
|---|
| <img width="1920" height="1080" alt="Screenshot 2026-04-16 092412" src="https://github.com/user-attachments/assets/f4a14ac1-0c89-4501-8c19-f53cfff7d3af" /> |

| Form Grafik Keuangan |
|---|
| <img width="1920" height="1080" alt="Screenshot 2026-04-16 092435" src="https://github.com/user-attachments/assets/0e526e25-ee66-4e91-90f6-3e2d41b4e1bc" /> |



---

## ✨ Fitur Utama

### 👤 Manajemen Data Atlet
- CRUD lengkap (tambah, lihat, ubah, hapus) data atlet
- Field: NIM, Nama, Prodi, Angkatan, Cabang Olahraga
- Validasi duplikasi NIM sebelum penyimpanan
- Import data massal dari file Excel (`.xlsx`)

### 🏆 Manajemen Prestasi
- Pencatatan prestasi atlet beserta tingkat dan tahun
- Relasi many-to-many antara atlet dan prestasi
- Tampilan data terurut berdasarkan nama dan tahun terbaru

### 💰 Manajemen Keuangan
- Pencatatan transaksi **Pemasukan** dan **Pengeluaran** per atlet
- Kalkulasi **saldo total** otomatis
- Validasi jenis transaksi dan jumlah
- Laporan keuangan dalam format PDF (RDLC Report Viewer)
- Import data dari Excel

### 📅 Manajemen Event
- Pendataan event olahraga (nama, jenis, tanggal, keterangan)
- Pencatatan partisipasi atlet beserta peran dalam event
- Relasi event dengan data atlet yang terdaftar

### ⚡ Performa & Teknis
- **In-memory caching** (5 menit) menggunakan `System.Runtime.Caching` untuk mengurangi query berulang ke database
- **Database indexing** otomatis pada kolom yang sering di-query (`nama`, `cabor`, `tanggal`, `nim`, dll.)
- Seluruh operasi write menggunakan **SQL Transaction** dengan rollback jika terjadi error
- Semua query menggunakan **Stored Procedure** dan **parameterized query** untuk mencegah SQL Injection

---

## 🗂️ Struktur Proyek

```
ucp2/
├── FormMenu.cs            # Form menu utama / navigasi antar modul
├── FormAtlet.cs           # Modul manajemen data atlet
├── FormKeuangan.cs        # Modul manajemen keuangan atlet
├── FormPrestasi.cs        # Modul manajemen prestasi atlet
├── FormEvent.cs           # Modul manajemen event & partisipasi
├── ReviewForm.cs          # Form preview sebelum import Excel
├── report.cs              # Report viewer laporan keuangan (RDLC)
├── reportEvent.cs         # Report viewer laporan event (RDLC)
├── ReportKeuangan.rdlc    # Template laporan keuangan
├── ReportEvent.rdlc       # Template laporan event
├── DataSetKeuangan.xsd    # Dataset typed untuk laporan keuangan
├── DataSetEvent.xsd       # Dataset typed untuk laporan event
├── App.config             # Konfigurasi aplikasi
├── packages.config        # Daftar NuGet packages
└── Properties/
    └── AssemblyInfo.cs
```

---

## 🛠️ Tech Stack

| Komponen | Teknologi |
|---|---|
| Language | C# |
| Framework | .NET Framework 4.8 |
| UI | Windows Forms (WinForms) |
| Database | SQL Server (SQL Server Express) |
| ORM / DB Access | ADO.NET (`SqlConnection`, `SqlDataAdapter`) |
| Reporting | Microsoft ReportViewer (RDLC) |
| Excel Import | NPOI 2.7.3 |
| Caching | `System.Runtime.Caching.MemoryCache` |

---

## ⚙️ Prasyarat

Sebelum menjalankan aplikasi, pastikan sudah terinstall:

- [Visual Studio 2019/2022](https://visualstudio.microsoft.com/) (dengan workload **.NET desktop development**)
- [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) atau SQL Server Developer
- [SQL Server Management Studio (SSMS)](https://aka.ms/ssmsfullsetup)
- .NET Framework 4.8

---

## 🚀 Cara Menjalankan

### 1. Clone repository

```bash
git clone https://github.com/nisrinahikmawan/ucp2.git
cd ucp2
```

### 2. Setup Database

Buka SSMS dan jalankan script SQL berikut untuk membuat database dan tabel:

```sql
CREATE DATABASE keuangan2;
GO

USE keuangan2;
GO

-- Tabel Atlet
CREATE TABLE Atlet (
    nim        VARCHAR(20)  PRIMARY KEY,
    nama       VARCHAR(100) NOT NULL,
    prodi      VARCHAR(50)  NOT NULL,
    angkatan   VARCHAR(10)  NOT NULL,
    cabor      VARCHAR(50)  NOT NULL
);

-- Tabel Prestasi
CREATE TABLE Prestasi (
    id_prestasi     INT IDENTITY(1,1) PRIMARY KEY,
    nama_prestasi   VARCHAR(100) NOT NULL,
    tingkat_prestasi VARCHAR(50) NOT NULL,
    tahun_prestasi  INT NOT NULL
);

-- Tabel relasi Atlet <-> Prestasi
CREATE TABLE Prestasi_Atlet (
    id_prestasi INT NOT NULL,
    nim         VARCHAR(20) NOT NULL,
    PRIMARY KEY (id_prestasi, nim),
    FOREIGN KEY (id_prestasi) REFERENCES Prestasi(id_prestasi),
    FOREIGN KEY (nim) REFERENCES Atlet(nim)
);

-- Tabel Keuangan
CREATE TABLE DataKeuangan (
    id_keuangan      INT IDENTITY(1,1) PRIMARY KEY,
    nim              VARCHAR(20)  NOT NULL,
    jenis_transaksi  VARCHAR(20)  NOT NULL CHECK (jenis_transaksi IN ('Pemasukan', 'Pengeluaran')),
    keterangan       VARCHAR(200) NOT NULL,
    jumlah           DECIMAL(18,2) NOT NULL,
    tanggal          DATETIME DEFAULT GETDATE(),
    saldo_total      DECIMAL(18,2),
    FOREIGN KEY (nim) REFERENCES Atlet(nim)
);

-- Tabel Event
CREATE TABLE Event (
    id_event    INT IDENTITY(1,1) PRIMARY KEY,
    nama_event  VARCHAR(100) NOT NULL,
    jenis_event VARCHAR(50)  NOT NULL,
    keterangan  VARCHAR(200),
    tanggal     DATETIME NOT NULL
);

-- Tabel relasi Atlet <-> Event
CREATE TABLE Partisipasi_Atlet (
    id_partisipasi    INT IDENTITY(1,1) PRIMARY KEY,
    id_event          INT NOT NULL,
    nim               VARCHAR(20) NOT NULL,
    peran_partisipasi VARCHAR(50),
    FOREIGN KEY (id_event) REFERENCES Event(id_event),
    FOREIGN KEY (nim) REFERENCES Atlet(nim)
);
```

Kemudian buat Stored Procedure yang digunakan aplikasi:

```sql
-- SP Atlet
CREATE PROCEDURE AddAtlet
    @nim VARCHAR(20), @nama VARCHAR(100), @prodi VARCHAR(50),
    @angkatan VARCHAR(10), @cabor VARCHAR(50)
AS
    INSERT INTO Atlet (nim, nama, prodi, angkatan, cabor)
    VALUES (@nim, @nama, @prodi, @angkatan, @cabor);
GO

CREATE PROCEDURE UpdateAtlet
    @nim VARCHAR(20), @nama VARCHAR(100), @prodi VARCHAR(50),
    @angkatan VARCHAR(10), @cabor VARCHAR(50)
AS
    UPDATE Atlet SET nama=@nama, prodi=@prodi, angkatan=@angkatan, cabor=@cabor
    WHERE nim=@nim;
GO

CREATE PROCEDURE DeleteAtlet @nim VARCHAR(20)
AS
    DELETE FROM Atlet WHERE nim=@nim;
GO

-- SP Keuangan
CREATE PROCEDURE AddTransaksiKeuangan
    @jenis_transaksi VARCHAR(20), @keterangan VARCHAR(200),
    @jumlah DECIMAL(18,2), @nim VARCHAR(20)
AS
BEGIN
    DECLARE @saldo DECIMAL(18,2) = 0;
    SELECT @saldo = ISNULL(saldo_total, 0) FROM DataKeuangan
    WHERE nim = @nim AND id_keuangan = (
        SELECT MAX(id_keuangan) FROM DataKeuangan WHERE nim = @nim
    );
    IF @jenis_transaksi = 'Pemasukan'
        SET @saldo = @saldo + @jumlah;
    ELSE
        SET @saldo = @saldo - @jumlah;
    INSERT INTO DataKeuangan (nim, jenis_transaksi, keterangan, jumlah, tanggal, saldo_total)
    VALUES (@nim, @jenis_transaksi, @keterangan, @jumlah, GETDATE(), @saldo);
END
GO
```

### 3. Konfigurasi Connection String

Buka `FormAtlet.cs` (dan form lainnya), sesuaikan `connectionString` dengan nama SQL Server instance kamu:

```csharp
// Ganti "SAS\\SQLEXPRESS" dengan nama server kamu
// Contoh: "localhost\\SQLEXPRESS" atau ".\SQLEXPRESS"
private readonly string connectionString =
    "Server=NAMA_SERVER\\SQLEXPRESS;Database=keuangan2;Integrated Security=True";
```

> 💡 Atau bisa dipindahkan ke `App.config` agar lebih mudah dikonfigurasi tanpa recompile.

### 4. Restore NuGet Packages & Build

Buka file `ucp2.sln` di Visual Studio, lalu:

1. Klik kanan pada solution → **Restore NuGet Packages**
2. Tekan `Ctrl+Shift+B` untuk build
3. Tekan `F5` untuk run

---

## 📊 Skema Database

```
Atlet (nim PK, nama, prodi, angkatan, cabor)
  │
  ├──< DataKeuangan (id_keuangan PK, nim FK, jenis_transaksi, keterangan, jumlah, tanggal, saldo_total)
  │
  ├──< Prestasi_Atlet (id_prestasi FK, nim FK)
  │         └──> Prestasi (id_prestasi PK, nama_prestasi, tingkat_prestasi, tahun_prestasi)
  │
  └──< Partisipasi_Atlet (id_partisipasi PK, id_event FK, nim FK, peran_partisipasi)
            └──> Event (id_event PK, nama_event, jenis_event, keterangan, tanggal)
```

---

## 📦 NuGet Dependencies

| Package | Versi | Kegunaan |
|---|---|---|
| NPOI | 2.7.3 | Import file Excel (.xlsx) |
| Microsoft.ReportingServices.ReportViewerControl.Winforms | 150.1652.0 | Cetak laporan PDF (RDLC) |
| Microsoft.SqlServer.Types | 160.1000.6 | SQL Server type support |
| System.Runtime.Caching | (built-in) | In-memory caching |

---

## 👥 Tim Pengembang

| Nama | NIM |
|---|---|
| Muhammad Zaky Malika | 20230140017 |
| Nisrina Salsabila H | 20230140054 |
| Wahyu Agung Prasetyo | 20230140068 |
| Chesta Yurcel Zebada | 20230140072 |

---

## 📄 Lisensi

Project ini dibuat untuk keperluan akademik — **UCP2 (Ujian Capaian Pembelajaran 2)**, Program Studi Teknologi Informasi, Universitas Muhammadiyah Yogyakarta.
