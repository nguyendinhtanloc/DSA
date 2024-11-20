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
                Console.WriteLine("Menu chính:");
                Console.WriteLine("1. Đăng nhập");
                Console.WriteLine("2. Đăng ký tài khoản");
                Console.WriteLine("3. Thoát");
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