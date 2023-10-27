using Infrastructure;
using Infrastructure.Healthcheck.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Reflection;

namespace Application.Shared.Kernel.Web.AspNet.Healthcheck
{
    public class HealthCheckMySql : AbstractHealthCheck<ApplicationDbContext, Task<HealthCheckResult>>
    {
        public static Func<ApplicationDbContext, Task<HealthCheckResult>> CheckMySqlBackend = new Func<ApplicationDbContext, Task<HealthCheckResult>>((databaseService) =>
        {
            HealthStatus healthStatus = HealthStatus.Unhealthy;
            string desciption = null;
            try
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                var data = databaseService.Users.FromSqlRaw("SELECT 1;");

                stopwatch.Stop();
                if (data != null)
                {
                    if (stopwatch.ElapsedMilliseconds > 1000)
                    {
                        healthStatus = HealthStatus.Degraded;
                        desciption = "Up-state=Up;SELECT 1=1;fetch-time=" + stopwatch.ElapsedMilliseconds + " (too long);errors=no;warning=yes;details=;";
                    }
                    else if (data.HasData)
                    {
                        bool convertedResponse = int.TryParse(data.Data.Rows[0][0].ToString(), out int number);
                        if (number == 1 && convertedResponse)
                        {
                            healthStatus = HealthStatus.Healthy;
                            desciption = "Up-state=Up;SELECT 1=1;fetch-time=" + stopwatch.ElapsedMilliseconds + ";errors=no;warning=no;details=;";
                        }
                        else
                        {
                            desciption = "Up-state=Up;SELECT 1=?;fetch-time=" + stopwatch.ElapsedMilliseconds + ";errors=yes;warning=yes;details=convertion error or not equal than 1;";
                        }
                    }
                    else if (!data.HasData || data.HasErrors)
                    {
                        desciption = "Up-state=Up;SELECT 1=?;fetch-time=" + stopwatch.ElapsedMilliseconds + ";errors=yes;warning=no;details=;";
                    }
                }
                Assembly currentAssembly = Assembly.GetExecutingAssembly();
                AssemblyName currentAssemblyName = currentAssembly.GetName();
                string currentWorkingDir = Directory.GetParent(currentAssembly.Location).FullName;
                string libFileName = "MySql.Data.dll";
                Version versionClient = AssemblyName.GetAssemblyName(Path.Combine(currentWorkingDir, libFileName)).Version;

                QueryResponseData dataVersion = databaseService.ExecuteQuerySync("SELECT @@version;");
                if (dataVersion.HasData)
                {
                    string versionServer = dataVersion.Data.Rows[0][0].ToString();
                    bool versionCompare = versionClient.ToString().ToLower() != versionServer.ToLower();
                    desciption += "server-version=" + versionServer + ";client-version=" + versionClient.ToString() + ";version-compare=" + (versionCompare ? "mismatch (incombabilities may there)" : "match") + ";";
                    healthStatus = versionCompare ? HealthStatus.Healthy : HealthStatus.Degraded;
                }
                else
                {
                    desciption += "server-version=n.a.;client-version=" + versionClient.ToString() + ";version-compare=mismatch (incombabilities may there);";
                    healthStatus = HealthStatus.Degraded;
                }
            }
            catch (MySqlException ex)
            {
                desciption = "Up-state=Down;SELECT 1=?;fetch-time=?;errors=yes;warning=no;details=" + ex.Message + ";";
                healthStatus = HealthStatus.Unhealthy;
            }
            return Task.FromResult(new HealthCheckResult(healthStatus, desciption));
        });

        private readonly ApplicationDbContext _databaseHandler;
        public HealthCheckMySql(Func<ApplicationDbContext, Task<HealthCheckResult>> func, ApplicationDbContext databaseHandler) : base(func)
        {
            _databaseHandler = databaseHandler;
        }
        public override Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return (Task<HealthCheckResult>)CheckHealthAction.DynamicInvoke(new object[] { _databaseHandler });
        }
    }
}
