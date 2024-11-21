using System;
using AccountManagement;

namespace MainPage
{
    public class MainPage
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Khởi tạo file và tài khoản admin
            AccountManager.InitializeAdminAccount();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=============================================");
                Console.WriteLine("                Menu Chính                  ");
                Console.WriteLine("=============================================");

                // Hiển thị các lựa chọn menu với căn chỉnh
                Console.WriteLine("| {0,-12} | {1,-25} |", "1.", "Đăng nhập");
                Console.WriteLine("| {0,-12} | {1,-25} |", "2.", "Đăng ký tài khoản");
                Console.WriteLine("| {0,-12} | {1,-25} |", "3.", "Thoát");
                Console.WriteLine("=============================================");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng nhập số từ 1 đến 3.");
                    Console.ReadKey();
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AccountManager.Login();
                        break;
                    case 2:
                        AccountManager.Register();
                        break;
                    case 3:
                        if (AccountManager.ConfirmExit())
                        {
                            return;
                        }
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ.");
                        break;
                }
            }
        }
    }
}