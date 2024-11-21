using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace RoomManagement
{
    // Lớp Room để đại diện cho thông tin phòng
    public class Room
    {
        public string RoomNumber { get; set; }
        public int TotalBeds { get; set; }
        public int AvailableBeds { get; set; }
        public string Status { get; set; }

        // Constructor để khởi tạo phòng với thông tin ban đầu
        public Room(string roomNumber, int totalBeds, int availableBeds, string status)
        {
            RoomNumber = roomNumber;
            TotalBeds = totalBeds;
            AvailableBeds = availableBeds;
            Status = status;
        }

        // Cập nhật thông tin phòng
        public void UpdateRoomInfo(int totalBeds, int availableBeds, string status)
        {
            TotalBeds = totalBeds;
            AvailableBeds = availableBeds;
            Status = status;
        }

        // Tính số giường đã sử dụng
        public int UsedBeds()
        {
            return TotalBeds - AvailableBeds;
        }
    }

    // Lớp quản lý các thao tác liên quan đến phòng
    public class RoomOperations
    {

        public List<Room> roomList = new List<Room>();  // Danh sách các phòng
        public const string roomFile = "Room.txt";  // File lưu trữ thông tin phòng
        public const string studentRoomFile = "StudentInfo.txt";
        string filePath = "studentInformation.txt";

        // Nạp danh sách phòng từ file Room.txt
        public void LoadRoomsFromFile()
        {
            try
            {
                if (File.Exists(roomFile))
                {
                    var lines = File.ReadLines(roomFile);
                    foreach (var line in lines)
                    {
                        var parts = line.Split(',');
                        if (parts.Length == 4)
                        {
                            string roomNumber = parts[0];
                            int totalBeds = int.Parse(parts[1]);
                            int availableBeds = int.Parse(parts[2]);
                            string status = parts[3];

                            // Tạo phòng và thêm vào danh sách
                            Room room = new Room(roomNumber, totalBeds, availableBeds, status);
                            roomList.Add(room);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi đọc phòng từ file: " + ex.Message);
            }
        }
        public void BinarySearchStudentById(string studentId)
        {
            string studentRoomFile = "StudentInformation.txt";  // Đảm bảo rằng bạn có đường dẫn đúng đến file

            if (!File.Exists(studentRoomFile))
            {
                Console.WriteLine("Không có dữ liệu sinh viên.");
                return;
            }

            // Đọc danh sách sinh viên từ file và sắp xếp theo mã sinh viên
            var studentList = File.ReadLines(studentRoomFile)
                                  .Select(line => line.Split(','))
                                  .Where(parts => parts.Length == 7)  // Chắc chắn số trường của mỗi sinh viên là 7 (hoặc số cần thiết)
                                  .OrderBy(parts => parts[1])  // Sắp xếp theo mã sinh viên (thường sẽ ở cột 1)
                                  .ToList();

            // Tìm kiếm nhị phân trong danh sách sinh viên
            int left = 0;
            int right = studentList.Count - 1;
            bool found = false;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                var student = studentList[mid];

                int comparison = string.Compare(student[1], studentId, StringComparison.OrdinalIgnoreCase); // So sánh mã sinh viên
                if (comparison == 0) // Tìm thấy sinh viên
                {
                    Console.WriteLine("Thông tin sinh viên:");
                    Console.WriteLine("-------------------------------------------------------------------");
                    Console.WriteLine("| Mã số sinh viên       | Tên sinh viên           | Số phòng       |");
                    Console.WriteLine("-------------------------------------------------------------------");
                    Console.WriteLine($"| {student[1],-20} | {student[2],-25} | Lớp: {student[6],-6}   |");
                    Console.WriteLine("-------------------------------------------------------------------");
                    Console.WriteLine($"Email: {student[0]}");
                    Console.WriteLine($"Địa chỉ: {student[3]}");
                    Console.WriteLine($"Số điện thoại: {student[4]}");
                    Console.WriteLine($"Khoa: {student[5]}");
                    found = true;
                    break;
                }
                else if (comparison < 0)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            if (!found)
            {
                Console.WriteLine("Không tìm thấy sinh viên với mã số này.");
            }
        }




        public void AddStudentToRoom(string roomNumber, string studentName, string studentId)
        {
            Room room = roomList.FirstOrDefault(r => r.RoomNumber == roomNumber);
            if (room == null)
            {
                Console.WriteLine("Phòng không tồn tại.");
                return;
            }

            if (room.AvailableBeds <= 0)
            {
                Console.WriteLine("Phòng đã hết giường trống.");
                return;
            }

            // Đếm số sinh viên đã có trong file để xác định số thứ tự
            int studentCount = 1;
            if (File.Exists(studentRoomFile))
            {
                studentCount = File.ReadLines(studentRoomFile).Count() + 1;
            }

            // Ghi thông tin sinh viên vào file StudentInfo.txt
            try
            {
                using (StreamWriter sw = new StreamWriter(studentRoomFile, true))
                {
                    sw.WriteLine($"{studentCount},{roomNumber},{studentName},{studentId}");
                }
                room.AvailableBeds--; // Giảm số giường trống của phòng
                SaveAllRoomsToFile();  // Cập nhật lại file Room.txt
                Console.WriteLine("Đã thêm sinh viên vào phòng thành công.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm sinh viên vào phòng: " + ex.Message);
            }
        }
        public void DisplayStudentList()
        {
            if (!File.Exists(studentRoomFile))
            {
                Console.WriteLine("Không có dữ liệu sinh viên.");
                return;
            }

            Console.WriteLine("Danh sách sinh viên trong các phòng:");
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine("| Số phòng | Tên sinh viên                  | Mã số sinh viên       |");
            Console.WriteLine("---------------------------------------------------------------------");

            // Đọc từng dòng trong file và hiển thị
            foreach (var line in File.ReadLines(studentRoomFile))
            {
                var parts = line.Split(',');
                if (parts.Length == 4)
                {
                    Console.WriteLine($"| {parts[1],-9} | {parts[2],-15}               | {parts[3],-15}       |");
                }
            }

            Console.WriteLine("---------------------------------------------------------------------");
        }


        public void TransferStudent(string studentId, string newRoomNumber)
        {
            Room newRoom = roomList.FirstOrDefault(r => r.RoomNumber == newRoomNumber);
            if (newRoom == null)
            {
                Console.WriteLine("Phòng mới không tồn tại.");
                return;
            }

            if (newRoom.AvailableBeds <= 0)
            {
                Console.WriteLine("Phòng mới đã hết giường trống.");
                return;
            }

            if (!File.Exists(studentRoomFile))
            {
                Console.WriteLine("Không có dữ liệu sinh viên.");
                return;
            }

            var lines = File.ReadAllLines(studentRoomFile).ToList();
            bool studentFound = false;
            string oldRoomNumber = "";

            for (int i = 0; i < lines.Count; i++)
            {
                var parts = lines[i].Split(',');
                if (parts.Length == 4 && parts[3] == studentId)
                {
                    oldRoomNumber = parts[1];
                    parts[1] = newRoomNumber;
                    lines[i] = string.Join(",", parts);
                    studentFound = true;
                    break;
                }
            }

            if (studentFound)
            {
                File.WriteAllLines(studentRoomFile, lines);

                // Cập nhật số giường trống của phòng cũ và phòng mới
                Room oldRoom = roomList.FirstOrDefault(r => r.RoomNumber == oldRoomNumber);
                if (oldRoom != null) oldRoom.AvailableBeds++;
                newRoom.AvailableBeds--;

                SaveAllRoomsToFile();  // Cập nhật lại file Room.txt
                Console.WriteLine("Đã chuyển phòng cho sinh viên thành công.");
            }
            else
            {
                Console.WriteLine("Không tìm thấy sinh viên với mã số này.");
            }
        }
        public void RemoveStudentFromRoom(string studentId)
        {
            if (!File.Exists(studentRoomFile))
            {
                Console.WriteLine("Không có dữ liệu sinh viên.");
                return;
            }

            var lines = File.ReadAllLines(studentRoomFile).ToList();
            bool studentFound = false;
            string roomNumber = "";

            for (int i = 0; i < lines.Count; i++)
            {
                var parts = lines[i].Split(',');
                if (parts.Length == 4 && parts[3] == studentId)
                {
                    roomNumber = parts[1];
                    lines.RemoveAt(i);
                    studentFound = true;
                    break;
                }
            }

            if (studentFound)
            {
                File.WriteAllLines(studentRoomFile, lines);
                Room room = roomList.FirstOrDefault(r => r.RoomNumber == roomNumber);
                if (room != null)
                {
                    room.AvailableBeds++; // Tăng số giường trống của phòng
                    SaveAllRoomsToFile();  // Cập nhật lại file Room.txt
                }
                Console.WriteLine("Đã xóa sinh viên khỏi phòng thành công.");
            }
            else
            {
                Console.WriteLine("Không tìm thấy sinh viên với mã số này.");
            }
        }
        public void AddRoom(Room room)
        {
            // Kiểm tra nếu phòng đã tồn tại
            if (IsRoomExist(room.RoomNumber))
            {
                Console.WriteLine("Số phòng đã tồn tại. Vui lòng chọn số phòng khác.");
            }
            else
            {
                roomList.Add(room);
                SaveRoomToFile(room);  // Lưu phòng vào file ngay sau khi thêm
                Console.WriteLine("Đã thêm phòng thành công.");
            }
        }


        // Lưu thông tin phòng vào file Room.txt
        public void SaveRoomToFile(Room room)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(roomFile, true))
                {
                    // Lưu thông tin phòng dưới dạng chuỗi phân cách bởi dấu phẩy
                    sw.WriteLine($"{room.RoomNumber},{room.TotalBeds},{room.AvailableBeds},{room.Status}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lưu phòng vào file: " + ex.Message);
            }
        }

        // Lưu lại tất cả các phòng vào file (sau khi cập nhật hoặc xóa phòng)
        public void SaveAllRoomsToFile()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(roomFile, false)) // Ghi đè lên file cũ
                {
                    foreach (var room in roomList)
                    {
                        sw.WriteLine($"{room.RoomNumber},{room.TotalBeds},{room.AvailableBeds},{room.Status}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lưu danh sách phòng vào file: " + ex.Message);
            }
        }
        public void FindRoom(string roomCode)
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
                    Console.WriteLine("Thông tin phòng tìm thấy:");
                    Console.WriteLine($"Mã phòng: {parts[0]}");
                    Console.WriteLine($"Tổng số giường: {parts[1]}");
                    Console.WriteLine($"Giường trống: {parts[2]}");
                    Console.WriteLine($"Trạng thái: {parts[3]}");

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

        // Hiển thị danh sách phòng
        public void DisplayRoomList()
        {
            if (roomList.Count == 0)
            {
                Console.WriteLine("Chưa có phòng nào được tạo.");
                return;
            }

            Console.WriteLine("-------------------------------------------------------------------------------------------------");
            Console.WriteLine("| Số phòng      | Tổng số giường | Giường trống  | Giường đã sử dụng |Tháng/Tiền phòng/Trạng Thái|  ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------");

            int index = 1;
            foreach (var room in roomList.OrderBy(r => r.AvailableBeds))
            {
                Console.WriteLine($"| {room.RoomNumber,-11}   | {room.TotalBeds,-15} | {room.AvailableBeds,-12} | {room.UsedBeds(),-17} | {room.Status,-18} ");
                index++;
            }

            Console.WriteLine("-------------------------------------------------------------------------------------------------");
        }

        // Kiểm tra xem phòng đã tồn tại hay chưa
        public bool IsRoomExist(string roomNumber)
        {
            return roomList.Any(r => r.RoomNumber == roomNumber);
        }

        // Cập nhật thông tin phòng
        public void UpdateRoom(string roomNumber, int totalBeds, int availableBeds, string status)
        {
            Room room = roomList.FirstOrDefault(r => r.RoomNumber == roomNumber);
            if (room != null)
            {
                room.UpdateRoomInfo(totalBeds, availableBeds, status);
                SaveAllRoomsToFile();  // Cập nhật lại file sau khi thay đổi thông tin phòng
                Console.WriteLine("Đã cập nhật thông tin phòng.");
            }
            else
            {
                Console.WriteLine("Phòng không tồn tại.");
            }
        }

        // Xóa phòng khỏi danh sách và file
        public void RemoveRoom(string roomNumber)
        {
            Room roomToRemove = roomList.FirstOrDefault(r => r.RoomNumber == roomNumber);
            if (roomToRemove != null)
            {
                roomList.Remove(roomToRemove);
                SaveAllRoomsToFile();  // Cập nhật lại file sau khi xóa phòng
                Console.WriteLine("Đã xóa phòng thành công.");
            }
            else
            {
                Console.WriteLine("Không tìm thấy phòng để xóa.");
            }
        }

        public static void editStudentInformation()
        {
            string filePath = "StudentInformation.txt";
            string filePath2 = "Notification.txt";

            try
            {
                var lines = File.ReadAllLines(filePath2);

                Console.WriteLine("==== BẢNG THÔNG TIN THAY ĐỔI ====");
                Console.WriteLine("| {0,-10} | {1,-20} | {2,-15} | {3,-12} | {4,-10} | {5,-10} |",
                                  "Mã SV", "Tên Mới", "Quê Mới", "SĐT Mới", "Khoa Mới", "Lớp Mới");
                Console.WriteLine("---------------------------------------------------------------------------");

                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    string studentId = parts[0];
                    string newName = string.IsNullOrEmpty(parts[1]) ? "Không đổi" : parts[1];
                    string newHometown = string.IsNullOrEmpty(parts[2]) ? "Không đổi" : parts[2];
                    string newPhone = string.IsNullOrEmpty(parts[3]) ? "Không đổi" : parts[3];
                    string newDepartment = string.IsNullOrEmpty(parts[4]) ? "Không đổi" : parts[4];
                    string newClass = string.IsNullOrEmpty(parts[5]) ? "Không đổi" : parts[5];

                    Console.WriteLine("| {0,-10} | {1,-20} | {2,-15} | {3,-12} | {4,-10} | {5,-10} |",
                                      studentId, newName, newHometown, newPhone, newDepartment, newClass);
                }

                Console.WriteLine("---------------------------------------------------------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi đọc file: " + ex.Message);
            }

            Console.Write("Nhập mã số sinh viên cần sửa: ");
            string studentIdToEdit = Console.ReadLine();

            Console.WriteLine("Nhập các thông tin cần sửa (để trống nếu không muốn thay đổi):");
            Console.Write("Họ và tên: ");
            string fullName = Console.ReadLine();
            Console.Write("Quê quán: ");
            string hometown = Console.ReadLine();
            Console.Write("Số điện thoại: ");
            string phoneNumber = Console.ReadLine();
            Console.Write("Khoa: ");
            string faculty = Console.ReadLine();
            Console.Write("Lớp: ");
            string className = Console.ReadLine();

            Dictionary<string, string> updatedInfo = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(fullName)) updatedInfo["FullName"] = fullName;
            if (!string.IsNullOrEmpty(hometown)) updatedInfo["Hometown"] = hometown;
            if (!string.IsNullOrEmpty(phoneNumber)) updatedInfo["PhoneNumber"] = phoneNumber;
            if (!string.IsNullOrEmpty(faculty)) updatedInfo["Faculty"] = faculty;
            if (!string.IsNullOrEmpty(className)) updatedInfo["Class"] = className;

            bool result = EditStudentInfo(filePath, studentIdToEdit, updatedInfo);

            if (result)
            {
                Console.WriteLine("Sửa thông tin thành công!");

                // Xóa dòng chứa mã sinh viên khỏi Notification.txt
                var notificationLines = File.ReadAllLines(filePath2).ToList();
                notificationLines.RemoveAll(line => line.StartsWith(studentIdToEdit + ","));
                File.WriteAllLines(filePath2, notificationLines);

                Console.WriteLine("Dòng thông tin thay đổi của sinh viên đã được xóa khỏi file Notification.txt.");
            }
            else
            {
                Console.WriteLine("Không tìm thấy sinh viên cần sửa.");
            }
        }

        static bool EditStudentInfo(string filePath, string studentId, Dictionary<string, string> updatedInfo)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("File không tồn tại.");
                    return false;
                }

                var lines = File.ReadAllLines(filePath).ToList();
                bool studentFound = false;

                for (int i = 0; i < lines.Count; i++)
                {
                    var fields = lines[i].Split(',');

                    if (fields.Length > 1 && fields[1] == studentId)
                    {
                        studentFound = true;

                        if (updatedInfo.ContainsKey("FullName")) fields[2] = updatedInfo["FullName"];
                        if (updatedInfo.ContainsKey("Hometown")) fields[3] = updatedInfo["Hometown"];
                        if (updatedInfo.ContainsKey("PhoneNumber")) fields[4] = updatedInfo["PhoneNumber"];
                        if (updatedInfo.ContainsKey("Faculty")) fields[5] = updatedInfo["Faculty"];
                        if (updatedInfo.ContainsKey("Class")) fields[6] = updatedInfo["Class"];

                        lines[i] = string.Join(",", fields);
                        break;
                    }
                }

                if (studentFound)
                {
                    File.WriteAllLines(filePath, lines);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
                return false;
            }
        }
    }




        // Lớp Menu quản lý phòng
        public static class RoomOperationsMenu
    {
        public static void RoomManagementMenu()
        {
            RoomOperations roomOperations = new RoomOperations();
            roomOperations.LoadRoomsFromFile();// Nạp phòng từ file khi bắt đầu

            while (true)
            {
                Console.Clear(); // Xóa màn hình
                Console.WriteLine("=====================================");
                Console.WriteLine("    Menu Quản Lý Phòng Ký Túc Xá   ");
                Console.WriteLine("=====================================");
                Console.WriteLine("| {0,-3} | {1,-30} |", "STT", "Chức Năng");
                Console.WriteLine("|-----|--------------------------------|");
                Console.WriteLine("| {0,-3} | {1,-30} |", "1", "Thêm phòng");
                Console.WriteLine("| {0,-3} | {1,-30} |", "2", "Xóa phòng");
                Console.WriteLine("| {0,-3} | {1,-30} |", "3", "Cập nhật thông tin phòng");
                Console.WriteLine("| {0,-3} | {1,-30} |", "4", "Hiển thị danh sách phòng");
                Console.WriteLine("| {0,-3} | {1,-30} |", "5", "Thêm sinh viên vào phòng");
                Console.WriteLine("| {0,-3} | {1,-30} |", "6", "Hiển thị sinh viên trong phòng");
                Console.WriteLine("| {0,-3} | {1,-30} |", "7", "Chuyển phòng cho sinh viên");
                Console.WriteLine("| {0,-3} | {1,-30} |", "8", "Xóa sinh viên");
                Console.WriteLine("| {0,-3} | {1,-30} |", "9", "Tìm kiếm sinh viên");
                Console.WriteLine("| {0,-3} | {1,-30} |", "10", "Quay lại");
                Console.WriteLine("=====================================");
                Console.Write("Chọn chức năng: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng nhập số từ 1 đến 5.");
                    Console.ReadKey();
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        // Thêm phòng
                        Console.Write("Nhập số lượng phòng muốn thêm: ");
                        int roomCount;
                        while (!int.TryParse(Console.ReadLine(), out roomCount) || roomCount <= 0)
                        {
                            Console.WriteLine("Vui lòng nhập một số nguyên dương.");
                            Console.Write("Nhập số lượng phòng muốn thêm: ");
                        }

                        for (int i = 0; i < roomCount; i++)
                        {
                            Console.WriteLine($"\nNhập thông tin cho phòng thứ {i + 1}:");
                            Console.Write("Nhập số phòng: ");
                            string roomNumberAdd = Console.ReadLine();

                            Console.Write("Nhập tổng số giường: ");
                            int totalBedsAdd;
                            while (!int.TryParse(Console.ReadLine(), out totalBedsAdd) || totalBedsAdd <= 0)
                            {
                                Console.WriteLine("Vui lòng nhập số nguyên dương cho tổng số giường.");
                                Console.Write("Nhập tổng số giường: ");
                            }

                            Console.Write("Nhập giường trống: ");
                            int availableBedsAdd;
                            while (!int.TryParse(Console.ReadLine(), out availableBedsAdd) || availableBedsAdd < 0 || availableBedsAdd > totalBedsAdd)
                            {
                                Console.WriteLine("Số giường trống phải là số không âm và nhỏ hơn hoặc bằng tổng số giường.");
                                Console.Write("Nhập giường trống: ");
                            }

                            Console.Write("Trạng thái tiền phòng: ");
                            string statusAdd = Console.ReadLine();

                            Room roomAdd = new Room(roomNumberAdd, totalBedsAdd, availableBedsAdd, statusAdd);
                            roomOperations.AddRoom(roomAdd);
                        }
                        break;

                    case 2:
                        // Xóa phòng
                        Console.Write("Nhập số phòng cần xóa: ");
                        string roomNumberRemove = Console.ReadLine();
                        roomOperations.RemoveRoom(roomNumberRemove);  // Xóa phòng
                        break;

                    case 3:
                        // Cập nhật phòng
                        
                        Console.Write("Nhập số phòng cần cập nhật: ");
                        string roomNumberUpdate = Console.ReadLine();
                        
                        if (!roomOperations.IsRoomExist(roomNumberUpdate)) // Kiểm tra phòng tồn tại
                        {
                            Console.WriteLine("Số phòng không tồn tại.");
                            break;
                        }
                        roomOperations.FindRoom(roomNumberUpdate);
                        Console.Write("Đây là thông tin phòng lưu ý trước khi thay đổi: ");
                        Console.Write("Nhập tổng số giường: ");
                        int totalBedsUpdate = int.Parse(Console.ReadLine());
                        Console.Write("Nhập giường trống: ");
                        int availableBedsUpdate = int.Parse(Console.ReadLine());
                        Console.Write("Tiền phòng/Trạng thái thanh toán: ");
                        string statusUpdate = Console.ReadLine();

                        roomOperations.UpdateRoom(roomNumberUpdate, totalBedsUpdate, availableBedsUpdate, statusUpdate); // Cập nhật thông tin phòng
                        break;

                    case 4:
                        // Hiển thị danh sách phòng
                        roomOperations.DisplayRoomList();
                        break;

                    case 5:
                        Console.Write("Nhập số phòng: ");
                        string roomNumber = Console.ReadLine();
                        Console.Write("Nhập tên sinh viên: ");
                        string studentName = Console.ReadLine();
                        Console.Write("Nhập mã số sinh viên: ");
                        string studentId = Console.ReadLine();

                        roomOperations.AddStudentToRoom(roomNumber, studentName, studentId);
                        roomOperations.DisplayStudentList();
                        break; // Quay lại menu chính
                    case 6:
                        roomOperations.DisplayStudentList();
                        break;
                    
                    case 7:
                        // Chuyển phòng cho sinh viên
                        Console.Write("Nhập mã số sinh viên: ");
                        string studentIdTransfer = Console.ReadLine();
                        Console.Write("Nhập số phòng mới: ");
                        string newRoomNumber = Console.ReadLine();
                        roomOperations.TransferStudent(studentIdTransfer, newRoomNumber);
                        roomOperations.DisplayStudentList();
                        break;
                    case 8:
                        Console.Write("Nhập mã số sinh viên cần xóa: ");
                        string studentIdRemove = Console.ReadLine();
                        roomOperations.RemoveStudentFromRoom(studentIdRemove);
                        roomOperations.DisplayStudentList();
                        break;
                    case 9:
                        Console.Write("Nhập mã số sinh viên cần tìm: ");
                        string studentIdSearch = Console.ReadLine();
                        roomOperations.BinarySearchStudentById(studentIdSearch);
                        break;
                    
                    case 10:
                        return;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ.");
                        break;
                }

                Console.WriteLine("Nhấn phím bất kỳ để tiếp tục...");
                Console.ReadKey();
            }
        }
    }
}