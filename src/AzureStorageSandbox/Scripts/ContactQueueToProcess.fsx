#if !COMPILED 
#r "../../../packages/Microsoft.Azure.WebJobs/lib/net45/Microsoft.Azure.WebJobs.Host.dll"
#r "../../../packages/Microsoft.Azure.WebJobs.Extensions/lib/net45/Microsoft.Azure.WebJobs.Extensions.dll"
//#r "../../../packages/FSharp.Azure.StorageTypeProvider/lib/net452/FSharp.Azure.StorageTypeProvider.dll"
#r "C:/Program Files (x86)/Reference Assemblies/Microsoft/Framework/.NETFramework/v4.7/System.Configuration.dll"
#r "C:/Program Files (x86)/Reference Assemblies/Microsoft/Framework/.NETFramework/v4.7/System.Net.Http.dll"
open Microsoft.Azure.WebJobs
open Microsoft.Azure.WebJobs.Host
open System
open System.IO
open System.Configuration
open System.Net.Http
open System.Net
#endif

#r "Microsoft.WindowsAzure.Storage"

open Microsoft.WindowsAzure.Storage
open Microsoft.WindowsAzure.Storage.Queue
open Microsoft.WindowsAzure.Storage.Table
//open FSharp.Azure.StorageTypeProvider

//type Local = AzureTypeProvider<"UseDevelopmentStorage=true">
//let connString = ConfigurationManager.AppSettings.["tibrutest12_STORAGE"]
//type Local = AzureTypeProvider<connectionStringName = "tibrutest12_STORAGE">


type EventSource(sourceType, sourceName, message: string) =
    inherit TableEntity(partitionKey = sourceType, rowKey = sourceName)
    new() = EventSource(null, null, null)
    member val Message = message with get, set

let Run(queueItem: CloudQueueMessage, log: TraceWriter) =

    //let connString = ConfigurationManager.AppSettings.["tibrutest12_STORAGE"]
    //let storageAccount = CloudStorageAccount.Parse(connString)
    //Local.Tables.``Azure Metrics``

    let now = DateTime.UtcNow.ToLongTimeString()
    
    
    let storageAccount = CloudStorageAccount.Parse("UseDevelopmentStorage=true")
    let tableClient = storageAccount.CreateCloudTableClient()
    let table = tableClient.GetTableReference("testcontacts")
    table.CreateIfNotExists() |> ignore
    
    let fileName = System.IO.Path.GetFileName queueItem.AsString
    let eventSource = 
        EventSource("Contact", fileName, queueItem.AsString)

    let insertOp = TableOperation.Insert(eventSource)
    
    let tableResult =
        table.ExecuteAsync(insertOp)
        |> Async.AwaitTask
        |> Async.RunSynchronously

    if tableResult.HttpStatusCode = (int HttpStatusCode.Created) then 
        log.Info(sprintf "message: %s  processed at %s!" queueItem.AsString now, "CloudQueueMessage")
    else    
        if queueItem.DequeueCount < 5 then
            log.Info(sprintf "message: %s  failed at %s, dequeu count is %i!" queueItem.AsString now queueItem.DequeueCount, "CloudQueueMessage")
        else
            log.Info(sprintf "message: %s  failed at %s, dequeu count is %i!" queueItem.AsString now queueItem.DequeueCount, "CloudQueueMessage")
   // ()