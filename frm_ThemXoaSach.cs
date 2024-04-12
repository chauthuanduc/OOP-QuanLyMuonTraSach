using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_QuanLiMuonTraSach
{
    public partial class frm_ThemXoaSach : Form
    {
        BookList bookList = new BookList();
        string path = "book.txt";
        BookList deseriBL;
        //Varibles
        #region Varibles
        bool checkInputInsert = true; //Biến check đầu vào khi Insert
        bool checkInputDelete = true; //Biến check đầu vào khi Delete
        #endregion
        public frm_ThemXoaSach()
        {
            InitializeComponent();
        }
        //Functions
        #region Functions

        //HÀM CHECK INPUT KHI INSERT
        private void CheckInputForInsertSach(ref bool check) 
        {
            List<string> missingFields = new List<string>();

            if (string.IsNullOrWhiteSpace(textBox_TenSachInsertInput.Text))
                missingFields.Add("Tên sách");
            if (string.IsNullOrWhiteSpace(textBox_TacGiaInsertInput.Text))
                missingFields.Add("Tác giả");
            if (string.IsNullOrWhiteSpace(textBox_TheLoaiInsertInput.Text))
                missingFields.Add("Thể loại");
            if (string.IsNullOrWhiteSpace(textBox_SoLuongInsertInput.Text))
                missingFields.Add("Số lượng");
            if (string.IsNullOrWhiteSpace(textBox_NhaXuatBanInsertInput.Text))
                missingFields.Add("Nhà xuất bản");
            if (dateTimePicker_NamXuatBanInsertInput.Value == null)
                missingFields.Add("Năm xuất bản");

            if (missingFields.Count == 0) check = true;
            else
            {
                check = false;
                string missingFieldsMessage = "Các trường sau không được để trống:\n";
                foreach (string field in missingFields)
                {
                    missingFieldsMessage += field + "\n";
                }
                MessageBox.Show(missingFieldsMessage, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //HÀM INSERT SÁCH

        private void AddSach() //Hàm insert sách
        {
            DialogResult result = MessageBox.Show($"Bạn có chắc muốn thêm sách {textBox_TenSachInsertInput.Text} không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                BookList deseriBL = bookList.Deserialize<BookList>(path);

                Book sach = new Book();
                sach.IdSach = (deseriBL.Books.Count + 1).ToString();
                sach.TenSach = textBox_TenSachInsertInput.Text;
                sach.TacGia = textBox_TacGiaInsertInput.Text;
                sach.TheLoai = textBox_TheLoaiInsertInput.Text;
                sach.SoLuong = textBox_SoLuongInsertInput.Text;
                sach.NhaXuatBan = textBox_NhaXuatBanInsertInput.Text;
                sach.NamXuatBan = dateTimePicker_NamXuatBanInsertInput.Value;

                deseriBL.Add(sach);
                bookList.Serialize<BookList>(path, deseriBL);

                MessageBox.Show("Thêm sách thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Thêm sách chưa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        //HÀM ĐẶT CÁC INPUT VỀ NULL
        private void ResetControl() 
        {
            textBox_TenSachInsertInput.Text = null;
            textBox_TacGiaInsertInput.Text = null;
            textBox_TheLoaiInsertInput.Text = null;
            textBox_SoLuongInsertInput.Text = null;
            textBox_NhaXuatBanInsertInput.Text = null;
            dateTimePicker_NamXuatBanInsertInput.Value = DateTime.Now;
        }

        //Hàm check input khi Delete
        private void CheckInputForDeleteSach(ref bool check)
        {
            if (!string.IsNullOrWhiteSpace(textBox_IDSachDeleteInput.Text)) check = true;
            else
            {
                MessageBox.Show($"Trường {textBox_IDSachDeleteInput.Text} không được để trống", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                check = false;
            }
        }

        //HÀM DELETE SÁCH

        private void DeleteSach()
        {
            DialogResult result = MessageBox.Show($"Bạn có chắc muốn xóa sách có ID: {textBox_IDSachDeleteInput.Text} không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                BookList deseriBL = bookList.Deserialize<BookList>(path);

                int idDelete = Convert.ToInt32(textBox_IDSachDeleteInput.Text);
                Book sachToDelete = deseriBL.Books.FirstOrDefault(s => s.IdSach == idDelete.ToString());

                if (sachToDelete != null)
                {
                    deseriBL.Books.Remove(sachToDelete);
                    bookList.Serialize<BookList>(path, deseriBL);
                    MessageBox.Show("Xóa sách thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("ID sách không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Xóa sách thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //HÀM ĐẶT INPUT VỀ NULL
        private void ResetIdDelete() 
        {
            textBox_IDSachDeleteInput.Text = null;
        }
        #endregion

        //EVENTS
        #region Events
        private void button_SaveInsert_Click(object sender, EventArgs e)
        {
            CheckInputForInsertSach(ref checkInputInsert);
            if (checkInputInsert == true) AddSach();
        }

        private void button_ResetInsert_Click(object sender, EventArgs e)
        {
            ResetControl();
        }

        private void button_SaveDelete_Click(object sender, EventArgs e)
        {
            CheckInputForDeleteSach(ref checkInputDelete);
            if (checkInputDelete == true) DeleteSach();
        }

        private void button_ResetDelete_Click(object sender, EventArgs e)
        {
            ResetIdDelete();
        }
        #endregion
    }
}
