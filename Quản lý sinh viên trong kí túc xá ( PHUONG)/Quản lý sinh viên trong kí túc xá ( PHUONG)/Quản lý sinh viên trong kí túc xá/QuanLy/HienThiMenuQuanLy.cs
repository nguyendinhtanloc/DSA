using System;
using RoomManagement;
using DormitoryManagementSystem;
using BillManagement;
using System.IO;
namespace DisplayMenuManagement
{
    public class Management
    {
        //private static string[] args;

        public static void MenuManagement()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Menu quản lý:");
                Console.WriteLine("1. Kiểm thông báo");
                Console.WriteLine("2. Thao tác phòng");
                Console.WriteLine("3. Kỷ luật");
                Console.WriteLine("4. Hoa DOn");
                Console.WriteLine("5. Đăng xuất");
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
                        Console.WriteLine("Kiểm thông báo...");
                        ManagerNotificationMenu.ShowManagerNotificationMenu();
                        Console.ReadKey();
                        break;
                    case 2:
                        RoomOperationsMenu.RoomManagementMenu(); // Gọi phương thức hiển thị menu quản lý phòng
                        break;
                    case 3:
                        Console.WriteLine("Kỷ luật...");
                        // Logic kỷ luật
                        Console.ReadKey();
                        break;
                    case 4:
                        string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BillStorage");
                        BillManagementUI ui = new BillManagementUI(basePath);
                        ui.ShowMainMenu();
                        break;
                    case 5:
                        return; // Đăng xuất
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ.");
                        break;
                }
            }
        }
    }
}