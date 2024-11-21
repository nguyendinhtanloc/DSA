using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
// keekekek
using StudentInformation;
using DisplayStudentMenu;
using DisplayMenuManagement;

namespace AccountManagement
{
    public static class AccountManager
    {
        public static string accountFile = "Account.txt"; // File lưu trữ tài khoản

        // Initialize default admin account if file doesn't exist
        public static void InitializeAdminAccount()
        {
            if (!File.Exists(accountFile))
            {
                using (StreamWriter sw = new StreamWriter(accountFile, true))
                {
                    sw.WriteLine("admin,123"); // Default admin account
                }
            }
        }

        // Login method
        public static void Login()
        {
            while (true)
            {
                Console.WriteLine("=============================================");
                Console.WriteLine("          Đăng Nhập Hệ Thống                ");
                Console.WriteLine("=============================================");

                // Hiển thị yêu cầu nhập tài khoản và mật khẩu
                Console.Write("| {0,-12}|", "Tài khoản");     
                string username = Console.ReadLine();
                Console.Write("| {0,-12}|", "Mật khẩu:");
                string password = Console.ReadLine();

                if (CheckAccount(username, password))
                {
                    Console.WriteLine("Đăng nhập thành công!");
                    UserSession.Username = username;
                    // Nếu đăng nhập vào tài khoản admin
                    if (username == "admin" && password == "123")
                    {
                        // Gọi Menu quản lý
                        Management.MenuManagement();
                        break;
                    }

                    // Check if student has personal information
                    if (!ManagementStudentInformation.StudentHasInformation(username))
                    {
                        Console.WriteLine("Vui lòng hoàn thành thông tin sinh viên của bạn.");
                        ManagementStudentInformation.UpdateStudentInformation(username); // Prompt student to enter their information
                    }

                    // Show student menu
                    StudentMenu.ShowMenuStutdent();
                    break;
                }
                else
                {
                    Console.WriteLine("Sai tài khoản hoặc mật khẩu.");
                    Console.WriteLine("[1] Nhập lại");
                    Console.WriteLine("[2] Quay lại");
                    if (int.TryParse(Console.ReadLine(), out int choice) && choice == 2)
                    {
                        break;
                    }
                }
                
            }
            
        }
        

        // Check if the account exists and the password is correct
        public static bool CheckAccount(string username, string password)
        {
            try
            {
                var lines = File.ReadLines(accountFile);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    if (parts[0] == username && parts[1] == password)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi đọc file: " + ex.Message);
            }
            return false;
        }

        // Register a new student account
        public static void Register()
        {
            Console.Clear();
            Console.Write("Nhập tài khoản (định dạng @student.hcmute.edu.vn): ");
            string username = Console.ReadLine();
            Console.Write("Nhập mật khẩu: ");
            string password = Console.ReadLine();

            if (username.EndsWith("@student.hcmute.edu.vn"))
            {
                if (IsAccountExist(username))
                {
                    Console.WriteLine("Tài khoản đã tồn tại. Vui lòng chọn tài khoản khác.");
                }
                else
                {
                    SaveAccount(username, password);
                    Console.WriteLine("Đăng ký thành công!");
                }
            }
            else
            {
                Console.WriteLine("Tài khoản không đúng định dạng. Vui lòng đăng ký tài khoản sinh viên.");
            }

            Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
        }

        // Check if the account already exists
        public static bool IsAccountExist(string username)
        {
            try
            {
                return File.ReadLines(accountFile).Any(line => line.Split(',')[0] == username);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi đọc file: " + ex.Message);
            }
            return false;
        }

        // Save account to file
        public static void SaveAccount(string username, string password)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(accountFile, true))
                {
                    sw.WriteLine($"{username},{password}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lưu tài khoản: " + ex.Message);
            }
        }

        // Confirm exit action
        public static bool ConfirmExit()
        {
            Console.WriteLine("Bạn có chắc chắn muốn thoát? [Y/N]");
            string choice = Console.ReadLine().ToUpper();
            return choice == "Y";
        }
    }
    public static class UserSession
    {
        public static string Username { get; set; }

    }
}