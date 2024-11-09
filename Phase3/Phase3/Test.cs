using System;

namespace Phase3
{
    public class TestProgram
    {
        public static void Main(string[] args)
        {
            QLyNeNepVaQuyDinh qlyQuyDinh = new QLyNeNepVaQuyDinh();
            QLyKyLuat qlyKyLuat = new QLyKyLuat();
            QLyThanhToan qlyThanhToan = new QLyThanhToan();

            bool isRunning = true;
            while (isRunning)
            {
                // Menu
                Console.Clear();
                Console.WriteLine("===== MENU =====");
                Console.WriteLine("1. Quản lý quy định và thông báo");
                Console.WriteLine("2. Quản lý kỷ luật");
                Console.WriteLine("3. Quản lý thanh toán");
                Console.WriteLine("4. Thoát");
                Console.Write("Chọn thao tác: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        QLyNeNepVaQuyDinh.HandleQuyDinhThongBao(qlyQuyDinh);
                        break;
                    case "2":
                        QLyKyLuat.HandleKyLuat(qlyKyLuat);
                        break;
                    case "3":
                        QLyThanhToan.HandleThanhToan(qlyThanhToan);
                        break;
                    case "4":
                        Console.WriteLine("Thoát khỏi chương trình.");
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng chọn lại.");
                        break;
                }

                // Cho nguoi dung
                if (isRunning)
                {
                    Console.WriteLine("\nNhấn Enter để tiếp tục...");
                    Console.ReadLine();
                }
            }
        }
    }
}
