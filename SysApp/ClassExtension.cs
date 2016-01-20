using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
//using System.Data.Linq;
using System.ComponentModel;

namespace SysApp
{
        public class ClassExtension
        {
            private string iConnection = string.Format(@"Data Source=SPC160SRV;Initial Catalog=HROS2013;Persist Security Info=True;User ID=HROS2013;Password=hronline2013");
            public SqlConnection IConnection { get; set; }
            public ClassExtension()
            {
                IConnection = new SqlConnection(iConnection);
            }
            public ClassExtension(string _connectionString)
                : this()
            {
                IConnection = new SqlConnection(_connectionString);
            }
        }

        [DisplayName("EMPLWORKSCHEDULE")]
        public class WorkScheduleExtension : ClassExtension, IDisposable
        {
            private List<ShiftTablePiswinItem> listShiftTableItem = null;
            private List<DisplayItem> listDisplayItem = null;
            private DisplayItem DisplayItems
            {
                get { return new DisplayItem(); }
            }
            private string iConnect = "";
            public string IConnect
            {
                get { return iConnect; }
                set { iConnect = value; }
            }


            protected PropertyDescriptorCollection properties = null;
            public PropertyDescriptorCollection DisplayPropCollection
            {
                get { return TypeDescriptor.GetProperties(this.DisplayItems); }
            }

            protected DataTable piswinPwtime2 = null;
            public object PiswinPwtime2 { get { return piswinPwtime2; } }

            protected DataTable ivz_hrtaWorkSchedule = null;
            public object EmplWorkSchedule { get { return ivz_hrtaWorkSchedule; } }

            protected string emplId = string.Empty;
            public string EmplId { get { return emplId; } set { emplId = value; } }

            protected string altNum = string.Empty;
            public string AltNum { get { return altNum; } set { altNum = value; } }

            protected int yearNum;
            public int YearNum { set { yearNum = value; } }

            protected int monthNum;
            public int MonthNum { set { monthNum = value; } }

            protected WorkScheduleSource enumSource = WorkScheduleSource.Piswin7;

            public WorkScheduleExtension()
            {
                this.CreateObjectSchema();
                this.listDisplayItem = new List<DisplayItem>();
                this.listShiftTableItem = new List<ShiftTablePiswinItem>();
                this.LoadShiftTablePiswin();
            }
            public WorkScheduleExtension(string _connectString)
                : this()
            {
                iConnect = _connectString;
            }
            public WorkScheduleExtension(object _id, object _year, object _month, WorkScheduleSource _source)
                : this()
            {
                switch (_source)
                {
                    case WorkScheduleSource.AX:
                        emplId = _id.ToString();
                        break;
                    default:
                        altNum = _id.ToString();
                        break;
                }

                try
                {
                    yearNum = (_year == null) ? 0 : int.Parse(_year.ToString());
                    monthNum = (_month == null) ? 0 : int.Parse(_month.ToString());
                }
                catch (FormatException) { yearNum = 0; monthNum = 0; }
                catch (NullReferenceException) { yearNum = 0; monthNum = 0; }

                enumSource = _source;
            }

            protected void CreateObjectSchema()
            {
                piswinPwtime2 = new DataTable();
                piswinPwtime2.TableName = this.piswinPwtime2.ToString();
                ivz_hrtaWorkSchedule = new DataTable(this.DisplayItems.ToString());
                properties = TypeDescriptor.GetProperties(this.DisplayItems);
                foreach (PropertyDescriptor property in properties)
                {
                    ivz_hrtaWorkSchedule.Columns.Add(new DataColumn(property.Name, property.PropertyType));
                    //sb.AppendFormat(@"Name: {0}, Desc: {1}, Type: {2}", property.Name, property.Description, property.PropertyType.ToString());
                }

                listDisplayItem = new List<DisplayItem>();
            }
            internal object LoadShiftTablePiswin()
            {
                DataTable shiftTablePiswin = new DataTable("SHIFTTABLEPISWIN");

