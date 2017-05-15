using OrmBenchmark.Core;
using Mighty;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmBenchmark.MightyORM
{
    public class MightyORMExecuter : IOrmExecuter
    {
		Mighty.MightyORM modelDynamic;
		DbConnection connectionDynamic;

		MightyORM<Post> modelGeneric;
		DbConnection connectionGeneric;

		public string Name
        {
            get
            {
                return "MightyORM";
            }
        }

        public void Init(string connectionString)
        {
			modelDynamic = new Mighty.MightyORM(connectionString + ";ProviderName=System.Data.SqlClient");
			connectionDynamic = modelDynamic.OpenConnection();
			modelGeneric = new MightyORM<Post>(connectionString + ";ProviderName=System.Data.SqlClient");
			connectionGeneric = modelGeneric.OpenConnection();
		}

		public IPost GetItemAsObject(int Id)
        {
            return modelGeneric.Query("select * from Posts where Id=@0", connectionGeneric, Id).First();
        }
        
        public dynamic GetItemAsDynamic(int Id)
        {
            return modelDynamic.Query("select * from Posts where Id=@0", connectionDynamic, Id).First();
        }

        public IList<IPost> GetAllItemsAsObject()
        {
            return modelGeneric.Query("select * from Posts", connectionGeneric).ToList<IPost>();
        }

        public IList<dynamic> GetAllItemsAsDynamic()
        {
            return modelDynamic.Query("select * from Posts", connectionDynamic).ToList();
        }

        public void Finish()
        {
			connectionDynamic.Close();
			connectionGeneric.Close();
		}

	}
}
