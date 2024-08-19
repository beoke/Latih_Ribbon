﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace latihribbon
{
   
    public partial class FormKeluar : Form
    {
        private DbDal db;
        public FormKeluar()
        {
            InitializeComponent();
            db = new DbDal();
            Refersh();
        }

        public void Refersh()
        {
            string sql = @"SELECT k.Id,k.Nis,s.Nama,s.Kelas,k.Tanggal,k.JamKeluar,k.JamMasuk,k.Tujuan 
                            FROM Keluar k INNER JOIN siswa s ON k.Nis=s.Nis";
            dataGridView1.DataSource = db.GetKeluar(sql);
        }
        public string Filter(string nis,string nama, string kelas,DateTime tgl1,DateTime tgl2)
        {
            List<string> fltr = new List<string>();
            string sql = @"SELECT k.Id,k.Nis,s.Nama,s.Kelas,k.Tanggal,k.JamKeluar,k.JamMasuk,k.Tujuan 
                            FROM Keluar k INNER JOIN siswa s ON k.Nis=s.Nis";
            if (nis != "") fltr.Add("k.Nis LIKE @nis+'%'");
            if (nama != "") fltr.Add("s.Nama LIKE @nama+'%'");
            if (kelas != "") fltr.Add("s.Kelas LIKE @kelas+'%'");
            //if (tgl1 != null && tgl2 != null) fltr.Add("Tanggal BETWEEN @tgl1 AND @tgl2");

            if (fltr.Count > 0)
            {
                sql += " WHERE " + string.Join(" AND ", fltr);
            }
            return sql;
        }

        public void Filter2()
        {
            string nis, nama, kelas;
            DateTime tgl1, tgl2;

            nis = txtNIS.Text;
            nama = txtNama.Text;
            kelas = txtKelas.Text;
            tgl1 = tglsatu.Value;
            tgl2 = tgldua.Value;
            

            //string sql = Filter(nis,nama,kelas,tgl1,tgl2);
            //MessageBox.Show(sql);
            string sql = @"SELECT k.Id,k.Nis,s.Nama,s.Kelas,k.Tanggal,k.JamKeluar,k.JamMasuk,k.Tujuan 
                            FROM Keluar k INNER JOIN siswa s ON k.Nis=s.Nis WHERE k.Nis LIKE '16677%'";
            var select = db.GetKeluarFilter(sql, new {nis=nis, nama=nama,kelas=kelas,tgl1=tgl1,tgl2=tgl2});

        }

        
        private void txtNIS_TextChanged(object sender, EventArgs e)
        {
            //Filter2();
        }

        private void txtNama_TextChanged(object sender, EventArgs e)
        {
            //Filter2();
        }

        private void txtKelas_TextChanged(object sender, EventArgs e)
        {
            //Filter2();
        }

        private void txtTahun_TextChanged(object sender, EventArgs e)
        {
            Filter2();
        }

        private void tglsatu_ValueChanged(object sender, EventArgs e)
        {
            Filter2();
        }

        private void tgldua_ValueChanged(object sender, EventArgs e)
        {
            Filter2();
        }
   

        private void button2_Click(object sender, EventArgs e)
        {
            Filter2();
        }
    }
}
