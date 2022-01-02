using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;

namespace ServiceRest_20190140015_Muhamad_Arief_P_Suradi
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class TI_UMY : ITI_UMY
    {
        string constr = "Data Source = DESKTOP-K30E79F;Initial Catalog=\"TI UMY\";Persist Security Info=True;User ID=sa;Password=A12zpanMDO";
        public string CreateMahasiswa(Mahasiswa mhs)
        {
            string msg = "GAGAL";

            SqlConnection con = new SqlConnection(constr);
            string query = String.Format("insert into dbo.Mahasiswa values('{0}','{1}','{2}','{3}')",mhs.nim,mhs.nama,mhs.prodi,mhs.angkatan);
            SqlCommand com = new SqlCommand(query, con);
            try
            {
                con.Open();
                Console.WriteLine(query);
                com.ExecuteNonQuery();
                con.Close();
                msg = "SUKSES";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(query);
                msg = "GAGAL";
            }
            return msg;
        }

        public List<Mahasiswa> GetAllMahasiswa()
        {
            List<Mahasiswa> list = new List<Mahasiswa>();

            string query = "select NIM,Nama,Prodi,Angkatan from dbo.Mahasiswa";
            SqlConnection con = new SqlConnection(constr);
            SqlCommand com = new SqlCommand(query, con);
            try
            {
                con.Open();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Mahasiswa mhs = new Mahasiswa();
                    mhs.nama = reader.GetString(1);
                    mhs.nim = reader.GetString(0);
                    mhs.prodi = reader.GetString(2);
                    mhs.angkatan = reader.GetString(3);
                    list.Add(mhs);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(query);
            }
            return list;
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public Mahasiswa GetMahasiswaByNIM(string nim)
        {
            Mahasiswa mhs = new Mahasiswa();
            SqlConnection con = new SqlConnection(constr);
            string query = String.Format("select NIM,Nama,Prodi,Angkatan from dbo.Mahasiswa where NIM = '{0}'",nim);
            SqlCommand com = new SqlCommand(query, con);

            try
            {
                con.Open();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    mhs.nim = reader.GetString(0);
                    mhs.nama = reader.GetString(1);
                    mhs.prodi = reader.GetString(2);
                    mhs.angkatan = reader.GetString(3);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(query);
            }
            return mhs;
        }
    }
}
