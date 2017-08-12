//open System.Net
//open System.Net.Http
//open System.Threading.Tasks

#if !COMPILED 
#r "../../../packages/Microsoft.Azure.WebJobs/lib/net45/Microsoft.Azure.WebJobs.Host.dll"
#r "../../../packages/Microsoft.Azure.WebJobs.Extensions/lib/net45/Microsoft.Azure.WebJobs.Extensions.dll"
open Microsoft.Azure.WebJobs
open Microsoft.Azure.WebJobs.Host
#endif

open System

let Run(myTimer: TimerInfo, log: TraceWriter ) =
    if (myTimer. IsPastDue) then
        log.Info("running late", "TestScheduled")
    let now = DateTime.UtcNow.ToLongTimeString()
    log.Info(sprintf "executed at %s!" now, "TestScheduled")

