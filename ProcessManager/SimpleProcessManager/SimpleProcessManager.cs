using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessManager.SimpleProcessManager
{
    public class SimpleProcessManager
    {
        public async Task<SimpleProcess> StartProcessAsync(string fileName)
        {
            return await Task.Run(() =>
            {
                var process = new Process
                {
                    StartInfo =
                    {
                        FileName = fileName, UseShellExecute = true
                    }
                };

                try
                {
                    process.Start();

                    return new SimpleProcess()
                    {
                        Pid = process.Id,
                        ProcessName = process.ProcessName
                    };
                }
                catch (System.ComponentModel.Win32Exception ex)
                {
                    Console.WriteLine("Handled exception: " + ex.Message);
                }

                return null;
            });
        }

        public async Task<List<SimpleProcess>> GetProcessesAsync()
        {
            return await Task.Run(() => new List<SimpleProcess>(Process.GetProcesses().Select(p => new SimpleProcess()
            {
                Pid = p.Id,
                ProcessName = p.ProcessName
            })));
        }

        public async Task KillProcessAsync(SimpleProcess process, bool processTree = false)
        {
            await Task.Run(() =>
            {
                var p = Process.GetProcessById(process.Pid);

                p.Kill(processTree);
            });
        }
    }
}