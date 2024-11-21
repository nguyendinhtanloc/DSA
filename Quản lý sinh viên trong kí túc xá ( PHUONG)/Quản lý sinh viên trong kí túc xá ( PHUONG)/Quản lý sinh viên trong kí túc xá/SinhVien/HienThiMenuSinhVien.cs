using RoomManagement;
using RoomRegistrationStudent;
using System;
//using ThongBao;  // Thư viện ThongBao có thể chứa các lớp và phương thức liên quan đến thông báo
using StudentInformation;
using static AccountManagement.AccountManager;
using Rules;
using AccountManagement;

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
                

                // Hiển thị các lựa chọn menu
                Console.Clear(); // Xóa màn hình trước khi hiển thị menu
                Console.WriteLine("============================================");
                Console.WriteLine("|                Menu Sinh Viên            |");
                Console.WriteLine("============================================");
                Console.WriteLine("| {0,-2} | {1,-30}     |", "STT", "Chức năng");
                Console.WriteLine("============================================");
                Console.WriteLine("| {0,-2} | {1,-30}      |", "1", "Nội quy");
                Console.WriteLine("| {0,-2} | {1,-30}      |", "2", "Thông tin sinh viên");
                Console.WriteLine("| {0,-2} | {1,-30}      |", "3", "Đăng ký phòng");
                Console.WriteLine("| {0,-2} | {1,-30}      |", "4", "Kiểm tra tiền phòng");
                Console.WriteLine("| {0,-2} | {1,-30}      |", "5", "Kiểm tra kỷ luật");
                Console.WriteLine("| {0,-2} | {1,-30}      |", "6", "Thông báo");
                Console.WriteLine("| {0,-2} | {1,-30}      |", "7", "Đăng xuất");
                Console.WriteLine("============================================");
                Console.Write("Chọn chức năng: ");

                // Kiểm tra nếu đầu vào là một số nguyên hợp lệ
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    // Nếu đầu vào không hợp lệ, hiển thị thông báo lỗi
                    Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng nhập số từ 1 đến 7.");
                    Console.ReadKey();  // Đợi người dùng nhấn phím để tiếp tục
                    continue;  // Quay lại đầu vòng lặp, hiển thị lại menu
                }
                string currentUsername = UserSession.Username;
                string first8Chars = currentUsername.Substring(0, 8);
                // Dựa trên lựa chọn của người dùng, thực hiện hành động tương ứng
                switch (choice)
                {
                    case 1:
                        NoiQuy.DisplayKTXRules();
                        Console.ReadKey();  // Đợi người dùng nhấn phím để tiếp tục
                        break;
                    case 2:
                       
                        ManagementStudentInformation.MenuInformationStudent(first8Chars);
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
                        Console.WriteLine("Kiểm tra tiền phòng");
                        string roomid = RoomRegistration.FindRoomByStudentId(first8Chars);
                        // Logic cho việc kiểm tra hóa đơn
                        RoomRegistration.FindRoom2(roomid);
                        Console.ReadKey();  // Đợi người dùng nhấn phím để tiếp tục
                        break;
                    case 5:
                        Console.WriteLine("Kiểm Tra Kỷ Luật");
                        RoomRegistration.FindKyLuat(first8Chars);
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