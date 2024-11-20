//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace QuanLyPhong
//{
//    public class Phong
//    {
//        public string SoPhong;
//        public int TongSoGiuong;
//        public int GiuongTrong;
//        public string TrangThai;

//        public Phong(string soPhong, int tongSoGiuong, int giuongTrong, string trangThai)
//        {
//            SoPhong = soPhong;
//            TongSoGiuong = tongSoGiuong;
//            GiuongTrong = giuongTrong;
//            TrangThai = trangThai;
//        }

//        public void CapNhatThongTinPhong(int tongSoGiuong, int giuongTrong, string trangThai)
//        {
//            TongSoGiuong = tongSoGiuong;
//            GiuongTrong = giuongTrong;
//            TrangThai = trangThai;
//        }

//        public int GiuongDaSuDung()
//        {
//            return TongSoGiuong - GiuongTrong;
//        }
//    }

//    public class ThaoTacPhong
//    {
//        private List<Phong> dsPhong = new List<Phong>();

//        public void ThemPhong(Phong phong)
//        {
//            dsPhong.Add(phong);
//        }

//        public void XoaPhong(string soPhong)
//        {
//            dsPhong.RemoveAll(p => p.SoPhong == soPhong);
//        }

//        public bool CapNhatPhong(string soPhong, int tongSoGiuong, int giuongTrong, string trangThai)
//        {
//            Phong phong = dsPhong.FirstOrDefault(p => p.SoPhong == soPhong);
//            if (phong != null)
//            {
//                phong.CapNhatThongTinPhong(tongSoGiuong, giuongTrong, trangThai);
//                return true;
//            }
//            return false;
//        }

//        public bool KiemTraPhongTonTai(string soPhong)
//        {
//            return dsPhong.Any(p => p.SoPhong == soPhong);
//        }

//        public void HienThiDanhSachPhong()
//        {
//            Console.WriteLine("------------------------------------------------------------------------------------");
//            Console.WriteLine("| STT | Số phòng      | Tổng số giường | Giường trống  | Giường đã sử dụng | Ghi chú  ");
//            Console.WriteLine("------------------------------------------------------------------------------------");

//            int index = 1;
//            foreach (var phong in dsPhong.OrderBy(p => p.GiuongTrong))
//            {
//                Console.WriteLine($"| {index,-3} | {phong.SoPhong,-11} | {phong.TongSoGiuong,-15} | {phong.GiuongTrong,-12} | {phong.GiuongDaSuDung(),-17} | {phong.TrangThai,-18} ");
//                index++;
//            }

//            Console.WriteLine("------------------------------------------------------------------------------------");
//        }

//        public void HienThiMenuQuanLyPhong()
//        {
//            // Nội dung của phương thức không thay đổi
//        }

//        private void NhapThongTinPhong() { /* ... */ }
//        private void XoaPhong() { /* ... */ }
//        private void CapNhatThongTinPhong() { /* ... */ }
//    }
//}
