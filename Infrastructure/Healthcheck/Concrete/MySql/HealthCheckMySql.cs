using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Data.Common;
using System.Diagnostics;
using System.Reflection;

namespace Infrastructure.Healthcheck.Concrete.MySql
{
    public class HealthCheckMySql : IHealthCheck
    {
        private readonly IHealtCheckMySqlHandler _databaseHandler;

        public HealthCheckMySql(IHealtCheckMySqlHandler mySqlHandler)
        {
            _databaseHandler = mySqlHandler;
        }

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {

            HealthStatus healthStatus = HealthStatus.Unhealthy;
            string desciption = null;
            try
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                var data = _databaseHandler.ExecuteQueryWithDapper<int>("SELECT 1;");

                stopwatch.Stop();
                if (data != null)
                {
                    if (stopwatch.ElapsedMilliseconds > 1000)
                    {
                        healthStatus = HealthStatus.Degraded;
                        desciption = "Up-state=Up;SELECT 1=1;fetch-time=" + stopwatch.ElapsedMilliseconds + " (too long);errors=no;warning=yes;details=;";
                    }
                    else if (data.HasStorageData)
                    {
                        bool convertedResponse = data.FirstRow==1;
                        if (convertedResponse)
                        {
                            healthStatus = HealthStatus.Healthy;
                            desciption = "Up-state=Up;SELECT 1=1;fetch-time=" + stopwatch.ElapsedMilliseconds + ";errors=no;warning=no;details=;";
                        }
                        else
                        {
                            desciption = "Up-state=Up;SELECT 1=?;fetch-time=" + stopwatch.ElapsedMilliseconds + ";errors=yes;warning=yes;details=convertion error or not equal than 1;";
                        }
                    }
                    else if (!data.HasStorageData || data.HasErrors)
                    {
                        desciption = "Up-state=Up;SELECT 1=?;fetch-time=" + stopwatch.ElapsedMilliseconds + ";errors=yes;warning=no;details=;";
                    }
                }
                /*Assembly currentAssembly = Assembly.GetExecutingAssembly();
                AssemblyName currentAssemblyName = currentAssembly.GetName();
                string currentWorkingDir = Directory.GetParent(currentAssembly.Location).FullName;
                string libFileName = "MySql.Data.dll";
                Version versionClient = AssemblyName.GetAssemblyName(Path.Combine(currentWorkingDir, libFileName)).Version;

                var dataVersion = _databaseHandler.ExecuteQueryWithDapper<string>("SELECT @@version;");
                if (dataVersion.HasStorageData)
                {
                    string versionServer = dataVersion.FirstRow.ToString();
                    bool versionCompare = versionClient.ToString().ToLower() != versionServer.ToLower();
                    desciption += "server-version=" + versionServer + ";client-version=" + versionClient.ToString() + ";version-compare=" + (versionCompare ? "mismatch (incombabilities may there)" : "match") + ";";
                    healthStatus = HealthStatus.Healthy;
                }
                else
                {
                    desciption += "server-version=n.a.;client-version=" + versionClient.ToString() + ";version-compare=mismatch (incombabilities may there);";
                    healthStatus = HealthStatus.Degraded;
                }*/
            }
            catch (DbException ex)
            {
                desciption = "Up-state=Down;SELECT 1=?;fetch-time=?;errors=yes;warning=no;details=" + ex.Message + ";";
                healthStatus = HealthStatus.Unhealthy;
            }
            return Task.FromResult(new HealthCheckResult(healthStatus, desciption));
        }
    }
}
