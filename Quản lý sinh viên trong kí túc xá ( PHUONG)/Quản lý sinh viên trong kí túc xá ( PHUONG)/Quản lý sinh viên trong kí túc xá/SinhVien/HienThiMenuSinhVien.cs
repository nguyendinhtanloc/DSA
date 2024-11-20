using RoomManagement;
using RoomRegistrationStudent;
using System;
using ThongBao;  // Thư viện ThongBao có thể chứa các lớp và phương thức liên quan đến thông báo
using StudentInformation;
using static AccountManagement.AccountManager;
namespace DisplayStudentMenu
{
    public class StudentMenu
    {
        // Phương thức hiển thị menu sinh viên
        public static void ShowMenuStutdent()
        {
            // Vòng lặp để tiếp tục hiển thị menu cho đến khi người dùng đăng xuất
            while (true)
            {
                // Xóa màn hình console trước khi hiển thị lại menu
                Console.Clear();

                // Hiển thị các lựa chọn menu
                Console.WriteLine("Menu sinh viên:");
                Console.WriteLine("1. Nội quy");
                Console.WriteLine("2. Thông tin sinh viên");
                Console.WriteLine("3. Đăng ký phòng");
                Console.WriteLine("4. Kiểm tra hóa đơn");
                Console.WriteLine("5. Phản hồi & đánh giá");
                Console.WriteLine("6. Thông báo");
                Console.WriteLine("7. Đăng xuất");
                Console.Write("Chọn chức năng: ");

                // Kiểm tra nếu đầu vào là một số nguyên hợp lệ
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    // Nếu đầu vào không hợp lệ, hiển thị thông báo lỗi
                    Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng nhập số từ 1 đến 7.");
                    Console.ReadKey();  // Đợi người dùng nhấn phím để tiếp tục
                    continue;  // Quay lại đầu vòng lặp, hiển thị lại menu
                }

                // Dựa trên lựa chọn của người dùng, thực hiện hành động tương ứng
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Nội quy...");
                        // Logic cho việc hiển thị nội quy
                        Console.ReadKey();  // Đợi người dùng nhấn phím để tiếp tục
                        break;
                    case 2:
                        string retrievedValue = UserSession.FirstSevenCharacters;
                        Console.WriteLine(retrievedValue);
                        Console.ReadKey();
                        ManagementStudentInformation.MenuInformationStudent(retrievedValue);
                        Console.ReadKey();  // Đợi người dùng nhấn phím để tiếp tục
                        break;
                    case 3:
                        Console.WriteLine("Đăng ký phòng...");
                        RoomOperations roomOperations = new RoomOperations();
                        roomOperations.LoadRoomsFromFile();  // Nạp danh sách phòng từ file

                        // Gọi phương thức đăng ký phòng, truyền đối tượng roomOperations vào
                        RoomRegistrationMenu.ShowRoomRegistrationMenu(roomOperations); Console.ReadKey();  // Đợi người dùng nhấn phím để tiếp tục
                        break;
                    case 4:
                        Console.WriteLine("Kiểm tra hóa đơn...");
                        // Logic cho việc kiểm tra hóa đơn
                        Console.ReadKey();  // Đợi người dùng nhấn phím để tiếp tục
                        break;
                    case 5:
                        Console.WriteLine("Phản hồi & đánh giá...");
                        // Logic cho việc phản hồi và đánh giá
                        Console.ReadKey();  // Đợi người dùng nhấn phím để tiếp tục
                        break;
                    case 6:
                        Console.WriteLine("Thông báo...");
                        // Gọi phương thức hiển thị thông báo
                        //NotificationManagement.ShowNotifications();
                        Console.ReadKey();  // Đợi người dùng nhấn phím để tiếp tục
                        break;
                    case 7:
                        return;  // Đăng xuất và thoát khỏi menu
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ.");
                        break;
                }
            }
        }
    }
}