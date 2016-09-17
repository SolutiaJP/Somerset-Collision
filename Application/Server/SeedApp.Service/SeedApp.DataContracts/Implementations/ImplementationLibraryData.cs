using System;
using System.Data;
using SeedApp.DataContracts.Interfaces;

namespace SeedApp.DataContracts.Implementations
{
	[Serializable]
	public class ColumnMetaData : IColumnMetaData
	{
		public int ColumnOrdinal { get; set; }
		public String ColumnName { get; set; }
		public String BaseColumnName { get; set; }
		public SqlDbType SqlDataType { get; set; }
		public String DataType { get; set; }
		public String DataTypeName { get; set; }
		public Boolean AllowDbNull { get; set; }
		public Boolean IsIdentity { get; set; }
		public Boolean IsAutoIncrement { get; set; }
		public Boolean IsLong { get; set; }
		public Boolean IsReadOnly { get; set; }
	}
}
