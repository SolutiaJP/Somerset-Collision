using System;
using System.Data;


namespace SeedAppTenant.DataContracts.Interfaces
{
	public interface IColumnMetaData
	{
		int ColumnOrdinal { get; set; }
		String ColumnName { get; set; }
		String BaseColumnName { get; set; }
		SqlDbType SqlDataType { get; set; }
		String DataType { get; set; }
		String DataTypeName { get; set; }
		Boolean AllowDbNull { get; set; }
		Boolean IsIdentity { get; set; }
		Boolean IsAutoIncrement { get; set; }
		Boolean IsLong { get; set; }
		Boolean IsReadOnly { get; set; }
	}
}
