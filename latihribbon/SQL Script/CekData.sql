select * from Kelas;


SELECT DISTINCT Tahun
                                 FROM siswa
                                 ORDER BY Tahun ASC

delete from Jurusan


select * from siswa where Nis = 11111
update siswa set IdKelas =  4 where Nis = 17978

SELECT CAST (ROUND (AVG(Bintang), 1) AS FLOAT)
FROM Rating
WHERE Bintang BETWEEN 1 AND 5;

SELECT s.Nis,s.Nama,s.JenisKelamin,s.Persensi,s.IdKelas,k.NamaKelas,s.Tahun FROM siswa s 
                                INNER JOIN Kelas k ON s.IdKelas = k.Id
                                WHERE s.Nis=16653



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


    SELECT Rombel,Id FROM Kelas WHERE idJurusan= 1 AND Tingkat='X'

    DELETE FROM RiwayatLogin;





    select * from Users;

    select *from masuk

 INSERT INTO Masuk (NIS, Tanggal, JamMasuk, Alasan) VALUES 
(16666, '2024-10-30', '17:00:29', '14:00:29', 'ddededed e dede'),
(17777, '2024-10-02', '10:00:00', '14:00:00', 'makan nasi tikus'),
(16653, '2024-10-30', '03:00:00', '05:00:00', 'Makan Kuyang'),
(17778, '2024-10-30', '02:00:00', '05:00:00', 'eek'),
(16690, '2024-10-30', '03:00:00', '07:00:00', 'hugguiya'),

(16701, '2024-10-05', '12:30:00', '14:30:00', 'ddededed e dede'),
(16702, '2024-10-06', '15:45:00', '17:00:00', 'makan nasi tikus'),
(16703, '2024-10-07', '09:00:00', '11:00:00', 'Makan Kuyang'),
(16704, '2024-10-08', '13:00:00', '15:30:00', 'eek'),
(16705, '2024-10-09', '14:00:00', '16:30:00', 'hugguiya'),
(16799, '2024-10-28', '08:00:00', '10:00:00', 'makan nasi tikus');

INSERT INTO Masuk (NIS, Tanggal, JamMasuk, Alasan) VALUES 
(17777, '2024-10-30', '13:46:55', 'Makan Nasi Padang bersama di kampung magetan dengan ownernya'),
(16670, '2024-10-30', '13:57:59', 'Makan Di Gereja Patalan'),
(16666, '2024-10-30', '13:58:46', 'dcddcdcddc'),
(16653, '2024-10-30', '04:00:00', 'scsscs cs c sc s cs c s'),
(17777, '2024-10-30', '00:00:00', 'd d d d d'),
(16670, '2024-10-30', '00:03:00', 'cscscsc'),
(16778, '2024-10-30', '00:00:00', 'dcdcdccdcdc');