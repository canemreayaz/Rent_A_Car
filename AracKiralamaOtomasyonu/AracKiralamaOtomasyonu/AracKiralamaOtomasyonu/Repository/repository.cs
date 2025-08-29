using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracKiralamaOtomasyonu.Repository
{
    public class repository
    {
        // connectionString SQL  Servera nasıl bağlanacağını belirten metindir.
        // readonly : Bu değişken constructora bir kez atanır sonradan değiştirelemez.
        private readonly string connectionString;

        // new repository() : Çalıştığında otomatik çalışır
        public repository()
        {
            
            connectionString = "Data Source=AYAZ;Initial Catalog=AracKiralamaOtomasyonu;User ID=sa; password=1";
        }

        // Bu metot çağrıldığında yeni bir SqlConnection nesnesi döndürür.
        // procted repository() : Bu metot repository sınıfından türetilen sınıflar tarafından erişilebilir.
        // SqlConnection : new SqlConnection(connectionString);her çağrıda taze bağlantı oluşturur.
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
