using Base.CrossCuttingConcerns.Serilog.ConfigurationModels;
using Base.CrossCuttingConcerns.Serilog.Messages;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace Base.CrossCuttingConcerns.Serilog.Logger
{
    public class MsSqlLogger : LoggerServiceBase
    {

        //https://github.com/serilog-mssql/serilog-sinks-mssqlserver
        public MsSqlLogger(IConfiguration configuration)
        {
            MsSqlConfiguration logConfiguration =
                configuration.GetSection("SeriLogConfigurations:MsSqlConfiguration").Get<MsSqlConfiguration>()
                ?? throw new Exception(SerilogMessages.NullOptionsMessage);

            MSSqlServerSinkOptions sinkOptions = new()
            {
                TableName = logConfiguration.TableName,
                AutoCreateSqlDatabase = logConfiguration.AutoCreateSqlTable
            };

            ColumnOptions columnOptions = new();

            global::Serilog.Core.Logger seriLogConfig = new LoggerConfiguration().WriteTo
                .MSSqlServer(logConfiguration.ConnectionString, sinkOptions, columnOptions: columnOptions)
                .CreateLogger();

            Logger = seriLogConfig;
        }
    }
}
