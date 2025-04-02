using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace worklist_server
{
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public static class ConfigManager
    {
        private const string CONFIG_FILE = "config_system.json";
        private static ConfigSettings _settings;
        private static readonly object _lock = new object();  // Thêm từ khóa 'object'
        private const string RegistryPathDefaut = @"SOFTWARE\DICOM_Worklist_Server";
        private const string InstallDateKeyDefaut = "InstallDate";

        public static ConfigSettings Settings
        {
            get
            {
                lock (_lock)
                {
                    return _settings ??= LoadOrCreateConfig();
                }
            }
        }

        private static ConfigSettings LoadOrCreateConfig()
        {
            try
            {
                if (File.Exists(CONFIG_FILE))
                {
                    string json = File.ReadAllText(CONFIG_FILE);
                    return JsonConvert.DeserializeObject<ConfigSettings>(json) ?? CreateDefaultConfig();
                }
            }
            catch (Exception ex)
            {
                // Log error if needed
                Console.WriteLine($"Error loading config: {ex.Message}");
            }
            return CreateDefaultConfig();
        }

        private static ConfigSettings CreateDefaultConfig()
        {
            return new ConfigSettings
            {
                Registry = new RegistryConfig
                {
                    AppName = "WorklistApp",
                    Version = "1.0.0",
                    RegistryPath = RegistryPathDefaut,
                    InstallDateKey= InstallDateKeyDefaut
                },
                Database = new DatabaseConfig
                {
                    ConnectionString = "", // Mã hóa tự động khi lưu
                    SelectedDatabase = "SQLServer"
                },
                Logging = new LoggingConfig
                {
                    LogLevel = "Information",
                    EnableFileLog = true
                },
                Feature = new FeatureConfig
                {
                    IsEnableNotice = true
                }
            };
        }

        public static void SaveConfig()
        {
            lock (_lock)
            {
                string json = JsonConvert.SerializeObject(Settings, Formatting.Indented);
                File.WriteAllText(CONFIG_FILE, json);
            }
        }
    }

    public static class ConfigSecurity
    {
        public static string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) return "";
            byte[] encrypted = ProtectedData.Protect(
                Encoding.UTF8.GetBytes(plainText),
                null,
                DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encrypted);
        }

        public static string Decrypt(string encryptedText)
        {
            if (string.IsNullOrEmpty(encryptedText)) return "";
            byte[] decrypted = ProtectedData.Unprotect(
                Convert.FromBase64String(encryptedText),
                null,
                DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(decrypted);
        }
    }

    public class ConfigSettings
    {
        public RegistryConfig Registry { get; set; }
        public DatabaseConfig Database { get; set; }
        public FeatureConfig Feature { get; set; } 
        public LoggingConfig Logging { get; set; } 
    }

    // Các lớp con cho từng nhóm cấu hình
    public class RegistryConfig
    {
        public string AppName { get; set; }
        public string Version { get; set; }

        [JsonProperty]
        private string _RegistryPath;

        [JsonIgnore]
        public string RegistryPath
        {
            get => ConfigSecurity.Decrypt(_RegistryPath);
            set => _RegistryPath = ConfigSecurity.Encrypt(value);
        }
        [JsonProperty]
        private string _InstallDateKey;

        [JsonIgnore]
        public string InstallDateKey
        {
            get => ConfigSecurity.Decrypt(_InstallDateKey);
            set => _InstallDateKey = ConfigSecurity.Encrypt(value);
        }
    }

    public class DatabaseConfig
    {
        [JsonProperty]
        private string _connectionString;

        [JsonIgnore]
        public string ConnectionString
        {
            get => ConfigSecurity.Decrypt(_connectionString);
            set => _connectionString = ConfigSecurity.Encrypt(value);
        }
        public string SelectedDatabase { get; set; }
    }

    public class LoggingConfig
    {
        public string LogLevel { get; set; }
        public bool EnableFileLog { get; set; }
    }
    public class FeatureConfig
    {
        public bool IsEnableNotice { get; set; }

    }
}
