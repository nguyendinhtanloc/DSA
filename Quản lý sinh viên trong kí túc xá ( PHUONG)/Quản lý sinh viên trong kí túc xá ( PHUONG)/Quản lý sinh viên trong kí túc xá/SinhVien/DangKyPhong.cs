using RoomManagement;
using System;
using System.IO;
using System.Linq;

namespace RoomRegistrationStudent
{
    public class RoomRegistration
    {
        public RoomOperations roomOperations; // Sử dụng RoomOperations đã tạo sẵn

        public RoomRegistration(RoomOperations roomOperations)
        {
            this.roomOperations = roomOperations;
        }

        // Kiểm tra mã sinh viên có tồn tại trong hệ thống
        public bool IsStudentExist(string studentID)
        {
            try
            {
                var lines = File.ReadLines("StudentInformation.txt"); // File chứa thông tin sinh viên
                return lines.Any(line => line.Split(',')[0] == studentID); // Kiểm tra mã sinh viên có tồn tại không
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi kiểm tra mã sinh viên: " + ex.Message);
                return false;
            }
        }
        public static string FindRoomByStudentId(string studentId, string filePath = "StudentInfo.txt")
        {
            try
            {
                // Đọc tất cả các dòng từ file
                var lines = File.ReadAllLines(filePath);

                foreach (var line in lines)
                {
                    // Chia dòng thành các phần tử bằng dấu phẩy
                    var parts = line.Split(',');

                    // Kiểm tra nếu dòng đủ thông tin và mã số sinh viên khớp
                    if (parts.Length == 4 && parts[3].Trim() == studentId)
                    {
                        return parts[1]; // Trả về số phòng (phần tử thứ 2 trong dòng)
                    }
                }

                // Nếu không tìm thấy sinh viên
                return "Không tìm thấy số phòng của sinh viên.";
            }
            catch (FileNotFoundException)
            {
                return "File không tồn tại.";
            }
            catch (Exception ex)
            {
                return $"Đã xảy ra lỗi: {ex.Message}";
            }
        }
        //Kiểm tra thanh toán
        public static void FindRoom2(string roomCode)
        {
            string filePath = "Room.txt"; // Đường dẫn đến file chứa thông tin phòng

            // Kiểm tra nếu file tồn tại
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File không tồn tại.");
                return;
            }

            // Đọc tất cả các dòng trong file
            string[] lines = File.ReadAllLines(filePath);
            bool roomFound = false;

            // Duyệt qua từng dòng trong file
            foreach (string line in lines)
            {
                // Tách thông tin trong mỗi dòng theo dấu phẩy
                string[] parts = line.Split(',');

                // Kiểm tra nếu mã phòng khớp với mã phòng tìm kiếm
                if (parts.Length == 4 && parts[0].Trim().Equals(roomCode, StringComparison.OrdinalIgnoreCase))
                {
                    // Xuất thông tin phòng
                    Console.WriteLine("===============================================================================");
                    Console.WriteLine("| {0,-10} | {1,-10} | {2,-10}|", "Mã phòng", "Tên phòng", "Trạng thái (Y: Đã thanh Toán / N: Chưa thanh toán) ");
                    Console.WriteLine("===============================================================================");
                    Console.WriteLine("| {0,-10} | {1,-10} | {2,-10}                                         |", parts[0], parts[1], parts[3]);
                    Console.WriteLine("===============================================================================");

                    roomFound = true;
                    break;
                }
            }

            // Nếu không tìm thấy phòng
            if (!roomFound)
            {
                Console.WriteLine("Không tìm thấy phòng với mã phòng: " + roomCode);
            }
        }
        public static void FindKyLuat(string studentID)
        {
            string filePath = "Discipline.txt"; // Đường dẫn đến file chứa thông tin phòng

            // Kiểm tra nếu file tồn tại
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File không tồn tại.");
                return;
            }

            // Đọc tất cả các dòng trong file
            string[] lines = File.ReadAllLines(filePath);
            bool student = false;

