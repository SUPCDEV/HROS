using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using Telerik;
using Telerik.WinControls.UI;

namespace SysApp
{
    public static class MiscFunctions
    {
        public static void initializeGridViewCell(ref DataGridView _datagridview, string _tableName)
        {
            DataGridViewTextBoxColumn cell;
            //GridViewTextBoxColumn cell;
            //GridViewComboBoxColumn cellComboBox;
            DataTable tableSchema = getTableSchema(_tableName);
            foreach (DataRow row in tableSchema.Rows)
            {
                cell = new DataGridViewTextBoxColumn();
                cell.Name = row["COLUMN_NAME"].ToString();
                cell.DataPropertyName = row["COLUMN_NAME"].ToString();
                cell.HeaderText = row["COLUMN_DESCRIPTION"].ToString();
                cell.Visible = true;
                cell.ReadOnly = true;
                cell.ValueType = typeOf(row["DATA_TYPE"].ToString());
                _datagridview.Columns.Add(cell);
            }
        }
        public static void initializeRadgridCell(ref RadGridView _radgrid, string _tableName)
        {
            Telerik.WinControls.UI.GridViewTextBoxColumn cell;
            Telerik.WinControls.UI.GridViewCheckBoxColumn cellCb;
            var schema = getTableSchema(_tableName);
            foreach (DataRow row in schema.Rows)
            {
                if (!IsCheckboxCellType(row["COLUMN_NAME"].ToString().ToUpper()))
                {
                    cellCb = new GridViewCheckBoxColumn();
                    cellCb.Name = row["COLUMN_NAME"].ToString();
                    cellCb.FieldName = row["COLUMN_NAME"].ToString();
                    cellCb.HeaderText = row["COLUMN_DESCRIPTION"].ToString();
                    cellCb.IsVisible = true;
                    cellCb.ReadOnly = true;
                    cellCb.DataType = typeOf(row["DATA_TYPE"].ToString());
                    cellCb.ThreeState = false;
                    cellCb.BestFit();
                    _radgrid.MasterTemplate.Columns.Add(cellCb);
                }
                else
                {
                    cell = new GridViewTextBoxColumn();
                    cell.Name = row["COLUMN_NAME"].ToString();
                    cell.FieldName = row["COLUMN_NAME"].ToString();
                    cell.HeaderText = row["COLUMN_DESCRIPTION"].ToString();
                    cell.IsVisible = true;
                    cell.ReadOnly = true;
                    cell.DataType = typeOf(row["DATA_TYPE"].ToString());//getType(row["DATA_TYPE"].ToString());
                    //cell.FormatString = (cell.DataType == typeof(decimal)) ? "{0:N2}" : (cell.DataType == typeof(DateTime) && row["COLUMN_NAME"].ToString() == "") ? "{0:dd/MM/yyyy}" : "{0}";
                    cell.FormatString = getCellFormatString(cell.DataType, cell.FieldName);
                    cell.BestFit();
                    _radgrid.MasterTemplate.Columns.Add(cell);
                }
            }
            //return _radgrid;
        }
        internal static bool IsCheckboxCellType(string _fieldName)
        {
            List<string> localField = new List<string> { "HDAPPROVED", "HRAPPROVED" };
            var ret = localField.Find(delegate(string s) { return s.ToString() == _fieldName; });
            return string.IsNullOrEmpty(ret);
        }
        public static void setRadGridCellVisible(ref  Telerik.WinControls.UI.RadGridView _radgrid, List<string> _listCellName, bool _isVisible)
        {
            foreach (var col in _radgrid.Columns)
            {
                if (_listCellName.FindIndex(delegate(string o) { return o == col.Name; }) > -1)
                    col.IsVisible = _isVisible;
            }
        }
        public static void setRadGridCellReadOnly(ref  Telerik.WinControls.UI.RadGridView _radgrid, List<string> _listCellName, bool _isReadOnly)
        {
            foreach (var col in _radgrid.Columns)
            {
                if (_listCellName.FindIndex(delegate(string o) { return o == col.Name; }) > -1)
                    col.ReadOnly = _isReadOnly;
            }
        }
        public static void setVisibleInColumnChooser(ref  Telerik.WinControls.UI.RadGridView _radgrid, List<string> _listCellName, bool _isVisibleInColChooser)
        {
            foreach (var col in _radgrid.Columns)
            {
                if (_listCellName.FindIndex(delegate(string o) { return o == col.Name; }) > -1)
                    col.VisibleInColumnChooser = _isVisibleInColChooser;
            }
        }
        
        public static DataTable getTableSchema(string _tableName)
        {
            DataTable ret = new DataTable(string.Format(@"{0}_SCHEMA", _tableName));
            DataTable localTable = new DataTable(_tableName);
            //            var queryRun =
            //                    string.Format(@"SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, TABLE_NAME,ORDINAL_POSITION, IS_NULLABLE 
            //                    FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE '{0}'
            //                    ORDER BY ORDINAL_POSITION", _tableName);

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


            using (SqlDataAdapter da = new SqlDataAdapter(queryRun, DatabaseConfig.ServerConStr))
            {
                if (da.Fill(ret) < 0)
                    ret = null;
                //foreach (DataRow row in ret.Rows)
                //{
                //    localTable.Columns.Add(new DataColumn(row["COLUMN_NAME"].ToString(), getType(row["DATA_TYPE"].ToString())));
                //}
            }

            return ret;
        }
        public static System.Type typeOf(object _type)
        {
            switch (_type.ToString())
            {
                case "bigint": return typeof(long);
                case "bit": return typeof(bool);
                case "char": return typeof(char);
                case "datetime": return typeof(DateTime);
                case "decimal": return typeof(decimal);
                case "float": return typeof(double);
                case "int": return typeof(int);
                case "money": return typeof(decimal);
                case "nchar":
                case "ntext":
                case "nvarchar":
                    return typeof(string);
                case "numeric":
                    return typeof(decimal);
                case "real":
                    return typeof(float);
                case "smalldatetime": return typeof(DateTime);
                case "smallint": return typeof(short);
                case "smallmoney": return typeof(decimal);
                case "sql_variant":
                case "sysname":
                case "text":
                    return typeof(string);
                case "timestamp": return typeof(DateTime);
                case "varchar":
                case "uniqueidentifier":
                    return typeof(string);
                case "binary":
                case "tinyint":
                case "varbinary":
                default:
                    return typeof(object);
            }
        }
        internal static string getCellFormatString(Type _type, string _fieldName)
        {
            if (_type == typeof(decimal))
                return "{0:N2}";
            if (_type == typeof(System.DateTime) && !IsSpecialFieldFormat(_fieldName))
                return "{0:dd/MM/yyyy}";
            else
                return "{0}";
        }
        internal static bool IsSpecialFieldFormat(string _fieldName)
        {
            List<string> localField = new List<string> { "TRANSDATE", "SHIFTDATE", "VALIDFROM", "VALIDFROMDATE", "VALIDTO", "VALIDTODATE", "STARTDATE", "ENDDATE", "EMPLOYMENTDATE", "RESIGNATIONDATE" };
            var ret = localField.Find(delegate(string s) { return s.ToString() == _fieldName; });
            return string.IsNullOrEmpty(ret);
        }
    }
}