                string query =
                    string.Format(@"SELECT	RTRIM(PWTIME0) AS SHIFTID, RTRIM(PWDESC) AS SHIFTNAME
                                FROM	PWTIME0 WITH (NOLOCK)
                                WHERE	((PWTIME0 LIKE 'T[A-Z][A-Z]%')
		                                OR (PWTIME0 LIKE 'PT[A-Z][A-Z]%')
		                                OR (PWTIME0 LIKE 'T[AB][0-9]%')
		                                OR (PWTIME0 LIKE 'PT[AB][0-9]%'))
		                                AND (RTRIM(PWTIME0) != '')");
                try
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, base.IConnection.ConnectionString))
                    {
                        if (adapter.Fill(shiftTablePiswin) < 1)
                        {
                            listShiftTableItem = null;
                            return listShiftTableItem;
                        }
                        else
                        {
                            foreach (DataRow row in shiftTablePiswin.Rows)
                            {
                                listShiftTableItem.Add(
                                    new ShiftTablePiswinItem()
                                    {
                                        SHIFTIDs = row["SHIFTID"].ToString(),
                                        SHIFTNAMEs = row["SHIFTNAME"].ToString()
                                    });
                            }
                            return listShiftTableItem;
                        }
                    }
                }
                catch (Exception) { return listShiftTableItem; }
            }
            internal object LoadWorkSchedule()
            {
                object ret = null;
                using (SqlDataAdapter adapter = new SqlDataAdapter(
                    "SELECT *   FROM    PWTIME2 WITH (NOLOCK) WHERE (PWTIME0 = @PWTIME0) AND (PWYEAR = @PWYEAR) AND (PWMONTH = @PWMONTH)", base.IConnection.ConnectionString))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("PWTIME0", altNum);
                    adapter.SelectCommand.Parameters.AddWithValue("PWYEAR", yearNum);
                    adapter.SelectCommand.Parameters.AddWithValue("PWMONTH", monthNum);

                    if (adapter.Fill(piswinPwtime2) < 1)
                        ret = new List<DisplayItem>();
                    else
                        ret = TranspostItems(piswinPwtime2);
                }
                return ret;
            }
            protected object TranspostItems(object _object)
            {
                int day = 0;
                DataTable tableWorkSchd;
                DataTable schema;

                if (_object == null) return listDisplayItem;

                tableWorkSchd = (DataTable)_object;
                schema = getTableSchema("PWTIME2");
                for (var i = 0; i < tableWorkSchd.Rows.Count; i++)
                {
                    foreach (DataRow row in schema.Rows)
                    {
                        var collection = row.ItemArray.ToArray();

                        if (collection[1].ToString().StartsWith("PWTIME") && collection[1].ToString().EndsWith("1"))
                        {
                            DateTime transDate;
                            string date = string.Format(@"{0}-{1}-{2}",
                                int.Parse(tableWorkSchd.Rows[i]["PWYEAR"].ToString()),
                                int.Parse(tableWorkSchd.Rows[i]["PWMONTH"].ToString()),
                                ++day);
                            if (DateTime.TryParse(date, out transDate))
                            {
                                listDisplayItem.Add(new DisplayItem()
                                {
                                    ALTNUM = tableWorkSchd.Rows[i]["PWTIME0"].ToString(),
                                    YEARNUM = int.Parse(tableWorkSchd.Rows[i]["PWYEAR"].ToString()),
                                    MONTHNUM = int.Parse(tableWorkSchd.Rows[i]["PWMONTH"].ToString()),
                                    TRANSDATE = transDate,
                                    FIELDNAME = collection[1].ToString(),
                                    SHIFTID = tableWorkSchd.Rows[i][collection[1].ToString()].ToString(),
                                    SHIFTNAME = FindShiftName(tableWorkSchd.Rows[i][collection[1].ToString()].ToString().ToString()).ToString()
                                });
                            }
                        }
                    }
                    day = 0;
                }
                return listDisplayItem;
            }
            internal void RefreshProps()
            {
                this.CreateObjectSchema();
            }
            protected DataTable getTableSchema(string _tableName)
            {
                DataTable ret = new DataTable(string.Format(@"{0}_SCHEMA", _tableName));
                DataTable localTable = new DataTable(_tableName);
                var queryRun =
                    string.Format(@"SELECT
	                            [TABLE_NAME] = INFORMATION_SCHEMA.COLUMNS.TABLE_NAME,
	                            [COLUMN_NAME] = INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME,
	                            [DATA_TYPE] = INFORMATION_SCHEMA.COLUMNS.DATA_TYPE,
	                            [COLUMN_DESCRIPTION] = ISNULL(SYS.EXTENDED_PROPERTIES.VALUE, '')
                            FROM	INFORMATION_SCHEMA.COLUMNS
	                            LEFT OUTER JOIN	SYS.EXTENDED_PROPERTIES
	                            ON	SYS.EXTENDED_PROPERTIES.MAJOR_ID = OBJECT_ID(INFORMATION_SCHEMA.COLUMNS.TABLE_SCHEMA + '.' + INFORMATION_SCHEMA.COLUMNS.TABLE_NAME)
		                            AND SYS.EXTENDED_PROPERTIES.MINOR_ID = INFORMATION_SCHEMA.COLUMNS.ORDINAL_POSITION
		                            AND SYS.EXTENDED_PROPERTIES.NAME = 'MS_DESCRIPTION'
                            WHERE
	                            OBJECTPROPERTY(OBJECT_ID(INFORMATION_SCHEMA.COLUMNS.TABLE_SCHEMA + '.' + INFORMATION_SCHEMA.COLUMNS.TABLE_NAME), 'ISMSSHIPPED') = 0
	                            AND INFORMATION_SCHEMA.COLUMNS.TABLE_NAME = '{0}'
                            ORDER BY
	                            INFORMATION_SCHEMA.COLUMNS.TABLE_NAME, INFORMATION_SCHEMA.COLUMNS.ORDINAL_POSITION", _tableName);


                //using (SqlDataAdapter da = new SqlDataAdapter(queryRun, CalendarDemo.Properties.Resources.HROS2013))
                using (SqlDataAdapter da = new SqlDataAdapter(queryRun, base.IConnection.ConnectionString))
                {
                    if (da.Fill(ret) < 1)
                        ret = null;
                }

                return ret;
            }
            public enum WorkScheduleSource
            {
                AX = 0, Piswin7 = 1
            }
            internal virtual string FindShiftName(object _shiftId)
            {
                string ret = string.Empty;
                try
                {
                    var xFind = listShiftTableItem.Where(c => c.SHIFTIDs == _shiftId.ToString().Trim()).FirstOrDefault().SHIFTNAMEs;
                    return xFind;
                }
                catch (NullReferenceException) { return ret; }
            }

            #region IDisposable Members

            void IDisposable.Dispose()
            {
                GC.SuppressFinalize(this);
            }

            #endregion
        }
        [DisplayName("EMPLWORKSCHEDULETABLE"), Description("ตารางเวลาทำงาน")]
        public class DisplayItem
        {
            [Description("หมายเลขบัตร")]
            public string ALTNUM { get; set; }
            [Description("ปี")]
            public int YEARNUM { get; set; }
            [Description("เดือน")]
            public int MONTHNUM { get; set; }
            [Description("วันที่")]
            public DateTime TRANSDATE { get; set; }
            [Description("ฟิลด์"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public string FIELDNAME { get; set; }
            [Description("รหัสกะ")]
            public string SHIFTID { get; set; }
            private string shiftName = string.Empty;
            [Description("คำอธิบายกะ")]
            public string SHIFTNAME { get { return shiftName; } set { shiftName = value; } }

        }
        [DisplayName("SHIFTTABLE"), Description("ตารางแสดงรหัสกะ")]
        public class ShiftTablePiswinItem
        {
            [Description("รหัสกะ")]
            public string SHIFTIDs { get; set; }
            [Description("คำอธิบายกะ")]
            public string SHIFTNAMEs { get; set; }
        }

        public class AxShiftTable : ClassExtension
        {
            private AxShiftTableItem axShiftTableItem;
            public PropertyDescriptorCollection AxShiftTableItemPropCollection
            {
                get { return TypeDescriptor.GetProperties(this.axShiftTableItem); }
            }

            protected string iConnect = string.Empty;
            public string IConnect
            {
                get { return iConnect; }
                set { iConnect = value; }
            }

            private List<AxShiftTableItem> listAxShiftTableItem = null;
            public List<AxShiftTableItem> ListAXShiftTableItem
            {
                get { return listAxShiftTableItem; }
            }

            private DataTable ivz_HRTAShiftTable = null;
            public DataTable IVZ_HRTASHIFTTABLE
            {
                get { return ivz_HRTAShiftTable; }
            }

            public AxShiftTable(string _connectionString)
                : base(_connectionString)
            {
                IConnect = _connectionString;
                this.ivz_HRTAShiftTable = new DataTable("IVZ_HRTASHIFTTABLE");
                this.axShiftTableItem = new AxShiftTableItem();
                this.listAxShiftTableItem = new List<AxShiftTableItem>();
                this.LoadAXHRTAShiftTable();
            }

            void LoadAXHRTAShiftTable()
            {
                try
                {
                    string query =
                        string.Format(@"SELECT  *   FROM IVZ_HRTASHIFTTABLE WITH (NOLOCK) WHERE DATAAREAID = N'SPC'");
                    using (SqlDataAdapter da = new SqlDataAdapter(query, base.IConnection.ConnectionString))
                    {
                        if (da.Fill(ivz_HRTAShiftTable) < 1)
                        { }
                        else
                        {
                            //this.dataGridView3.DataSource = ivz_HrtaShiftTable.DefaultView;
                            foreach (DataRow row in ivz_HRTAShiftTable.Rows)
                            {
                                listAxShiftTableItem.Add(new AxShiftTableItem()
                                {
                                    SHIFTID = row["SHIFTID"].ToString(),
                                    NAME = row["NAME"].ToString(),
                                    VALIDSTARTTIME = row["VALIDSTARTTIME"].ToString(),
                                    STARTTIMEKEYIN = row["STARTTIMEKEYIN"].ToString(),
                                    ENDTIMEKEYIN = row["ENDTIMEKEYIN"].ToString(),
                                    WORKINGTIME = this.calcTimeHour24Diff(int.Parse(row["ENDTIMEKEYIN"].ToString()), int.Parse(row["STARTTIMEKEYIN"].ToString())),
                                    LATEINTIME = row["LATEINTIME"].ToString(),
                                    EARLYOUTTIME = row["EARLYOUTTIME"].ToString()
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }
            internal string calcTimeHour24(int _time)
            {
                string ret = string.Empty;
                var hour = string.Format("{0}", ((((_time - (_time % 60)) / 60) - (((_time - (_time % 60)) / 60) % 60)) / 60).ToString("D2"));
                var min = string.Format("{0}", (((_time - (_time % 60)) / 60) % 60).ToString("D2"));
                var second = string.Format("{0}", (_time % 60).ToString("D2"));
                ret = string.Format(@"{0}:{1}:{2}", hour, min, second);
                return ret;
            }
            internal string calcTimeHour24Diff(int _endTimeKeyIn, int _startTimeKeyIn)
            {
                string ret = string.Empty;
                int year = DateTime.Now.Year;
                int month = DateTime.Now.Month;
                int day = DateTime.Now.Day;

                DateTime transDate, dateStartKeyIn, dateEndKeyIn;
                transDate = new DateTime(year, month, day);

                dateEndKeyIn = DateTime.Parse(string.Format(@"{0} {1}", transDate.ToLongDateString(), calcTimeHour24(_endTimeKeyIn)));
                dateStartKeyIn = DateTime.Parse(string.Format(@"{0} {1}", transDate.ToLongDateString(), calcTimeHour24(_startTimeKeyIn)));

                if (_endTimeKeyIn < _startTimeKeyIn & _endTimeKeyIn >= 0)
                    dateEndKeyIn = dateEndKeyIn.AddDays(1);

                TimeSpan sp = dateEndKeyIn.Subtract(dateStartKeyIn);
                ret = sp.Ticks.ToString();
                ret = new DateTime(sp.Ticks).ToLongTimeString();
                return ret;
            }
            internal int convertToTimeHour24(int _hour, int _minute, int _second)
            {
                int ret;
                int hourTime, minuteTime, secondTime;

                hourTime = _hour * 3600;
                minuteTime = _minute * 60;
                secondTime = _second;
                ret = hourTime + minuteTime + secondTime;
                return ret;
            }
            public List<AxShiftTableItem> Find(string _id)
            {
                var findItems = listAxShiftTableItem.Where(c => c.SHIFTID.Contains(_id.ToString())).ToList();
                return findItems;
            }
            public List<AxShiftTableItem> FindByTimeKeyInTimeKeyOut(object _timeKeyIn, object _timeKeyOut)
            {
                //var findItems = listAxShiftTableItem.Where(c => c.SHIFTID.Contains(_id.ToString())).ToList();
                var findItems = listAxShiftTableItem.Where(
                            c => (c.STARTTIMEKEYIN.Contains(_timeKeyIn.ToString().Trim().Replace(".", ":")))
                            && (c.ENDTIMEKEYIN.Contains(_timeKeyOut.ToString().Trim().Replace(".", ":")))).ToList();

                return findItems;
            }
        }
        public class AxShiftTableItem
        {
            [Description("รหัสกะ")]
            public string SHIFTID { get; set; }
            [Description("ชื่อ")]
            public string NAME { get; set; }
            private string validStartTime = string.Empty;
            [Description("จุดตัดวัน")]
            public string VALIDSTARTTIME
            {
                get { return validStartTime; }
                set { validStartTime = calcTimeHour24(int.Parse(value)); }
            }
            private string startTimeKeyIn = string.Empty;
            [Description("เริ่มต้นเวลา")]
            public string STARTTIMEKEYIN
            {
                get { return startTimeKeyIn; }
                set { startTimeKeyIn = calcTimeHour24(int.Parse(value)); }
            }
            private string endTimeKeyIn = string.Empty;
            [Description("สิ้นสุดเวลา")]
            public string ENDTIMEKEYIN
            {
                get { return endTimeKeyIn; }
                set { endTimeKeyIn = calcTimeHour24(int.Parse(value)); }
            }
            private string workingTime = string.Empty;
            [Description("จำนวนชั่วโมงต่อวัน")]
            public string WORKINGTIME
            {
                get { return workingTime; }
                set { workingTime = value; }
            }
            private string lateInTime = string.Empty;
            [Description("เข้าสาย")]
            public string LATEINTIME
            {
                get { return lateInTime; }
                set { lateInTime = calcTimeHour24(int.Parse(value)); }
            }
            private string earlyOutTime = string.Empty;
            [Description("ออกก่อน")]
            public string EARLYOUTTIME
            {
                get { return earlyOutTime; }
                set { earlyOutTime = calcTimeHour24(int.Parse(value)); }
            }

            internal string calcTimeHour24(int _time)
            {
                string ret = string.Empty;
                var hour = string.Format("{0}", ((((_time - (_time % 60)) / 60) - (((_time - (_time % 60)) / 60) % 60)) / 60).ToString("D2"));
                var min = string.Format("{0}", (((_time - (_time % 60)) / 60) % 60).ToString("D2"));
                var second = string.Format("{0}", (_time % 60).ToString("D2"));
                ret = string.Format(@"{0}:{1}:{2}", hour, min, second);
                //MessageBox.Show(ret);
                return ret;
            }
            internal int convertToTimeHour24(int _hour, int _minute, int _second)
            {
                int ret;
                int hourTime, minuteTime, secondTime;

                hourTime = _hour * 3600;
                minuteTime = _minute * 60;
                secondTime = _second;
                ret = hourTime + minuteTime + secondTime;
                return ret;
            }
        }
}
