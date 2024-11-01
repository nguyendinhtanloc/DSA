using System;
using System.Collections.Generic;
using System.Linq;

namespace CTDL
{
    public class Room
    {
        public string RoomNumber;
        public int TotalBeds;
        public int EmptyBeds;
        public string Status;

        public Room(string roomNumber, int totalBeds, int emptyBeds, string status)
        {
            RoomNumber = roomNumber;
            TotalBeds = totalBeds;
            EmptyBeds = emptyBeds;
            Status = status;
        }

        public void UpdateRoomInfo(int totalBeds, int emptyBeds, string status)
        {
            TotalBeds = totalBeds;
            EmptyBeds = emptyBeds;
            Status = status;
        }

        // Tinh toan so giuong da su dung
        public int OccupiedBeds()
        {
            return TotalBeds - EmptyBeds;
        }
    }

    public class RoomManager
    {
        private List<Room> rooms = new List<Room>();

        // Them phong
        public void AddRoom(Room room)
        {
            rooms.Add(room);
        }

        // Xoa phong
        public void RemoveRoom(string roomNumber)
        {
            rooms.RemoveAll(r => r.RoomNumber == roomNumber);
        }

        // Cap nhat thong tin phong
        public bool UpdateRoom(string roomNumber, int totalBeds, int emptyBeds, string status)
        {
            Room room = rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
            if (room != null)
            {
                room.UpdateRoomInfo(totalBeds, emptyBeds, status);
                return true;
            }
            return false;
        }

        // Kiem tra xem phong co ton tai khong
        public bool RoomExists(string roomNumber)
        {
            return rooms.Any(r => r.RoomNumber == roomNumber);
        }

        // Hien thi danh sach phong
        public void DisplayRooms()
        {
            Console.WriteLine("------------------------------------------------------------------------------------");
            Console.WriteLine("| STT | So phong    | Tong so giuong | Giuong trong  | Giuong da su dung | Ghi chu  ");
            Console.WriteLine("------------------------------------------------------------------------------------");

            int index = 1;
            foreach (var room in rooms.OrderBy(r => r.EmptyBeds))
            {
                Console.WriteLine($"| {index,-3} | {room.RoomNumber,-11} | {room.TotalBeds,-15} | {room.EmptyBeds,-12} | {room.OccupiedBeds(),-17} | {room.Status,-18} ");
                index++;
            }

            Console.WriteLine("------------------------------------------------------------------------------------");
        }

        // Quay lai trang chu
        public void GoBackToHome()
        {
            Console.WriteLine("Quay lai trang chu...");
        }
    }

    public class main
    {
        public static void Main(string[] args)
        {
            RoomManager roomManager = new RoomManager();
            bool roomRunning = true;

            while (roomRunning)
            {
                ShowRoomManagementMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddRoomFromInput(roomManager);
                        break;
                    case "2":
                        RemoveRoomFromInput(roomManager);
                        break;
                    case "3":
                        UpdateRoomFromInput(roomManager);
                        break;
                    case "4":
                        roomManager.DisplayRooms();
                        break;
                    case "5":
                        roomRunning = false; // Thoat quan ly phong
                        break;
                    default:
                        Console.WriteLine("Lua chon khong hop le. Vui long thu lai.");
                        break;
                }
            }
        }

        // Hien thi menu quan ly phong
        static void ShowRoomManagementMenu()
        {
            Console.WriteLine("\nMenu Quan Ly Phong:");
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("| STT | Hanh dong                                 |");
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("| 1   | Them phong                                |");
            Console.WriteLine("| 2   | Xoa phong                                 |");
            Console.WriteLine("| 3   | Cap nhat phong                            |");
            Console.WriteLine("| 4   | Hien thi danh sach phong                  |");
            Console.WriteLine("| 5   | Quay lai menu chinh                       |");
            Console.WriteLine("---------------------------------------------------");
            Console.Write("Lua chon cua ban: ");
        }

        // Phuong thuc nhap chi tiet phong va them phong
        static void AddRoomFromInput(RoomManager roomManager)
        {
            Console.Write("Nhap so luong phong can them: ");
            int numberOfRooms = int.Parse(Console.ReadLine());

            for (int i = 1; i <= numberOfRooms; i++)
            {
                Console.WriteLine($"\nNhap thong tin cho phong {i}:");

                Console.Write("Nhap so phong (bao gom 4 so nguyen, 2 so dau la tang, 2 so cuoi la phong): ");
                string roomNumber = Console.ReadLine();

                Console.Write("Nhap tong so giuong: ");
                int totalBeds = int.Parse(Console.ReadLine());

                int emptyBeds;
                do
                {
                    Console.Write("Nhap so giuong trong: ");
                    emptyBeds = int.Parse(Console.ReadLine());

                    if (emptyBeds > totalBeds)
                    {
                        Console.WriteLine("Loi: So giuong trong khong duoc lon hon tong so giuong. Vui long nhap lai.");
                    }

                } while (emptyBeds > totalBeds);

                Console.Write("Ghi chu ");

                string status = Console.ReadLine();

                Room newRoom = new Room(roomNumber, totalBeds, emptyBeds, status);
                roomManager.AddRoom(newRoom);

                Console.WriteLine("Them phong thanh cong!");
            }
        }

        // Phuong thuc nhap so phong va xoa phong
        static void RemoveRoomFromInput(RoomManager roomManager)
        {
            Console.Write("Nhap so phong can xoa: ");
            string roomNumber = Console.ReadLine();

            roomManager.RemoveRoom(roomNumber);
            Console.WriteLine("Xoa phong thanh cong!");
        }

        // Phuong thuc nhap chi tiet phong va cap nhat phong
        static void UpdateRoomFromInput(RoomManager roomManager)
        {
            Console.Write("Nhap so phong can cap nhat: ");
            string roomNumber = Console.ReadLine();

            if (!roomManager.RoomExists(roomNumber))
            {
                Console.WriteLine("Phong khong ton tai. Ban co muon:");
                Console.WriteLine("1. Thu lai");
                Console.WriteLine("2. Tao phong moi");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    UpdateRoomFromInput(roomManager);
                }
                else if (choice == "2")
                {
                    AddRoomFromInput(roomManager);
                }
                else
                {
                    Console.WriteLine("Lua chon khong hop le. Quay lai menu.");
                }

                return;
            }

            Console.Write("Nhap tong so giuong moi: ");
            int totalBeds = int.Parse(Console.ReadLine());

            int emptyBeds;
            do
            {
                Console.Write("Nhap so giuong trong moi: ");
                emptyBeds = int.Parse(Console.ReadLine());

                if (emptyBeds > totalBeds)
                {
                    Console.WriteLine("Loi: So giuong trong khong duoc lon hon tong so giuong. Vui long nhap lai.");
                }

            } while (emptyBeds > totalBeds);

            Console.Write("Ghi chu: ");
            string status = Console.ReadLine();

            roomManager.UpdateRoom(roomNumber, totalBeds, emptyBeds, status);
            Console.WriteLine("Cap nhat thong tin phong thanh cong!");
        }
    }
}
