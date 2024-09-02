﻿using Dapper;
using latihribbon.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace latihribbon
{
    public class DbDal
    {
        private const string _connString = "Server=(local);Database=RekapSiswa;Trusted_Connection=True;TrustServerCertificate=True";

        // Function untuk mendapatkan user dari database
        public IEnumerable<UserModel> GetUsers()
        {
                using (var connection = new SqlConnection(_connString))
                {
                    connection.Open();
                    var users = connection.Query<UserModel>("SELECT * FROM Users");
                    return users;
                }
        }
        // Function untuk mendapatkan data siswa dari database
        public SiswaModel GetSiswaByNis(int nis)
        {
            using (var connection = new SqlConnection(_connString))
            {
                connection.Open();
                var siswa = connection.QuerySingleOrDefault<SiswaModel>("SELECT * FROM siswa WHERE nis = @nis", new { Nis = nis });
                return siswa;
            }
        }

        public int IUD(string sql, object param) //Template
        {
            using (var koneksi = new SqlConnection(_connString))
            {
                var iud = koneksi.Execute(sql,param);
                return iud;
            }
        }

    }
}
