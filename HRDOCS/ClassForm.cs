using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HRDOCS
{
    class ClassForm
    {
        public static Form ActiveForm(Form activeFrm, Form mdiFrm, string caption)
        {
            foreach (Form childFrm in mdiFrm.MdiChildren)
            {
                if (childFrm.GetType() == activeFrm.GetType())
                {
                    childFrm.Activate();
                    return childFrm;
                }
            }

            //activeFrm.Icon = Properties.Resources.bullet_ball_glass_green;
            activeFrm.MdiParent = mdiFrm;
            activeFrm.WindowState = FormWindowState.Maximized;
            //activeFrm.Size = new System.Drawing.Size(780, 500);
            activeFrm.Text = caption;
            activeFrm.BackColor = System.Drawing.Color.White;
            //activeFrm.Icon = Properties.Resources.bullet_ball_glass_red;
            activeFrm.StartPosition = FormStartPosition.CenterParent;
            activeFrm.Show();

            return activeFrm;
        }

        public static DialogResult ShowDialog(Form frm)
        {
            //frm.Icon = Properties.Resources.bullet_ball_glass_blue;
            frm.WindowState = FormWindowState.Normal;
            frm.StartPosition = FormStartPosition.CenterParent;

            if (frm.MaximizeBox)
            {
                //frm.Size = new System.Drawing.Size(780, 500);
            }

            return frm.ShowDialog();
        }

    }
}
