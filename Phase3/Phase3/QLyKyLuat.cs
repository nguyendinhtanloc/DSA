using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phase3
{
    public class QLyKyLuat
    {
        private Dictionary<string, int> soLanKyLuat;

        //Constructor
        public QLyKyLuat()
        {
            soLanKyLuat = new Dictionary<string, int>();
        }

        //Them hoac cap nhat so lan ky luat
        public void AddSoLanKyLuat(string studentId)
        {
            if (soLanKyLuat.ContainsKey(studentId))
            {
                soLanKyLuat[studentId]++;
            }

            else
            {
                soLanKyLuat[studentId] = 1;
            }

            Console.WriteLine("Cập nhật kỷ luật cho sinh viên: " + studentId + ". Số lần kỷ luật là: " + soLanKyLuat[studentId]);
        }

        //Kiem tra so lan vi pham
        public void KiemTraSoLanViPham(string studentId)
        {
            if (soLanKyLuat.TryGetValue(studentId, out int demKyLuat))
            {
                Console.WriteLine("Sinh viên " + studentId + "có " + demKyLuat + " lần bị kỷ luật ");
            }
            else
            {
                Console.WriteLine("Sinh viên " + studentId + " chưa có vi phạm");
            }
        }

        private void SendNotification( string studentId)
        {
            if (soLanKyLuat[studentId] == 1)
            {
                Console.WriteLine("Thông báo: Sinh viên " + studentId + " đã bị kỉ luật");
            }
            else
            {
                Console.WriteLine("Thông báo: Sinh viên " + studentId + " đã bị kỷ luật " + soLanKyLuat[studentId] + " lần.");
            }
        }

        public void Thoat()
        {
            Console.WriteLine("Thoát");
            return;
        }

        public static void HandleKyLuat(QLyKyLuat qlyKyLuat)
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("===== Quản lý Kỷ Luật =====");
                Console.WriteLine("1. Thêm kỷ luật cho sinh viên");
                Console.WriteLine("2. Kiểm tra số lần vi phạm");
                Console.WriteLine("3. Quay lại");
                Console.Write("Chọn thao tác: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Nhập mã sinh viên: ");
                        string studentId = Console.ReadLine();
                        qlyKyLuat.AddSoLanKyLuat(studentId);
                        break;
                    case "2":
                        Console.Write("Nhập mã sinh viên để kiểm tra: ");
                        studentId = Console.ReadLine();
                        qlyKyLuat.KiemTraSoLanViPham(studentId);
                        break;
                    case "3":
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
