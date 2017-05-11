using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NHibernate;

namespace HibernateTest.Models.Classes
{
    public partial class FrmMain : Form
    {
        public IList<Employee> Faves;//List of locally stored presets
        public FrmMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Populate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ISession session = NHibertnateSession.OpenSession();

            Employee emp = new Employee();
            emp.FirstName = tbFirstName.Text;
            emp.LastName = tbLastName.Text;
            emp.Designation = tbDesignation.Text;
            emp.SecondDesignation = tbSecondDesignation.Text;
            emp.ThirdDesignation = tbThirdDesignation.Text;

            session.Save(emp);
            session.Close();

        }

        public void Retrieve(int index)
        {

            tbFirstName.Text = Faves[index].FirstName;
            tbLastName.Text = Faves[index].LastName;
            tbDesignation.Text = Faves[index].Designation;
            tbSecondDesignation.Text = Faves[index].Designation;
            tbThirdDesignation.Text = Faves[index].ThirdDesignation;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (cbPreset.SelectedIndex == -1)
            {
                MessageBox.Show(@"You need to select a preset to remove first.", @"Message", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {

                if (MessageBox.Show(@"Are you sure you want to remove favourite '" + cbPreset.SelectedItem + @"'?",
                        @"Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    using (ISession session = NHibertnateSession.OpenSession())
                    {
                        using (ITransaction transaction = session.BeginTransaction())
                        {
                            var faveObject = Faves[cbPreset.SelectedIndex];
                            session.Delete(faveObject);
                            transaction.Commit();
                        }
                        Faves.RemoveAt(cbPreset.SelectedIndex);
                        cbPreset.Items.Clear();

                        Populate();

                        cbPreset.Text = "";
                    }
                }

            }
        }

        private void Populate() //repopulates combobox with newly saved objects
        {
            //remove old favourites before redrawing new list
            cbPreset.Items.Clear();

            using (ISession session = NHibertnateSession.OpenSession())
            {
                //get * from object
                IQuery query = session.CreateQuery("FROM Employee");
                Faves = query.List<Employee>();

            }

            if (Faves.Count > 0)
            {
                //fill favourite combobox with favourite objects
                for (int i = 0; i < Faves.Count; ++i)
                {
                    try
                    {
                        cbPreset.Items.Add(Faves[i].FirstName);
                    }
                    catch
                    {
                        MessageBox.Show(@"Something went wrong.");
                    }
                }

            }
            else
            {
                MessageBox.Show(@"Database contains " + Faves.Count + @" favourites.", @"Message", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void cbPreset_SelectedIndexChanged(object sender, EventArgs e)
        {
            Retrieve(cbPreset.SelectedIndex);
        }
    }
}

