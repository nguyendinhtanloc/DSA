using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phase3
{
    public  class QLyThanhToan
    {
        private LinkedList<string> hoaDon;

        //Constructor
        public QLyThanhToan() 
        {
            hoaDon = new LinkedList<string>();
        }

        //Them hoa don moi
        public void themHoaDon(string hoaDonMoi)
        {
            hoaDon.AddLast(hoaDonMoi);
        }

        //Hien thi danh sach hoa don
        public void HienThiDanhSach()
        {
            Console.WriteLine("Dach sách hóa đơn: ");
            foreach ( var hoaDonMoi in hoaDon)
            {
                Console.WriteLine("Hóa đơn: "+ hoaDonMoi);
            } 
                
        }

        //Cap nhat trang thai thanh toan
        public void CapNhatTrangThaiHoaDon(string hoaDonMoi, bool isPaid)
        {
            if (isPaid)
            {
                Console.WriteLine("Hóa đơn đã thanh toán: " + hoaDonMoi);
            }

            else
            {
                Console.WriteLine("Hóa đơn chưa thanh toán: " + hoaDonMoi);
            }
        }

        //Thoat
        public void Thoat()
        {
            Console.WriteLine("Thoát");
            return;
        }

        // Thuc hien cac yeu cau
        public static void HandleThanhToan(QLyThanhToan qlyThanhToan)
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("===== Quản lý Thanh Toán =====");
                Console.WriteLine("1. Thêm hóa đơn mới");
                Console.WriteLine("2. Hiển thị danh sách hóa đơn");
                Console.WriteLine("3. Cập nhật trạng thái hóa đơn");
                Console.WriteLine("4. Quay lại");
                Console.Write("Chọn thao tác: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Nhập thông tin hóa đơn: ");
                        string hoaDonMoi = Console.ReadLine();
                        qlyThanhToan.themHoaDon(hoaDonMoi);
                        break;
                    case "2":
                        qlyThanhToan.HienThiDanhSach();
                        break;
                    case "3":
                        Console.Write("Nhập thông tin hóa đơn cần cập nhật: ");
                        hoaDonMoi = Console.ReadLine();
                        Console.Write("Hóa đơn đã thanh toán (true/false): ");
                        bool isPaid = bool.Parse(Console.ReadLine());
                        qlyThanhToan.CapNhatTrangThaiHoaDon(hoaDonMoi, isPaid);
                        break;
                    case "4":
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng chọn lại.");
                        break;
                }

                if (isRunning)
                {
                    Console.WriteLine("\nNhấn Enter để tiếp tục...");
                    Console.ReadLine();
                }
            }
        }

    }
}
