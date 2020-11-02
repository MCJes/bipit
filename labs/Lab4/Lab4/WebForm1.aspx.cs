using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Data;

namespace Lab4
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        WebService1 ws = new WebService1();
        protected void Page_Load(object sender, EventArgs e)
        {
            addPanel.Visible = false;
            table.Visible = true;
            if (!IsPostBack)
            {
                selectAuto.DataSource = ws.GetAutos();
                selectAuto.DataBind();
                selectWork.DataSource = ws.GetWorks();
                selectWork.DataBind();
                bindTable(ws.GetData());
            }
            
        }

        public void bindTable(Dictionary<int, List<object>> rows)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(string));
            dt.Columns.Add("NumOfAuto", typeof(string));
            dt.Columns.Add("Producer", typeof(string));
            dt.Columns.Add("Model", typeof(string));
            dt.Columns.Add("Work", typeof(string));
            dt.Columns.Add("Cost", typeof(string));
            dt.Columns.Add("MaxWorkTime", typeof(string));
            dt.Columns.Add("ServiceTime", typeof(string));
            dt.Columns.Add("ServiceDate", typeof(string));

            foreach (var row in rows.Values)
            {
                DataRow newRow = dt.NewRow();
                newRow[0] = (string)row[0];
                newRow[1] = (string)row[1];
                newRow[2] = (string)row[2];
                newRow[3] = (string)row[3];
                newRow[4] = (string)row[4];
                newRow[5] = (string)row[5];
                newRow[6] = (string)row[6];
                newRow[7] = (string)row[7];
                newRow[8] = ((string)row[8]).Split(' ')[0];
                dt.Rows.Add(newRow);
            }

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void LinkList_Click(object sender, EventArgs e)
        {
            addPanel.Visible = false;
            table.Visible = true;
        }

        protected void LinkAdd_Click(object sender, EventArgs e)
        {
            addPanel.Visible = true;
            table.Visible = false;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //GridView1.
            foreach(TableRow row in GridView1.Rows)
            {
                if(((CheckBox)row.Cells[0].Controls[1]).Checked)
                {
                    ws.Delete(Convert.ToInt32(row.Cells[1].Text));
                }
            }
            bindTable(ws.GetData());
        }

        protected void btnShowByDate_Click(object sender, EventArgs e)
        {
            if (dateFrom.Value == "" || dateEnd.Value == "")
            {
                bindTable(ws.GetData());
                return;
            }
            var from = dateFrom.Value.Split('-');
            string newFrom = $"{from[1]}.{from[2]}.{from[0]}";
            var end = dateEnd.Value.Split('-');
            string newEnd = $"{end[1]}.{end[2]}.{end[0]}";
            bindTable(ws.GetDataBetween(newFrom, newEnd));
            dateFrom.Value = "";
            dateEnd.Value = "";
        }

        protected void Addbutton_Click(object sender, EventArgs e)
        {
            Regex rgx = new Regex(@"(\d\d:\d\d:\d\d)");
            string dates = workDate.Value;
            var date = dates.Split('-');
            string newDate = $"{date[1]}.{date[2]}.{date[0]}";
            if (rgx.IsMatch(workTime.Value))
            {
                var time = workTime.Value.Split(':');
                if (Convert.ToInt32(time[0]) < 24 || Convert.ToInt32(time[1]) < 60 || Convert.ToInt32(time[3]) < 60)
                    ws.AddNewService(selectAuto.Text, selectWork.Text, workTime.Value, newDate);
                bindTable(ws.GetData());
            }
        }
    }
}