using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SharpCompress;
using System.Collections;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Healthcheck.Response
{
    public static class HealthCheckResponseWriter
    {

        public static async Task<Task> WriteResponse(HttpContext context, HealthReport result)
        {
            context.Response.ContentType = "application/json";

            var options = new JsonWriterOptions
            {
                Indented = true
            };

            Process currentProcess = null;
            HealthCheckResponseObject healthCheckResponseObject = new HealthCheckResponseObject();
            ConcurrentDictionary<int, ProcessUsage> usages = new ConcurrentDictionary<int, ProcessUsage>();
            //List<Task> getCpuUsageFromProcesses = new List<Task>();
            try
            {
                foreach (Process proc in Process.GetProcesses())
                {
                    if (proc.Id == healthCheckResponseObject.ProcessId)
                    {
                        currentProcess = proc;
                    }
                    //var task = GetProcessUsage(proc, usages);
                    //getCpuUsageFromProcesses.Add(task);
                }
                //Task.WaitAll(getCpuUsageFromProcesses.ToArray());
                var processUsage = await GetProcessUsage(currentProcess);
                //healthCheckResponseObject.ProcessorUsageAll = usages.Values.Select(x => x.CpuUsage).Aggregate((current, usage) => current + usage);
                healthCheckResponseObject.EnvironmentVars = Environment.GetEnvironmentVariables()
                                                                .Cast<DictionaryEntry>()
                                                                .ToDictionary(
                                                                    entry => entry.Key.ToString(),
                                                                    entry => entry.Value.ToString()
                                                                );
                healthCheckResponseObject.RamSystem = GC.GetGCMemoryInfo().TotalAvailableMemoryBytes / 1024.0 / 1024.0;
                healthCheckResponseObject.RamUsage = processUsage.RamUsage;
                healthCheckResponseObject.ProcessStartTime = currentProcess.StartTime;
                healthCheckResponseObject.ProcessorUsage = processUsage.CpuUsage;
            }
            catch(Exception ex)
            {

            }


            using (var stream = new MemoryStream())
            {
                HealthCheckNodeResponseObject healthCheckNodeResponseObject = new HealthCheckNodeResponseObject();
                foreach (var key in result.Entries.Keys)
                {
                    HealthReportEntry value = result.Entries[key];
                    healthCheckResponseObject.HealthCheckChain.Add(key, new HealthCheckNodeResponseObject(value));
                }
                healthCheckResponseObject.RefreshStatistics();

                string jsonStr = JsonSerializer.Serialize(healthCheckResponseObject);

                byte[] data = Encoding.UTF8.GetBytes(jsonStr);
                var json = Encoding.UTF8.GetString(data);
                var response = context.Response.WriteAsync(json);
                return response;
            }
        }
        public class ProcessUsage
        {
            public double RamUsage { get; set; }
            public double CpuUsage { get; set; }

            public override string ToString()
            {
                return "CPU: " + CpuUsage + "%, RAM: " + RamUsage + "Mb";
            }
        }
        public static async Task<ProcessUsage> GetProcessUsage(Process proc, ConcurrentDictionary<int, ProcessUsage> usage = null)
        {
            ProcessUsage processUsage = new ProcessUsage();
            double cpuUsageTotal = 0;
            try
            {
                if (proc.Responding)
                {
                    var startTime = DateTime.UtcNow;
                    var startCpuUsage = proc.TotalProcessorTime;
                    await Task.Delay(500);

                    var endTime = DateTime.UtcNow;
                    var endCpuUsage = proc.TotalProcessorTime;
                    var cpuUsedMs = (endCpuUsage - startCpuUsage).TotalMilliseconds;
                    var totalMsPassed = (endTime - startTime).TotalMilliseconds;
                    cpuUsageTotal = cpuUsedMs / (Environment.ProcessorCount * totalMsPassed) * 100.0;
                    processUsage.CpuUsage = cpuUsageTotal;
                    processUsage.RamUsage = proc.WorkingSet64 / 1024.0 / 1024.0;
                }
            }
            catch (Exception ex)
            {

            }

            if (usage != null)
                usage.TryAdd(proc.Id, processUsage);
            return processUsage;
        }
    }
}
