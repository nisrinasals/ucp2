using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ucp2
{
    internal class koneksi
    {
        public string connectionString() // untuk membangun dan mengembalikan string koneksi ke database
        {
            string connectStr = "";
            try
            {
                string localIP = GetLocalIPAddress(); // mendeklarasikan ipaddress
                connectStr = $"Data Source={localIP};Initial Catalog=keuangan2;Integrated Security=True;TrustServerCertificate=True";
                return connectStr;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return string.Empty;
            }
        }
        public static string GetLocalIPAddress() // untuk mengambil Address pada PC yang menjalankan aplikasi
        {
            // mengambil informasi tentang Local host
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork) // mengambil Ipv4
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Tidak ada alamat IP yang ditemukan.");
        }
    }
}
