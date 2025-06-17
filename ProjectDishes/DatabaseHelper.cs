using System;
using System.Windows.Forms;
using Supabase;
using Supabase.Gotrue;  
using Supabase.Postgrest;
using Supabase.Storage;   
using Newtonsoft.Json;
using System.Data;
using System.Threading.Tasks;

namespace ProjectDishes
{
    public static class DatabaseHelper
    {
        private const string SupabaseUrl = "https://mrwucqzxbhmxsyfgqgwn.supabase.co";
        private const string SupabaseKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im1yd3VjcXp4YmhteHN5ZmdxZ3duIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDk3MzA3MjgsImV4cCI6MjA2NTMwNjcyOH0.JR9e-Q3GH7QjI3DSOTAyR-Y9QeWJ5Dj71wWBvBoVWKw";

        private static readonly Supabase.Client Client;

        static DatabaseHelper()
        {
            Client = new Supabase.Client(SupabaseUrl, SupabaseKey);
            Client.InitializeAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public static async Task<bool> ExecuteNonQuery(string functionName, object parameters = null)
        {
            try
            {
                await Client.Rpc<dynamic>(functionName, parameters);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DatabaseHelper.ExecuteNonQuery", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public static async Task<DataTable> ExecuteQuery(string functionName, object parameters = null)
        {
            var dt = new DataTable();
            try
            {
                var result = await Client.Rpc<dynamic>(functionName, parameters);
                var token = JsonConvert.SerializeObject(result);
                dt = JsonConvert.DeserializeObject<DataTable>(token);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DatabaseHelper.ExecuteQuery", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }
        public static int ExecuteNonQueryWithReturnValue(string functionName, object parameters = null)
        {
            try
            {
                var result = Client.Rpc<dynamic>(functionName, parameters).GetAwaiter().GetResult();

                if (result is long l)
                    return (int)l;
                if (result is int i)
                    return i;
                return Convert.ToInt32(result?.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DatabaseHelper.ExecuteNonQueryWithReturnValue", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }
    }
}
