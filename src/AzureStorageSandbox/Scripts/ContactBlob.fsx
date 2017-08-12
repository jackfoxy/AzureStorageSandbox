#if !COMPILED 
#r "../../../packages/Microsoft.Azure.WebJobs/lib/net45/Microsoft.Azure.WebJobs.Host.dll"
#r "../../../packages/Microsoft.Azure.WebJobs.Extensions/lib/net45/Microsoft.Azure.WebJobs.Extensions.dll"
open Microsoft.Azure.WebJobs
open Microsoft.Azure.WebJobs.Host
open System.IO

#endif

#r "Microsoft.WindowsAzure.Storage"

open System
open Microsoft.WindowsAzure.Storage
open Microsoft.WindowsAzure

let Run(rawConnections: Stream, folder: string,  filename: string, extension: string, log: TraceWriter ) =

    let storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

    let now = DateTime.UtcNow.ToLongTimeString()
    log.Info(sprintf "folder: %s  filename: %s extension %s detected at %s!" folder filename extension now, "ContactBlob")
