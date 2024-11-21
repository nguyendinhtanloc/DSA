using System;
using System.Collections.Generic;
using System.IO;

namespace DormitoryManagement
{
     class DisciplineManager
    {
        // Chuyển thông tin từ file StudentInfo.txt sang Discipline.txt
        public static void TransferDisciplineInformation()
        {
            string inputFile = "StudentInfo.txt";
            string outputFile = "Discipline.txt";

            try
            {
                // Kiểm tra nếu file đầu vào không tồn tại
                if (!File.Exists(inputFile))
                {
                    Console.WriteLine("File đầu vào không tồn tại!");
                    return;
                }

                // Mở file đầu vào và file đầu ra
                var lines = File.ReadAllLines(inputFile);

                // Mở file đầu ra và thêm dữ liệu vào
                using (StreamWriter writer = new StreamWriter(outputFile, false))
                {
                    // Ghi tiêu đề cột cho file mới
                    writer.WriteLine("Tên phòng,Mã số sinh viên,Tên sinh viên,Số lần kỷ luật");

                    // Đọc từng dòng trong file đầu vào
                    foreach (var line in lines)
                    {
                        // Tách các thông tin trong mỗi dòng
                        var parts = line.Split(',');

                        if (parts.Length >= 3)
                        {
                            string roomName = parts[1].Trim(); // Tên phòng
                            string studentName = parts[2].Trim(); // Tên sinh viên
                            string studentId = parts[3].Trim(); // Mã số sinh viên

                            // Tạo thông tin cho file đầu ra với số lần kỷ luật là 0 (có thể thay đổi sau)
                            string disciplineEntry = $"{roomName},{studentId},{studentName},0";

                            // Ghi dòng vào file Discipline.txt
                            writer.WriteLine(disciplineEntry);
                        }
                    }
                }

                Console.WriteLine("Chuyển thông tin thành công!");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        // Hàm hiển thị nội dung file Discipline.txt với định dạng đẹp
        public void DisplayDisciplineFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                string[] lines = File.ReadAllLines(fileName);
                Console.WriteLine("Danh sách vi phạm kỷ luật:");

                // In tiêu đề cột với căn chỉnh

                Console.WriteLine(new string('-', 60)); // Dòng phân cách

                // In từng dòng dữ liệu với căn chỉnh
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');

                    // Kiểm tra và in dữ liệu với căn chỉnh
                    if (parts.Length == 4)
                    {
                        Console.WriteLine(String.Format("{0,-10} {1,-20} {2,-15} {3,-15}",
                                parts[0], parts[1], parts[2], parts[3]));
                    }
                }
            }
            else
            {
                Console.WriteLine("File không tồn tại.");
            }
        }

        // Hàm thay đổi số lần vi phạm kỷ luật của sinh viên theo mã số sinh viên
        public void UpdateDisciplineViolation(string fileName, string studentID, int newViolationCount)
        {
            if (File.Exists(fileName))
            {
                string[] lines = File.ReadAllLines(fileName);
                bool studentFound = false;

                // List để lưu lại nội dung đã thay đổi
                List<string> updatedLines = new List<string>();

                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');

                    // Kiểm tra mã số sinh viên
                    if (parts.Length == 4 && parts[1].Trim() == studentID)
                    {
                        // Thay đổi số lần vi phạm kỷ luật
                        parts[3] = newViolationCount.ToString();
                        studentFound = true;
                    }

                    // Thêm dòng vào danh sách đã cập nhật
                    updatedLines.Add(string.Join(",", parts));
                }

                if (studentFound)
                {
                    // Ghi lại nội dung đã thay đổi vào file
                    File.WriteAllLines(fileName, updatedLines);
                    Console.WriteLine("Số lần vi phạm kỷ luật của sinh viên đã được cập nhật.");
                }
                else
                {
                    Console.WriteLine("Không tìm thấy sinh viên với mã số " + studentID);
                }
            }
            else
            {
                Console.WriteLine("File không tồn tại.");
            }
        }
    }

    // Lớp thực thi công việc quản lý kỷ luật của sinh viên
    public static class DisciplineManagerApp
    {
       public  static void DisciplineManagerMenu()
        {
            DisciplineManager manager = new DisciplineManager();
            string fileName = "Discipline.txt";

            while (true)
            {
                // Hiển thị menu cho người dùng
                Console.Clear(); // Xóa màn hình
        Console.WriteLine("==============================================");
        Console.WriteLine("          Menu Quản Lý Kỷ Luật               ");
        Console.WriteLine("==============================================");
        Console.WriteLine("| {0,-3} | {1,-35} |", "STT", "Chức Năng");
        Console.WriteLine("|-----|-------------------------------------|");
        Console.WriteLine("| {0,-3} | {1,-35} |", "1", "Hiển thị danh sách vi phạm kỷ luật");
        Console.WriteLine("| {0,-3} | {1,-35} |", "2", "Thay đổi số lần vi phạm kỷ luật   ");
        Console.WriteLine("| {0,-3} | {1,-35} |", "3", "Thoát");
        Console.WriteLine("==============================================");
        Console.Write("Chọn một tùy chọn (1-3): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        // Hiển thị nội dung file Discipline.txt
                        manager.DisplayDisciplineFile(fileName);
                        Console.WriteLine("\nNhấn phím bất kỳ để quay lại menu...");
                        Console.ReadKey();
                        break;

                    case "2":
                        // Thay đổi số lần vi phạm kỷ luật cho một sinh viên
                        Console.WriteLine("\nNhập mã số sinh viên cần thay đổi số lần vi phạm:");
                        string studentID = Console.ReadLine();

                        int newViolationCount = 0;
                        while (true)
                        {
                            Console.WriteLine("Nhập số lần vi phạm mới:");
                            bool isValid = int.TryParse(Console.ReadLine(), out newViolationCount);
                            if (isValid && newViolationCount >= 0)
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Số lần vi phạm không hợp lệ, vui lòng nhập lại.");
                            }
                        }

                        manager.UpdateDisciplineViolation(fileName, studentID, newViolationCount);

                        // Hiển thị lại nội dung file sau khi thay đổi
                        Console.WriteLine("\nNội dung file sau khi thay đổi:");
                        manager.DisplayDisciplineFile(fileName);
                        Console.WriteLine("\nNhấn phím bất kỳ để quay lại menu...");
                        Console.ReadKey();
                        break;

                    case "3":
                        // Thoát chương trình
                        Console.WriteLine("Thoát chương trình...");
                        return;

                    default:
                        // Thông báo nếu người dùng nhập lựa chọn không hợp lệ
                        Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng chọn lại.");
                        Console.WriteLine("\nNhấn phím bất kỳ để quay lại menu...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
