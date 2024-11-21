using AccountManagement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace StudentInformation
{
    public class ManagementStudentInformation
    {
        public string StudentID;
        public string FullName;
        public string Hometown;
        public string PhoneNumber;
        public string Faculty;
        public string Class;

        // Constructor khởi tạo thông tin sinh viên
        public ManagementStudentInformation(string studentID, string fullName, string hometown, string phoneNumber, string faculty, string studentClass)
        {
            StudentID = studentID;
            FullName = fullName;
            Hometown = hometown;
            PhoneNumber = phoneNumber;
            Faculty = faculty;
            Class = studentClass;
        }

        // Phương thức lưu thông tin sinh viên vào tệp
        public static void SaveStudentInformation(string username, ManagementStudentInformation student)
        {
            try
            {
                string fileName = "StudentInformation.txt"; // Tệp lưu trữ thông tin sinh viên
                bool fileExists = File.Exists(fileName); // Kiểm tra xem tệp đã tồn tại chưa
                var studentList = ReadStudentInformation();

                // Kiểm tra MSSV trùng lặp
                if (studentList.Any(s => s.StudentID == student.StudentID && username != s.StudentID))
                {
                    Console.WriteLine("Mã số sinh viên này đã tồn tại. Không thể lưu thông tin.");
                    return;
                }


                // Mở tệp để ghi thông tin sinh viên vào
                using (StreamWriter sw = new StreamWriter(fileName, true))
                {
                    // Nếu tệp chưa tồn tại, thêm header
                    if (!fileExists)
                    {
                        sw.WriteLine("Username,StudentID,FullName,Hometown,PhoneNumber,Faculty,Class"); // Tiêu đề
                    }

                    // Ghi thông tin sinh viên vào tệp
                    sw.WriteLine($"{username},{student.StudentID},{student.FullName},{student.Hometown},{student.PhoneNumber},{student.Faculty},{student.Class}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lưu thông tin sinh viên: " + ex.Message); // Thông báo lỗi khi lưu
            }
        }

        // Phương thức đọc thông tin sinh viên từ tệp
        public static List<ManagementStudentInformation> ReadStudentInformation()
        {
            List<ManagementStudentInformation> studentList = new List<ManagementStudentInformation>();

            try
            {
                // Kiểm tra xem tệp có tồn tại không
                if (File.Exists("StudentInformation.txt"))
                {
                    string[] lines = File.ReadAllLines("StudentInformation.txt");
                    // Duyệt qua từng dòng dữ liệu trong tệp (bỏ qua dòng tiêu đề)
                    foreach (var line in lines.Skip(1))
                    {
                        string[] parts = line.Split(',');

                        // Kiểm tra nếu có đủ 7 phần (mỗi trường thông tin)
                        if (parts.Length == 7)
                        {
                            string username = parts[0];
                            string studentID = parts[1];
                            string fullName = parts[2];
                            string hometown = parts[3];
                            string phoneNumber = parts[4];
                            string faculty = parts[5];
                            string studentClass = parts[6];

                            // Tạo đối tượng sinh viên từ dữ liệu đã đọc
                            var student = new ManagementStudentInformation(studentID, fullName, hometown, phoneNumber, faculty, studentClass);
                            studentList.Add(student);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi đọc thông tin sinh viên: " + ex.Message); // Thông báo lỗi khi đọc
            }

            return studentList;
        }
        //Gửi yêu cầu đổi thông tin cá nhân
        public static void UpdateStudentInformation2(string username)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== GỬI YÊU CẦU CẬP NHẬT THÔNG TIN ===");
                Console.WriteLine("Vui lòng nhập mới ở các mục muốn thay đổi, mục không thai đổi nhập như ban đầu ");
                string currentUsername = UserSession.Username;         
                string studentId = currentUsername.Substring(0, 8);
                Console.Write("Nhập tên: ");
                string name = Console.ReadLine();
                Console.Write("Nhập quê quán: ");
                string hometown = Console.ReadLine();
                string phoneNumber;
                while (true)
                {
                    Console.Write("Nhập số điện thoại (10 chữ số): ");
                    phoneNumber = Console.ReadLine();

                    // Kiểm tra số ký tự và chỉ chứa số
                    if (phoneNumber.Length == 10 && phoneNumber.All(char.IsDigit))
                    {
                        break; // Thoát vòng lặp nếu hợp lệ
                    }

                    Console.WriteLine("Số điện thoại không hợp lệ! Vui lòng nhập lại.");
                }
                Console.Write("Nhập khoa: ");
                string department = Console.ReadLine();
                Console.Write("Nhập lớp: ");
                string className = Console.ReadLine();

                // Định dạng thông tin để lưu vào file
                string notification = $"{studentId},{name},{hometown},{phoneNumber},{department},{className}";

                // Lưu thông tin vào file Notification.txt
                string filePath = "Notification.txt";
                File.AppendAllText(filePath, notification + Environment.NewLine);

                Console.WriteLine("Yêu cầu cập nhật thông tin đã được gửi!");
                Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra lỗi: {ex.Message}");
            }
        }


        // Phương thức kiểm tra xem sinh viên đã có thông tin trong tệp hay chưa
        public static bool StudentHasInformation(string username)
        {
            try
            {
                var lines = File.ReadAllLines("StudentInformation.txt");
                // Kiểm tra xem sinh viên có thông tin trong file chưa
                return lines.Any(line => line.Split(',')[0] == username);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi kiểm tra thông tin sinh viên: " + ex.Message);
                return false;
            }
        }
        public static bool StudentHasInformation2(string username)
        {
            try
            {
                var lines = File.ReadAllLines("StudentInformation.txt");
                // Kiểm tra xem sinh viên có thông tin trong file chưa
                return lines.Any(line => line.Split(',')[1] == username);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi kiểm tra thông tin sinh viên: " + ex.Message);
                return false;
            }
        }

        // Phương thức cập nhật thông tin sinh viên

       // Phương thức cập nhật thông tin sinh viên
        public static void UpdateStudentInformation(string username)
        {

            // Nhập mã số sinh viên
            Console.Write("Nhập mã số sinh viên: ");
            string studentID = Console.ReadLine();

            //var studentList = ReadStudentInformation();
            //if (studentList.Any(s => s.StudentID == studentID && username != s.StudentID))
            //{
            //    Console.WriteLine("Mã số sinh viên này đã tồn tại. Vui lòng nhập mã khác.");
            //    return;
            //}

            // Kiểm tra và yêu cầu nhập lại nếu trường thông tin bị bỏ trống
            string fullName = "";
            while (string.IsNullOrEmpty(fullName))
            {
                Console.Write("Nhập họ tên: ");
                fullName = Console.ReadLine();
                if (string.IsNullOrEmpty(fullName))
                {
                    Console.WriteLine("Họ tên không được để trống. Vui lòng nhập lại.");
                }
            }

            string hometown = "";
            while (string.IsNullOrEmpty(hometown))
            {
                Console.Write("Nhập quê quán: ");
                hometown = Console.ReadLine();
                if (string.IsNullOrEmpty(hometown))
                {
                    Console.WriteLine("Quê quán không được để trống. Vui lòng nhập lại.");
                }
            }

            string phoneNumber = "";
            while (string.IsNullOrEmpty(phoneNumber) || !Regex.IsMatch(phoneNumber, @"^\d{10}$"))
            {
                Console.Write("Nhập số điện thoại (10 chữ số): ");
                phoneNumber = Console.ReadLine();
                if (string.IsNullOrEmpty(phoneNumber))
                {
                    Console.WriteLine("Số điện thoại không được để trống. Vui lòng nhập lại.");
                }
                else if (!Regex.IsMatch(phoneNumber, @"^\d{10}$"))
                {
                    Console.WriteLine("Số điện thoại không đúng định dạng. Vui lòng nhập lại (10 chữ số).");
                }
            }

            string faculty = "";
            while (string.IsNullOrEmpty(faculty))
            {
                Console.Write("Nhập khoa: ");
                faculty = Console.ReadLine();
                if (string.IsNullOrEmpty(faculty))
                {
                    Console.WriteLine("Khoa không được để trống. Vui lòng nhập lại.");
                }
            }

            string studentClass = "";
            while (string.IsNullOrEmpty(studentClass))
            {
                Console.Write("Nhập lớp: ");
                studentClass = Console.ReadLine();
                if (string.IsNullOrEmpty(studentClass))
                {
                    Console.WriteLine("Lớp không được để trống. Vui lòng nhập lại.");
                }
            }

            // Tạo đối tượng sinh viên mới từ thông tin đã nhập
            ManagementStudentInformation student = new ManagementStudentInformation(studentID, fullName, hometown, phoneNumber, faculty, studentClass);

            // Lưu thông tin sinh viên đã cập nhật vào tệp
            SaveStudentInformation(username, student);

            Console.WriteLine("Thông tin sinh viên đã được lưu thành công."); // Thông báo thành công
            Console.WriteLine("Nhấn phím bất kỳ để tiếp tục...");
            Console.ReadKey();
        }

        // Phương thức hiển thị thông tin sinh viên
        public static void ViewStudentInformation(string studentID)
        {
            Console.WriteLine("==== XEM THÔNG TIN SINH VIÊN ====");
            var studentList = ReadStudentInformation();
            var student = studentList.FirstOrDefault(s => s.StudentID == studentID);

            if (student != null)
            {
                Console.WriteLine($"Mã số sinh viên: {student.StudentID}");
                Console.WriteLine($"Họ tên: {student.FullName}");
                Console.WriteLine($"Quê quán: {student.Hometown}");
                Console.WriteLine($"Số điện thoại: {student.PhoneNumber}");
                Console.WriteLine($"Khoa: {student.Faculty}");
                Console.WriteLine($"Lớp: {student.Class}");
            }
            else
            {
                Console.WriteLine("Không tìm thấy thông tin sinh viên.");
            }

            Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
        }

        // Menu chính để chọn các chức năng về thông tin sinh viên
        public static void MenuInformationStudent(string username )
        {
            
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== THÔNG TIN SINH VIÊN ====");
                Console.WriteLine("[1] Xem thông tin cá nhân");
                Console.WriteLine("[2] Gửi yêu cầu cập nhật thông tin cá nhân");
                Console.WriteLine("[3] Quay lại");
                Console.Write("Chọn chức năng: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ViewStudentInformation(username);
                        break;
                    case "2":
                        if (StudentHasInformation2(username))
                        {
                            UpdateStudentInformation2(username);
                        }
                        else
                        {
                            Console.WriteLine("Không có thông tin để cập nhật.");
                            Console.ReadKey();
                        }
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ! Nhấn phím bất kỳ để thử lại.");
                        Console.ReadKey();
                        break;
                }
            }
        
        }
    }
}