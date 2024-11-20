using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BillManagement
{
    public class Node<T>
    {
        internal T data;
        internal Node<T> next;

        public Node(T value)
        {
            data = value;
            next = null;
        }
    }
    public class CustomQueue<T>
    {
        private Node<T> _front;
        private Node<T> _rear;
        private int _size;

        public int Count => _size;
        public bool IsEmpty() => _front == null;

        public void EnQueue(T item)
        {
            Node<T> newNode = new Node<T>(item);
            if (_rear == null)
            {
                _front = _rear = newNode;
            }
            else
            {
                _rear.next = newNode;
                _rear = newNode;
            }
            _size++;
        }

        public T DeQueue()
        {
            if (_front == null)
                throw new InvalidOperationException("Queue is empty!");

            T item = _front.data;
            _front = _front.next;
            _size--;

            if (_front == null)
                _rear = null;

            return item;
        }
    }

    public abstract class BaseBill
    {
        public string RoomNumber { get; set; }
        public bool IsPaid { get; set; }
        public DateTime CreatedDate { get; set; }

        protected BaseBill(string roomNumber)
        {
            RoomNumber = roomNumber;
            IsPaid = false;
            CreatedDate = DateTime.Now;
        }

        public abstract decimal CalculateTotal();
    }

    public class LivingExpenseBill : BaseBill
    {
        public decimal ElectricityBill { get; set; }
        public decimal WaterBill { get; set; }
        public decimal ServiceFee { get; set; }

        public LivingExpenseBill(string roomNumber, decimal electricityBill, decimal waterBill, decimal serviceFee)
            : base(roomNumber)
        {
            ElectricityBill = electricityBill;
            WaterBill = waterBill;
            ServiceFee = serviceFee;
        }

        public override decimal CalculateTotal()
        {
            return ElectricityBill + WaterBill + ServiceFee;
        }
    }

    // Accommodation Bill class
    public class AccommodationBill : BaseBill
    {
        public string StudentName { get; set; }
        public string StudentID { get; set; }
        public decimal AccommodationFee { get; set; }

        public AccommodationBill(string studentName, string studentID, string roomNumber, decimal accommodationFee)
            : base(roomNumber)
        {
            StudentName = studentName;
            StudentID = studentID;
            AccommodationFee = accommodationFee;
        }

        public override decimal CalculateTotal()
        {
            return AccommodationFee;
        }
    }

    // Bill Manager class
    public class BillManager
    {
        private CustomQueue<LivingExpenseBill> _livingExpenseQueue;
        private CustomQueue<AccommodationBill> _accommodationQueue;
        private string _basePath;

        public BillManager(string basePath)
        {
            _livingExpenseQueue = new CustomQueue<LivingExpenseBill>();
            _accommodationQueue = new CustomQueue<AccommodationBill>();
            _basePath = basePath;
            InitializeDirectories();
        }

        private void InitializeDirectories()
        {
            var directories = new[]
            {
                "LivingExpense",
                "Accommodation"
            };

            foreach (var dir in directories)
            {
                var path = Path.Combine(_basePath, dir);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
            }
        }

        // Bill Loading Methods
        public void LoadLivingExpenseBills(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Bill data file not found.");

            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split(',');
                if (parts.Length == 4 && decimal.TryParse(parts[1], out decimal electricity) &&
                    decimal.TryParse(parts[2], out decimal water) &&
                    decimal.TryParse(parts[3], out decimal service))
                {
                    var bill = new LivingExpenseBill(parts[0].Trim(), electricity, water, service);
                    _livingExpenseQueue.EnQueue(bill);
                }
            }
        }

        public void LoadAccommodationBills(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Bill data file not found.");

            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split(',');
                if (parts.Length == 4 && decimal.TryParse(parts[3], out decimal fee))
                {
                    var bill = new AccommodationBill(parts[0].Trim(), parts[1].Trim(),
                                                   parts[2].Trim(), fee);
                    _accommodationQueue.EnQueue(bill);
                }
            }
        }

        // Payment Processing Methods
        public void ProcessLivingExpensePayment(string roomNumber)
        {
            var count = _livingExpenseQueue.Count;
            for (int i = 0; i < count; i++)
            {
                var bill = _livingExpenseQueue.DeQueue();
                if (bill.RoomNumber == roomNumber && !bill.IsPaid)
                {
                    bill.IsPaid = true;
                    Console.WriteLine($"Xử lý thanh toán hóa đơn cho phòng {roomNumber}");
                    PrintLivingExpenseBill(bill);
                    return;
                }
                _livingExpenseQueue.EnQueue(bill);
            }
        }

        public void PrintLivingExpenseBill(LivingExpenseBill bill)
        {
            StringBuilder billContent = new StringBuilder();
            string separator = new string('-', 50);

            billContent.AppendLine(separator);
            billContent.AppendLine("            HÓA ĐƠN ĐIỆN NƯỚC KÝ TÚC XÁ");
            billContent.AppendLine($"                  {DateTime.Now:dd/MM/yyyy HH:mm}");
            billContent.AppendLine(separator);
            billContent.AppendLine($"Phòng: {bill.RoomNumber}");
            billContent.AppendLine(separator);
            billContent.AppendLine("CHI TIẾT HÓA ĐƠN:");
            billContent.AppendLine($"1. Tiền điện:          {bill.ElectricityBill,15:N0} VNĐ");
            billContent.AppendLine($"2. Tiền nước:          {bill.WaterBill,15:N0} VNĐ");
            billContent.AppendLine($"3. Phí dịch vụ:        {bill.ServiceFee,15:N0} VNĐ");
            billContent.AppendLine(separator);
            billContent.AppendLine($"Tổng tiền:             {bill.CalculateTotal(),15:N0} VNĐ");
            billContent.AppendLine(separator);
            billContent.AppendLine($"Trạng thái: {(bill.IsPaid ? "Đã thanh toán" : "Chưa thanh toán")}");
            billContent.AppendLine("Lưu ý: Vui lòng thanh toán trước ngày 20 hàng tháng.");
            billContent.AppendLine(separator);

            // Print to console
            Console.WriteLine(billContent.ToString());

            // Save to file
            SaveLivingExpenseBillToFile(bill, billContent.ToString());
        }

        private void SaveLivingExpenseBillToFile(LivingExpenseBill bill, string content)
        {
            string fileName = $"LivingExpense_{bill.RoomNumber}_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            var filePath = Path.Combine(_basePath, "LivingExpense", fileName);
            try
            {
                File.WriteAllText(filePath, content, Encoding.UTF8);
                Console.WriteLine($"Hóa đơn đã được lưu tại: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lưu hóa đơn: {ex.Message}");
            }
        }

        public void ProcessAccommodationPayment(string studentId)
        {
            var count = _accommodationQueue.Count;
            for (int i = 0; i < count; i++)
            {
                var bill = _accommodationQueue.DeQueue();
                if (bill.StudentID == studentId && !bill.IsPaid)
                {
                    bill.IsPaid = true;
                    Console.WriteLine($"Xử lý thanh toán hóa đơn cho học sinh {bill.StudentName}");
                    PrintAccommodationBill(bill);
                    return;
                }
                _accommodationQueue.EnQueue(bill);
            }
        }

        public void PrintAccommodationBill(AccommodationBill bill)
        {
            StringBuilder billContent = new StringBuilder();
            string separator = new string('-', 50);

            billContent.AppendLine(separator);
            billContent.AppendLine("            HÓA ĐƠN TIỀN Ở KÝ TÚC XÁ");
            billContent.AppendLine($"                  {DateTime.Now:dd/MM/yyyy HH:mm}");
            billContent.AppendLine(separator);
            billContent.AppendLine("THÔNG TIN SINH VIÊN:");
            billContent.AppendLine($"Họ và tên: {bill.StudentName}");
            billContent.AppendLine($"MSSV: {bill.StudentID}");
            billContent.AppendLine($"Phòng: {bill.RoomNumber}");
            billContent.AppendLine(separator);
            billContent.AppendLine("CHI TIẾT HÓA ĐƠN:");
            billContent.AppendLine($"Tiền ở KTX:           {bill.AccommodationFee,15:N0} VNĐ");
            billContent.AppendLine(separator);
            billContent.AppendLine($"Tổng tiền:            {bill.CalculateTotal(),15:N0} VNĐ");
            billContent.AppendLine(separator);
            billContent.AppendLine($"Trạng thái: {(bill.IsPaid ? "Đã thanh toán" : "Chưa thanh toán")}");
            billContent.AppendLine(separator);

            // Print to console
            Console.WriteLine(billContent.ToString());

            // Save to file
            SaveAccommodationBillToFile(bill, billContent.ToString());
        }

        private void SaveAccommodationBillToFile(AccommodationBill bill, string content)
        {
            string fileName = $"Accommodation_{bill.StudentID}_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            string filePath = Path.Combine(_basePath, "Accommodation", fileName);

            try
            {
                File.WriteAllText(filePath, content, Encoding.UTF8);
                Console.WriteLine($"Hóa đơn đã được lưu tại: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lưu hóa đơn: {ex.Message}");
            }
        }
        public void GenerateUnpaidBillsReport()
        {
            Console.WriteLine("\n=== Unpaid Living Expense Bills ===");
            var leCount = _livingExpenseQueue.Count;
            for (int i = 0; i < leCount; i++)
            {
                var bill = _livingExpenseQueue.DeQueue();
                if (!bill.IsPaid)
                    PrintBillDetails(bill);
                _livingExpenseQueue.EnQueue(bill);
            }

            Console.WriteLine("\n=== Unpaid Accommodation Bills ===");
            var acCount = _accommodationQueue.Count;
            for (int i = 0; i < acCount; i++)
            {
                var bill = _accommodationQueue.DeQueue();
                if (!bill.IsPaid)
                    PrintBillDetails(bill);
                _accommodationQueue.EnQueue(bill);
            }
        }

        private void PrintBillDetails(BaseBill bill)
        {
            if (bill is LivingExpenseBill leBill)
            {
                Console.WriteLine($"Room {leBill.RoomNumber}: {leBill.CalculateTotal():C}");
            }
            else if (bill is AccommodationBill acBill)
            {
                Console.WriteLine($"Student {acBill.StudentName} ({acBill.StudentID}): {acBill.CalculateTotal():C}");
            }
        }
    }

    public class BillManagementUI
    {
        private BillManager _billManager;

        public BillManagementUI(string basePath)
        {
            _billManager = new BillManager(basePath);
        }

        public void ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== HỆ THỐNG QUẢN LÝ HÓA ĐƠN ===");
                Console.WriteLine("1. Xử lý hóa đơn chi phí sinh hoạt");
                Console.WriteLine("2. Xử lý hóa đơn tiền ở");
                Console.WriteLine("3. Tạo báo cáo hóa đơn chưa thanh toán");
                Console.WriteLine("4. Đọc hóa đơn từ file");
                Console.WriteLine("5. Thoát");
                Console.Write("\nChọn chức năng: ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            ProcessLivingExpenseBill();
                            break;
                        case 2:
                            ProcessAccommodationBill();
                            break;
                        case 3:
                            _billManager.GenerateUnpaidBillsReport();
                            break;
                        case 4:
                            LoadBillsFromFile();
                            break;
                        case 5:
                            return;
                        default:
                            Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng nhập lại. ");
                            break;
                    }
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        private void ProcessLivingExpenseBill()
        {
            Console.Write("Nhập số phòng: ");
            var roomNumber = Console.ReadLine();
            _billManager.ProcessLivingExpensePayment(roomNumber);
        }

        private void ProcessAccommodationBill()
        {
            Console.Write("Nhập mã số sinh viên: ");
            var studentId = Console.ReadLine();
            _billManager.ProcessAccommodationPayment(studentId);
        }

        private void LoadBillsFromFile()
        {
            Console.WriteLine("1. Load Living Expense Bills");
            Console.WriteLine("2. Load Accommodation Bills");
            Console.Write("Chọn chức năng: ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.Write("Nhập file path: ");
                var filePath = Console.ReadLine();

                try
                {
                    if (choice == 1)
                        _billManager.LoadLivingExpenseBills(filePath);
                    else if (choice == 2)
                        _billManager.LoadAccommodationBills(filePath);
                    else
                        Console.WriteLine("Lựa chọn không hợp lệ.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading bills: {ex.Message}");
                }
            }
        }
    }
}