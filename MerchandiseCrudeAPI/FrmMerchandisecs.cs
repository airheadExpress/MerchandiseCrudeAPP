using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MerchandiseCrudeAPI
{
    public partial class FrmMerchandisecs : Form
    {
        public FrmMerchandisecs()
        {
            InitializeComponent();
        }

        MerchDBEntities data = new MerchDBEntities();
        public static int Id = -1;

        private void FrmMerchandisecs_Load(object sender, EventArgs e)
        {
            LoadData();

        }

        public void LoadData() 
        {
            List<MerchTable> merchandises = new List<MerchTable>();
            merchandises = data.MerchTables.ToList();
            dgMerchandises.DataSource = merchandises;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            //int qtyPie = Int32.Parse(txtQty.Text);
            MerchTable merch = new MerchTable();
            merch.merchName = txtName.Text;
            merch.merchDescription = txtDesc.Text;
            merch.merchQuantity = txtQty.Text;

            data.MerchTables.Add(merch);
            data.SaveChanges();
            MessageBox.Show("Data Added");
            LoadData();
        }

        private void dgMerchandises_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Id = Convert.ToInt32(dgMerchandises.Rows[e.RowIndex].Cells[0].Value.ToString());
            txtName.Text = dgMerchandises.Rows[e.RowIndex].Cells[1].Value == DBNull.Value ? "" : dgMerchandises.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtDesc.Text = dgMerchandises.Rows[e.RowIndex].Cells[2].Value == DBNull.Value ? "" : dgMerchandises.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtQty.Text = dgMerchandises.Rows[e.RowIndex].Cells[3].Value == DBNull.Value ? "" : dgMerchandises.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Id > 0) 
            {
                MerchTable merch = data.MerchTables.Where(m => m.merchID == Id).SingleOrDefault();
                merch.merchName = txtName.Text;
                merch.merchDescription = txtDesc.Text;
                merch.merchQuantity = txtQty.Text;

                data.SaveChanges();
                MessageBox.Show("Update Success");

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgMerchandises.SelectedRows.Count > 0) 
            {
                foreach (DataGridViewRow row in dgMerchandises.SelectedRows) 
                {
                    int id = (int)row.Cells[0].Value;
                    var merch = data.MerchTables.SingleOrDefault(x => x.merchID == id);
                    data.MerchTables.Remove(merch);
                    data.SaveChanges();
                }
                MessageBox.Show("Data Deleted");
            }
            LoadData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "") 
            {
                List<MerchTable> merchandises = new List<MerchTable>();
                merchandises = data.MerchTables.Where(m => m.merchName.Contains(txtName.Text)).ToList();
                dgMerchandises.DataSource = merchandises;
            }
        }
    }
}
