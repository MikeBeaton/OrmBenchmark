using OrmBenchmark.Core;
using Massive;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmBenchmark.Massive
{
	public class MassiveExecuter : IOrmExecuter
	{
		DynamicModel modelDynamic;
		DbConnection connectionDynamic;

		public string Name
		{
			get
			{
				return "Massive";
			}
		}

		public void Init(string connectionString)
		{
			modelDynamic = new DynamicModel(connectionString);
			connectionDynamic = modelDynamic.OpenConnection();
		}

		public IPost GetItemAsObject(int Id)
		{
			return null;
		}

		public dynamic GetItemAsDynamic(int Id)
		{
			return modelDynamic.Query("select * from Posts where Id=@0", connectionDynamic, Id).First();
		}

		public IList<IPost> GetAllItemsAsObject()
		{
			return null;
		}

		public IList<dynamic> GetAllItemsAsDynamic()
		{
			return modelDynamic.Query("select * from Posts", connectionDynamic).ToList();
		}

		public void Finish()
		{
			connectionDynamic.Close();
		}

	}
}
