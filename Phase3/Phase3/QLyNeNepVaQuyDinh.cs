using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phase3
{
    public class QLyNeNepVaQuyDinh
    {
        private LinkedList<string> quyDinh;
        private LinkedList<string> thongBao;

        // Constructor
        public QLyNeNepVaQuyDinh()
        {
            quyDinh = new LinkedList<string>();
            thongBao = new LinkedList<string>();

        }

        //Them quy dinh moi
        public void ThemQuyDinhMoi(string quyDinhMoi)
        {
            quyDinh.AddLast(quyDinhMoi);
        }

        //Them thong bao moi
        public void ThemThongBaoMoi(string thongBaoMoi) 
        {
            thongBao.AddLast(thongBaoMoi);
        }

        //Hien thi cac quy dinh
        public void HienThiQuyDinh()
        {
            Console.WriteLine("Danh sách các quy định: ");
            foreach (var quyDinhMoi in quyDinh)
            {
                Console.WriteLine("Quy định: " + quyDinhMoi);
            }
        }

        //Hien thi cac thong bao
        public void HienThiCacThongBao()
        {
            Console.WriteLine("Danh sách các thông báo: ");
            foreach (var thongBaoMoi in thongBao)
            {
                Console.WriteLine("Thông báo: " + thongBaoMoi);
            }
        }
        public void Thoat()
        {
            Console.WriteLine("Thoát");
            return;
        }
        public static void HandleQuyDinhThongBao(QLyNeNepVaQuyDinh qlyQuyDinh)
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("===== Quản lý Quy Định và Thông Báo =====");
                Console.WriteLine("1. Thêm quy định mới");
                Console.WriteLine("2. Thêm thông báo mới");
                Console.WriteLine("3. Hiển thị quy định");
                Console.WriteLine("4. Hiển thị thông báo");
                Console.WriteLine("5. Quay lại");
                Console.Write("Chọn thao tác: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Nhập quy định mới: ");
                        string quyDinhMoi = Console.ReadLine();
                        qlyQuyDinh.ThemQuyDinhMoi(quyDinhMoi);
                        break;
                    case "2":
                        Console.Write("Nhập thông báo mới: ");
                        string thongBaoMoi = Console.ReadLine();
                        qlyQuyDinh.ThemThongBaoMoi(thongBaoMoi);
                        break;
                    case "3":
                        qlyQuyDinh.HienThiQuyDinh();
                        break;
                    case "4":
                        qlyQuyDinh.HienThiCacThongBao();
                        break;
                    case "5":
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