            // Duyệt qua từng dòng trong file
            foreach (string line in lines)
            {
                // Tách thông tin trong mỗi dòng theo dấu phẩy
                string[] parts = line.Split(',');

                // Kiểm tra nếu mã phòng khớp với mã phòng tìm kiếm
                if (parts.Length == 4 && parts[1].Trim().Equals(studentID, StringComparison.OrdinalIgnoreCase))
                {
                    // Xuất thông tin phòng
                    Console.WriteLine("===============================================================================");
                    Console.WriteLine("| {0,-10} | {1,-10} | {2,-10}", "Mã số SV", "Tên Sinh Viên", "Số lần vi phạm ");
                    Console.WriteLine("===============================================================================");
                    Console.WriteLine("| {0,-10} | {1,-10} | {2,-10}                                         ", parts[1], parts[2], parts[3]);
                    Console.WriteLine("===============================================================================");

                    student = true;
                    break;
                }
            }

            // Nếu không tìm thấy phòng
            if (!student)
            {
                Console.WriteLine("Không tìm thấy phòng với mã phòng: " + studentID);
            }
        }

        // Kiểm tra sinh viên đã đăng ký phòng chưa
        public bool IsStudentRegistered(string studentID)
        {
            try
            {
                var lines = File.ReadLines("StudentInformation.txt"); // File chứa thông tin sinh viên
                return lines.Any(line => line.Split(',')[0] == studentID); // Kiểm tra nếu sinh viên đã đăng ký phòng
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi kiểm tra thông tin đăng ký sinh viên: " + ex.Message);
                return false;
            }
        }

        // Kiểm tra phòng đã đầy hay chưa
        public bool IsRoomAvailable(string roomNumber)
        {
            var room = roomOperations.roomList.FirstOrDefault(r => r.RoomNumber == roomNumber);
            return room != null && room.AvailableBeds > 0;
        }

        // Đăng ký phòng cho sinh viên
        public void RegisterRoom(string studentID, string roomNumber)
        {
            // Kiểm tra mã sinh viên có hợp lệ không
            if (!IsStudentExist(studentID))
            {
                Console.WriteLine("Mã sinh viên không hợp lệ.");
                return;
            }

            // Kiểm tra sinh viên đã đăng ký phòng chưa
            if (IsStudentRegistered(studentID))
            {
                Console.WriteLine("Sinh viên đã đăng ký phòng rồi. Không thể đăng ký lại.");
                return;
            }

            // Kiểm tra nếu phòng có tồn tại và còn giường trống
            if (!IsRoomAvailable(roomNumber))
            {
                Console.WriteLine("Phòng này không tồn tại hoặc đã đầy.");
                return;
            }

            // Tìm phòng và cập nhật số giường trống
            
           

            // Lưu lại thông tin phòng đã cập nhật vào file
            roomOperations.SaveAllRoomsToFile();

            // Lưu thông tin đăng ký của sinh viên vào file
            SaveStudentRegistration(studentID, roomNumber);

            // Lưu thông tin yêu cầu vào file Notification.txt
            SaveRoomRegistrationNotification(studentID, roomNumber);

            Console.WriteLine($"Đăng ký phòng {roomNumber} thành công!");
        }

        // Lưu thông tin đăng ký sinh viên vào file StudentInformation.txt
        public void SaveStudentRegistration(string studentID, string roomNumber)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("StudentInformation.txt", true))
                {
                    sw.WriteLine($"{studentID},{roomNumber}"); // Ghi thông tin sinh viên và phòng vào file
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lưu thông tin đăng ký sinh viên: " + ex.Message);
            }
        }

        // Lưu thông tin yêu cầu đăng ký vào file Notification.txt
        public void SaveRoomRegistrationNotification(string studentID, string roomNumber)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("Notification.txt", true))
                {
                    sw.WriteLine($"Tiêu đề: Đăng ký phòng");
                    sw.WriteLine($"Sinh viên: {studentID}");
                    sw.WriteLine($"Phòng đăng ký: {roomNumber}");
                    sw.WriteLine("--------------------------------------------------------");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi ghi vào file Notification.txt: " + ex.Message);
            }
        }

        // Yêu cầu chuyển phòng
        public void RequestRoomTransfer(string studentID, string currentRoom, string newRoom, string reason)
        {
            if (!IsStudentExist(studentID))
            {
                Console.WriteLine("Mã sinh viên không hợp lệ.");
                return;
            }

            // Check if student is already registered for the current room
            if (!IsStudentRegistered(studentID))
            {
                Console.WriteLine("Sinh viên chưa đăng ký phòng nào.");
                return;
            }

            if (!IsRoomAvailable(newRoom))
            {
                Console.WriteLine("Phòng mới không khả dụng.");
                return;
            }

            // Handle room transfer logic (update files, notify, etc.)
            Console.WriteLine("Đang xử lý yêu cầu chuyển phòng...");
            // Example notification logic
            SaveRoomTransferNotification(studentID, currentRoom, newRoom, reason);
        }

        // Lưu thông tin yêu cầu chuyển phòng vào file Notification.txt
        public void SaveRoomTransferNotification(string studentID, string currentRoom, string newRoom, string reason)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("Notification.txt", true))
                {
                    sw.WriteLine($"Tiêu đề: Yêu cầu chuyển phòng");
                    sw.WriteLine($"Sinh viên: {studentID}");
                    sw.WriteLine($"Phòng hiện tại: {currentRoom}");
                    sw.WriteLine($"Phòng mới: {newRoom}");
                    sw.WriteLine($"Lý do: {reason}");
                    sw.WriteLine("--------------------------------------------------------");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi ghi vào file Notification.txt: " + ex.Message);
            }
        }
    }

    public static class RoomRegistrationMenu
    {
        public static void ShowRoomRegistrationMenu(RoomOperations roomOperations)
        {
            // Đảm bảo không khởi tạo lại roomOperations
            var roomRegistration = new RoomRegistration(roomOperations); // Truyền vào RoomOperations
           
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Menu Đăng Ký Phòng:");
                Console.WriteLine("1. Hiển thị danh sách phòng");
                Console.WriteLine("2. Đăng ký phòng");
                Console.WriteLine("3. Yêu cầu chuyển phòng");
                Console.WriteLine("4. Quay lại");
                Console.Write("Chọn chức năng: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng nhập số từ 1 đến 4.");
                    Console.ReadKey();
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        roomOperations.DisplayRoomList(); // Hiển thị danh sách phòng
                        Console.ReadKey();
                        break;

                    case 2:
                        // Đăng ký phòng
                        Console.Write("Nhập mã sinh viên: ");
                        string studentID = Console.ReadLine();

                        // Kiểm tra mã sinh viên có hợp lệ không
                        //if (!roomRegistration.IsStudentExist(studentID))
                        //{
                        //    Console.WriteLine("Mã sinh viên không hợp lệ.");
                        //    break;
                        //}

                        //// Kiểm tra sinh viên đã đăng ký phòng chưa
                        if (roomRegistration.IsStudentRegistered(studentID))
                        {
                            Console.WriteLine("Sinh viên đã đăng ký phòng rồi. Không thể đăng ký lại.");
                            break;
                        }

                        Console.Write("Nhập số phòng muốn đăng ký: ");
                        string roomNumber = Console.ReadLine();
                        roomRegistration.RegisterRoom(studentID, roomNumber); // Đăng ký phòng
                        break;

                    case 3:
                        // Yêu cầu chuyển phòng
                        Console.Write("Nhập mã sinh viên: ");
                        studentID = Console.ReadLine();
                        Console.Write("Nhập số phòng hiện tại: ");
                        string currentRoom = Console.ReadLine();
                        Console.Write("Nhập số phòng muốn chuyển: ");
                        string newRoom = Console.ReadLine();
                        Console.Write("Nhập lý do chuyển phòng: ");
                        string reason = Console.ReadLine();
                        roomRegistration.RequestRoomTransfer(studentID, currentRoom, newRoom, reason); // Yêu cầu chuyển phòng
                        break;

                    case 4:
                        return; // Quay lại menu trước
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ.");
                        break;
                }
            }
        }
    }
}