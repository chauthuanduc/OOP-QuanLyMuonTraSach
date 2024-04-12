using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_QuanLiMuonTraSach
{
    public partial class frm_ThongTinSach : Form
    {
        bool menuExpand = false;
        BookList bookList = new BookList();
        string path = "book.txt";
        BookList deseriBL;
        List<Button> buttonChangePageList = new List<Button>(); //List chứa các button phân trang
        int pageNumber = 1; //Biến thể hiện trang hiện tại
        int numberRecord = 10; //Biến thể hiện số dòng hiển thị
        int totalRecord = 0; //Biến chứa tổng số dòng trong bảng
        int lastPageNumber = 0; //Biến thể hiện trang cuối cùng trong bảng
        int doubleClickCount = 0; //Biến đếm số lần double click
        

        public frm_ThongTinSach()
        {
            InitializeComponent();
            AddBookToFile();
            panel_ChinhSuaSach.Height = 0;

            deseriBL = bookList.Deserialize<BookList>(path);

            bookList.FindBook(deseriBL, "3");

            bookList.Serialize<BookList>(path, deseriBL);

            LoadData(deseriBL);
        }

        //Functions
        #region Functions
        private void AddBookToFile()
        {
            BookList deseriBL = new BookList();
            deseriBL.Add(new Book { IdSach = "1", TenSach = "Đắc nhân tâm", TacGia = "Dale Carnegie", TheLoai = "Self help", SoLuong = "10", NhaXuatBan = "NXB Kim Đồng", NamXuatBan = new DateTime (1936, 06, 20)/*"20/06/1936"*/ });
            deseriBL.Add(new Book { IdSach = "2", TenSach = "Nhà giả kim", TacGia = "Paulo Coelho", TheLoai = "Tiểu thuyết", SoLuong = "5", NhaXuatBan = "NXB Trẻ", NamXuatBan = new DateTime (1988, 05, 22) /*"20/05/1988"*/ });
            deseriBL.Add(new Book { IdSach = "3", TenSach = "Đời thay đổi khi chúng ta thay đổi", TacGia = "Andrew Matthews", TheLoai = "Self help", SoLuong = "10", NhaXuatBan = "NXB Thanh Niên", NamXuatBan = new DateTime(2016, 5, 15) /*"15/5/2016"*/ });
            deseriBL.Add(new Book { IdSach = "4", TenSach = "Harry Potter và Bảo bối tử thần", TacGia = "J.K Rowling", TheLoai = "Tiểu thuyết", SoLuong = "10", NhaXuatBan = "NXB Trẻ", NamXuatBan = new DateTime(2007, 07, 21) /*"21/7/2007"*/ });
            deseriBL.Add(new Book { IdSach = "5", TenSach = "Dune", TacGia = "Frank Herbert", TheLoai = "Tiểu thuyết", SoLuong = "10", NhaXuatBan = "NXB Kim Đồng", NamXuatBan = new DateTime(1965, 03, 21) /*"21/3/1965"*/ });
            deseriBL.Add(new Book { IdSach = "6", TenSach = "Bố già", TacGia = "Mario Puzo", TheLoai = "Tiểu thuyết", SoLuong = "10", NhaXuatBan = "NXB Kim Đồng", NamXuatBan = new DateTime(1969, 04, 03) /*"3/4/1969"*/ });
            deseriBL.Add(new Book { IdSach = "7", TenSach = "Sherlock Holmes", TacGia = "Athur Conan Doyle", TheLoai = "Trinh thám", SoLuong = "10", NhaXuatBan = "NXB Trẻ", NamXuatBan = new DateTime(1887, 02 ,21) /*"21/2/1887"*/ });
            deseriBL.Add(new Book { IdSach = "8", TenSach = "Sapiens: Lược sử loài người", TacGia = "Yuval Noah Harari", TheLoai = "Khoa học", SoLuong = "10", NhaXuatBan = "NXB Thanh Niên", NamXuatBan = new DateTime(2011, 01, 11) /*"11/1/2011"*/ });
            deseriBL.Add(new Book { IdSach = "9", TenSach = "Đường vào lập trình Python", TacGia = "Nguyễn Ngọc Giang", TheLoai = "Công nghệ", SoLuong = "10", NhaXuatBan = "NXB Thanh Niên", NamXuatBan = new DateTime(2019, 02, 02) /*"2/2/2019"*/ });
            deseriBL.Add(new Book { IdSach = "10", TenSach = "Lập trình và cuộc sống", TacGia = "Jeff Atwood", TheLoai = "Công nghệ", SoLuong = "10", NhaXuatBan = "NXB Trẻ", NamXuatBan = new DateTime(2017, 02, 04) /*"4/2/2017"*/ });
            deseriBL.Add(new Book { IdSach = "11", TenSach = "Mãi mãi là bí ẩn", TacGia = "TONY Hưng", TheLoai = "Tài liệu", SoLuong = "10", NhaXuatBan = "NXB Thanh Niên", NamXuatBan = new DateTime(2012, 07, 30) /*"30/7/2012"*/ });
            deseriBL.Add(new Book { IdSach = "12", TenSach = "Chú thuật hồi chiến", TacGia = "Gege Akutami", TheLoai = "Truyện tranh", SoLuong = "10", NhaXuatBan = "NXB Trẻ", NamXuatBan = new DateTime(2018, 12, 24) /*"24/12/2018"*/ });
            deseriBL.Add(new Book { IdSach = "13", TenSach = "Tam thể", TacGia = "Lưu Từ Hân", TheLoai = "Khoa học viễn tưởng", SoLuong = "10", NhaXuatBan = "NXB Kim Đồng", NamXuatBan = new DateTime(2008, 02, 28) /*"28/2/2008"*/ });


            deseriBL.Serialize<BookList>(path, deseriBL);
        }

        private void ResetLabelTextToNull(Label label) //Đặt text của lable về null
        {
            label.Text = null;
        }

        private void SetLabelText(Label label, string text) //Set text cho label
        {
            label.Text = text;
        }

        private void FocusTextBox(TextBox textBox) //Focus vào textBox
        {
            textBox.Focus();
        } 

        private void SearchBooksByGeneral() //Hàm tìm kiếm nhân viên 1 cách tổng quát
        {
            IEnumerable<Book> result = null;

            // Tìm kiếm sách theo các trường thông tin tổng quát
            string searchText = textBox_SearchName.Text;
            result = deseriBL.Books.Where(c => c.IdSach.ToString().Contains(textBox_SearchName.Text)
                                                     || c.TenSach.Contains(textBox_SearchName.Text)
                                                     || c.TacGia.Contains(textBox_SearchName.Text)
                                                     || c.TheLoai.Contains(textBox_SearchName.Text)
                                                     || c.NhaXuatBan.Contains(textBox_SearchName.Text));
                if (result != null)
                {
                    // Tính toán lại số trang khi có kết quả mới
                    totalRecord = result.Count();
                    lastPageNumber = (int)Math.Ceiling((double)totalRecord / numberRecord);

                    // Hiển thị trang đầu tiên
                    result = result.OrderBy(s => s.IdSach)
                                 .Skip((pageNumber - 1) * numberRecord)
                                 .Take(numberRecord);

                    // Hiển thị kết quả trong DataGridView
                    dataGridView_ChinhSuaSach.DataSource = result.Select(s => new
                    {
                        s.IdSach,
                        s.TenSach,
                        s.TacGia,
                        s.TheLoai,
                        s.SoLuong,
                        s.NhaXuatBan,
                        s.NamXuatBan
                    }).ToList();
                    AdjustRowHeight();
                    AdjustColumnWidth();
                    ChangeHeader();
                }
        }

        private void LoadData( BookList deseriBL) //Hàm hiển thị dữ liệu
        {
            //BookList bl = bookList.Deserialize<BookList>(path);
            totalRecord = deseriBL.Books.Count;
            lastPageNumber = (int)Math.Ceiling((double)totalRecord / numberRecord); //Công thức tính trang cuối cùng trong 
            dataGridView_ChinhSuaSach.DataSource = LoadRecord(deseriBL,pageNumber, numberRecord);
            AdjustColumnWidth();
            AdjustRowHeight();
            ChangeHeader();
        }

        private List<object> LoadRecord(BookList deseriBL, int page, int recordNum)
        {
            List<object> result = new List<object>();
            result = deseriBL.Books.Skip((page - 1) * recordNum)
                .Take(recordNum)
                 .Select(e => new
                 {
                     e.IdSach,
                     e.TenSach,
                     e.TacGia,
                     e.TheLoai,
                     e.SoLuong,
                     e.NhaXuatBan,
                     e.NamXuatBan
                 }).ToList<object>();
            return result;
        }

        public void AdjustRowHeight() //Hàm customize lại height các dòng
        {
            //Biến thể hiện height của các dòng sao cho bằng nhau
            int desiredHeight = dataGridView_ChinhSuaSach.Height / (dataGridView_ChinhSuaSach.Rows.Count + 1);
            if (dataGridView_ChinhSuaSach.Rows.Count > 0 && dataGridView_ChinhSuaSach.Rows.Count < 5)
            {
                foreach (DataGridViewRow row in dataGridView_ChinhSuaSach.Rows)
                {
                    row.Height = 60;
                }
            }
            else
            {
                // Thiết lập chiều cao cho mỗi dòng
                foreach (DataGridViewRow row in dataGridView_ChinhSuaSach.Rows)
                {
                    row.Height = desiredHeight;
                }
            }
        }

        private void AdjustColumnWidth() //Hàm customize lại width các dòng
        {
            if (dataGridView_ChinhSuaSach.Columns.Count > 0)
            {
                dataGridView_ChinhSuaSach.Columns[0].Width = dataGridView_ChinhSuaSach.Width * 5 / 100;
                dataGridView_ChinhSuaSach.Columns[1].Width = dataGridView_ChinhSuaSach.Width * 20 / 100;
                dataGridView_ChinhSuaSach.Columns[2].Width = dataGridView_ChinhSuaSach.Width * 20 / 100;
                dataGridView_ChinhSuaSach.Columns[3].Width = dataGridView_ChinhSuaSach.Width * 10 / 100;
                dataGridView_ChinhSuaSach.Columns[4].Width = dataGridView_ChinhSuaSach.Width * 10 / 100;
                dataGridView_ChinhSuaSach.Columns[5].Width = dataGridView_ChinhSuaSach.Width * 20 / 100;
                dataGridView_ChinhSuaSach.Columns[6].Width = dataGridView_ChinhSuaSach.Width * 15 / 100;
            }
        }

        private void ChangeHeader() //Hàm thay đổi tiêu đề hiển thị trên dataGridView
        {
            if (dataGridView_ChinhSuaSach.Columns.Count > 0)
            {
                dataGridView_ChinhSuaSach.Columns[0].HeaderText = "ID";
                dataGridView_ChinhSuaSach.Columns[1].HeaderText = "Tên sách";
                dataGridView_ChinhSuaSach.Columns[2].HeaderText = "Tác giả";
                dataGridView_ChinhSuaSach.Columns[3].HeaderText = "Thể loại";
                dataGridView_ChinhSuaSach.Columns[4].HeaderText = "Số lượng";
                dataGridView_ChinhSuaSach.Columns[5].HeaderText = "Nhà xuất bản";
                dataGridView_ChinhSuaSach.Columns[6].HeaderText = "Năm xuất bản";
            }
        }

        private void AddButtonChangePageList() //Hàm thêm các button phân trang vào list phân trang
        {
            buttonChangePageList.Add(button_ChangePage1);
            buttonChangePageList.Add(button_ChangePage2);
            buttonChangePageList.Add(button_ChangePage3);
            buttonChangePageList.Add(button_ReturnFirstPage);
            buttonChangePageList.Add(button_ReturnLastPage);
        }

        //Hàm tạo thứ tự cho các button phân trang căn cứ vào trang hiện tại
        private void CreateOrderForButtonChangePageByPageNumber(int pageNumber)
        {
            button_ChangePage1.Text = (pageNumber - 1).ToString();
            button_ChangePage2.Text = pageNumber.ToString();
            button_ChangePage3.Text = (pageNumber + 1).ToString();
        }

        //Hàm đặt các button phân trang về mặc định: 1 2 3
        private void SetDefaultButtonChangePageText()
        {
            button_ChangePage1.Text = "1";
            button_ChangePage2.Text = "2";
            button_ChangePage3.Text = "3";
        }

        //Hàm tạo thứ tự cho các button phân trang căn cứ vào trang cuối cùng
        private void CreateOrderForButtonChangePageByLastPageNumber(int lastPageNumber)
        {
            button_ChangePage1.Text = (lastPageNumber - 2).ToString();
            button_ChangePage2.Text = (lastPageNumber - 1).ToString();
            button_ChangePage3.Text = lastPageNumber.ToString();
        }

        private void ResetColorButton() //Hàm đặt lại màu của các button phân trang
        {
            AddButtonChangePageList();
            foreach (Button button in buttonChangePageList)
            {
                button.BackColor = Color.White;
                button.ForeColor = Color.Black;
            }
        }

        private void HighlightButtonCurrentPage(object obj) //Hàm hightlight button phân trang được truyền vào
        {
            Button sender = obj as Button;
            sender.BackColor = Color.FromArgb(0, 95, 105);
            sender.ForeColor = Color.White;
        }
        private void Sidebar_Transition(ref bool menuExpand, Panel panel, Timer timer)
        {
            if (menuExpand == false)
            {
                panel.Height += 5;
                if (panel.Height >= 195)
                {
                    StopTimer(timer);
                    menuExpand = true;
                }
            }
            else
            {
                panel.Height -= 5;
                if (panel.Height <= 0)
                {
                    StopTimer(timer);
                    menuExpand = false;
                }
            }
        }

        private void StartTimer(Timer timer) //Hàm để Start Timer
        {
            timer.Start();
        }

        private void StopTimer(Timer timer) //Hàm để Stop Timer
        {
            timer.Stop();
        }

        private void BindingDataSelected() //Hàm binding dữ liệu có trong dataGridView
        {
            if (dataGridView_ChinhSuaSach.SelectedCells.Count > 0)  
            {
                int selectedID = Convert.ToInt32(dataGridView_ChinhSuaSach.SelectedCells[0].OwningRow.Cells["IdSach"].Value.ToString());
                Book selectedBook = deseriBL.FirstOrDefault(book => book.IdSach == selectedID.ToString());
                if (selectedBook != null)
                {
                    textBox_IDSachUpdateInput.Text = selectedBook.IdSach;
                    textBox_TenSachUpdateInput.Text = selectedBook.TenSach;
                    textBox_TacGiaUpdateInput.Text = selectedBook.TacGia;
                    textBox_SoLuongUpdateInput.Text = selectedBook.SoLuong;
                    textBox_TheLoaiUpdateInput.Text = selectedBook.TheLoai;
                    textBox_NhaXuatBanUpdateInput.Text = selectedBook.NhaXuatBan;
                    dateTimePicker_NamXuatBanUpdateInput.Value = selectedBook.NamXuatBan; //DateTime.ParseExact(selectedBook.NamXuatBan, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
            }
        }

        private void UpdateSach() //Hàm update lại sách
        {
            if (dataGridView_ChinhSuaSach.SelectedCells.Count > 0)
            {
                int selectedID = Convert.ToInt32(dataGridView_ChinhSuaSach.SelectedCells[0].OwningRow.Cells["IdSach"].Value.ToString());
                Book selectedBook = null;
                foreach (Book book in deseriBL.Books)
                {
                    if (book.IdSach == selectedID.ToString())
                    {
                        selectedBook = book;
                        break;
                    }
                }
                if (selectedBook != null)
                {
                    List<string> updateFields = new List<string>();

                    if (textBox_TenSachUpdateInput.Text != selectedBook.TenSach)
                        updateFields.Add("Tên sách");
                    if (textBox_TacGiaUpdateInput.Text != selectedBook.TacGia)
                        updateFields.Add("Tác giả");
                    if (textBox_TheLoaiUpdateInput.Text != selectedBook.TheLoai)
                        updateFields.Add("Thể loại");
                    if (textBox_SoLuongUpdateInput.Text != selectedBook.SoLuong)
                        updateFields.Add("Số lượng");
                    if (textBox_NhaXuatBanUpdateInput.Text != selectedBook.NhaXuatBan)
                        updateFields.Add("Nhà xuất bản");
                    if (dateTimePicker_NamXuatBanUpdateInput.Value != selectedBook.NamXuatBan) //DateTime.ParseExact(selectedBook.NamXuatBan, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                        updateFields.Add("Năm xuất bản");

                    string updateFieldMessage = "Bạn muốn thay đổi \n";
                    foreach (string field in updateFields)  
                    {
                        updateFieldMessage += field + "\n";
                    }
                    updateFieldMessage += $"của sách {selectedBook.TenSach}";

                    DialogResult result = MessageBox.Show(updateFieldMessage, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        selectedBook.TenSach = textBox_TenSachUpdateInput.Text;
                        selectedBook.TacGia = textBox_TacGiaUpdateInput.Text;
                        selectedBook.TheLoai = textBox_TheLoaiUpdateInput.Text;
                        selectedBook.SoLuong = textBox_SoLuongUpdateInput.Text;
                        selectedBook.NhaXuatBan = textBox_NhaXuatBanUpdateInput.Text;
                        selectedBook.NamXuatBan = selectedBook.NamXuatBan;
                        deseriBL.Serialize<BookList>(path, deseriBL); // Lưu thay đổi vào tệp tin
                        MessageBox.Show("Chỉnh sửa thông tin sách thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData(deseriBL);
                    }
                    else
                    {
                        MessageBox.Show("Chỉnh sửa thông tin sách thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        #endregion

        //Events
        #region Events
        private void timer_ChinhSuaSachTransition_Tick(object sender, EventArgs e)
        {
            Sidebar_Transition(ref menuExpand, panel_ChinhSuaSach, timer_ChinhSuaSachTransition);
        }

        private void Form_ChinhSuaSach_Resize(object sender, EventArgs e)
        {

            AdjustRowHeight();
            AdjustColumnWidth();
        }

        private void dataGridView_ChinhSuaSach_Resize(object sender, EventArgs e)
        {
            AdjustRowHeight();
        }
        private void frm_ThongTinSach_Resize(object sender, EventArgs e)
        {
            AdjustColumnWidth();
            AdjustRowHeight();
        }

        private void textBox_SearchName_TextChanged(object sender, EventArgs e)
        {
            if (textBox_SearchName.Text.Length != 0)
            {
                ResetLabelTextToNull(label_SearchName);//Nếu text trong ô textBox được nhập thì xóa label Search
                pageNumber = 1;
                SetDefaultButtonChangePageText();
                ResetColorButton();
                HighlightButtonCurrentPage(button_ChangePage1);
                SearchBooksByGeneral();
            }
            else
            {
                SetLabelText(label_SearchName, "Search by name, author, major..."); //Nếu text rỗng thì hiện lại label Search
                pageNumber = 1;
                SetDefaultButtonChangePageText();
                ResetColorButton();
                HighlightButtonCurrentPage(button_ChangePage1);
                LoadData(deseriBL);
            }
        }

        private void label_SearchName_Click(object sender, EventArgs e)
        {
            textBox_SearchName.Focus();
            ResetLabelTextToNull(label_SearchName);
        }

        private void textBox_SearchName_Click(object sender, EventArgs e)
        {
            ResetLabelTextToNull(label_SearchName);
        }

        private void textBox_SearchName_Leave(object sender, EventArgs e)
        {
            label_SearchName.Text = "Search by id, name...";
        }

        private void button_ChangePage1_Click(object sender, EventArgs e)
        {
            pageNumber = Convert.ToInt32(button_ChangePage1.Text);

            if (button_ChangePage1.Text != "1")
            {
                if (textBox_SearchName.Text != null)
                {
                    SearchBooksByGeneral();
                    CreateOrderForButtonChangePageByPageNumber(pageNumber);
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage2);
                }
                else
                {
                    LoadData(deseriBL);
                    CreateOrderForButtonChangePageByPageNumber(pageNumber);
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage2);
                }
            }
            else
            {
                if (textBox_SearchName.Text != null)
                {
                    SearchBooksByGeneral();
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage1);
                }
                else
                {
                    LoadData(deseriBL);
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage1);
                }
            }
        }

        private void button_ChangePage2_Click(object sender, EventArgs e)
        {
            if (lastPageNumber == 1) return;
            pageNumber = Convert.ToInt32(button_ChangePage2.Text);
            if (textBox_SearchName.Text != null)
            {
                SearchBooksByGeneral();
                ResetColorButton();
                HighlightButtonCurrentPage(sender);
            }
            else
            {
                LoadData(deseriBL);
                ResetColorButton();
                HighlightButtonCurrentPage(sender);
            }
        }

        private void button_ChangePage3_Click(object sender, EventArgs e)
        {
            if (lastPageNumber <= 2) return;
            pageNumber = Convert.ToInt32(button_ChangePage3.Text);

            if (lastPageNumber > pageNumber)
            {
                if (textBox_SearchName.Text != null)
                {
                    SearchBooksByGeneral();
                    CreateOrderForButtonChangePageByPageNumber(pageNumber);
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage2);
                }
                else
                {
                    LoadData(deseriBL);
                    CreateOrderForButtonChangePageByPageNumber(pageNumber);
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage2);
                }
            }
            else
            {
                if (textBox_SearchName.Text != null)
                {
                    SearchBooksByGeneral();
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage3);
                }
                else
                {
                    LoadData(deseriBL);
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage3);
                }
            }
        }

        private void button_ReturnFirstPage_Click(object sender, EventArgs e)
        {
            pageNumber = 1;
            if (textBox_SearchName.Text != null)
            {
                SearchBooksByGeneral();
                ResetColorButton();
                HighlightButtonCurrentPage(button_ChangePage1);
                SetDefaultButtonChangePageText();
            }
            else
            {
                LoadData(deseriBL);
                ResetColorButton();
                HighlightButtonCurrentPage(button_ChangePage1);
                SetDefaultButtonChangePageText();
            }
        }

        private void button_ReturnLastPage_Click(object sender, EventArgs e)
        {
            pageNumber = lastPageNumber;
            if (textBox_SearchName.Text != null)
            {
                SearchBooksByGeneral();
                if (pageNumber == 1)
                {
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage1);
                    return;
                }
                else if (pageNumber == 2)
                {
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage2);
                    return;
                }
                else
                {
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage3);
                    CreateOrderForButtonChangePageByLastPageNumber(lastPageNumber);
                }
            }
            else
            {
                LoadData(deseriBL);
                if (pageNumber == 1)
                {
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage1);
                    return;
                }
                else if (pageNumber == 2)
                {
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage2);
                    return;
                }
                else
                {
                    ResetColorButton();
                    HighlightButtonCurrentPage(button_ChangePage3);
                    CreateOrderForButtonChangePageByLastPageNumber(lastPageNumber);
                }
            }
        }

        private void dataGridView_ChinhSuaSach_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            menuExpand = false;
            StartTimer(timer_ChinhSuaSachTransition);
            if (panel_ChinhSuaSach.Height == 195) StopTimer(timer_ChinhSuaSachTransition);
            doubleClickCount++;
            if (doubleClickCount == 1)
            {
                numberRecord = 5;
                if (textBox_SearchName.Text != null)
                {
                    SearchBooksByGeneral();
                }
                else
                {
                    LoadData(deseriBL);
                }
            }
        }

        private void pictureBox_Exit_Click(object sender, EventArgs e)
        {
            menuExpand = true;
            StartTimer(timer_ChinhSuaSachTransition);
            numberRecord = 10;
            LoadData(deseriBL);
            doubleClickCount = 0;
        }

        private void dataGridView_ChinhSuaSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (doubleClickCount > 0)
                BindingDataSelected();
        }

        private void button_SaveUpdate_Click(object sender, EventArgs e)
        {
            UpdateSach();
        }

        private void button_ResetUpdate_Click(object sender, EventArgs e)
        {
            BindingDataSelected();
        }
        private void button_InsertDelete_Click(object sender, EventArgs e)
        {
            frm_ThemXoaSach form = new frm_ThemXoaSach();
            form.Show();
        }
        #endregion
    }
}
