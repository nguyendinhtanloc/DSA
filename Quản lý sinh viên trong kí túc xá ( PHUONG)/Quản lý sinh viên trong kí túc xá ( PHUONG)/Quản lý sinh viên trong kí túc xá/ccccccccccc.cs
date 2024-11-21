using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn
{
    internal class DieuHuong
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            mainMenu();
        }

        static void mainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== QUẢN LÝ SINH VIÊN TRONG KÝ TÚC XÁ ====");
                Console.WriteLine("[1] Sinh Viên");
                Console.WriteLine("[2] Quản Lý");
                Console.WriteLine("[3] Thoát");
                Console.Write("Chọn chức năng: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        studentMenu();
                        break;
                    case "2":
                        adminMenu();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng nhập phím bất kỳ để thử lại.");
                        Console.ReadKey();
                        break;
                }
            }
        }
        //====Menu Sinh viên====
        static void studentMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== MENU SINH VIÊN ====");
                Console.WriteLine("[1] Đăng Nhập");
                Console.WriteLine("[2] Đăng Ký Phòng");
                Console.WriteLine("[3] Quay Lại");
                Console.Write("Chọn chức năng: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        loginStudent();
                        break;
                    case "2":
                        registerRoom();
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

        static void loginStudent()
        {
            Console.Clear();
            Console.WriteLine("==== ĐĂNG NHẬP SINH VIÊN ====");
            Console.WriteLine("[1] Nhập tài khoản mật khẩu");
            Console.WriteLine("[2] Quay lại");
            Console.Write("Chọn chức năng: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    // Thêm logic đăng nhập tại đây
                    break;
                case "2":
                    return;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ! Nhấn phím bất kỳ để thử lại.");
                    Console.ReadKey();
                    break;
            }
        }

        static void registerRoom()
        {
            Console.Clear();
            Console.WriteLine("==== ĐĂNG KÝ PHÒNG ====");
            // Thêm logic đăng ký phòng tại đây
            Console.WriteLine("Chức năng chưa được triển khai.");
            Console.ReadKey();
        }

        //=== MENU ADMIN ===
        static void adminMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== MENU QUẢN LÝ ====");
                Console.WriteLine("[1] Quản Lý Kỷ Luật");
                Console.WriteLine("[2] Quản Lý Thông Báo");
                Console.WriteLine("[3] Quản Lý Hóa Đơn");
                Console.WriteLine("[4] Quản Lý Phòng");
                Console.WriteLine("[5] Thông Tin Cá Nhân");
                Console.WriteLine("[6] Quay Lại");
                Console.Write("Chọn chức năng: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        disciplineManagement();
                        break;
                    case "2":
                        notificationManagement();
                        break;
                    case "3":
                        invoiceManagement();
                        break;
                    case "4":
                        roomManagement();
                        break;
                    case "5":
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ! Nhấn phím bất kỳ để thử lại.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void disciplineManagement()
        {
            Console.Clear();
            Console.WriteLine("==== QUẢN LÝ KỶ LUẬT ====");
            Console.WriteLine("[1] Danh Sách Sinh Viên Kỷ Luật");
            Console.WriteLine("[2] Thêm Kỷ Luật");
            Console.WriteLine("[3] Quay Lại");
            Console.Write("Chọn chức năng: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    // Thêm logic 
                    Console.WriteLine("Hiển thị danh sách sinh viên bị kỷ luật.");
                    Console.ReadKey();
                    break;
                case "2":
                    //Thêm logic 
                    Console.WriteLine("Thêm sinh viên bị kỷ luật.");
                    Console.ReadKey();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ! Nhấn phím bất kỳ để thử lại.");
                    Console.ReadKey();
                    break;
            }
        }

        static void notificationManagement()
        {
            Console.Clear();
            Console.WriteLine("==== QUẢN LÝ THÔNG BÁO ====");
            Console.WriteLine("[1] Kiểm Tra Thông Báo");
            Console.WriteLine("[2] Gửi Thông Báo");
            Console.WriteLine("[3] Quay Lại");
            Console.Write("Chọn chức năng: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    //Thêm logic
                    Console.WriteLine("Kiểm tra thông báo là sao???");
                    Console.ReadKey();
                    break;
                case "2":
                    Console.WriteLine("Gửi thông báo cho các sinh viên");
                    Console.ReadKey();
                    //Thêm logic
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ! Nhấn phím bất kỳ để thử lại.");
                    Console.ReadKey();
                    break;
            }
        }

        static void invoiceManagement()
        {
            Console.Clear();
            Console.WriteLine("==== DANH SÁCH HÓA ĐƠN ====");
            Console.WriteLine("[1] Hóa Đơn Sinh Hoạt");
            Console.WriteLine("[2] Hóa Đơn Tiền Ở");
            Console.WriteLine("[3] Quay Lại");
            Console.Write("Chọn chức năng: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Hiển thị hóa đơn sinh hoạt.");
                    Console.ReadKey();
                    break;
                case "2":
                    Console.WriteLine("Hiển thị hóa đơn tiền ở.");
                    Console.ReadKey();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ! Nhấn phím bất kỳ để thử lại.");
                    Console.ReadKey();
                    break;
            }
        }

        static void roomManagement()
        {
            Console.Clear();
            Console.WriteLine("==== QUẢN LÝ PHÒNG ====");
            Console.WriteLine("[1] Thông Tin Phòng");
            Console.WriteLine("[2] Danh Sách Phòng");
            Console.WriteLine("[3] Chỉnh Sửa Phòng");
            Console.WriteLine("[4] Quay Lại");
            Console.Write("Chọn chức năng: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Hiển thị thông tin của phòng.");
                    Console.ReadKey();
                    break;
                case "2":
                    Console.WriteLine("Danh sách phòng.");
                    Console.ReadKey();
                    break;
                case "3":
                    Console.WriteLine("Thêm logic chỉnh sửa phòng.");
                    Console.ReadKey();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ! Nhấn phím bất kỳ để thử lại.");
                    Console.ReadKey();
                    break;
            }
        }
    }
}
