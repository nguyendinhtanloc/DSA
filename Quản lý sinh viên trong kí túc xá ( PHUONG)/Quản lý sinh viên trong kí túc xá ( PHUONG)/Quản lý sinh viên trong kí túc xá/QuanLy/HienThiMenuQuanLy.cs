using System;
using RoomManagement;
using DormitoryManagementSystem;
using BillManagement;
using System.IO;
using DormitoryManagement;

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
                Console.WriteLine("==============================================");
                Console.WriteLine("          Menu Quản Lý Hệ Thống Quản Lý       ");
                Console.WriteLine("==============================================");
                Console.WriteLine("| {0,-3} | {1,-30}        |", "STT", "Chức Năng");
                Console.WriteLine("|-----|---------------------------------------|");
                Console.WriteLine("| {0,-3} | {1,-30}   |", "1", "Kiểm thông báo điểu chỉnh thông tin");
                Console.WriteLine("| {0,-3} | {1,-30}        |", "2", "Kiểm thông báo đăng kí phòng");
                Console.WriteLine("| {0,-3} | {1,-30}        |", "3", "Kiểm thông báo chuyển phòng");
                Console.WriteLine("| {0,-3} | {1,-30}        |", "4", "Kiểm thông báo rời ktx");
                Console.WriteLine("| {0,-3} | {1,-30}        |", "5", "Thao tác phòng và sinh viên");
                Console.WriteLine("| {0,-3} | {1,-30}        |", "6", "Kỷ luật");
                Console.WriteLine("| {0,-3} | {1,-30}        |", "7", "Đăng xuất");
                Console.WriteLine("==============================================");
                Console.Write("Chọn chức năng: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng nhập số từ 1 đến 7.");
                    Console.ReadKey();
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Kiểm thông báo điều chỉnh thông tin sinh viên");
                        RoomOperations.editStudentInformation();
                        break;
                    case 2:
                        Console.WriteLine("Kiểm thông báo đăng kí phòng");
                        //ManagerNotificationMenu.ShowManagerNotificationMenu();
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.WriteLine("Kiểm thông báo chuyển phòng");
                        //ManagerNotificationMenu.ShowManagerNotificationMenu();
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.WriteLine("Kiểm thông báo sinh viên rời ktx");
                        //ManagerNotificationMenu.ShowManagerNotificationMenu();
                        Console.ReadKey();
                        break;
                    case 6:
                        DisciplineManager.TransferDisciplineInformation();
                        DisciplineManagerApp.DisciplineManagerMenu();
                        Console.ReadKey();
                        break;
                    case 5:
                        RoomOperationsMenu.RoomManagementMenu(); // Gọi phương thức hiển thị menu quản lý phòng
                        break;
                    case 7:
                        return; // Đăng xuất
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ.");
                        break;
                }
            }
        }
    }
}