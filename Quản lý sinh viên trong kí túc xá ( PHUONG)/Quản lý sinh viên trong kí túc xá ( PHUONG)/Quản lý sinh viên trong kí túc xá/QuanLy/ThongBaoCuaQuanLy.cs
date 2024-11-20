using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace DormitoryManagementSystem
{
    // Lớp quản lý thông báo
    public class NotificationManagement
    {
        private static Queue<string> requestQueue = new Queue<string>(); // Hàng đợi yêu cầu

        // Gửi thông báo vào file
        public static void SendNotification(string title, string content)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("StudentNotifications.txt", true))
                {
                    sw.WriteLine($"Title: {title}");
                    sw.WriteLine($"Content: {content}");
                    sw.WriteLine("--------------------------------------------------------");
                }
                Console.WriteLine("Thông báo đã được gửi thành công.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi gửi thông báo: {ex.Message}");
            }
        }

        // Xử lý yêu cầu đăng ký phòng (Duyệt hoặc Hủy)
        public static void ProcessRegistrationRequest(string studentID, string roomNumber, string reason, bool approve)
        {
            try
            {
                // Kiểm tra nếu yêu cầu tồn tại trong file Notification.txt
                var lines = File.ReadAllLines("Notification.txt").ToList();
                var request = lines.FirstOrDefault(r => r.Contains(studentID) && r.Contains(roomNumber) && r.Contains("DKP"));

                if (request == null)
                {
                    Console.WriteLine("Không tìm thấy yêu cầu đăng ký này.");
                    return;
                }

                // Thông báo cho sinh viên, sử dụng toán tử 3 ngôi với approve là var kiểu bool
                string title = approve ? "Thông báo duyệt yêu cầu đăng ký phòng" : "Thông báo hủy yêu cầu đăng ký phòng";
                string content = approve
                    ? $"Sinh viên {studentID} đã được duyệt đăng ký phòng {roomNumber}.\nLý do: {reason}"
                    : $"Yêu cầu đăng ký phòng {roomNumber} của sinh viên {studentID} đã bị hủy.\nLý do: {reason}";

                // Gửi thông báo vào file cho sinh viên
                SendNotification(title, content);

                // Cập nhật file nếu yêu cầu được duyệt
                if (approve)
                {
                    SaveStudentRegistration(studentID, roomNumber);
                }

                // Sau khi xử lý, xóa yêu cầu khỏi Notification.txt
                lines.Remove(request);
                File.WriteAllLines("Notification.txt", lines);

                // Thông báo kết quả
                string action = approve ? "Duyệt" : "Hủy";
                Console.WriteLine($"Đã {action} yêu cầu đăng ký phòng {roomNumber} của sinh viên {studentID}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi xử lý yêu cầu: {ex.Message}");
            }
        }

        // Lưu thông tin đăng ký vào file StudentInformation.txt
        private static void SaveStudentRegistration(string studentID, string roomNumber)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("StudentInformation.txt", true))
                {
                    sw.WriteLine($"{studentID},{roomNumber}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lưu thông tin đăng ký vào file StudentInformation.txt: {ex.Message}");
            }
        }

        // Thêm yêu cầu vào hàng đợi
        public static void AddRequestToQueue(string request)
        {
            requestQueue.Enqueue(request);
            Console.WriteLine("Yêu cầu đã được thêm vào hàng đợi.");
        }

        // Xử lý yêu cầu từ hàng đợi
        public static void ProcessQueue()
        {
            if (requestQueue.Count == 0)
            {
                Console.WriteLine("Hàng đợi trống, không có yêu cầu nào để xử lý.");
                return;
            }

            while (requestQueue.Count > 0)
            {
                string request = requestQueue.Dequeue();
                Console.WriteLine($"Đang xử lý yêu cầu: {request}");

                // Tách các thông tin trong yêu cầu
                var requestDetails = request.Split(',');
                string studentID = requestDetails[0];
                string roomNumber = requestDetails[1];
                string reason = requestDetails.Length > 2 ? requestDetails[2] : string.Empty;

                // Gọi phương thức để xử lý yêu cầu
                Console.WriteLine("Chọn hành động: ");
                Console.WriteLine("1. Duyệt yêu cầu");
                Console.WriteLine("2. Hủy yêu cầu");
                Console.Write("Chọn chức năng: ");
                int actionChoice = int.Parse(Console.ReadLine());

                if (actionChoice == 1)
                {
                    ProcessRegistrationRequest(studentID, roomNumber, reason, true);
                }
                else if (actionChoice == 2)
                {
                    ProcessRegistrationRequest(studentID, roomNumber, reason, false);
                }
                else
                {
                    Console.WriteLine("Lựa chọn không hợp lệ.");
                }
            }
        }
    }

    // Lớp Menu cho quản lý ký túc xá
    public class ManagerNotificationMenu
    {
        public static void ShowManagerNotificationMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Menu Quản Lý Ký Túc Xá:");
                Console.WriteLine("1. Kiểm tra thông báo");
                Console.WriteLine("2. Xử lý yêu cầu trong hàng đợi");
                Console.WriteLine("3. Quay lại");
                Console.Write("Chọn chức năng: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng nhập số từ 1 đến 3.");
                    Console.ReadKey();
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        // Kiểm tra thông báo
                        CheckNotifications();
                        break;
                    case 2:
                        // Xử lý yêu cầu trong hàng đợi
                        NotificationManagement.ProcessQueue();
                        break;
                    case 3:
                        return; // Quay lại menu trước
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ.");
                        break;
                }
            }
        }

        private static void CheckNotifications()
        {
            var lines = File.ReadAllLines("Notification.txt").ToList();
            var notifications = lines.Where(line => line.Contains("DKP")).ToList();

            if (!notifications.Any())
            {
                Console.WriteLine("Không có thông báo đăng ký phòng nào.");
                return;
            }

            // Hiển thị các thông báo yêu cầu đăng ký phòng
            foreach (var notification in notifications)
            {
                Console.WriteLine(notification);  // In thông báo đăng ký phòng
            }

            Console.WriteLine("\nNhập mã thông báo để xử lý (DKP): ");
            string notificationID = Console.ReadLine();

            if (notificationID != "DKP")
            {
                Console.WriteLine("Mã thông báo không hợp lệ.");
                return;
            }

            // Lựa chọn hành động duyệt hoặc hủy
            Console.WriteLine("Chọn hành động: ");
            Console.WriteLine("1. Duyệt yêu cầu");
            Console.WriteLine("2. Hủy yêu cầu");
            Console.Write("Chọn chức năng: ");
            int actionChoice = int.Parse(Console.ReadLine());

            // Nhập thông tin của sinh viên và số phòng
            Console.Write("Nhập mã sinh viên: ");
            string studentID = Console.ReadLine();

            Console.Write("Nhập số phòng: ");
            string roomNumber = Console.ReadLine();

            // Lý do hành động (có thể bỏ trống)
            Console.Write("Nhập lý do (có thể bỏ trống): ");
            string reason = Console.ReadLine();

            // Thêm yêu cầu vào hàng đợi
            string request = $"{studentID},{roomNumber},{reason}";
            NotificationManagement.AddRequestToQueue(request);
        }
    }
}