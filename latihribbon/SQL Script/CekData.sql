DROP TABLE Rating;




SELECT CAST (ROUND (AVG(Bintang), 1) AS FLOAT)
FROM Rating
WHERE Bintang BETWEEN 1 AND 5;



SELECT k.Id,k.NamaKelas,k.Rombel,k.IdJurusan,k.Tingkat,j.NamaJurusan FROM Kelas k
                                INNER JOIN Jurusan j ON k.IdJurusan=j.Id  
                                ORDER BY CASE 
                                        WHEN k.Tingkat = 'X' THEN 1
                                        WHEN k.Tingkat = 'XI' THEN 2
                                        WHEN k.Tingkat = 'XII' THEN 3
                                        ELSE 4
                                    END, idJurusan, Rombel


SELECT * FROM Jurusan ORDER BY Id ASC

select * from RiwayatLogin;

INSERT INTO RiwayatLogin(UserLogin, Tanggal, Waktu)
VALUES ('jhbdjcsdmn fvd', '2024-02-20', '12:58:23');



DROP TABLE Survey;

create table Survey (
    SurveyId INT IDENTITY(1,1),
    HasilSurvey INT NOT NULL DEFAULT(0),
    Tanggal DATETIME,
    Waktu TIME
    )


    select * from Survey




    SELECT DISTINCT Tahun
                                 FROM siswa
                                 ORDER BY Tahun ASC








                                 SELECT k.Id,k.NamaKelas,k.Rombel,k.IdJurusan,k.Tingkat,j.NamaJurusan FROM Kelas k
                                INNER JOIN Jurusan j ON k.IdJurusan=j.Id  
                                ORDER BY Id, CASE 
                                        WHEN k.Tingkat = 'X' THEN 1
                                        WHEN k.Tingkat = 'XI' THEN 2
                                        WHEN k.Tingkat = 'XII' THEN 3
                                        ELSE 4
                                    ENd

                                    select * from Siswa where Tahun = '2026' 





                                    SELECT s.Nis,s.Nama,s.JenisKelamin,s.Persensi,k.NamaKelas,s.Tahun FROM siswa s 
                                INNER JOIN Kelas k ON s.IdKelas = k.Id  
                                ORDER BY 
                                    CAST(Tahun AS INT) DESC, s.IdKelas ASC,
                                    SUBSTRING( k.NamaKelas, CHARINDEX(' ', k.NamaKelas) + 1, LEN(k.NamaKelas)) ASC 
                                   SELECT s.Nis, s.Nama, s.JenisKelamin, s.Persensi, k.NamaKelas, s.Tahun 
FROM siswa s 
INNER JOIN Kelas k ON s.IdKelas = k.Id  
ORDER BY 
    CAST(s.Tahun AS INT) DESC, s.IdKelas ASC,
    RIGHT(k.NamaKelas, LEN(k.NamaKelas) - CHARINDEX(' ', k.NamaKelas)) ASC;
