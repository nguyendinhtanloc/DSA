using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ThongBao
{
    public class ThongBao
    {
        public int Id;
        public DateTime DateTime;
        public string Message;
        public string Status; // Trạng thái: "Đã đọc" hoặc "Chưa đọc"
        public string Sender;  // Người gửi (Sinh viên, Quản lý, ...)

        public ThongBao(int id, string message, DateTime dateTime, string status, string sender)
        {
            Id = id;
            Message = message;
            DateTime = dateTime;
            Status = status;
            Sender = sender;
        }

        public override string ToString()
        {
            return $"{Id} - {DateTime.ToString("yyyy-MM-dd HH:mm:ss")} - {Sender} - {Message} - {Status}";
        }

        // Đánh dấu thông báo là đã đọc
        public void MarkAsRead()
        {
            Status = "Đã đọc";
        }
    }

    public class QuanLyThongBao
    {
        private static readonly string filePath = "ThongBao.txt"; // Đường dẫn đến file lưu thông báo

        // Đọc tất cả thông báo từ file
        public static List<ThongBao> ReadThongBaoFromFile()
        {
            List<ThongBao> thongBaos = new List<ThongBao>();

            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 5)
                    {
                        int id = int.Parse(parts[0]);
                        DateTime dateTime = DateTime.Parse(parts[1]);
                        string sender = parts[2];
                        string message = parts[3];
                        string status = parts[4];

                        thongBaos.Add(new ThongBao(id, message, dateTime, status, sender));
                    }
                }
            }

            return thongBaos;
        }

        // Ghi lại danh sách thông báo vào file
        public static void WriteThongBaoToFile(List<ThongBao> thongBaos)
        {
            var lines = thongBaos.Select(t => $"{t.Id},{t.DateTime.ToString("yyyy-MM-dd HH:mm:ss")},{t.Sender},{t.Message},{t.Status}").ToArray();
            File.WriteAllLines(filePath, lines);
        }

        // Đánh dấu thông báo là đã đọc và cập nhật vào file
        public static void MarkThongBaoAsRead(int id)
        {
            var thongBaos = ReadThongBaoFromFile();
            var thongBao = thongBaos.FirstOrDefault(t => t.Id == id);
            if (thongBao != null)
            {
                thongBao.MarkAsRead();
                WriteThongBaoToFile(thongBaos); // Cập nhật lại vào file
                Console.WriteLine($"Thông báo {id} đã được đánh dấu là đã đọc.");
            }
            else
            {
                Console.WriteLine("Không tìm thấy thông báo với ID này.");
            }
        }

        // Xóa thông báo và cập nhật lại file
        public static void DeleteThongBao(int id)
        {
            var thongBaos = ReadThongBaoFromFile();
            var thongBao = thongBaos.FirstOrDefault(t => t.Id == id);
            if (thongBao != null)
            {
                thongBaos.Remove(thongBao);
                WriteThongBaoToFile(thongBaos); // Cập nhật lại vào file
                Console.WriteLine($"Thông báo {id} đã được xóa.");
            }
            else
            {
                Console.WriteLine("Không tìm thấy thông báo với ID này.");
            }
        }

        // Hiển thị danh sách thông báo
        public static void DisplayThongBao()
        {
            var thongBaos = ReadThongBaoFromFile();
            Console.WriteLine("-------------------------------------------------------------------------------------");
            Console.WriteLine("| ID | Ngày gửi      | Người gửi  | Nội dung  | Trạng thái ");
            Console.WriteLine("-------------------------------------------------------------------------------------");
            foreach (var thongBao in thongBaos)
            {
                Console.WriteLine($"| {thongBao.Id,-2} | {thongBao.DateTime.ToString("yyyy-MM-dd HH:mm:ss"),-15} | {thongBao.Sender,-10} | {thongBao.Message,-30} | {thongBao.Status,-10} ");
            }
            Console.WriteLine("-------------------------------------------------------------------------------------");
        }

        // Hiển thị menu quản lý thông báo
        public static void MenuThongBao()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("************************** Quản lý Thông Báo **************************");
                Console.WriteLine("1. Hiển thị danh sách thông báo");
                Console.WriteLine("2. Đánh dấu thông báo đã đọc");
                Console.WriteLine("3. Xóa thông báo");
                Console.WriteLine("4. Quay lại menu chính");
                Console.Write("Chọn thao tác: ");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Lựa chọn không hợp lệ.");
                    Console.ReadKey();
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        DisplayThongBao();
                        break;

                    case 2:
                        MarkThongBaoAsReadMenu();
                        break;

                    case 3:
                        DeleteThongBaoMenu();
                        break;

                    case 4:
                        return; // Quay lại menu chính

                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ.");
                        break;
                }
            }
        }

        // Đánh dấu thông báo là đã đọc
        private static void MarkThongBaoAsReadMenu()
        {
            Console.Write("\nNhập ID thông báo cần đánh dấu là đã đọc: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                MarkThongBaoAsRead(id);
            }
            else
            {
                Console.WriteLine("ID không hợp lệ.");
            }
            Console.ReadKey();
        }

        // Xóa thông báo sau khi xác nhận
        private static void DeleteThongBaoMenu()
        {
            Console.Write("\nNhập ID thông báo cần xóa: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine($"Bạn có chắc chắn muốn xóa thông báo này? (ID: {id})");
                Console.Write("Nhấn Y để xác nhận, bất kỳ phím nào khác để hủy: ");
                char confirmation = Console.ReadKey().KeyChar;
                if (char.ToUpper(confirmation) == 'Y')
                {
                    DeleteThongBao(id);
                }
                else
                {
                    Console.WriteLine("\nHành động xóa đã bị hủy.");
                }
            }
            else
            {
                Console.WriteLine("ID không hợp lệ.");
            }
            Console.ReadKey();
        }
    }
}