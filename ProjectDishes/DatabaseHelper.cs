using System;
using System.Windows.Forms;
using Supabase;
using Supabase.Gotrue;  
using Supabase.Postgrest;
using Supabase.Storage;   
using Newtonsoft.Json;
using System.Data;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Http;

namespace ProjectDishes
{
    public static class DatabaseHelper
    {
        private const string SupabaseUrl = "https://mrwucqzxbhmxsyfgqgwn.supabase.co";
        private const string SupabaseKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im1yd3VjcXp4YmhteHN5ZmdxZ3duIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDk3MzA3MjgsImV4cCI6MjA2NTMwNjcyOH0.JR9e-Q3GH7QjI3DSOTAyR-Y9QeWJ5Dj71wWBvBoVWKw";
        private static Supabase.Client Client;
        private static bool _offlineMessageShown = false;

        private static bool HasInternet()
        {
            try
            {
                using (var ping = new Ping())
                {
                    var reply = ping.Send("8.8.8.8", 1000);
                    return reply.Status == IPStatus.Success;
                }
            }
            catch
            {
                return false;
            }
        }
        static DatabaseHelper()
        {
            if (!HasInternet())
            {
                if (!_offlineMessageShown)
                {
                    MessageBox.Show("Отсутствует подключение к интернету.", "Предупреждение",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    _offlineMessageShown = true;
                }
                return;
            }
            else
            {
                try
                {
                    Client = new Supabase.Client(SupabaseUrl, SupabaseKey);
                    Client.InitializeAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                    _offlineMessageShown = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка инициализации DatabaseHelper", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public static async Task<bool> ExecuteNonQuery(string functionName, object parameters = null)
        {
            if (Client == null && HasInternet())
            {
                try
                {
                    Client = new Supabase.Client(SupabaseUrl, SupabaseKey);
                    Client.InitializeAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                    _offlineMessageShown = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка инициализации DatabaseHelper",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            if (!HasInternet() || Client == null)
            {
                if (!_offlineMessageShown)
                {
                    MessageBox.Show("Отсутствует подключение к интернету.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _offlineMessageShown = true;
                }
                return false;
            }
            else
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
        }
        public static async Task<DataTable> ExecuteQuery(string functionName, object parameters = null)
        {
            if (Client == null && HasInternet())
            {
                try
                {
                    Client = new Supabase.Client(SupabaseUrl, SupabaseKey);
                    Client.InitializeAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                    _offlineMessageShown = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка инициализации DatabaseHelper", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new DataTable();
                }
            }
            var dt = new DataTable();
            if (!HasInternet() || Client == null)
            { 
                if (!_offlineMessageShown)
                {
                    MessageBox.Show("Отсутствует подключение к интернету.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _offlineMessageShown = true;
                }
                return dt;
            }
            else
            {
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
        }
        public static async Task<decimal?> ExecuteRpcScalarAsync(string functionName, object parameters = null)
        {
            if (Client == null && HasInternet())
            {
                try
                {
                    Client = new Supabase.Client(SupabaseUrl, SupabaseKey);
                    Client.InitializeAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                    _offlineMessageShown = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка инициализации DatabaseHelper", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            if (!HasInternet() || Client == null)
            {
                if (!_offlineMessageShown)
                {
                    MessageBox.Show("Отсутствует подключение к интернету.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _offlineMessageShown = true;
                }
                return null;
            }
            else
            {
                try
                {
                    var result = await Client.Rpc<decimal?>(functionName, parameters);
                    return result;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "DatabaseHelper.ExecuteRpcScalarAsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
        }
        public static int ExecuteNonQueryWithReturnValue(string functionName, object parameters = null)
        {
            if (Client == null && HasInternet())
            {
                try
                {
                    Client = new Supabase.Client(SupabaseUrl, SupabaseKey);
                    Client.InitializeAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                    _offlineMessageShown = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка инициализации DatabaseHelper", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
            }
            if (!HasInternet() || Client == null)
            {
                if (!_offlineMessageShown)
                {
                    MessageBox.Show("Отсутствует подключение к интернету.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _offlineMessageShown = true;
                }
                return -1;
            }
            else
            {
                try
                {
                    var result = Client.Rpc<dynamic>(functionName, parameters).GetAwaiter().GetResult();
                    if (result is long l)
                    {
                        return (int)l;
                    }
                    if (result is int i)
                    {
                        return i;
                    }
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
}
