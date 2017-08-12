#if !COMPILED 
#r "../../../packages/Microsoft.Azure.WebJobs/lib/net45/Microsoft.Azure.WebJobs.Host.dll"
#r "../../../packages/Microsoft.Azure.WebJobs.Extensions/lib/net45/Microsoft.Azure.WebJobs.Extensions.dll"
#r "C:/Program Files (x86)/Reference Assemblies/Microsoft/Framework/.NETFramework/v4.7/System.Configuration.dll"
open Microsoft.Azure.WebJobs
open Microsoft.Azure.WebJobs.Host
open System.IO

#endif

#r "Microsoft.WindowsAzure.Storage"
open System
open System.Configuration
open Microsoft.WindowsAzure.Storage
open Microsoft.WindowsAzure.Storage.Queue

let Run(rawConnections: Stream, folder: string,  filename: string, extension: string, log: TraceWriter) =
    let now = DateTime.UtcNow.ToLongTimeString()
    log.Info(sprintf "folder: %s  filename: %s extension %s detected at %s!" folder filename extension now, "ContactBlob")

    let connString = ConfigurationManager.AppSettings.["tibrutest12_STORAGE"]
    
    let storageAccount = CloudStorageAccount.Parse(connString)
    let queueClient = storageAccount.CreateCloudQueueClient()
    let queue = queueClient.GetQueueReference("rawcontacts")
    queue.CreateIfNotExists() |> ignore
    let cloudQueueMessage = new CloudQueueMessage(sprintf "%s/%s.%s" folder filename extension)
    queue.AddMessage(cloudQueueMessage)
